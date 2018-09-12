using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.ViewModels;
using XML_Batch_Editor.Views;

namespace XML_Batch_Editor.Services
{
    public class ServiceMain : IService
    {
        private const string extension = "*.xml";
        private const string outputDirectory = "OUT";

        public int FilesCount(string path)
        {
            return Directory.GetFiles(path, extension).ToList().Count;
        }

        private bool ConvertFile(XmlDocument xml, string xpath, string search, string replace=null, Regex regex=null, Action<string>Log=null)
        {
            if (!ValidateXSD(xml))
            {
                Log?.Invoke("ОШИБКА: Документ не соответствует XSD схеме");
                return false;
            }

            int count = 0;
            XmlElement xRoot = xml.DocumentElement;
            XmlNodeList nodes = xRoot?.SelectNodes(xpath);

            if (nodes != null) foreach (XmlNode node in nodes)
            {
                if (regex == null)
                {
                    int nodeOccurences = node.InnerXml.Occurencies(search);
                    count += nodeOccurences;
                    if (nodeOccurences > 0 && replace != null) node.InnerXml = node.InnerXml.Replace(search, replace);
                }
                else
                {
                    int nodeOccurences = regex.Matches(node.InnerXml).Count;
                    count += nodeOccurences;
                    if (nodeOccurences > 0 && replace != null) node.InnerXml = regex.Replace(node.InnerXml, replace);
                }
            }

            Log?.Invoke($"Найдено {count} значений в документе");
            if (replace == null) return true;

            Log?.Invoke("Значения заменены, проверяем документ");
            if (!ValidateXSD(xml))
            {
                Log?.Invoke("ОШИБКА: После замены документ не соответствует XSD схеме");
                return false;
            }

            return true;
        }

        private bool ValidateXSD(XmlDocument xml)
        {
            bool result = true;
            if (xml.Schemas.Count < 1) return true;
            xml.Validate((o, ev) => result = false);
            return result;
        }

        public async void Convert(VM_Main vmMain, VM_Work vmWork)
        {
            var files = Directory.GetFiles(vmMain.PathToInputDirectory, extension).ToList();

            string outputPath = Path.Combine(vmMain.PathToInputDirectory, outputDirectory);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            int filesProcessed = 0;
            vmWork.pgBarState.Maximum = files.Count;

            vmWork.Log.TryAdd($"Входная директория: {vmMain.PathToInputDirectory}");
            vmWork.Log.TryAdd($"Выходная директория: {outputPath}");
            if (vmMain.UseXSD) vmWork.Log.TryAdd($"XSD схема для проверки: {vmMain.PathToXSD}");
            vmWork.Log.TryAdd($"Поиск по {(vmMain.UseRegularExpressions ? "регулярному выражению" : "подстроке")}: {vmMain.Search}");
            if (vmMain.NeedReplace) vmWork.Log.TryAdd($"Заменять на: {vmMain.Replace}");

            Regex regex = vmMain.UseRegularExpressions ? new Regex(vmMain.Search, RegexOptions.Compiled) : null;
            String replace = vmMain.NeedReplace ? (vmMain.Replace ?? "") : null;

            Stopwatch elapsedTotal = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            foreach (var file in files)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    Stopwatch elapsed = Stopwatch.StartNew();
                    string filename = Path.GetFileName(file) ?? throw new ArgumentException("Can't get short path for: " + file);

                    XmlDocument xml = new XmlDocument();
                    xml.Load(file);
                    if (vmMain.UseXSD) xml.Schemas.Add(null, vmMain.PathToXSD);

                    try
                    {
                        bool success = ConvertFile(xml, vmMain.XPath, vmMain.Search, replace, regex, msg => vmWork.Log.TryAdd(msg + " " + filename));
                        if (success) xml.Save(Path.Combine(outputPath, filename));
                    }
                    catch (XmlException exXml)
                    {
                        vmWork.Log.TryAdd($"ОШИБКА: {exXml.Message} в документе {filename}");
                    }

                    vmWork.Log.TryAdd($"Документ \"{filename}\" обработан за {elapsed.Elapsed}ms");
                    vmWork.pgBarState.Value = Interlocked.Increment(ref filesProcessed);
                    vmWork.LabelStatus = $"Обработано документов: {vmWork.pgBarState.Value}/{vmWork.pgBarState.Maximum} (через {TaskScheduler.Current.GetType().Name})";
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray());
            elapsedTotal.Stop();
            vmWork.Log.TryAdd($"Суммарное время обработки {files.Count} документов: {elapsedTotal.Elapsed}");
        }

    }
}

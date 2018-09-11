using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

            foreach (XmlNode node in nodes)
            {
                if (regex == null)
                {
                    int nodeOccurences = node.InnerXml.Occurencies(search);
                    count += nodeOccurences;
                    if (nodeOccurences > 0 && !string.IsNullOrEmpty(replace)) node.InnerXml = node.InnerXml.Replace(search, replace);
                }
                else
                {
                    int nodeOccurences = regex.Matches(node.InnerXml).Count;
                    count += nodeOccurences;
                    if (nodeOccurences > 0 && !string.IsNullOrEmpty(replace)) node.InnerXml = regex.Replace(node.InnerXml, replace);
                }
            }

            Log?.Invoke($"Найдено {count} значений в документе");
            if (string.IsNullOrEmpty(replace)) return true;

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

        public void Convert(VM_Main main)
        {
            // TODO: WinForms недопустимо вызывать из ViewModel или сервиса, необходимо понять куда перенести эту ответственность
            ViewWork work = new ViewWork();
            var VM = work.ViewModel;

            var files = Directory.GetFiles(main.PathToInputDirectory, extension).ToList();

            string outputPath = Path.Combine(main.PathToInputDirectory, outputDirectory);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            int filesProcessed = 0;
            VM.pgBarState.Maximum = files.Count;

            Random rnd = new Random();

            if (main.UseXSD) VM.Log.TryAdd($"XSD схема для проверки: {main.PathToXSD}");
            VM.Log.TryAdd($"Поиск по {(main.UseRegularExpressions ? "регулярному выражению" : "подстроке")}: {main.Search}");
            if (main.NeedReplace) VM.Log.TryAdd($"Заменять на: {main.Replace}");

            Regex regex = main.UseRegularExpressions ? new Regex(main.Search, RegexOptions.Compiled) : null;
            String replace = main.NeedReplace ? main.Replace : null;

            // Возможно для этой задачи стоит заменить INPC вьюмодели с Dispatcher-ом на Timer для формы
            // Иначе при быстрых задачах в UI поток будет сыпаться огромное количество запросов на обновление UI
            // Но от этого пострадает читаемость кода, поэтому оставлю пока всё как есть
            foreach (var file in files)
            {
                Task.Factory.StartNew(() =>
                {
                    string filename = Path.GetFileName(file) ?? throw new ArgumentException("Can't get short path for: " + file);

                    int time = rnd.Next(50, 200) + 20 * (filesProcessed % 10);
                    Thread.Sleep(time);
                    //await Task.Delay(time);

                    XmlDocument xml = new XmlDocument();
                    xml.Load(file);
                    if (main.UseXSD) xml.Schemas.Add(null, main.PathToXSD);

                    bool success = ConvertFile(xml, main.XPath, main.Search, replace, regex, msg => VM.Log.TryAdd(msg + " " + filename));
                    if (success) xml.Save(Path.Combine(outputPath, filename));

                    VM.Log.TryAdd($"Документа \"{filename}\" обработан за {time}ms");
                    VM.pgBarState.Value = Interlocked.Increment(ref filesProcessed);
                    VM.LabelStatus = $"Обработано документов: {VM.pgBarState.Value}/{VM.pgBarState.Maximum} (через {TaskScheduler.Current.GetType().Name})";
                }).ContinueWith(task =>
                {
                    if (task.Exception != null)
                        throw task.Exception;
                });
            }

            work.ShowDialog();
        }

    }
}

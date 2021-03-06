﻿using System;
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
        private static readonly string extension = "*.xml";
        private static readonly string outputDirectory = "OUT";
        public static readonly Regex regexAttribute = new Regex("(^[a-zA-Zа-яА-Я][^=\\W]+)=\\\"(.*)\\\"", RegexOptions.Compiled);

        public int FilesCount(string path)
        {
            return Directory.GetFiles(path, extension).ToList().Count;
        }

        public Tuple<int,IList<XmlAttribute>> Search(XmlDocument xml, string xpath, string search, Regex regex=null, string attributeToSearch=null)
        {
            List<XmlAttribute> attributes = new List<XmlAttribute>();

            int count = 0;
            XmlElement xRoot = xml.DocumentElement;
            XmlNodeList nodes = xRoot?.SelectNodes(xpath);

            if (nodes != null) foreach (XmlNode node in nodes)
            {
                if (node.Attributes == null) continue;

                foreach (XmlAttribute attr in node.Attributes)
                {
                    if (attributeToSearch != null && !attr.Name.Equals(attributeToSearch)) continue;

                    int nodeOccurences = regex?.Matches(attr.Value).Count ?? attr.Value.Occurencies(search);
                    count += nodeOccurences;

                    if (nodeOccurences > 0) attributes.Add(attr);
                }
            }

            return new Tuple<int, IList<XmlAttribute>>(count, attributes);
        }

        public void Replace(XmlDocument xml, IList<XmlAttribute> attributes, string search, string replace, Regex regex = null)
        {
            if (!ValidateXSD(xml)) throw new ArgumentException("ОШИБКА: После замены документ не соответствует XSD схеме");

            foreach (XmlAttribute attr in attributes)
            {
                attr.Value = regex?.Replace(attr.Value, replace) ?? attr.Value.Replace(search, replace);
            }
        }

        private bool ValidateXSD(XmlDocument xml)
        {
            bool result = true;
            if (xml.Schemas.Count < 1) return true;
            xml.Validate((o, ev) => result = false);
            return result;
        }

        private void getAttributeToSearch(ref string search, out string attributeToSearch)
        {
            attributeToSearch = null;
            var match = regexAttribute.Match(search);
            if (match.Success && match.Groups.Count == 3)
            {
                attributeToSearch = match.Groups[1].Value;
                search = match.Groups[2].Value;
            }
        }

        private string logSearchLine(bool isRegex, string search, string attributeToSearch)
        {
            string searchAttr = attributeToSearch == null ? "любого аттрибута" : $"аттрибута с именем \"{attributeToSearch}\"";
            string searchType = isRegex ? "регулярному выражению" : "подстроке";

            return $"Поиск {searchAttr} по {searchType}: {search}";
        }

        public async void Convert(VM_Main vmMain, VM_Work vmWork)
        {
            var files = Directory.GetFiles(vmMain.PathToInputDirectory, extension).ToList();

            string outputPath = Path.Combine(vmMain.PathToInputDirectory, outputDirectory);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            int filesProcessed = 0;
            vmWork.pgBarState.Maximum = files.Count;

            string search = vmMain.Search;
            getAttributeToSearch(ref search, out string attributeToSearch);
            Regex regex = vmMain.UseRegularExpressions ? new Regex(search, RegexOptions.Compiled) : null;
            String replace = vmMain.NeedReplace ? (vmMain.Replace ?? "") : null;

            vmWork.Log.TryAdd($"Входная директория: {vmMain.PathToInputDirectory}");
            vmWork.Log.TryAdd($"Выходная директория: {outputPath}");
            if (vmMain.UseXSD) vmWork.Log.TryAdd($"XSD схема для проверки: {vmMain.PathToXSD}");
            vmWork.Log.TryAdd(logSearchLine(vmMain.UseRegularExpressions, search, attributeToSearch));
            if (vmMain.NeedReplace) vmWork.Log.TryAdd($"Заменять на: {vmMain.Replace}");

            Stopwatch elapsedTotal = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            foreach (var file in files)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    string filename = Path.GetFileName(file) ?? throw new ArgumentException("Can't get short path for: " + file);
                    Stopwatch elapsed = Stopwatch.StartNew();

                    try
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(file);
                        if (vmMain.UseXSD) xml.Schemas.Add(null, vmMain.PathToXSD);

                        if (!ValidateXSD(xml))
                        {
                            vmWork.Log.TryAdd("ОШИБКА: Документ не соответствует XSD схеме");
                            goto method_end;
                        }

                        var result = Search(xml, vmMain.XPath, search, regex, attributeToSearch);
                        vmWork.Log.TryAdd($"Найдено {result.Item1} значений в документе");

                        if (vmMain.NeedReplace)
                        {
                            Replace(xml, result.Item2, search, replace, regex);

                            if (!ValidateXSD(xml))
                            {
                                vmWork.Log.TryAdd("ОШИБКА: После замены документ не соответствует XSD схеме");
                                goto method_end;
                            }

                            xml.Save(Path.Combine(outputPath, filename));
                        }

                        method_end:;
                    }
                    catch (XmlException ex)
                    {
                        vmWork.Log.TryAdd($"ОШИБКА: {ex.Message} в документе {filename}");
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

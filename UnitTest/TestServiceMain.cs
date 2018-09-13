using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.Services;
using XML_Batch_Editor.ViewModels;

namespace XML_Batch_Editor.UnitTest
{
    [TestClass]
    public class TestServiceMain
    {
        public ServiceMain serviceMain;
        public XmlDocument xml;

        [TestInitialize]
        public void Prepare()
        {
            ServiceManager.Instance.Register<ServiceMain>();
            serviceMain = ServiceManager.Instance.Resolve<ServiceMain>();

            xml = new XmlDocument();
            xml.Load("test_xml.user");
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("//СвНП", "45", 9)]
        [DataRow("//СвНП", "ОКТМО=\"45\"", 3)]
        [DataRow("//СвНП[@ГРУППА=\"301\"]", "45", 6)]
        [DataRow("//СвНП[@ГРУППА=\"301\"]", "КОД=\"45\"", 2)]
        [DataRow("//СвНП[@АЙДИ=\"1\"]", "45", 3)]
        [DataRow("//СвНП[@АЙДИ=\"1\"]", "ПОЛЕ=\"45\"", 1)]
        public void TestSearchString(string xpath, string search, int results)
        {
            getAttributeToSearch(ref search, out string attributeToSearch);

            var value = serviceMain.Search(xml, xpath, search, null, attributeToSearch);
            Assert.AreEqual(value.Item1, results);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("//СвНП[@АЙДИ=\"1\"]", "ОКТМО=\"[^\"+]\"", new[]{ "45493868" })]
        [DataRow("//НПЮЛ", "ИННЮЛ=\"[^\"+]\"", new[] { "7387481131", "7661998677", "7322125755" })]
        public void TestSearchRegEx(string xpath, string search, string[] values)
        {
            getAttributeToSearch(ref search, out string attributeToSearch);

            Regex regex = new Regex(search, RegexOptions.Compiled);

            var searchResults = serviceMain.Search(xml, xpath, search, regex, attributeToSearch);
            CollectionAssert.AreEqual(values, searchResults.Item2.Select(x => x.Value).ToArray());
        }



        private void getAttributeToSearch(ref string search, out string attributeToSearch)
        {
            attributeToSearch = null;
            var match = ServiceMain.regexAttribute.Match(search);
            if (match.Success && match.Groups.Count == 3)
            {
                attributeToSearch = match.Groups[1].Value;
                search = match.Groups[2].Value;
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.Services;

namespace XML_Batch_Editor.UnitTest
{
    [TestClass]
    public class TestServiceMain
    {
        public static readonly string SampleXML =
            "<Document name=\"Example\">\r\n" +
            "\t<group type=\"info\">\r\n" +
            "\t\t<field name=\"PERSON\" type=\"string\" length=\"140\">$FIO, проживающий(ая) по адресу $ADDRESS</field>\r\n" +
            "\t\t<field name=\"SUMMA\" type=\"numeric\" length=\"6,2\">$SUMMA</field>\r\n" +
            "\t\t<field name=\"DATAOPL\" type=\"date\" format=\"yyyy-MM-dd\" length=\"8\">$DATEOPL</field>\r\n" +
            "\t\t<field name=\"NACHDATE\" type=\"date\" format=\"yyyy-MM-dd\" length=\"8\">$NACHDATE</field>\r\n" +
            "\t</group>\r\n" +
            "</Document>";

        public ServiceMain serviceMain;

        [TestInitialize]
        public void Prepare()
        {
            ServiceManager.Instance.Register<ServiceMain>();
            serviceMain = ServiceManager.Instance.Resolve<ServiceMain>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            ViewModel vm = new ViewModel();
            

        }
    }
}

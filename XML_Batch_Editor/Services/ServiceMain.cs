using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.ViewModels;

namespace XML_Batch_Editor.Model
{
    class ServiceMain : IService
    {
        private const string extension = "*.xml";

        public int FilesCount(string path)
        {
            return Directory.GetFiles(path, extension).ToList().Count;
        }

        public void Convert(VM_Main main, VM_Work work, bool Replace)
        {

        }

    }
}

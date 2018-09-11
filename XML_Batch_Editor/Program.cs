using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.Services;
using XML_Batch_Editor.Views;

namespace XML_Batch_Editor
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Обязательно регистрировать все сервисы здесь
            ServiceManager.Instance.Register<ServiceMain>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewMain());
        }
    }
}

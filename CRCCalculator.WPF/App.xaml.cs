using Prism.Ioc;
using CRCCalculator.WPF.Views;
using System.Windows;
using System.IO.IsolatedStorage;
using System.IO;
using System;

namespace CRCCalculator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private const string SETTINGS_PATH = "App.settings";

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using var stream = new IsolatedStorageFileStream(SETTINGS_PATH, FileMode.Open, storage);
                using var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    var data = reader.ReadLine().Split(",");
                    var key = data[0];
                    var value = data[1];
                    Properties[key] = value;
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException ||
                                       ex.InnerException is FileNotFoundException)
            {
                // Handle when file is not found in isolated storage:
                // * When the first application session
                // * When file has been deleted
            }
            catch (Exception ex)
            {
                throw ex;
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            using var stream = new IsolatedStorageFileStream(SETTINGS_PATH, FileMode.Create, storage);
            using var writer = new StreamWriter(stream);
            foreach (var key in Properties.Keys)
            {
                var value = Properties[key];
                writer.WriteLine($"{key},{value}");
            }
        }
    }
}

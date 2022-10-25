using MobileMainProject.Data.DataBase;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileMainProject
{
    public partial class App : Application
    {
        private static TranslateDataBase _translateDB;

        public static TranslateDataBase TranslateDB
        {
            get
            {
                if (_translateDB is null)
                {
                    _translateDB = new TranslateDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TranslateDataBase.db"));

                    if (_translateDB.TranslateChunkCount == 0)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();

                        using (Stream stream = assembly.GetManifestResourceStream("MobileMainProject.TranslateData.csv"))
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while (reader.ReadLine() is string str)
                            {
                                string[] tmp = str.Split(',');
                                _ = _translateDB.SaveTranslateChunkAsync(new TranslateChunk
                                {
                                    EnglishWord = tmp[0].ToLower().Trim(),
                                    TranslateWords = tmp[1].ToLower().Trim()
                                });
                            }
                        }
                    }
                }

                return _translateDB;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

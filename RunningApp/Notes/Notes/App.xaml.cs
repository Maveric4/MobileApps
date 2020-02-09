using System;
using System.IO;
using Xamarin.Forms;
using Notes.Data;

namespace Notes
{
    public partial class App : Application
    {
        static NoteDatabase database;

        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new NotesPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //ShowMessageDialog(title: "OnStartMessage", message: "App just started");

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //ShowMessageDialog(title: "OnSleepMessage", message: "App is sleeping");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            ShowMessageDialog(title: "OnResumeMessage", message: "Witaj z powrotem", cancel:"Kontynuuj");
        }

        async void ShowMessageDialog(string title, string message, string cancel="OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

    }
}
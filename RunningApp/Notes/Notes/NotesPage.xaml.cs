using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Notes.Models;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        Note selectedNote = null;
        public NotesPage()
        {   
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateLabels();
        }

        void UpdateLabels()
        {
            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            listView.ItemsSource = notes
                .OrderByDescending(d => d.Date)
                .ToList();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note()
            });
        }
        void OnNoteDeleteClicked(object sender, EventArgs e)
        {
            if (selectedNote != null)
            {
                if (File.Exists(selectedNote.Filename))
                {
                    File.Delete(selectedNote.Filename);
                }
            }
            UpdateLabels();
        }

        async void OnNoteEditClicked(object sender, EventArgs e)
        {
            if (selectedNote != null)
            {
                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = selectedNote as Note
                });
            }
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                selectedNote = (Note)e.SelectedItem;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Notes.Models;
using Notes.Data;

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
            DeleteToolbarItem.IsEnabled = false;
            EditToolbarItem.IsEnabled = false;
        }

        async void UpdateLabels()
        {
            listView.ItemsSource = (await App.Database.GetNotesAsync())
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
        async void OnNoteDeleteClicked(object sender, EventArgs e)
        {
            if (selectedNote != null)
            {
                await App.Database.DeleteNoteAsync(selectedNote);
            }
            selectedNote = null;
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
            //var editToolbarItem = this.FindByName<ToolbarItem>("Edit");
            DeleteToolbarItem.IsEnabled = true;
            EditToolbarItem.IsEnabled = true;
            if (e.SelectedItem != null)
            {
                selectedNote = (Note)e.SelectedItem;
            }
        }

        async void GoToMainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
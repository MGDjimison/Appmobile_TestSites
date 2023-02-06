using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var listNote = await App.Database.GetNotesDeletedAsync(true);
            // defini la couleur de fond en fonction de la validité de l'url
            foreach (Note note in listNote)
            {
                if (await App.Database.TestSiteAsync(note.url) == true)
                {
                    note.color = "lightgreen";
                }
                else
                {
                    note.color = "coral";
                }
            }

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = listNote;
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();

                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}
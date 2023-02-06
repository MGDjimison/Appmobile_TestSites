using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InactiveNotePage : ContentPage
    {
        public InactiveNotePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var listNoteInactif = await App.Database.GetNotesDeletedAsync(false);
            // defini la couleur de fond en fonction de la validité de l'url
            foreach (Note note in listNoteInactif)
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
            collectionView.ItemsSource = listNoteInactif;
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
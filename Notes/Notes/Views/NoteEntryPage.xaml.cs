using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        public static readonly HttpClient client = new HttpClient();
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public  NoteEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Note();

        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            note.isActive = true;
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnTestButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.TestSiteAsync(note.url);

        }
        async void OnActiveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.isActive = true;
            await App.Database.ActiveNote(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnDesactiveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.isActive = false;
            await App.Database.DesactiveNote(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}
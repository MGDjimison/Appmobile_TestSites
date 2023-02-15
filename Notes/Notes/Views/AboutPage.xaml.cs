using Notes.Models;
using System;
using System.Net.NetworkInformation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
       
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://aka.ms/xamarin-quickstart");
        }
        async void OnPingButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var ping = adresse.Text;
                var result = App.Database.Ping(ping);

                if (result)
                {
                    await DisplayAlert("Resultat", "SUCCESS", "OK");
                }
                else
                {
                    await DisplayAlert("Resultat", "FAILURE", "OK");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LOG " + ex.ToString());
            }
        }
    }
}
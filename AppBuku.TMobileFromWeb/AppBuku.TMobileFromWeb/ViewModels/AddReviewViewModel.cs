using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using AppBuku.Models;
using System.Threading.Tasks;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    public class AddReviewViewModel :BaseViewModel
    {
        // Deklarasi MyHttpClient Service + Constructor Class
        Services.MyHttpClient myHttpClient;
        Random rnd = new Random();

        public AddReviewViewModel()
        {
            //reviewBukuGet = new ReviewBuku(); 
            Title = "Tambah Ulasan Buku";

            IsBusy = true;
            string baseUri = Application.Current.Properties["BaseWebUri"] as string;
            myHttpClient = new Services.MyHttpClient(baseUri);

            int rating = rnd.Next(1, 5);
            Rating = rating.ToString();
            IsBusy = false;
        } 

        private ICommand cmdKirim;
        public ICommand CmdKirim
        {
            get
            {
                if (cmdKirim == null)
                {
                    cmdKirim = new Command(async () => await PerformCmdKirimAsync());
                }

                return cmdKirim;
            } 
        } 

        private async Task PerformCmdKirimAsync()
        {
            ReviewBuku r1 = new ReviewBuku()
            {
                BukuId = 1
            };

            r1.Nama = nama;
            r1.IsiReview = Reviewing;
            r1.Rating = int.Parse(Rating);

            IsBusy = true;
            try
            {
                string hsl = await myHttpClient.HttpPost("api/XReview", r1);
                StatusKirim = hsl;

                await Shell.Current.GoToAsync("..");
            } 
            catch (Exception ex)
            {
                statusKirim = "ERROR: " + ex.Message;
            } 
            finally
            {
                IsBusy = false;
            } 
        }

        private ICommand cmdBatal;
        public ICommand CmdBatal
        {
            get
            {
                if (cmdBatal == null)
                {
                    cmdBatal = new Command(PerformCmdBatal);
                }

                return cmdBatal;
            }
        }

        private async void PerformCmdBatal()
        {
            await Shell.Current.GoToAsync("..");
        }

        private string nama = "Imam Farisi";
        public string Nama
        { get => nama; set => SetProperty(ref nama, value); }

        private string statusKirim;
        public string StatusKirim 
        { get => statusKirim; set => SetProperty(ref statusKirim, value); } 

        private List<ReviewBuku> listReviewById;
        public List<ReviewBuku> ListReviewById
        { get => listReviewById; set => SetProperty(ref listReviewById, value); } 

        private ReviewBuku review;
        public ReviewBuku Review
        { get => review; set => SetProperty(ref review, value); }

        private string reviwing = "Lorem ipsum dolor sit amet, consectetur " +
                                  "adipiscing elit, sed do eiusmod tempor " +
                                  "incididunt ut labore et dolore magna aliqua.";
        public string Reviewing 
        { get => reviwing; set => SetProperty(ref reviwing, value); }
        
        private string rating;
        public string Rating 
        { get => rating; set => SetProperty(ref rating, value); }
    }
}

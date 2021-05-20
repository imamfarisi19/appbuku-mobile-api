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
            string nama = "Imam Farisi";
            int rating = rnd.Next(1, 5);
            //var rating = ReviewBukuGet.Rating;
            var isiUlasan = UlasanData.IsiReview;

            ReviewBuku r1 = new ReviewBuku()
            {
                // Id = 1,
                BukuId = 1,
                Nama = nama,
                Rating = UlasanData.Rating,
                IsiReview = isiUlasan
            }; 

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

        private ReviewBuku ulasanData;
        public ReviewBuku UlasanData
        {
            get
            {
                return ulasanData;
            }
            set
            {
                ulasanData = value;
                OnPropertyChanged(nameof(UlasanData));
            }
        }

        private string statusKirim;
        public string StatusKirim 
        { get => statusKirim; set => SetProperty(ref statusKirim, value); } 

        private List<ReviewBuku> listReviewBukuById;
        public List<ReviewBuku> ListReviewBukuById
        { get => listReviewBukuById; set => SetProperty(ref listReviewBukuById, value); } 

        private ReviewBuku reviewBukuGet;
        public ReviewBuku ReviewBukuGet
        { get => reviewBukuGet; set => SetProperty(ref reviewBukuGet, value); } 
    }
}

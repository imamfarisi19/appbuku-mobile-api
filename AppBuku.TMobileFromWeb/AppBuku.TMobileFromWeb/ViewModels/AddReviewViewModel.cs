using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using AppBuku.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    [QueryProperty(nameof(TheBukuId), nameof(TheBukuId))]
    [QueryProperty(nameof(Review), nameof(Review))]
    public class AddReviewViewModel : BaseViewModel
    {
        // Deklarasi MyHttpClient Service + Constructor Class
        Services.MyHttpClient myHttpClient;
        Random rnd = new Random();

        public AddReviewViewModel()
        {
            //reviewBukuGet = new ReviewBuku(); 
            Title = "Ulasan Buku";

            IsBusy = true;
            string baseUri = Application.Current.Properties["BaseWebUri"] as string;
            myHttpClient = new Services.MyHttpClient(baseUri);

            HapusIsVisible = false;
            IsBusy = false;
        }

        bool isNewItem = true;

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
            ReviewBuku r1 = new ReviewBuku(); 
            r1.Nama = ReviewById.Nama; 
            r1.IsiReview = ReviewById.IsiReview; 
            r1.Rating = ReviewById.Rating; 
            r1.BukuId = ReviewById.BukuId; 
            r1.Id = ReviewById.Id;
            r1.UserId = reviewById.UserId;

            if (BukuKe != null)
            {
                string hslReview = await myHttpClient.HttpGet("api/XReviewByBukuId/", BukuKe);
                ListReviewById = JsonConvert.DeserializeObject<List<ReviewBuku>>(hslReview);

                foreach (ReviewBuku array in listReviewById)
                {
                    if (r1.Id != array.Id && r1.UserId != array.UserId)
                        isNewItem = true;
                }
            }
            else
            {
                if (r1.Id == r2.Id)
                    isNewItem = false;
            }
            IsBusy = true;
            try
            {
                if (isNewItem)
                {
                    r1.Id = 0;
                    string hsl = await myHttpClient.HttpPost("api/XReview", r1);
                    StatusKirim = hsl;
                }
                else
                {
                    string hsl = await myHttpClient.HttpPut("api/XReview/", ReviewById.Id.ToString(), ReviewById);
                    StatusKirim = hsl;
                }
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

        private string theBukuId;
        public string TheBukuId
        {
            get
            {
                return theBukuId;
            }
            set
            {
                theBukuId = value;
                LoadByIdAsync(value);
            }
        }

        private async void LoadByIdAsync(string theBukuId)
        {
            if (string.IsNullOrEmpty(theBukuId))
                return;
            
            int id = 0;
            if (int.TryParse(theBukuId, out id) == false)
                return;

            BukuKe = theBukuId;

            string hsl = await myHttpClient.HttpGet("api/XReview/", theBukuId);
            reviewById = JsonConvert.DeserializeObject<ReviewBuku>(hsl);

            reviewById.BukuId = int.Parse(theBukuId);
        }
        public ReviewBuku r3 = new ReviewBuku();

        private string review;
        public string Review
        {
            get
            {
                return review;
            }
            set
            {
                review = value;
                LoadReviewById(value);
            }
        }

        private async void LoadReviewById(string review)
        {
            if (string.IsNullOrEmpty(review))
                return;

            int id = 0;
            if (int.TryParse(review, out id) == false)
                return;
            Title = "Perbarui Ulasan";

            string hsl = await myHttpClient.HttpGet("api/XReview/", review);
            reviewById = JsonConvert.DeserializeObject<ReviewBuku>(hsl);
            r2 = reviewById;

            HapusIsVisible = true;
            isNewItem = false;
        }

        public ReviewBuku r2 = new ReviewBuku();

        private string nama;
        public string Nama
        { get => nama; set => SetProperty(ref nama, value); }

        private string statusKirim;
        public string StatusKirim 
        { get => statusKirim; set => SetProperty(ref statusKirim, value); }

        private ReviewBuku reviewById;
        public ReviewBuku ReviewById
        { get => reviewById; set => SetProperty(ref reviewById, value); }

        private List<ReviewBuku> listReviewById;
        public List<ReviewBuku> ListReviewById
        { get => listReviewById; set => SetProperty(ref listReviewById, value); } 

        private string reviewing;
        public string Reviewing 
        { get => reviewing; set => SetProperty(ref reviewing, value); }
        
        private string rating;
        public string Rating 
        { get => rating; set => SetProperty(ref rating, value); }

        private string hasilGet;
        public string HasilGet
        { get => hasilGet; set => SetProperty(ref hasilGet, value); }

        private string bukuKe;
        public string BukuKe 
        { get => bukuKe; set => SetProperty(ref bukuKe, value); }

        private Buku bukuEdit;
        public Buku BukuEdit
        {
            get { return bukuEdit; }
            set { SetProperty(ref bukuEdit, value); }
        }

        private List<Buku> bukuById;
        public List<Buku> BukuById
        { get => bukuById; set => SetProperty(ref bukuById, value); }

        private bool hapusIsVisible;
        public bool HapusIsVisible
        {
            get => hapusIsVisible;
            set => SetProperty(ref hapusIsVisible, value);
        }

        private ICommand cmdHapus;

        public ICommand CmdHapus
        {
            get
            {
                if (cmdHapus == null)
                {
                    cmdHapus = new Command(PerformCmdHapus);
                }

                return cmdHapus;
            }
        }

        private async void PerformCmdHapus()
        {
            bool jwb = await Application.Current.MainPage.DisplayAlert("Hapus Ulasan",
                "Apakah anda yakin untuk menghapus Ulasan ini?", "Ya", "Tidak");
            if (jwb)
            {
                string hsl = await myHttpClient.HttpDelete("api/XReview/", r2.Id.ToString());
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}


//int rating = rnd.Next(1, 5);
//Rating = rating.ToString();

//"Lorem ipsum dolor sit amet, consectetur " +
//"adipiscing elit, sed do eiusmod tempor " +
//"incididunt ut labore et dolore magna aliqua."

//string hslXBuku = await myHttpClient.HttpGet("api/XBuku/", theBukuId);
//HasilGet = hslXBuku;
//BukuEdit = JsonConvert.DeserializeObject<Buku>(hslXBuku);

//ReviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);

//string hslXReview = await myHttpClient.HttpGet("api/XReview/", theBukuId);
//var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hslXReview);
//ListReviewById = aLists;

//var aLists = JsonConvert.DeserializeObject<ReviewBuku>(hslXReview);
//Review = aLists;
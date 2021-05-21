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
    //[QueryProperty(nameof(myBukuId), nameof(myBukuId))]
    public class AddReviewViewModel : BaseViewModel
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

            //ReviewBuku buku = new ReviewBuku();
            //Review.BukuId = buku.BukuId;

            HalamanBukuViewModel book = new HalamanBukuViewModel();
            string a = book.HasilGet;
            

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
            ReviewBuku r1 = new ReviewBuku();

            r1.Nama = nama;
            r1.IsiReview = Reviewing;
            r1.Rating = int.Parse(Rating);
            r1.BukuId = int.Parse(BukuKe);
            //r1.BukuId = int.Parse(BukuKe);

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
                //myBukuId = TheBukuId;
                LoadById(value);
            }
        }

        private void LoadById(string myBukuId)
        {
            if (string.IsNullOrEmpty(myBukuId))
                return;
            
            int id = 0;
            if (int.TryParse(myBukuId, out id) == false)
                return;

            BukuKe = myBukuId;
            
            //string hslXBuku = await myHttpClient.HttpGet("api/XBuku/", );

            //string hslXReview = await myHttpClient.HttpGet("api/XReviewByBukuId/", theBukuId);
            //string hslXBuku = await myHttpClient.HttpGet("api/XBuku/", theBukuId);
            //HasilGet = hslXBuku;
            //BukuEdit = JsonConvert.DeserializeObject<Buku>(hslXBuku);
            //ReviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);

            //var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hslXReview);
            //ListReviewById = aLists;

            //var aLists = JsonConvert.DeserializeObject<ReviewBuku>(hslXReview);
            //Review = aLists;

            //isNewItem = false;
            //HapusIsVisible = true;
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

        private string reviewing = "Lorem ipsum dolor sit amet, consectetur " +
                                  "adipiscing elit, sed do eiusmod tempor " +
                                  "incididunt ut labore et dolore magna aliqua.";
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
    }
}

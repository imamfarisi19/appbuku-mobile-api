using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AppBuku.Models;
using System.Collections.ObjectModel;
using AppBuku.TMobileFromWeb.Views;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    [QueryProperty(nameof(TheId), nameof(TheId))]
    
    public class ReviewBukuViewModel : BaseViewModel
    {
        // Deklarasi MyHttpClient Service + Constructor Class
        Services.MyHttpClient myHttpClient;
        public ReviewBukuViewModel()
        {
            //reviewBukuGet = new ReviewBuku(); 
            Title = "Rincian Buku"; 

            IsBusy = true; 
            string baseUri = Application.Current.Properties["BaseWebUri"] as string; 
            myHttpClient = new Services.MyHttpClient(baseUri); 
            IsBusy = false; 
        } 

        bool isNewItem = true;

        private Buku bukuEdit;
        public Buku BukuEdit
        {
            get { return bukuEdit; }
            set { SetProperty(ref bukuEdit, value); }
        }
        

        public string theId;
        public string TheId
        {
            get
            {
                return theId;
            }
            set
            {
                theId = value;
                LoadById(value);
            }
        }

        public async void LoadById(string theId)
        {
            if (string.IsNullOrEmpty(theId))
                return;

            int id = 0;
            if (int.TryParse(theId, out id) == false)
                return;

            string hslXReview = await myHttpClient.HttpGet("api/XReviewByBukuId/", theId);
            string hslXBuku = await myHttpClient.HttpGet("api/XBuku/", theId);
            HasilGet = hslXBuku;
            BukuEdit = JsonConvert.DeserializeObject<Buku>(hslXBuku);
            //ReviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);
            //idBuku = BukuEdit.Id.ToString();

            var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hslXReview);
            ListReviewBukuById = aLists;

            isNewItem = false;
            HapusIsVisible = true;
        }

        public Buku selectedCmdAdd;
        public Buku SelectedCmdAdd
        {
            get => selectedCmdAdd;
            set
            {
                SetProperty(ref selectedCmdAdd, value);
                PerformCmdAddTapped(value);
            }
        }

        private Command<Buku> cmdAddTapped;
        public Command<Buku> CmdAddTapped
        {
            get
            {
                if (cmdAddTapped == null)
                {
                    cmdAddTapped = new Command<Buku>(PerformCmdAddTapped);
                }

                return cmdAddTapped;
            }
        }

        async void PerformCmdAddTapped(Buku item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(AddReviewPage)}?{nameof(AddReviewViewModel.TheBukuId)}={item.Id}");
            //await Shell.Current.GoToAsync(
            //    $"{nameof(AddReviewPage)}?{nameof(AddReviewViewModel.myIdBook)}={item.Id}");
        }

        public Buku cmdAdded;
        public Buku CmdAdded
        {
            get => cmdAdded;
            set
            {
                SetProperty(ref cmdAdded, value);
                PerformCmdAddTapped(value);
            }
        }

        private ICommand cmdAdd;
        public ICommand CmdAdd
        {
            get
            {
                if (cmdAdd == null)
                {
                    cmdAdd = new Command(PerformCmdAdd);
                }

                return cmdAdd;
            }
        }

        private async void PerformCmdAdd()
        {

            await Shell.Current.GoToAsync(
                $"{nameof(AddReviewPage)}?{nameof(AddReviewViewModel)}");
        }

        public ReviewBuku selectedReview;
        public ReviewBuku SelectedReview
        {
            get => selectedReview;
            set
            {
                SetProperty(ref selectedReview, value);
                PerformReviewTapped(value);
            }
        }

        private Command<ReviewBuku> reviewTapped;
        public Command<ReviewBuku> ReviewTapped
        {
            get
            {
                if (reviewTapped == null)
                {
                    reviewTapped = new Command<ReviewBuku>(PerformReviewTapped);
                }

                return reviewTapped;
            }
        }

        async void PerformReviewTapped(ReviewBuku item)
        {
            if (item == null)
                return;

            //await Shell.Current.GoToAsync(
            //    $"{nameof(AddReviewPage)}?{nameof(AddReviewViewModel.TheBukuId)}={item.BukuId}");
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
            bool jwb = await Application.Current.MainPage.DisplayAlert("Hapus data",
                "Apakah anda yakin untuk menghapus data ini?", "Ya", "Tidak");
            if (jwb)
            {
                //baseBukuProcessing.Delete(BukuEdit.Id);
                await Shell.Current.GoToAsync("..");
            }
        }

        private ICommand cmdSimpan;
        public ICommand CmdSimpan
        {
            get
            {
                if (cmdSimpan == null)
                {
                    cmdSimpan = new Command(PerformCmdSimpan);
                }

                return cmdSimpan;
            }
        }

        private async void PerformCmdSimpan()
        {
            if (isNewItem)
            {
                //baseBukuProcessing.Insert(BukuEdit);
            }
            else
            {
                //baseBukuProcessing.Update(BukuEdit);
            }
            await Shell.Current.GoToAsync("..");
        }

        private bool hapusIsVisible;
        public bool HapusIsVisible
        {
            get => hapusIsVisible;
            set => SetProperty(ref hapusIsVisible, value);
        }

        private string hasilGet;
        public string HasilGet
        { get => hasilGet; set => SetProperty(ref hasilGet, value); }

        private ReviewBuku reviewBukuGet;
        public ReviewBuku ReviewBukuGet
        { get => reviewBukuGet; set => SetProperty(ref reviewBukuGet, value); }

        private List<ReviewBuku> listReviewBukuById;
        public List<ReviewBuku> ListReviewBukuById
        { get => listReviewBukuById; set => SetProperty(ref listReviewBukuById, value); }

        [Obsolete]
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new Uri(url));
        });

        public string idBuku;
        public string IdBuku
        { get => idBuku; set => SetProperty(ref idBuku, value); }
    }
}

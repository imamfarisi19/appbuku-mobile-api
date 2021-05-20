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


        private string theId;
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

        private async void LoadById(string theId)
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

            var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hslXReview);
            ListReviewBukuById = aLists;

            isNewItem = false;
            HapusIsVisible = true;
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
            await Shell.Current.GoToAsync(
                $"{nameof(AddReviewPage)}?{nameof(AddReviewViewModel)}");
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


        private ICommand cmdUrl;
        public ICommand CmdUrl
        {
            get
            {
                if (cmdUrl == null)
                {
                    cmdUrl = new Command(PerformCmdUrl);
                }

                return cmdUrl;
            }
        }

        private void PerformCmdUrl()
        {
            string tes = "testing gesture tap";
            CmdUrltest = tes;
        }

        private string cmdUrltest;
        public string CmdUrltest
        { get => cmdUrltest; set => SetProperty(ref cmdUrltest, value); }

        private ICommand cmdTapUrl;
        public ICommand CmdTapUrl
        {
            get
            {
                if (cmdTapUrl == null)
                {
                    cmdTapUrl = new Command(PerformCmdTapUrl);
                }

                return cmdTapUrl;
            }
        }

        private void PerformCmdTapUrl()
        {
        }

        [Obsolete]
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new Uri(url));
        });
    }
}

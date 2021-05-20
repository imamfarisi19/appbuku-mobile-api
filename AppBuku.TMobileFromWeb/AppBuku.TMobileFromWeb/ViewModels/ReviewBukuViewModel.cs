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

namespace AppBuku.TMobileFromWeb.ViewModels
{
    [QueryProperty(nameof(TheId), nameof(TheId))]
    public class ReviewBukuViewModel : BaseViewModel
    {
        // Deklarasi MyHttpClient Service + Constructor Class
        Services.MyHttpClient myHttpClient;
        public ReviewBukuViewModel()
        {
            reviewBukuGet = new ReviewBuku();
            Title = "Ulasan";

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

            string hsl = await myHttpClient.HttpGet("api/XReviewByBukuId/", "1");
            string hsl2 = await myHttpClient.HttpGet("api/XBuku/");
            HasilGet = hsl;
            BukuEdit = JsonConvert.DeserializeObject<Buku>(hsl2);
            ReviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);
            var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hsl);
            ListReviewBukuById = aLists;

            //Buku b1 = Get(id);
            //BukuEdit = b1;
            isNewItem = false;
            HapusIsVisible = true;
            Title = "Edit Buku";
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
    }
}

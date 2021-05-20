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
using System.Linq;
using AppBuku.TMobileFromWeb.Views;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    public class HalamanBukuViewModel : BaseViewModel
    {
        Services.MyHttpClient myHttpClient;

        public HalamanBukuViewModel()
        {
            Title = "Daftar Buku";
            //reviewBukuGet = new ReviewBuku();

            IsBusy = true;
            string baseUri = Application.Current.Properties["BaseWebUri"] as string;
            myHttpClient = new Services.MyHttpClient(baseUri);
            IsBusy = false;

        }

        private List<Buku> bukuSet;
        public List<Buku> BukuSet { get => bukuSet; set => SetProperty(ref bukuSet, value); }

        private string hasilGet;
        public string HasilGet { get => hasilGet; set => SetProperty(ref hasilGet, value); }

        private ICommand cmdReload;
        public ICommand CmdReload
        {
            get
            {
                if (cmdReload == null)
                {
                    cmdReload = new Command(PerformCmdReload);
                }

                return cmdReload;
            }
        }

        private async void PerformCmdReload()
        {
            if (!myHttpClient.IsEnable)
            {
                HasilGet = "MyHttpClient disabled!";
                return;
            }

            IsBusy = true;
            try
            {
                string hsl = await myHttpClient.HttpGet("api/XBuku/");                
                var aLists = JsonConvert.DeserializeObject<List<Buku>>(hsl);
                BukuSet = aLists;

            }
            catch (Exception ex)
            {
                HasilGet = "ERROR: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;

        }

        public Buku selectedBuku;
        public Buku SelectedBuku
        {
            get => selectedBuku;
            set
            {
                SetProperty(ref selectedBuku, value);
                PerformBukuTapped(value);
            }
        }

        private Command<Buku> bukuTapped;
        public Command<Buku> BukuTapped
        {
            get
            {
                if (bukuTapped == null)
                {
                    bukuTapped = new Command<Buku>(PerformBukuTapped);
                }

                return bukuTapped;
            }
        }

        async void PerformBukuTapped(Buku item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(ReviewBukuPage)}?{nameof(ReviewBukuViewModel)}={item.Id}");
        }


    }
}

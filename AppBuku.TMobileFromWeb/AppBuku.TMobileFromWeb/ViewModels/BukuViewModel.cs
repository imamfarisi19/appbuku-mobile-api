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
using Xamarin.Forms;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    public class BukuViewModel : BaseViewModel
    {
        Services.MyHttpClient myHttpClient;

        public BukuViewModel()
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

        private ICommand cmdGetData;
        public ICommand CmdGetData
        {
            get
            {
                if (cmdGetData == null)
                {
                    cmdGetData = new Command(async () => await PerformCmdGetDataAsync());
                }

                return cmdGetData;
            }
        }

        private async Task PerformCmdGetDataAsync()
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
                HasilGet = hsl;
                // reviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);
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
    }
}

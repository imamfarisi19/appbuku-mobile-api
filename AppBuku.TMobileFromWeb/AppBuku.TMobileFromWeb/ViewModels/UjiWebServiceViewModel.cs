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
    public class UjiWebServiceViewModel : BaseViewModel
    {
        // Deklarasi MyHttpClient Service + Constructor Class
        Services.MyHttpClient myHttpClient;
        Random rnd = new Random();
        string[] NamaReviewers = new string[] { "Adam", "Samuel", "Dave", "Joe" };

        public UjiWebServiceViewModel()
        {
            Title = "UJI WEB SERVICE";
            reviewBukuGet = new ReviewBuku();
            IsBusy = true;

            string baseUri = Application.Current.Properties["BaseWebUri"] as string;
            myHttpClient = new Services.MyHttpClient(baseUri);
            IsBusy = false;

        }

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
                string hsl = await myHttpClient.HttpGet("api/XReviewByBukuId/", "1");
                HasilGet = hsl;
                // reviewBukuGet = JsonConvert.DeserializeObject<ReviewBuku>(hsl);
                var aLists = JsonConvert.DeserializeObject<List<ReviewBuku>>(hsl);
                ListReviewBukuById = aLists;

                // untuk setiap data yang ada pada aLists
                foreach (ReviewBuku listform in aLists)
                {
                    ReviewBukuGet = listform;
                }
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

        private List<ReviewBuku> listReviewBukuById;
        public List<ReviewBuku> ListReviewBukuById
        { get => listReviewBukuById; set => SetProperty(ref listReviewBukuById, value); }

        private ReviewBuku reviewBukuGet;
        public ReviewBuku ReviewBukuGet
        { get => reviewBukuGet; set => SetProperty(ref reviewBukuGet, value); }

        private ICommand cmdPutData;

        public ICommand CmdPutData
        {
            get
            {
                if (cmdPutData == null)
                {
                    cmdPutData = new Command(async () => await PerformCmdPutDataAsync());
                }

                return cmdPutData;
            }
        }

        private async Task PerformCmdPutDataAsync()
        {
            string nama = NamaReviewers[rnd.Next(NamaReviewers.Length)];
            int rating = rnd.Next(1, 5);
            ReviewBuku r1 = new ReviewBuku()
            {
                Id = 3,
                BukuId = 1,
                Nama = nama,
                Rating = rating,
                IsiReview = $"Bapak {nama} menyatakan bahwa nilai buku adalah {rating}"
            };

            IsBusy = true;
            try
            {
                string hsl = await myHttpClient.HttpPut("api/XReview", "3", r1);
                StatusPut = hsl;
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

        private ICommand cmdPostData;

        public ICommand CmdPostData
        {
            get
            {
                if (cmdPostData == null)
                {
                    cmdPostData = new Command(async () => await PerformCmdPostDataAsync());
                }

                return cmdPostData;
            }
        }

        private async Task PerformCmdPostDataAsync()
        {
            string nama = "Imam Farisi";
            int rating = rnd.Next(1, 5);
            ReviewBuku r1 = new ReviewBuku()
            {
                // Id = 1,
                BukuId = 1,
                Nama = nama,
                Rating = rating,
                IsiReview = $"{nama} menyatakan bahwa rating buku adalah {rating}. Lorem " +
                            "ipsum dolor sit amet, consectetur adipiscing elit, sed do " +
                            "eiusmod tempor incididunt ut labore et dolore magna aliqua."
            };

            IsBusy = true;
            try
            {
                string hsl = await myHttpClient.HttpPost("api/XReview", r1);
                StatusPost = hsl;
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

        private ICommand cmdDeleteData;

        public ICommand CmdDeleteData
        {
            get
            {
                if (cmdDeleteData == null)
                {
                    cmdDeleteData = new Command(async () => await PerformCmdDeleteDataAsync());
                }

                return cmdDeleteData;
            }
        }

        private async Task PerformCmdDeleteDataAsync()
        {
            IsBusy = true;
            try
            {
                string hsl = await myHttpClient.HttpDelete("api/XReview", "5");
                StatusDelete = hsl;
            }
            catch (Exception ex)
            {
                statusDelete = "ERROR: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private string statusPut;
        public string StatusPut { get => statusPut; set => SetProperty(ref statusPut, value); }

        private string hasilGet;
        public string HasilGet { get => hasilGet; set => SetProperty(ref hasilGet, value); }

        private string statusPost;
        public string StatusPost { get => statusPost; set => SetProperty(ref statusPost, value); }

        private string statusDelete;
        public string StatusDelete { get => statusDelete; set => SetProperty(ref statusDelete, value); }

    }
}

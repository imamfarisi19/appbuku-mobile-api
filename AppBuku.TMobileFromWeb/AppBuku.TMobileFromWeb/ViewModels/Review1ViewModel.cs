using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using AppBuku.Models;

namespace AppBuku.TMobileFromWeb.ViewModels
{
    public class Review1ViewModel: BaseViewModel
    {
        Services.MyHttpClient myHttpClient;

        public Review1ViewModel()
        {
            Title = "Single Review";
            IsBusy = true;

            string baseUri = Application.Current.Properties["BaseWebUri"] as string;
            myHttpClient = new Services.MyHttpClient(baseUri);

            IsBusy = false;
        }

        private string jwbWebApi;
        public string JwbWebApi { get => jwbWebApi; set => SetProperty(ref jwbWebApi, value); }

        private ReviewBuku review1;
        public ReviewBuku Review1 { get => review1; set => SetProperty(ref review1, value); }


        private ICommand tryQueryCmd;
        public ICommand TryQueryCmd
        {
            get
            {
                if (tryQueryCmd == null)
                {
                    tryQueryCmd = new Command(async() => await PerformTryQueryCmdAsync());
                }

                return tryQueryCmd;
            }
        }

        private async System.Threading.Tasks.Task PerformTryQueryCmdAsync()
        {
            IsBusy = true;
            if (!myHttpClient.IsEnable)
            {
                JwbWebApi = "HTTP Client disable";
                IsBusy = false;
                return;
            }

            JwbWebApi = await myHttpClient.HttpGet("api/XReview/","2");
            Review1 = JsonConvert.DeserializeObject<ReviewBuku>(JwbWebApi);
            IsBusy = false;
        }

        private bool isLoading;

        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

    }
}

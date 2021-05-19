using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuku.TMobileFromWeb.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HalamanBukuPage : ContentPage
    {
        ViewModels.HalamanBukuViewModel _viewModel;
        public HalamanBukuPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new ViewModels.HalamanBukuViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
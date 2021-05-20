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
    public partial class ReviewBukuPage : ContentPage
    {
        public ReviewBukuPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ReviewBukuViewModel();
        }
    }
}
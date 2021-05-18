using AppBuku.TMobileFromWeb.ViewModels;
using AppBuku.TMobileFromWeb.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppBuku.TMobileFromWeb
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

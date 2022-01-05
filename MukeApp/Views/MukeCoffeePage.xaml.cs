using MukeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MukeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MukeCoffeePage : ContentPage
    {
        public MukeCoffeePage()
        {
            InitializeComponent();
            
            //BindingContext = new MukeApp.ViewModels.MukeCoffeeViewModel();
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var coffee = ((ListView)sender).SelectedItem as Coffee;
            if (coffee == null)
            {
                return;
            }
            await DisplayAlert("Coffee Selected", coffee.Name, "OK");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var coffee = ((MenuItem)sender).BindingContext as Coffee;
            if (coffee == null)
            {
                return;
            }
            await DisplayAlert("Coffee Favorite", coffee.Name, "OK");
        }
    }
}
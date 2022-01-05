using MukeApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MukeApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
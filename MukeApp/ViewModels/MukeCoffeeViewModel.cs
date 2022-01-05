using MukeApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MukeApp.ViewModels
{
    public class MukeCoffeeViewModel : ViewModelBase
    {
        //call server command
        public ICommand CallServerCommand { get; }

        //obervalblecollection
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; }
        public AsyncCommand RefreshCommand { get; }
        public MukeCoffeeViewModel()
        {
            
            
            Coffee = new ObservableRangeCollection<Coffee>();
            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
            var coff = new List<Coffee> { 
                new Coffee{Roaster="Yes pliz", Name="Sip of sunshine", Image=image },
                new Coffee { Roaster="Yes pliz", Name="Potent potable", Image=image},
                new Coffee{Roaster="Blue bottle", Name="Kenya kiambu", Image=image},
                new Coffee{Roaster="Muke", Name="Muke today", Image=image },
                new Coffee { Roaster="Muke", Name="Muke Sweet", Image=image},
                new Coffee{Roaster="Muke", Name="Muke Orange", Image=image},
                new Coffee{Roaster="Muke", Name="Muke Purple", Image=image },
                new Coffee { Roaster="Muke", Name="Muke Green", Image=image},
                new Coffee{Roaster="Muke", Name="Muke Apple", Image=image}
            };
            Coffee.AddRange(coff);
            //add groupings
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();
            string[] groups = (from c in Coffee
                               select c.Roaster).Distinct().ToArray();
            foreach (string group in groups)
            {
                var col =Coffee.Where(c => c.Roaster == group).ToList();
                CoffeeGroups.Add(new Grouping<string, Coffee>(group, (IEnumerable<Coffee>)col));
            }
            //foreach(Coffee c in Coffee)
            //{
            //    CoffeeGroups.Add(new Grouping<string, Coffee>(c.Roaster, c));
            //}

            RefreshCommand = new AsyncCommand(Refresh);
            FavouriteCoffeeCommand = new AsyncCommand<Coffee>(Favourite);
            Title = "Muke's Coffee App";
        }


        int count = 0;

        public string countDisplay = "Click me!";

        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);

        }

        private void btnClick_Clicked(object sender, EventArgs e)
        {
            count++;
            CountDisplay = $"You clicked {count} time{(count == 1 ? "" : "s")}";
        }

        public ICommand IncreaseCount { get; }
        
        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} time{(count == 1 ? "" : "s")}";
        }

        

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }

        //MVVM for ListView events
        //backup coffee
        Coffee previouslySelectedCoffee;
        Coffee selectedCoffee;

        public Coffee SelectedCoffee { get => selectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    previouslySelectedCoffee = value;
                    value = null;
                }
                selectedCoffee = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand<Coffee> FavouriteCoffeeCommand { get; }
        public async Task Favourite(Coffee value)
        {
            if (value == null)
            {
                return;
            }
           await Application.Current.MainPage.DisplayAlert("Favourite Coffee", value.Name, "OK");
        }

    }
}

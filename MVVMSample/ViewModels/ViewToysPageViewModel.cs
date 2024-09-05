using MVVMSample.Models;
using MVVMSample.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMSample.ViewModels
{
    class ViewToysPageViewModel:ViewModelBase
    {
        #region Fields
        private double? price;
        private ObservableCollection<Toy> toys;
        private ToyService toyService;
        private List<Toy> fullList;
        private bool isRefreshing;



        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Toy> Toys
        {
            get => toys;
            set
            {
                if (toys != value)
                {
                    toys = value;
                    OnPropertyChanged();
                }
            }
        }
        public double? Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged();
                    RefreshCommands();
                }
            }
        }

        #endregion

        #region COMMANDS

        public ICommand DeleteCommand
        {
        get; private set; }
        public ICommand RefreshCommand
        {
            get; private set;
        }
        public ICommand FilterAbovePriceCommand
        {
            get; private set;
        }

        public ICommand FilterBelowPriceCommand
        {
            get;private set;
        }
        #endregion

        #region Constructor
        public ViewToysPageViewModel()
        {
            #region Init Data
            Price = null;
            toyService=new ToyService();
            //toys=new ObservableCollection<Toy>(toyService.GetToys());
           fullList = toyService.GetToys();
            Toys =new ObservableCollection<Toy>();
            foreach(var toy in fullList)
                Toys.Add(toy);
            #endregion

            #region Init Commands
            FilterAbovePriceCommand = new Command(execute:FilterAbove,()=>Toys!=null&&Toys.Count>0);
            FilterBelowPriceCommand = new Command(FilterBelow,()=>Price>0);
            RefreshCommand = new Command(Refresh);
            DeleteCommand = new Command<Toy>((t) => {if( toyService.DeleteToy(t))Refresh(); });

            #region Commands By LINQ
            //FilterAbovePriceCommand = new Command(() => Toys = new ObservableCollection<Toy>(Toys.Where(t => t.Price > Price)));
            //FilterBelowPriceCommand = new Command(() => {
            //    var toys = Toys.Where(t => t.Price > Price);
            //    foreach (var toy in toys)
            //    {
            //       Toys.Remove(toy);
            //    }
            //});
            #endregion

            #endregion

        }


        #endregion

        #region Methods
        private void Refresh()
        {
            IsRefreshing = true;
            fullList = toyService.GetToys();
            Toys = new ObservableCollection<Toy>(fullList);
            Price = null;
            RefreshCommands();
            IsRefreshing = false;
        }
        private void FilterAbove()
        {
            //כל הצעצועים שגדולים מהמחיר
            var toys = fullList.Where(t => t.Price > Price).ToList();
            Toys.Clear();
            foreach (var t in toys)
                Toys.Add(t);
            RefreshCommands();
            
        } private void FilterBelow()
        {
            var toys = fullList.Where(t => t.Price <= Price);
            Toys.Clear();
            foreach (var t in toys)
                Toys.Add(t);
            RefreshCommands();

        }

        private void RefreshCommands()
        {
            var filterabove = FilterAbovePriceCommand as Command;
            if (filterabove != null)
            {
                filterabove.ChangeCanExecute();

            }
            var filterbelow = FilterBelowPriceCommand as Command;

            filterbelow?.ChangeCanExecute();

            

        }

        
        #endregion


    }
}

using MVVMSample.Models;
using MVVMSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample.ViewModels
{
    #region פרמטרים ממסכים אחרים
   
    #endregion
    class ToyDetailsPageViewModel : ViewModelBase
    {
        private ToyService toyService;
        private int id;
        private Toy? selectedToy;


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                  
                    //Fetch the Toy by Id
                   
                }
               

            }
        }

        

        public Toy? SelectedToy
        {
            get => selectedToy;
            set
            {
                if (selectedToy != value)
                {
                    selectedToy = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SecondHandStatus));
                }
            }
        }

        

        public string SecondHandStatus => SelectedToy?.IsSecondHand == true ? "Condition: Second Hand" : "Condition: New";

        public ToyDetailsPageViewModel()
        {
            toyService = new ToyService();
            

        }


    }
}

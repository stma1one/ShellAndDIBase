using MVVMSample.Views;
using System.Windows.Input;

namespace MVVMSample
{
    public partial class AppShell : Shell
    {
      
        public AppShell()
        {
            InitializeComponent();
        
            #region רישום מסכים פנימיים
            Routing.RegisterRoute("/Details", typeof(ToyDetailsPage));
            #endregion
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await this.DisplayAlert("טרם פותח", "טרם פותח", "ביטול");
        }
    }
}

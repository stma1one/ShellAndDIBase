
using MVVMSample.ViewModels;

namespace MVVMSample.Views;

public partial class ViewToysPage : ContentPage
{
	public ViewToysPage()
	{
		InitializeComponent();
		BindingContext = new ViewToysPageViewModel();
		
	}
}
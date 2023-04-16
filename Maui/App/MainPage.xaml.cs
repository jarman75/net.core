using App.ViewModel;

namespace App;

public partial class MainPage : ContentPage
{	
	public MainPage(MainViewModel mv)
	{
		InitializeComponent();
		BindingContext = mv;
	}
	
}


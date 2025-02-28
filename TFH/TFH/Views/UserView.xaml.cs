using TFH.ViewModels;

namespace TFH.Views;

public partial class UserView : ContentPage
{
	public UserView(UserViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
    }
}
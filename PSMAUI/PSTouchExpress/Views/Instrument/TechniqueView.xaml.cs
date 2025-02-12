using NNN.Core.Presentation.MAUI.Helpers;

namespace PSTouchExpress.Views;

public partial class TechniqueView : ContentView
{
	public TechniqueView()
	{
		InitializeComponent();
        //ComboBoxStackLayout.IsVisible = true;
        UIElements();
    }

    async void UIElements()
    {
        await PSTasks.ActionTask(async () =>
        {
            await Task.Delay(100);
            //ComboBoxStackLayout.IsVisible = false;
        });
    }
}
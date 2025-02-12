using NNN.Core.Presentation.MAUI.Helpers;
//using PSTouchExpress.Models;
using System.Diagnostics;
//using Telerik.Maui.Controls;

namespace PSTouchExpress.Pages
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            Initialize();
            var screen = Application.Current;
           

        }
        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                InitializeComponent();
                if (this.BindingContext == null)
                    this.BindingContext = new MainPageModel();
                //BusyIndicator.IsBusy = false;
            });
        }

        //private void OnAddTabClicked(object sender, EventArgs eventArgs)
        //{
        //    var itemIndex = this.PlotDataTabView1.Items.Count;
        //    var itemText = $"Tab {itemIndex}";
        //    var tabViewItem = new TabViewItem
        //    {
        //        HeaderText = itemText,
        //        Content = new Label
        //        {
        //            Text = itemText,
        //            HorizontalOptions = LayoutOptions.Center,
        //            VerticalOptions = LayoutOptions.Center
        //        }
        //    };

        //    this.PlotDataTabView1.Items.Add(tabViewItem);
        //}

    }
}

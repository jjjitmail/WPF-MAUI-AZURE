using System;
using NNN.Core.Presentation.MAUI.Helpers;
using Telerik.Maui.Controls;

namespace PSTouchExpress.Pages
{
	public partial class DataPage : ContentView
    {
		public DataPage()
		{
            Initialize();
            var positionX = this.X;
            //this.Margin = new Thickness(200, 0, 0, 0);
        }

        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                InitializeComponent();
                //MessagingCenter.Subscribe<DataPage>(this, "DataPageDisplay", async (arg) =>
                //{
                //    // X, Y need to be updated based on type/format of the device
                //    await this.TranslateTo(arg.IsPresented ? 255 : -255, 0, 350, Easing.CubicInOut);
                //    //await this.RotateTo(360, 2000);
                //});
            });
        }

        private void OnAddTabClicked(object sender, EventArgs eventArgs)
        {
            var itemIndex = this.PlotDataTabView1.Items.Count;
            var itemText = $"Tab {itemIndex}";
            var tabViewItem = new TabViewItem
            {
                HeaderText = itemText,
                Content = new Label
                {
                    Text = itemText,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }
            };

            this.PlotDataTabView1.Items.Add(tabViewItem);
        }

        private void AddTab(object sender, System.EventArgs e)
        {
            //var tabViewItem = new TabViewItem
            //{
            //    HeaderText = "Tab1",
            //    Content = new Label
            //    {
            //        Text = "My Custom Tab Content"
            //    }
            //};

            //this.PlotDataTabView1.Items.Add(tabViewItem);
        }
    }
}
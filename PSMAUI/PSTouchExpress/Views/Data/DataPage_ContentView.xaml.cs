using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using PSTouchExpress.Helpers;
using Telerik.Maui.Controls;

namespace PSTouchExpress.Views
{
	public partial class DataPage_ContentView : ContentView
    {
		public DataPage_ContentView()
		{

            InitializeComponent();

        }
        async void Initialize()
        {
            await PSTasks.ActionTask(async () =>
            {
                InitializeComponent();
                //this.PlotDataTabView.Items[1].IsSelected = true;
                //this.PlotDataTabView.Items[2].IsSelected = true;

                //this.PlotDataTabView.Items[1].IsSelected = false;
                //this.PlotDataTabView.Items[2].IsSelected = false;
                //var tabViewItem = new TabViewItem
                //{
                //    HeaderText = "\uE9D9 My Custom Tab",
                //    Content = new Label
                //    {
                //        Text = "My Custom Tab Content"
                //    }
                //};

                //this.PlotDataTabView.Items.Add(tabViewItem);

            });
        }

        async void AddTab(object sender, EventArgs args)
        {
            var tabViewItem = new TabViewItem
            {
                HeaderText = "\uE9D9 Tab1",
                Content = new Label
                {
                    Text = "My Custom Tab Content"
                }
            };

            this.PlotDataTabView1.Items.Add(tabViewItem);

            //await PSTasks.ActionTask(async () =>
            //{
            //    var tabViewItem = new TabViewItem
            //    {
            //        HeaderText = "\uE9D9 Tab1",
            //        Content = new Label
            //        {
            //            Text = "My Custom Tab Content"
            //        }
            //    };

            //    this.PlotDataTabView.Items.Add(tabViewItem);
            //});
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

        //private void button_Clicked(object sender, System.EventArgs e)
        //{
        //    var tabViewItem = new TabViewItem
        //    {
        //        HeaderText = "Tab1",
        //        Content = new Label
        //        {
        //            Text = "My Custom Tab Content"
        //        }
        //    };

        //    this.PlotDataTabView1.Items.Add(tabViewItem);
        //}
    }
}
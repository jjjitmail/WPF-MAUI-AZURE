using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
//using PSTouchExpress.Models;
using PSTouchExpress.Pages;

namespace PSTouchExpress.Views
{
	public partial class NavigationBar : ContentView
    {
		public NavigationBar()
		{
            Initialize();
        }
        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                InitializeComponent();

                //((MainPageModel)this.BindingContext).AppWidth = MainPage.Width;
                //((MainPageModel)this.BindingContext).AppHeight = MainPage.Height;
            });
        }
    }
}
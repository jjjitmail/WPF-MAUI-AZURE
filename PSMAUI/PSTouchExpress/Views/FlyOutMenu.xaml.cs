using System;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
using NNN.Core.Presentation.MAUI.Models;

namespace PSTouchExpress.Views
{   
	public partial class FlyOutMenu : ContentView
    {
        public FlyOutMenu()
		{
            InitializeComponent();
            Initialize();
        }

        async void Initialize()
        {
            await PSTasks.ActionTask(() => 
            {                
                PSMessaging.Subscribe<FlyOutMenuStatus>(this, "fmenu", async (arg) =>
                {
                    // X, Y need to be updated based on type/format of the device
                    await this.TranslateTo(arg.IsPresented ? 255 : -255, 0, 350, Easing.CubicInOut);
                    //await this.RotateTo(360, 2000);
                });
                //this.Unfocused += FlyOutMenu_Unfocused; // still not working             
            });
        }

        private void FlyOutMenu_Unfocused(object sender, FocusEventArgs e)
        {
            this.TranslateTo(255, 0, 350, Easing.CubicInOut);
        }

        //public static readonly BindableProperty PageModelProperty =
        //    BindableProperty.Create(nameof(PageModel), typeof(IPageModel), typeof(FlyOutMenu), null);

        //public IPageModel PageModel
        //{
        //    get => (IPageModel)GetValue(FlyOutMenu.PageModelProperty);
        //    set => SetValue(FlyOutMenu.PageModelProperty, value);
        //}

    }
}
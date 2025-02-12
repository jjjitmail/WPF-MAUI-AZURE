using System;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Presentation.MAUI.Helpers;

namespace PSTouchExpress.Pages
{
	public partial class ConnectionsPage : ContentView
    {
		public ConnectionsPage()
		{
            Initialize();			
		}

        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                InitializeComponent();
            });
        }

        public static readonly BindableProperty TargetObjectProperty = BindableProperty.Create(nameof(TargetObject), typeof(object), typeof(ConnectionsPage), null);

        public object TargetObject
        {
            get { return (object)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }
    }
}
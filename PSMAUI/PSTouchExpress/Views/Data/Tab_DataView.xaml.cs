using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using NNN.Core.Presentation.MAUI.Helpers;

namespace PSTouchExpress.Views
{
	public partial class Tab_DataView // : TabbedPage
    {
		public Tab_DataView()
		{
            Initialize();
        }
        async void Initialize()
        {
            await PSTasks.ActionTask(async () =>
            {
                InitializeComponent();
            });
        }
    }
}
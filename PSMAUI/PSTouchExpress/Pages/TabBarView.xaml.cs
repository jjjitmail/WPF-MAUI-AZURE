using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace PSTouchExpress.Pages
{
	public partial class TabBarView : ContentView
	{
		public TabBarView()
		{
			InitializeComponent();
		}

		async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			await SetBackgroundColor();
			await Task.Delay(60);
			await SetBackBackgroundColor();
		}

		async Task SetBackgroundColor()
		{
			ConnectionLabel.BackgroundColor = Color.FromArgb("#15425c");
			//SemanticScreenReader.Announce(CounterLabel.Text);
		}

		async Task SetBackBackgroundColor()
		{
			ConnectionLabel.BackgroundColor = Color.FromArgb("#0090C9");
		}
	}
}
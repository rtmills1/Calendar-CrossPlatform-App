using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CalendarApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
		private async void NavigateButton_OnClicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new CalendarAppPage());
		}
	}
}

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
			string format = "D";
            currentTime.Text = DateTime.Now.ToString(format);
        }
		private async void NavigateButton_OnClicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new CalendarAppPage());
		}
		private async void NavigateButton_OnClicked1(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new AddDatesPage());
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CalendarApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
            NavigationPage.SetHasBackButton(this, false);
            // Gets current time to display in string
			string format = "D";
            currentTime.Text = DateTime.Now.ToString(format);


        }
        // Sends users to CalendarAppPage when button is clicked
		private async void NavigateButton_OnClicked(object sender, EventArgs e)
		{
                //Define some default data
                string name = null;
                Color realColor = Color.Gray;
                DateTime fullDate = new DateTime(1990, 1, 1, 1, 1, 1);
           // Sends to Calender page
            await Navigation.PushAsync(new CalendarAppPage(name, fullDate, realColor));
			
		}
        // Navigation function for the second button, sends to AddDatesPage
		private async void NavigateButton_OnClicked1(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new AddDatesPage());

		}

		// Navigation function for the third button, sends to AboutPage
		private async void NavigateButton_OnClicked2(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new AboutPage());

		}

		// Navigation function for the forth button, sends to LoginPage
		private async void NavigateButton_OnClicked3(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new LoginPage());

			var existingPages = Navigation.NavigationStack.ToList();
			foreach (var page in existingPages)
			{
				Navigation.RemovePage(page);
			}

		}

		
	}
}

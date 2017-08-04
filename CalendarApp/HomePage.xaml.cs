﻿using System;
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

            // Gets current time to display in string
			string format = "D";
            currentTime.Text = DateTime.Now.ToString(format);


			var existingPages = Navigation.NavigationStack.ToList();
			foreach (var page in existingPages)
			{
				Navigation.RemovePage(page);
			}

        }
        // Sends users to CalendarAppPage when button is clicked
		private async void NavigateButton_OnClicked(object sender, EventArgs e)
		{
                //Define some default data
                string name = "Go for a run";
                Color realColor = Color.MediumVioletRed;
                DateTime fullDate = DateTime.Now;
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

		
	}
}

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
                string name = "Go for a run";
                Color realColor = Color.Brown;
                DateTime fullDate = DateTime.Now;
           
            await Navigation.PushAsync(new CalendarAppPage(name, fullDate, realColor));
		}
		private async void NavigateButton_OnClicked1(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new AddDatesPage());
		}

	}
}

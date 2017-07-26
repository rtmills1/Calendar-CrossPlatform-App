using Xamarin.Forms;
using XamForms.Controls;
using System;

namespace CalendarApp
{
    public partial class CalendarAppPage : ContentPage
    {
        public CalendarAppPage()
        {
            InitializeComponent();

			Calendar calendar = new Calendar()
			{
				WidthRequest = 300,
				HeightRequest = 300
			};

			calendar.DateClicked += (object sender, DateTimeEventArgs e) =>
			{
				var dateSelect = calendar.SelectedDate;
			};
		}
		
        private async void NavigateButton2_OnClicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new HomePage());
		}
	}
}

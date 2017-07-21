using Xamarin.Forms;
using XamForms.Controls;

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
    }
}

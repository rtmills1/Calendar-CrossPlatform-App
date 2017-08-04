using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CalendarApp
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
			//Heading for the page
			Label header = new Label
			{
				Text = "This app was developed by Riley Mills, using Xarmarin for " +
                    "cross-platform development between Android and iOS.",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

			// Build the page.
			this.Content = new StackLayout
			{
				Children =
				{
					header
					
				}
			};
        }
    }
}

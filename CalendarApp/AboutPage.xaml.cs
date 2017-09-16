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
				Text = "This app was developed by Riley Mills SID:215075797, using Xarmarin for " +
                    "cross-platform development between Android and iOS.",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
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

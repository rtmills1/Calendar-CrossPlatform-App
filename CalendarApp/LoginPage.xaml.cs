using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

using Xamarin.Forms;

namespace CalendarApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();



			//Heading for the page
			Label header = new Label
			{
				Text = "Enter Login Details",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

            var usernameEntry = new Entry { Placeholder = "User Name" };

            var passwordEntry = new Entry { Placeholder = "Password", IsPassword = true };

			Button loginButton = new Button
			{
				Text = "Login",
				Font = Font.SystemFontOfSize(NamedSize.Medium),
				BorderWidth = 2,
				BorderRadius = 3,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("24BDFF"),
				BorderColor = Color.FromHex("006996")


			};
			Button registerButton = new Button
			{
				Text = "Register",
				Font = Font.SystemFontOfSize(NamedSize.Medium),
				BorderWidth = 1,
				BorderRadius = 3,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("24BDFF"),
				BorderColor = Color.FromHex("006996")

			};
			loginButton.Clicked += OnLoginButtonClicked;
			void OnLoginButtonClicked(object sender, EventArgs e)
			{

                Navigation.PushAsync(new HomePage());

				var existingPages = Navigation.NavigationStack.ToList();
				foreach (var page in existingPages)
				{
					Navigation.RemovePage(page);
				}

			}



			// Build the page.
			this.Content = new StackLayout
			{
				Children =
				{
                    header,
                    usernameEntry,
                    passwordEntry,
					loginButton

				}
			};



        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.IO;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;


namespace CalendarApp
{
    public partial class LoginPage : ContentPage
    {
		public async void RetrieveLogin()
		{
            if (userInput != null)
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://introtoapps.com/datastore.php?action=load&appid=215075797&objectid="+userInput);
                var response = await httpClient.SendAsync(request);
            }

            if(passwordInput != null){
				var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://introtoapps.com/datastore.php?action=load&appid=215075797&objectid=" + passwordInput);
				var response = await httpClient.SendAsync(request);
            } 
		}

		public async void SendLogin()
		{
            if (userInput != null)
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + userInput + "&data=" + userInput);
                var response = await httpClient.SendAsync(request);
            }
		}

		public async void SendPassword()
		{
            if (passwordInput != null)
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + passwordInput + "&data=" + passwordInput);
                var response = await httpClient.SendAsync(request);
            }
		}


        public string userInput { get; set; }

        public string passwordInput { get; set; }


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

            usernameEntry.TextChanged += OnUserNameEntry;
            void OnUserNameEntry(object sender, EventArgs e){
                userInput = usernameEntry.Text;
            }


            var passwordEntry = new Entry { Placeholder = "Password", IsPassword = true };

            passwordEntry.TextChanged += OnPasswordEntry;
            void OnPasswordEntry(object sender, EventArgs e){
                passwordInput = passwordEntry.Text;
            }


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
            registerButton.Clicked += OnRegisterButtonClicked;
			void OnRegisterButtonClicked(object sender, EventArgs e)
			{
                if (userInput != null)
                {
                    if (passwordInput != null)
                    {
                        SendLogin();
                        SendPassword();
                        DisplayAlert("Alert", "Account created", "OK");
                    } 
                } else { DisplayAlert("Alert", "Account creation failed", "OK");  }

			}

			loginButton.Clicked += OnLoginButtonClicked;
			void OnLoginButtonClicked(object sender, EventArgs e)
			{
                if (userInput != null)
                {
                    if (passwordInput != null)

                    {
                        RetrieveLogin();
                        Navigation.PushAsync(new HomePage());

                        var existingPages = Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            Navigation.RemovePage(page);
                        }
                    }
                }
                else { DisplayAlert("Alert", "Login failed", "OK"); }

			}



			// Build the page.
			this.Content = new StackLayout
			{
				Children =
				{
                    header,
                    usernameEntry,
                    passwordEntry,
					loginButton,
                    registerButton

				}
			};

        }

    }
}

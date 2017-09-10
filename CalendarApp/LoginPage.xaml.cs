using System;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net;
using Xamarin.Forms;



namespace CalendarApp
{
    public partial class LoginPage : ContentPage
    {
		public async void RetrieveLogin()
		{
            
            if (userInput != null && passwordInput != null)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://introtoapps.com/datastore.php?action=load&appid=215075797&objectid=" + userInput + "&data=" + passwordInput);
                    webRequest.Method = "GET";

                    string responseData = string.Empty;
                    HttpWebResponse httpResponse = (System.Net.HttpWebResponse)await webRequest.GetResponseAsync();

                    using (StreamReader responseReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseData = responseReader.ReadToEnd();
                    }
                    UserMatch = true;
                }
                catch (System.Net.WebException)
                {
                    //Code - If does not Exist 
                    await DisplayAlert("Alert", "Login doesnt exist", "OK");
                    UserMatch = false;
                }

            }
			if (UserMatch == true)
			{

                await Navigation.PushAsync(new HomePage());

				var existingPages = Navigation.NavigationStack.ToList();
				foreach (var page in existingPages)
				{
					Navigation.RemovePage(page);
				}
			}
		}

		public interface SHA512StringHash
		{
			string Hash(string input);
		}


		public async void SendLogin()
		{
            if (userInput != null)
            {
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + userInput);
                var response = await httpClient.SendAsync(request);
            }
		}

		public async void SendPassword()
		{
            if (passwordInput != null)
            {
                string hashedPassword = DependencyService.Get<SHA512StringHash>().Hash(passwordInput);

                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + userInput + "&data=" + hashedPassword);
                var response = await httpClient.SendAsync(request);
            }
		}



		public string userInput { get; set; }

        public string passwordInput { get; set; }

        public bool UserMatch { get; set; }



        public LoginPage()
        {
            InitializeComponent();

           

            userInput = null;
            passwordInput = null;

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
                if (userInput != null && passwordInput != null)
                {
                    RetrieveLogin();

                }
                else { DisplayAlert("Alert", "Login fields empty", "OK");}

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

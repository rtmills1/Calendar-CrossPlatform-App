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
        //Async method for retrieve login data from online datastore
		public async void RetrieveLogin()
		{
            if (userInput != null)
            {
                try
                {
                    // Create web request to retrieve the url matching the current user name and password
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://introtoapps.com/datastore.php?action=load&appid=215075797&objectid=" + userInput + "&data=" + userInput);
                    webRequest.Method = "GET";

                    string responseData = string.Empty;
                    HttpWebResponse httpResponse = (System.Net.HttpWebResponse)await webRequest.GetResponseAsync();

                    using (StreamReader responseReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseData = responseReader.ReadToEnd();
                    }
                    // Results in true value
                    UserMatch = true;
                }
                catch (System.Net.WebException)
                {
                    //Result if error occurs 
                    await DisplayAlert("Alert", "Login doesnt exist", "OK");
                    UserMatch = false;
                }

            }
			if (passwordInput != null)
			{
				try
				{
                    //Get hashed password
                    string hashedPassword = DependencyService.Get<SHA512StringHash>().Hash(passwordInput);

					// Create web request to retrieve the url matching the current user name and password
					HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://introtoapps.com/datastore.php?action=load&appid=215075797&objectid=" + userInput + "." + passwordInput + "&data=" + hashedPassword);
					webRequest.Method = "GET";

					string responseData = string.Empty;
					HttpWebResponse httpResponse = (System.Net.HttpWebResponse)await webRequest.GetResponseAsync();

					using (StreamReader responseReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						responseData = responseReader.ReadToEnd();
					}
                    // Results in true value
					PassMatch = true;
				}
				catch (System.Net.WebException)
				{
					// Result if error occurs
					await DisplayAlert("Alert", "Password doesnt exist", "OK");
					PassMatch = false;
				}

			}
            // Opens homepage if username and password match with online data
            if (UserMatch == true && PassMatch == true)
			{
                await Navigation.PushAsync(new HomePage());

				var existingPages = Navigation.NavigationStack.ToList();
				foreach (var page in existingPages)
				{
					Navigation.RemovePage(page);
				}
			}
		}

        // Interface that uses code from iOS and Android project files to hash passwords
		public interface SHA512StringHash
		{
			string Hash(string input);
		}

		// Async method for sending login date to online datastore
		public async void SendLogin()
		{
            if (userInput != null)
            {
                // Creates http request message to create a url with the users data
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + userInput + "&data=" + userInput);
                var response = await httpClient.SendAsync(request);
            }
		}

		// Async method for sending password data to online datastore
		public async void SendPassword()
		{
            if (passwordInput != null)
            {
                // Hashes password 
                string hashedPassword = DependencyService.Get<SHA512StringHash>().Hash(passwordInput);

                // Creates http request to create a url with the users data
                var httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://introtoapps.com/datastore.php?action=save&appid=215075797&objectid=" + userInput + "." + passwordInput + "&data=" + hashedPassword);
                var response = await httpClient.SendAsync(request);
            }
		}


        //Delcare public variables 
		public string userInput { get; set; }

        public string passwordInput { get; set; }

        public bool UserMatch { get; set; }

        public bool PassMatch { get; set; }


        public LoginPage()
        {
            InitializeComponent();

            // Empty input data
            userInput = null;
            passwordInput = null;

			// Heading for the page
			Label header = new Label
			{
				Text = "Enter Login Details",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

            // Creates entry field for username
            var usernameEntry = new Entry { Placeholder = "User Name" };
            // Gets current text from entry field
            usernameEntry.TextChanged += OnUserNameEntry;
            void OnUserNameEntry(object sender, EventArgs e){
                userInput = usernameEntry.Text;
            }

            // Create entry field for password
            var passwordEntry = new Entry { Placeholder = "Password", IsPassword = true };
            // Gets current text from entry field 
            passwordEntry.TextChanged += OnPasswordEntry;
            void OnPasswordEntry(object sender, EventArgs e){
                passwordInput = passwordEntry.Text;
            }

            // Creates login button
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
            // Creates register button
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
                // Check that username and input values contain values
                if (userInput != null)
                {
                    if (passwordInput != null)
                    {
                        // Runs sendlogin and sendpassword methods
                        SendLogin();
                        SendPassword();
                        DisplayAlert("Alert", "Account created", "OK");
                    } 
                    // Error message displayed
                } else { DisplayAlert("Alert", "Account creation failed", "OK");  }

			}

			loginButton.Clicked += OnLoginButtonClicked;
			void OnLoginButtonClicked(object sender, EventArgs e)
            {
                // Checks that users fields arent empty
                if (userInput != null && passwordInput != null)
                {
                    // Runs retrievelogin method
                    RetrieveLogin();

                }
                // Error message displayed
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

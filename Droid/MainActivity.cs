using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using static CalendarApp.LoginPage;
using System.Security.Cryptography;
using System.Text;
using static CalendarApp.Droid.MainActivity;
using static CalendarApp.Droid.ChooseContactDroid;

// Dependecy link for interfaces
[assembly: Xamarin.Forms.Dependency(typeof(SHA512StringHasImplementation))]
[assembly: Xamarin.Forms.Dependency(typeof(Mobile))]
namespace CalendarApp.Droid
{
    [Activity(Label = "CalendarApp.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

        }

		/* This idea to open contact list is from a user Xamarians at http://blog.xamarians.com/Blog/2017/5/27/xamarin-contacts-picker */
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
            if (Mobile.IntentHelper.IsMobileIntent(requestCode))
			{
                Mobile.IntentHelper.ActivityResult(requestCode, data);
			}
		}
		/* end Xamarians' idea*/

		public class SHA512StringHasImplementation : SHA512StringHash
		{
			public SHA512StringHasImplementation() { }

			/* This idea to hash passwords if from user Olias https://stackoverflow.com/questions/32589456/how-to-use-hash-sha-in-xamarin */
			public string Hash(string input)
			{
				SHA512 shaM = new SHA512Managed();
				// Convert the input string to a byte array and compute the hash.
				byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(input));
				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();
				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				// Return the hexadecimal string.
				input = sBuilder.ToString();
				return (input);
			}
			/* end of Olias idea */
		}

		

		

    }
}

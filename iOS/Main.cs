using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AddressBookUI;
using CalendarApp.iOS;
using Foundation;
using UIKit;
using static CalendarApp.CalendarAppPage;
using static CalendarApp.LoginPage;
using static CalendarApp.AddDatesPage;


[assembly: Xamarin.Forms.Dependency(typeof(SHA512StringHasImplementation))]
[assembly: Xamarin.Forms.Dependency(typeof(ChooseContact))]
namespace CalendarApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }

    public class SHA512StringHasImplementation : SHA512StringHash
    {
        public SHA512StringHasImplementation() { }

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


    }
    public class ChooseContact : IChooseContact
    {
        Task<string> IChooseContact.ChooseContact()
        {
            var task = new TaskCompletionSource<string>();
            try
            {
                ContactPicker.Select(GetController(), (obj) =>
                {
                    if (obj == null)
                    {
                        task.SetResult(null);
                    }
                    else
                    {
                        var values = obj.GetPhones().GetValues();
                        task.SetResult(values.Length > 0 ? values[0] : null);
                    }
                });
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
            return task.Task;
        }
        private UIViewController GetController()
        {
            return UIApplication.SharedApplication.KeyWindow.RootViewController;
        }
    }
}

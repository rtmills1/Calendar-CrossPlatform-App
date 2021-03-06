﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static CalendarApp.CalendarAppPage;

namespace CalendarApp
{
    public partial class AddDatesPage : ContentPage
    {
        // Dictionary to get Color from color name.
            Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
            {
                { "Aqua", Color.Aqua }, { "Black", Color.Black },
                { "Blue", Color.Blue }, { "Yellow", Color.Yellow },
                { "White", Color.White }, { "Green", Color.Green },
                { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
                { "Navy", Color.Navy }, { "Olive", Color.Olive },
                { "Purple", Color.Purple }, { "Red", Color.Red },
                { "Silver", Color.Silver }, { "Teal", Color.Teal },
            };

        // Declare lable
        Label label;

        // Interface for contact selector code located in iOS and Android project files
		public interface IChooseContact
		{
			Task<string> ChooseContact();
		}

        internal AddDatesPage()
        {
            InitializeComponent();

            // Text field for users to enter in name of calendar event
            var nameEntry = new Entry {Placeholder = "Event Name"};
            
            // Defines Date picker for users to select date of calendar event
            DatePicker datePicker = new DatePicker
            {
                Format = "D",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Time picker for users to select time of event
            var time = new TimePicker () { Time = new TimeSpan (17,0,0) };

            // Heading for the page
            Label header = new Label
            {
                Text = "Create New Date",
                FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Color picker for users to select colour for there event
            Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
                                                                             
            };

            // Gets colour definitions from string dictionary
			foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 65,
                HeightRequest = 65,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Color = Color.White
                                               
            };

            // Selected box views colour from index
            picker.SelectedIndexChanged += (sender, args) =>
                {
                    if (picker.SelectedIndex <= -1)
                    {
                        boxView.Color = Color.White;
                    }
                    else
                    {
                        string colorName = picker.Items[picker.SelectedIndex];
                        boxView.Color = nameToColor[colorName];
                    }
                };

            // Unused label, was used to display string outputs of selected values
            label = new Label
            {
                Text = "",
                //Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Entry field to enter in contact information
			var entryContact = new Entry { Placeholder = "Optional - Enter Contact Number or Name" };

            // Button that opens list of system contacts
			var btnChooseContact = new Button
			{
				Text = "Choose Contact",
			};
			btnChooseContact.Clicked += async (s, e) =>
			{
                // Uses contact selector code from iOS and Android projects
				entryContact.Text = await DependencyService.Get<IChooseContact>().ChooseContact();
			};
			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { entryContact, btnChooseContact }
			};


            // Button to pass all the users inputed values into the listview calendar
            Button button = new Button
            {
                Text = "Create Entry",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                BorderRadius = 3,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.White,
                BackgroundColor = Color.FromHex("24BDFF"),
                BorderColor = Color.FromHex("006996")


			};
            button.Clicked += OnButtonClicked;
            void OnButtonClicked(object sender, EventArgs e)
            {

                // Gets values from user entry fields/pickers
                var name = nameEntry.Text;
                var dateDate = datePicker.Date;
                var dateTime = time.Time;
                var contact = entryContact.Text;
                Color realColor = boxView.Color;

                // Combines both Date and time into one data value DateTime
                DateTime fullDate = dateDate + dateTime;

                // Checks if contact entry has a value
                if(contact != null){
                    // Adds contact information to name
                    name = name + " - w/ " + contact;
                }
               
                // label.Text = String.Format("{0}, {1}, {2}", name, fullDate, realColor);
                Navigation.InsertPageBefore(new HomePage(), this);
				Navigation.PopAsync();

                // Sends data to calendar and sends users back to calendar page
                Navigation.PushAsync(new CalendarAppPage(name, fullDate, realColor));
    
                
            }



			// Build the page.
			this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    nameEntry,
                    datePicker,
                    time,
                    picker,
                    boxView,
					entryContact,
					btnChooseContact,
                    button,
                    label


                }
            };
        }

		
            


    
    }
}

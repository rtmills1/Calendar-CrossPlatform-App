using System;
using System.Collections.Generic;
using System.Linq;



using Xamarin.Forms;

namespace CalendarApp
{
    public partial class AddDatesPage : ContentPage
    {
        // Dictionary to get Color from color name.
            Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
            {
                { "Aqua", Color.Aqua }, { "Black", Color.Black },
                { "Blue", Color.Blue }, 
                { "Gray", Color.Gray }, { "Green", Color.Green },
                { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
                { "Navy", Color.Navy }, { "Olive", Color.Olive },
                { "Purple", Color.Purple }, { "Red", Color.Red },
                { "Silver", Color.Silver }, { "Teal", Color.Teal },
                { "White", Color.White }, { "Yellow", Color.Yellow }
            };

        Label label;


        public AddDatesPage()
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

            //Heading for the page
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

                    //Gets values from user entry fields/pickers

                    var name = nameEntry.Text;
                    var dateDate = datePicker.Date;
                    var dateTime = time.Time;
                    Color realColor = boxView.Color;

                    //Combines both Date and time into one data value DateTime
                    DateTime fullDate = dateDate + dateTime;

                //label.Text = String.Format("{0}, {1}, {2}", name, fullDate, realColor);
                    Navigation.InsertPageBefore(new HomePage(), this);
				    Navigation.PopAsync();
				    
                    //Sends data to calendar and sends users back to calendar page
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
                    button,
                    label,

                }
            };
        }


            


    
    }
}

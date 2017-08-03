using System;
using System.Collections.Generic;



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


            var nameEntry = new Entry {Placeholder = "Event Name"};
            

            DatePicker datePicker = new DatePicker
            {
                Format = "D",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var time = new TimePicker () { Time = new TimeSpan (17,0,0) };

            Label header = new Label
            {
                Text = "Create New Date",
                FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
                                                                             
            };


			foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 100,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.White
                                               
            };

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

            label = new Label
            {
                Text = "",
                //Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

           Button button = new Button
            {
                Text = "Create Entry",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
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

                    //Sends data to calendar and sends users back to calendar page
                    Navigation.PushAsync(new CalendarAppPage(name, fullDate, realColor));

                
				
                
            }

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    datePicker,
                    time,
                    nameEntry,
                    picker,
                    button,
                    label,
                    boxView
                }
            };
        }


            


    
    }
}

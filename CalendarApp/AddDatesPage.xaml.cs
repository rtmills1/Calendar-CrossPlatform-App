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
        int clickTotal = 0;

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
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
                {
                    if (picker.SelectedIndex == -1)
                    {
                        boxView.Color = Color.Default;
                    }
                    else
                    {
                        string colorName = picker.Items[picker.SelectedIndex];
                        boxView.Color = nameToColor[colorName];
                    }
                };

            label = new Label
            {
                Text = "0 Date Entry",
                //Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

           Button button = new Button
            {
                Text = "Create Date",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked;
            void OnButtonClicked(object sender, EventArgs e)
            {
	            clickTotal += 1;
	            label.Text = String.Format("{0} Date{1}",
	                                       clickTotal, clickTotal == 1 ? "" : "s");
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

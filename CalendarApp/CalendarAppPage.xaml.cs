using Xamarin.Forms;
using XamForms.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CalendarApp
{
    public partial class CalendarAppPage : ContentPage
    {
        public CalendarAppPage()
        {
            InitializeComponent();

            /*Calendar calendar = new Calendar()
            {
                WidthRequest = 300,
                HeightRequest = 300
            };*/

            /*calendar.DateClicked += (object sender, DateTimeEventArgs e) =>
            {
                var dateSelect = calendar.SelectedDate;
            };*/

            Label header = new Label
            {
                Text = "Calendar",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<CalendarDate> calendarDate = new List<CalendarDate>
            {
                new CalendarDate("Movies", new DateTime(2017, 2, 1, 11, 30, 0), Color.Aqua),
                new CalendarDate("Holiday", new DateTime(2017, 8, 1, 13, 45, 0), Color.Black),
                new CalendarDate("Sell House", new DateTime(2018, 6, 10,14, 0, 0), Color.Purple),
                new CalendarDate("Anniversary", new DateTime(2019, 2, 5, 20, 30, 0), Color.Red)
            };

            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = calendarDate,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                    {
                        // Create views with bindings for displaying each property.
                        Label nameLabel = new Label();
                        nameLabel.SetBinding(Label.TextProperty, "Name");

                        Label dateLabel = new Label();
                        dateLabel.SetBinding(Label.TextProperty,
                            new Binding("Date", BindingMode.OneWay,
                                        null, null, "At {0:h:mm tt MM/dd/yy}"));

                        BoxView boxView = new BoxView();
                        boxView.SetBinding(BoxView.ColorProperty, "Colour");

                        // Return an assembled ViewCell.
                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    boxView,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            dateLabel
                                        }
                                        }
                                }
                            }
                        };
                    })
            };

            // Accomodate iPhone status bar.
            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };

        }

        class CalendarDate
        {
            public CalendarDate(string name, DateTime date, Color colour)
            {
                this.Name = name;
                this.Date = date;
                this.Colour = colour;
            }

            public string Name { private set; get; }

            public DateTime Date { private set; get; }

            public Color Colour { private set; get; }
        };

    }	
}

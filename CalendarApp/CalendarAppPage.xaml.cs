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

            Calendar calendar = new Calendar()
            {
                WidthRequest = 300,
                HeightRequest = 300
            };

            calendar.DateClicked += (object sender, DateTimeEventArgs e) =>
            {
                var dateSelect = calendar.SelectedDate;
            };

            Label header = new Label
            {
                Text = "Calendar",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<Person> people = new List<Person>
            {
                new Person("Movies", new DateTime(2017, 7, 27), Color.Aqua),
                new Person("Holiday", new DateTime(2017, 8, 1), Color.Black),
                new Person("Sell House", new DateTime(2018, 6, 10), Color.Purple),
                new Person("Anniversary", new DateTime(2019, 2, 5), Color.Red)
            };

            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = people,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                    {
                        // Create views with bindings for displaying each property.
                        Label nameLabel = new Label();
                        nameLabel.SetBinding(Label.TextProperty, "Name");

                        Label birthdayLabel = new Label();
                        birthdayLabel.SetBinding(Label.TextProperty,
                            new Binding("Birthday", BindingMode.OneWay,
                                null, null, "On {0:d}"));

                        BoxView boxView = new BoxView();
                        boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

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
                                            birthdayLabel
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

        class Person
        {
            public Person(string name, DateTime birthday, Color favoriteColor)
            {
                this.Name = name;
                this.Birthday = birthday;
                this.FavoriteColor = favoriteColor;
            }

            public string Name { private set; get; }

            public DateTime Birthday { private set; get; }

            public Color FavoriteColor { private set; get; }
        };

    }	
}

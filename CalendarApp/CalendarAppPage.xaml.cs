using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using static CalendarApp.AddDatesPage;
using System.Text.RegularExpressions;
using System.Linq;

namespace CalendarApp
{
    
    public partial class CalendarAppPage : ContentPage
    {
        // Declaring Calendar list
        public List<CalendarDate> list = new List<CalendarDate>();

        // Variables to hold local data for name, date and colour
        public string addName = null;
        public DateTime addDateTime = new DateTime(1900, 1, 1, 1, 1, 1);
        public Color addColour = Color.Gray;

        // Declaring switches
        SwitchCell deleteSwitch;
		SwitchCell todaySwitch;
		SwitchCell monthSwitch;
		SwitchCell yearSwitch;
         
        // Page recieves the variables name, fulldata and realcolor from AddDatePage
        public CalendarAppPage(string name, DateTime fullData, Color realColor)
        {
            InitializeComponent();

            // Saves calendar list to local storage
			if (Application.Current.Properties.ContainsKey("list"))
			{
                // Gives the list CalendarDates and identifier
				var id = Application.Current.Properties["list"] as List<CalendarDate>;
				list = id;
			}

            // Gets recieved values and saves them to local variables
            try
            {
                addName = name;
                addDateTime = fullData;
                addColour = realColor;
            }
            catch
            {
                DisplayAlert("Alert", "no values from add date", "OK");
                return;
            }


            // Switch buttons to filter calendar dates between different periods
            TableView tableView = new TableView
            {
                HeightRequest = 130,
                RowHeight = 30,
                VerticalOptions = LayoutOptions.Start,
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection("Delete")
                    {
                        (deleteSwitch = new SwitchCell
                        {
                            Text = "All Dates:",
                            On = false
                        })
                    },
                    new TableSection ("Filters")
                    {
                        (todaySwitch = new SwitchCell
                        {
                            Text = "Today:",
                            On = false
                        }),
         
                        (monthSwitch = new SwitchCell
                        {
                            Text = "Month:",
                            On = false
                        }),
              
                        (yearSwitch = new SwitchCell{
                            Text = "Year:",
                            On = false
                        })
                    
					}
				}
			};


            // Button that links to AddDatesPage so users can create a calendar item
			Button button = new Button
			{
				Text = "Create Date",
                Font = Font.SystemFontOfSize(NamedSize.Small),
				BorderWidth = 1,
                BorderRadius = 3,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("24BDFF"),
				BorderColor = Color.FromHex("006996")
                                               
			};
             
			// When button is clicked
			button.Clicked += OnButtonClicked;
			void OnButtonClicked(object sender, EventArgs e)
			{
				// Send users to AddDatesPage
				Navigation.PushAsync(new AddDatesPage());

			}


            // Saves calendar dates into the list if values arent empty
            if (addName != null && addDateTime != new DateTime(1900, 1, 1, 1, 1, 1) && addColour != Color.Gray)
			{
                list.Add(new CalendarDate(addName, addDateTime, addColour));
                // Saves current state of list to local storage
                Application.Current.Properties["list"] = list;
			}

            // Example - Testing data
			/*list.Add(new CalendarDate("Study", new DateTime(2017, 9, 9, 23, 30, 0), Color.Aqua));
			list.Add(new CalendarDate("Movies", new DateTime(2017, 2, 1, 11, 30, 0), Color.Aqua));
			list.Add(new CalendarDate("Holiday", new DateTime(2017, 8, 1, 13, 45, 00), Color.Green));
			list.Add(new CalendarDate("Sell House", new DateTime(2017, 8, 20, 14, 00, 00), Color.Purple));
			list.Add(new CalendarDate("Marathon", new DateTime(2017, 9, 5, 20, 30, 0), Color.Yellow));
			list.Add(new CalendarDate("Bills", new DateTime(2017, 10, 5, 20, 30, 0), Color.Blue));
			list.Add(new CalendarDate("Christmas", new DateTime(2017, 12, 25, 7, 0, 0), Color.Red));
			list.Add(new CalendarDate("Family Gathering", new DateTime(2018, 2, 12, 12, 30, 0), Color.GreenYellow));
			list.Add(new CalendarDate("Car Checkup", new DateTime(2018, 4, 5, 14, 45, 0), Color.Pink));
			list.Add(new CalendarDate("Snow", new DateTime(2018, 2, 6, 6, 30, 0), Color.DeepSkyBlue));
			list.Add(new CalendarDate("Mountain Biking", new DateTime(2019, 2, 5, 11, 0, 0), Color.Orange));*/


            // Create the ListView.
            ListView listView = new ListView
            {
                IsPullToRefreshEnabled = true,

                // Source of data items.
                ItemsSource = list,
				

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


            // Event for when delete switch is turned on
            deleteSwitch.OnChanged += deleteSwitch_Toggled;
			void deleteSwitch_Toggled(object sender, ToggledEventArgs e)
			{

				try
				{
                    // Removes all values from Calendar list
					list.RemoveAll(delegate (CalendarDate x) { return x.Name != null; });

				}
				catch (Exception) { Debug.WriteLine("error"); }
			}

            // Event for when today switch is turned on
			todaySwitch.OnChanged += todaySwitch_Toggled;
			void todaySwitch_Toggled(object sender, ToggledEventArgs e)
			{

                try
                {
                    // Removes all Calendar list items that arent today
                    listView.BeginRefresh();
                    list.RemoveAll(delegate (CalendarDate x) { return x.Date >= DateTime.Today.AddDays(1); });
                    list.RemoveAll(delegate (CalendarDate x) { return x.Date <= DateTime.Today; });
                    listView.EndRefresh();

                }
                catch (Exception) { Debug.WriteLine("error"); }
		    }

            // Event for when month switch is turned on
            monthSwitch.OnChanged += monthSwitch_Toggled;
			void monthSwitch_Toggled(object sender, ToggledEventArgs e)
			{

				try
				{
                    // Removes all calendar list items that arent within the current month
                    list.RemoveAll(delegate (CalendarDate x) { return x.Date >= DateTime.Now.AddMonths(1); });
                    list.RemoveAll(delegate (CalendarDate x) { return x.Date <= DateTime.Now; });
					listView.BeginRefresh();
					listView.EndRefresh();

				}
				catch (Exception) { Debug.WriteLine("error"); }
			}
            // Event for when yearswitch is turned on
			yearSwitch.OnChanged += yearSwitch_Toggled;
			void yearSwitch_Toggled(object sender, ToggledEventArgs e)
			{

				try
				{
                    // Removes all calendar list items that are within the current year
					list.RemoveAll(delegate (CalendarDate x) { return x.Date <= new DateTime(2017, 1, 1, 0, 0, 0); });
					list.RemoveAll(delegate (CalendarDate x) { return x.Date >= new DateTime(2018, 1, 1, 0, 0, 0); });
					listView.BeginRefresh();
					listView.EndRefresh();

				}
				catch (Exception) { Debug.WriteLine("error"); }
			}

            // Accomodate iPhone status bar.
            #pragma warning disable CS0618 // Type or member is obsolete
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);


            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    button,
                    listView,
                    tableView
                }
            };

        }

        public class CalendarDate
        {
            public CalendarDate(string name, DateTime date, Color colour)
            {
                    this.Name = name;
                    this.Date = date;
                    this.Colour = colour;

            }

            // Declare public variables 
            public string Name { private set; get; }

            public DateTime Date { private set; get; }

            public Color Colour { private set; get; }

           
        };


    }
	
}

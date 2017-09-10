using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalendarApp
{

    public partial class CalendarAppPage : ContentPage
    {
        
        public bool today;
        public CalendarAppPage(string name, DateTime fullData, Color realColor)
        {
            InitializeComponent();



            SwitchCell todaySwitch;
            SwitchCell weekSwitch;
            SwitchCell monthSwitch;

            List<CalendarDate> calendarDate;

            //Switch buttons to filter calendar dates between different periods
            TableView tableView = new TableView
            {
                HeightRequest = 130,
                RowHeight = 30,
                VerticalOptions = LayoutOptions.Start,
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection ("Filters")
                    {
                        (todaySwitch = new SwitchCell
                        {
                            Text = "Today:",
                            On = false
                        }),
         
                        (weekSwitch = new SwitchCell
                        {
                            Text = "Week:",
                            On = false
                        }),
              
                        (monthSwitch = new SwitchCell{
                            Text = "Month:",
                            On = false
                        })
                    
					}
				}
			};


            //Button that links to AddDatesPage so users can create a calendar item
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

			
            //When button is clicked
			button.Clicked += OnButtonClicked;
			void OnButtonClicked(object sender, EventArgs e)
			{
                //Send users to AddDatesPage
                Navigation.PushAsync(new AddDatesPage());

			}
            //Heading for the page
            /* Label header = new Label
             {
                 Text = "Calendar",
                 FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                 HorizontalOptions = LayoutOptions.Center
             };*/


            // Define some data.
            // Saves calendar dates into the list

            calendarDate = new List<CalendarDate>
            {
                
                new CalendarDate(name, fullData, realColor),
                new CalendarDate("Study", new DateTime(2017, 9, 9, 23, 30, 0), Color.Aqua),
                new CalendarDate("Movies", new DateTime(2017, 2, 1, 11, 30, 0), Color.Aqua),
                new CalendarDate("Holiday", new DateTime(2017, 8, 1, 13, 45, 00), Color.Green),
                new CalendarDate("Sell House", new DateTime(2017, 8, 20, 14, 00 ,00), Color.Purple),
                new CalendarDate("Marathon", new DateTime(2017, 9, 5, 20, 30, 0), Color.Yellow),
                new CalendarDate("Bills", new DateTime(2017, 10, 5, 20, 30, 0), Color.Blue),
                new CalendarDate("Christmas", new DateTime(2017, 12, 25, 7, 0, 0), Color.Red),
                new CalendarDate("Family Gathering", new DateTime(2018, 2, 12, 12, 30, 0), Color.GreenYellow),
                new CalendarDate("Car Checkup", new DateTime(2018, 4, 5, 14, 45, 0), Color.Pink),
                new CalendarDate("Snow", new DateTime(2018, 2, 6, 6, 30, 0), Color.DeepSkyBlue),
                new CalendarDate("Mountain Biking", new DateTime(2019, 2, 5, 11, 0, 0), Color.Orange),
            };
			
				
			
			//calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date <= new DateTime(2017, 1, 1, 0, 0, 0); });
			//calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date >= new DateTime(2018, 1, 1, 0, 0, 0); });
			
            //calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date >= DateTime.Now.AddMonths(1); });
            //calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date <= DateTime.Now; });

            // Create the ListView.
            ListView listView = new ListView
            {
                IsPullToRefreshEnabled = true,

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


			todaySwitch.OnChanged += todaySwitch_Toggled;
			void todaySwitch_Toggled(object sender, ToggledEventArgs e)
			{

                /*try
                {
                    
                    calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date >= DateTime.Today.AddDays(1); });
                    calendarDate.RemoveAll(delegate (CalendarDate x) { return x.Date <= DateTime.Today; });
                    listView.BeginRefresh();
                    listView.EndRefresh();

                }
                catch (Exception) { Debug.WriteLine("error"); }*/
			}


			// Accomodate iPhone status bar.
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

using System;
using AddressBookUI;
using UIKit;

namespace CalendarApp.iOS
{
	public static class ContactPicker
	{
		/* This idea to open contact list is from a user Xamarians at http://blog.xamarians.com/Blog/2017/5/27/xamarin-contacts-picker */
		static Action<AddressBook.ABPerson> _callback;
		static ABPeoplePickerNavigationController picker;
		static void Init()
		{
			if (picker != null)
				return;
			picker = new ABPeoplePickerNavigationController();
			picker.Cancelled += (s, e) =>
			{
				picker.DismissModalViewController(true);
				_callback(null);
			};
			picker.SelectPerson2 += (s, e) =>
			{
				picker.DismissModalViewController(true);
				_callback(e.Person);
			};
		}
		public static void Select(UIViewController parent, Action<AddressBook.ABPerson> callback)
		{
			_callback = callback;
			Init();
			parent.PresentModalViewController(picker, true);
		}
		/* end Xamarians' idea*/
	}
}

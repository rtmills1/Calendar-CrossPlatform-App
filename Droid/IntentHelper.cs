﻿using System;
using Android.App;
using Android.Content;
using Android.Provider;
using static CalendarApp.AddDatesPage;

namespace CalendarApp.Droid
{
	class IntentHelper
	{
		public static bool IsMobileIntent(int code)
		{
			return code == (int)RequestCodes.ContactPicker;
		}
		static Action<string> _callback;
		struct RequestCodes
		{
			public const int ContactPicker = 101;
		}
		static Activity CurrentActivity
		{
			get
			{
				return (Xamarin.Forms.Forms.Context as MainActivity);
			}
		}
		public static void ActivityResult(int requestCode, Intent data)
		{
			if (_callback == null)
				return;
			if (requestCode == RequestCodes.ContactPicker)
			{
				_callback(GetContactFromUri(data.Data));
			}
		}
		static string GetContactFromUri(Android.Net.Uri contactUri)
		{
			try
			{
				string[] projection = { ContactsContract.CommonDataKinds.Phone.Number };
				var cursor = Xamarin.Forms.Forms.Context.ContentResolver.Query(contactUri, projection, null, null, null);
				if (cursor.MoveToFirst())
				{
					return cursor.GetString(cursor.GetColumnIndex(projection[0]));
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		public static void OpenContactPicker(Action<string> callback)
		{
			_callback = callback;
			Intent intent = new Intent(Intent.ActionPick);
			intent.SetType(ContactsContract.CommonDataKinds.Phone.ContentType);
			CurrentActivity.StartActivityForResult(intent, RequestCodes.ContactPicker);
		}
	}
}

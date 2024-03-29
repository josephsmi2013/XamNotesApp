﻿using System;
namespace XamNotesApp.AppConstants
{
	public class Constants
	{
		public static string AppName = "OAuthNativeFlow";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "844060696235-gtoiepn6u6trvaoh5s6uo1a1a3hrcrnq.apps.googleusercontent.com";
		public static string AndroidClientId = "844060696235-ltgivjolb8v7ioidint435qa2o8ls38d.apps.googleusercontent.com";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "com.googleusercontent.apps.844060696235-gtoiepn6u6trvaoh5s6uo1a1a3hrcrnq:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.844060696235-ltgivjolb8v7ioidint435qa2o8ls38d:/oauth2redirect";
	}
}

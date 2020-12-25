using System;
using Xamarin.Auth;
using Xamarin.Forms;
using XamNotesApp.AuthHelpers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace XamNotesApp.Views
{
	public partial class LoginPageView : ContentPage
	{
		Account account;
		AccountStore store;
		public LoginPageView()
		{
			InitializeComponent();
			store = AccountStore.Create();

		}

		void Button_Clicked(System.Object sender, System.EventArgs e)
		{

			string clientId = null;
			string redirectUri = null;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					clientId = AppConstants.Constants.iOSClientId;
					redirectUri = AppConstants.Constants.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = AppConstants.Constants.AndroidClientId;
					redirectUri = AppConstants.Constants.AndroidRedirectUrl;
					break;
			}

			account = store.FindAccountsForService(AppConstants.Constants.AppName).FirstOrDefault();

			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				AppConstants.Constants.Scope,
				new Uri(AppConstants.Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(AppConstants.Constants.AccessTokenUrl),
				null,
				true);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);

		}
		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			User user = null;
			if (e.IsAuthenticated)
			{
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(AppConstants.Constants.UserInfoUrl), null, e.Account);
				var response = await request.GetResponseAsync();
				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<User>(userJson);
				}

				if (user != null)
				{
					App.Current.MainPage = new NavigationPage(new MainPageView());

				}

				//await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
				//await DisplayAlert("Email address", user.Email, "OK");
			}
		}

		void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}
	}
}

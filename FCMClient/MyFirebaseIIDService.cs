using System;
using Android.App;
using Firebase.Iid;
using Android.Util;

namespace FCMClient
{
  /* This service implements an OnTokenRefresh method that is invoked when the registration token is 
	 initially created or changed. When OnTokenRefresh runs, it retrieves the latest token from the 
	 FirebaseInstanceId.Instance.Token property (which is updated asynchronously by FCM). */
	
	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class MyFirebaseIIDService : FirebaseInstanceIdService
	{
		const string TAG = "MyFirebaseIIDService";

     /* OnTokenRefresh is invoked infrequently: it is used to update the token under the following circumstances:
        1) When the app is installed or uninstalled.
        2) When the user deletes app data.
        3) When the app erases the Instance ID.
        4) When the security of the token has been compromised. */
		
		public override void OnTokenRefresh()
		{
         // In this example, the refreshed token is logged so that it can be viewed in the output window:

			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);

			SendRegistrationToServer(refreshedToken);
		}

     /* OnTokenRefresh also calls SendRegistrationToAppServer to associate the user's registrations token with 
        the server-side account (if any) that is maintained by the application: */
		
		void SendRegistrationToServer(string token)
		{
			// Add custom implementation, as needed.
		}

       /* Because this implementation depends on the design of the app server, an empty method body is provided in this example. 
          If your app server requires FCM registration information, modify SendRegistrationToAppServer to associate the user's 
          FCM instance ID token with any server-side account maintained by your app. (Note that the token is opaque to the client app.)

         When a token is sent to the app server, SendRegistrationToAppServer should maintain a boolean to indicate whether the token 
         has been sent to the server. If this boolean is false, SendRegistrationToAppServer sends the token to the app server – otherwise, 
         the token was already sent to the app server in a previous call. In some cases (such as this FCMClient example), 
         the app server does not need the token; therefore, this method is not required for this example. */
	}
}

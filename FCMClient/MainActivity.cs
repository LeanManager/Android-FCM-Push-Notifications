using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

namespace FCMClient
{
	[Activity(Label = "FCMClient", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
        TextView msgText;
        const string TAG = "MainActivity";

       /* IsPlayServicesAvailable is called at the end of OnCreate so that the Google Play Services 
          check runs each time the app starts. If your app has an OnResume method, 
          it should call IsPlayServicesAvailable from OnResume as well. */
		
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

	        SetContentView(Resource.Layout.Main);

            msgText = FindViewById<TextView>(Resource.Id.msgText);

            /* When the user taps a notification issued from FCMClient, any data accompanying that 
			   notification message is made available in Intent extras. */
			
			if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

	        IsPlayServicesAvailable();

            // This code logs the current token to the output window when the Log Token button is tapped.
            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);

            logTokenButton.Click += delegate 
			{ 
				Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token); 
			};

            /* This code locates the Subscribe to Notification button in the layout and assigns its click 
			   handler to code that calls FirebaseMessaging.Instance.SubscribeToTopic, passing in the subscribed 
			   topic, news. When the user taps the Subscribe button, the app subscribes to the news topic. */
            var subscribeButton = FindViewById<Button>(Resource.Id.subscribeButton);

            subscribeButton.Click += delegate 
			{
                FirebaseMessaging.Instance.SubscribeToTopic("news");
                Log.Debug(TAG, "Subscribed to remote notifications");
            };
		}

       /* This code checks the device to see if the Google Play Services APK is installed. 
		  If it is not installed, a message is displayed in the TextBox that instructs the 
		  user to download an APK from the Google Play Store (or to enable it in the device's system settings). */
		
        public bool IsPlayServicesAvailable()
        {
	        int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

	        if (resultCode != ConnectionResult.Success)
	        {
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
				{
					msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
				}
		        else
		        {
			        msgText.Text = "This device is not supported";
			        Finish();
		        }

		        return false;
	        }
	        else
	        {
		        msgText.Text = "Google Play Services is available.";
		        return true;
            }
        }
	}
}


using Android.App;
using Android.Widget;
using Android.OS;
using Com.Wang.Avi;
using Android.Views;
using Java.Interop;

namespace Xamarin.Android.AVLoadingIndicatorViewBinding.Sample
{
    [Activity(Label = "Xamarin.Android.AVLoadingIndicatorViewBinding.Sample.IndicatorActivity", Icon = "@mipmap/icon")]
    public class IndicatorActivity : Activity
    {
        public static string ExtraIndicator = "indicator";

        private AVLoadingIndicatorView avi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_indicator);

            avi = FindViewById<AVLoadingIndicatorView>(Resource.Id.avi);
            avi.SetIndicator(Intent.GetStringExtra(ExtraIndicator));
        }

        [Export("HideClick")]
        public void HideClick(View view)
        {
            avi.Hide();
            // or avi.SmoothToHide();
        }

        [Export("ShowClick")]
        public void ShowClick(View view)
        {
            avi.Show();
            // or avi.SmoothToShow();
        }
    }
}


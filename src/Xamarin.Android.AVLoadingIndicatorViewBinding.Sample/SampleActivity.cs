using Android.App;
using Android.OS;
using Com.Wang.Avi;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Content;

namespace Xamarin.Android.AVLoadingIndicatorViewBinding.Sample
{
    [Activity(Label = "Xamarin.Android.AVLoadingIndicatorViewBinding.Sample.SampleActivity", MainLauncher = true, Icon = "@mipmap/icon")]
    public class SampleActivity : Activity
    {
        private readonly string[] _indicators = {
            "BallPulseIndicator",
            "BallGridPulseIndicator",
            "BallClipRotateIndicator",
            "BallClipRotatePulseIndicator",
            "SquareSpinIndicator",
            "BallClipRotateMultipleIndicator",
            "BallPulseRiseIndicator",
            "BallRotateIndicator",
            "CubeTransitionIndicator",
            "BallZigZagIndicator",
            "BallZigZagDeflectIndicator",
            "BallTrianglePathIndicator",
            "BallScaleIndicator",
            "LineScaleIndicator",
            "LineScalePartyIndicator",
            "BallScaleMultipleIndicator",
            "BallPulseSyncIndicator",
            "BallBeatIndicator",
            "LineScalePulseOutIndicator",
            "LineScalePulseOutRapidIndicator",
            "BallScaleRippleIndicator",
            "BallScaleRippleMultipleIndicator",
            "BallSpinFadeLoaderIndicator",
            "LineSpinFadeLoaderIndicator",
            "TriangleSkewSpinIndicator",
            "PacmanIndicator",
            "BallGridBeatIndicator",
            "SemiCircleSpinIndicator",
            "com.wang.avi.sample.MyCustomIndicator"
        };

        private RecyclerView _recycler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_sample);

            _recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            _recycler.SetLayoutManager(new GridLayoutManager(this, 4));
            _recycler.SetAdapter(new IndicatorAdapter(_indicators));
        }

        public class IndicatorAdapter : RecyclerView.Adapter
        {
            public string[] _indicators;

            public IndicatorAdapter(string[] indicators)
            {
                _indicators = indicators;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater
                    .From(parent.Context)
                    .Inflate(Resource.Layout.item_indicator, parent, false);

                return new IndicatorHolder(itemView);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var indicator = _indicators[position];
                var vh = holder as IndicatorHolder;
                vh.IndicatorView.SetIndicator(indicator);
                vh.ItemView.SetOnClickListener(new ClickListenerImplementation(indicator));
            }

            public override int ItemCount => _indicators.Length;

            private class ClickListenerImplementation : Java.Lang.Object, View.IOnClickListener
            {
                private readonly string _indicator;

                public ClickListenerImplementation(string indicator)
                {
                    _indicator = indicator;
                }

                public void OnClick(View v)
                {
                    var intent = new Intent(v.Context, typeof(IndicatorActivity));
                    intent.PutExtra(IndicatorActivity.ExtraIndicator, _indicator);
                    v.Context.StartActivity(intent);
                }
            }
        }

        public class IndicatorHolder : RecyclerView.ViewHolder
        {
            public AVLoadingIndicatorView IndicatorView { get; private set; }

            public IndicatorHolder(View itemView) : base(itemView)
            {
                IndicatorView = itemView.FindViewById<AVLoadingIndicatorView>(Resource.Id.indicator);
            }
        }
    }
}


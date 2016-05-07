using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Services;

namespace Xamarin.Native.Droid.Activities
{
    [Activity(Label = "PerformanceActivity")]
    public class PerformanceTestActivity : Activity
    {
        Stopwatch stopwatch;
        Button startButton;
        EditText digitsEntry;
        TextView ResultView;
        TextView TimeLabel;
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PerformanceTest);
            Title = GetString(Resource.String.menuPerformance);

            PerformanceTestService.Instance.PiCalculationCompleted += Instance_CalculationCompleted;

            digitsEntry = FindViewById<EditText>(Resource.Id.digitsEntry);
            ResultView = FindViewById<TextView>(Resource.Id.resultView);
            TimeLabel = FindViewById<TextView>(Resource.Id.timeLabel);
            startButton = FindViewById<Button>(Resource.Id.startButton);

            startButton.Click += StartCalculation;
        }

        private void StartCalculation(object sender, EventArgs e)
        {
            stopwatch = new Stopwatch();
            progressDialog = ProgressDialog.Show(this, "Przetwarzanie...", "");

            var digits = Convert.ToInt32(digitsEntry.Text);
            Task.Run(() =>
            {
                stopwatch.Start();
                PerformanceTestService.Instance.CalculatePi(digits);
            });
        }

        private void Instance_CalculationCompleted(string result)
        {
            stopwatch.Stop();
            RunOnUiThread(() =>
            {
                ResultView.Text = result;
                TimeLabel.Text = stopwatch.GetDurationInSeconds();
                progressDialog.Dismiss();
            });
        }
    }
}
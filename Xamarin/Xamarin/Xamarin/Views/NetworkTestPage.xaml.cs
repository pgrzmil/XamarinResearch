﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xamarin.Views
{
    public partial class NetworkTestPage : ContentPage
    {
        Stopwatch stopwatch;

        public NetworkTestPage()
        {
            InitializeComponent();
            Title = "Test obsługi sieci".ToUpper();
            AdressField.Text = "http://cdn.superbwallpapers.com/wallpapers/meme/doge-pattern-27481-2880x1800.jpg";
            NetworkDownloadService.Instance.ImageDownloadCompleted += Instance_DownloadCompleted;
        }

        private void StartDownloading(object sender, EventArgs e)
        {
            stopwatch = new Stopwatch();
            RefreshUI(true);

            stopwatch.Start();
            NetworkDownloadService.Instance.DownloadImage(AdressField.Text);
        }

        private void Instance_DownloadCompleted(byte[] bytes)
        {
            stopwatch.Stop();
            Device.BeginInvokeOnMainThread(() =>
            {
                DownloadedImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                RefreshUI(false);
                TimeLabel.Text = String.Format("Czas wykonania: {0}:{1}:{2}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
            });
        }

        private void RefreshUI(bool isDownloading)
        {
            StartButton.IsVisible = !isDownloading;
            AdressField.IsEnabled = !isDownloading;
            ActivityIndicator.IsVisible = isDownloading;
            ActivityIndicator.IsRunning = isDownloading;
        }
    }
}
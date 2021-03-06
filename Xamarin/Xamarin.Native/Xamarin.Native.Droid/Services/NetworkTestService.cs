﻿using Android.Graphics;
using Android.Util;
using Java.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Xamarin.Services
{
    public delegate void ImageDownloadEventHandler(Bitmap image);

    internal class NetworkDownloadService : Java.Lang.Object
    {
        public event ImageDownloadEventHandler ImageDownloadCompleted;

        public void DownloadImage(string urlString)
        {
            try
            {
                var url = new URL(urlString);
                var stream = url.OpenConnection().InputStream;
                var image = BitmapFactory.DecodeStream(stream);
                ImageDownloadCompleted?.Invoke(image);
            }
            catch (Exception e)
            {
                Log.Debug("Exception", "Image failed to download: " + e.ToString());
            }
        }
    }
}
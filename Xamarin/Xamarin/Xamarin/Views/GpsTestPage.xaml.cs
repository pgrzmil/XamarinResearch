﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xamarin.Views
{
    public partial class GpsTestPage : ContentPage
    {
        public GpsTestPage()
        {
            InitializeComponent();
            Title = "Test pozycji GPS".ToUpper();
        }
    }
}
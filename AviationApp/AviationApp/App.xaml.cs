﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AviationApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new WeightAndBalance.WeightAndBalancePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
﻿// © Copyright(C) Microsoft. All rights reserved.

using System;
using System.Globalization;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AthensDefaultApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            UpdateBoardInfo();
            UpdateNetworkInfo();
            UpdateDateTime();

            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Start();
        }

        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                UpdateNetworkInfo();
            });
        }

        private async void timer_Tick(object sender, object e)
        {
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                UpdateDateTime();
            });
        }

        private void UpdateBoardInfo()
        {
            DeviceInfoPresenter presenter = new DeviceInfoPresenter();
            BoardName.Text = presenter.GetBoardName();
            BoardImage.Source = new BitmapImage(presenter.GetBoardImageUri());
        }

        private void UpdateDateTime()
        {
            var t = DateTime.Now;
            this.CurrentTime.Text = t.ToString("t", CultureInfo.CurrentCulture);
        }

        private void UpdateNetworkInfo()
        {
            this.DeviceName.Text = DeviceInfoPresenter.GetDeviceName();
            this.IPAddress1.Text = DeviceInfoPresenter.GetCurrentIpv4Address();
            this.NetworkName1.Text = DeviceInfoPresenter.GetCurrentNetworkName();
        }

        private DispatcherTimer timer;
    }
}

﻿#pragma checksum "C:\WindowsPhoneProjects\DRPApp2\DRPApp2\DRPPanoramaPage1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B133115F7990AB7DB656F331C882B1D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace DRPApp2 {
    
    
    public partial class DRPPanoramaPage1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image image;
        
        internal Microsoft.Phone.Controls.PanoramaItem Item1;
        
        internal System.Windows.Controls.Grid WelcomeGrid;
        
        internal System.Windows.Controls.StackPanel topPanel;
        
        internal System.Windows.Controls.TextBlock nameTxt1b;
        
        internal System.Windows.Controls.TextBlock coordTxt1;
        
        internal System.Windows.Controls.TextBlock coordTxt2;
        
        internal System.Windows.Controls.TextBlock coordTx32;
        
        internal System.Windows.Controls.Image dashboard;
        
        internal Microsoft.Phone.Controls.LongListSelector RegionLLS;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ListBox tweetList;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/DRPApp2;component/DRPPanoramaPage1.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.image = ((System.Windows.Controls.Image)(this.FindName("image")));
            this.Item1 = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("Item1")));
            this.WelcomeGrid = ((System.Windows.Controls.Grid)(this.FindName("WelcomeGrid")));
            this.topPanel = ((System.Windows.Controls.StackPanel)(this.FindName("topPanel")));
            this.nameTxt1b = ((System.Windows.Controls.TextBlock)(this.FindName("nameTxt1b")));
            this.coordTxt1 = ((System.Windows.Controls.TextBlock)(this.FindName("coordTxt1")));
            this.coordTxt2 = ((System.Windows.Controls.TextBlock)(this.FindName("coordTxt2")));
            this.coordTx32 = ((System.Windows.Controls.TextBlock)(this.FindName("coordTx32")));
            this.dashboard = ((System.Windows.Controls.Image)(this.FindName("dashboard")));
            this.RegionLLS = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("RegionLLS")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tweetList = ((System.Windows.Controls.ListBox)(this.FindName("tweetList")));
        }
    }
}


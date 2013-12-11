using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace DRPApp2
{
    public partial class DRPPivotPage : PhoneApplicationPage
    {
        private RegionComposite regionComposite;

        public DRPPivotPage()
        {
            InitializeComponent();
            regionComposite = new RegionComposite();
           
        }

        /* contact tap */

        public void Picker_tap1(object o, EventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/DRPMap.xaml", UriKind.Relative));
               
            }
            catch (Exception ee) { }
        }

        /** App bar menu actions **/
        public void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton ic = (ApplicationBarIconButton)sender;

            if (ic.Text.Trim() == "contacts")
            {
                NavigationService.Navigate(new Uri("/ContactPage.xaml", UriKind.Relative));
            }
            if (ic.Text.Trim() == "maps")
            {
                NavigationService.Navigate(new Uri("/DRPMap.xaml", UriKind.Relative));
            }
            if (ic.Text.Trim() == "about")
            {
                NavigationService.Navigate(new Uri("/aboutPage.xaml", UriKind.Relative));
            }
            /*     if (ic.Text.Trim() == "appt")
                 {
                     ContactAppt(sender, e);
                 }*/
        }
        /* Tap event for list items on any pivot page */
        private void LLS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RegionComposite.CategoryItem item = ((FrameworkElement)e.OriginalSource).DataContext as RegionComposite.CategoryItem;
            RegionComposite.selectedItem = item; // save the category item selected here
            
            if (item != null)
            {
                //set the geocords for the selected item
                String geocords = item.Geo;
                String rid = item.Regionid; //get the geo from the app class
                var mm = geocords.Split(',');

                var obj = App.Current as App;
                obj.RegLat = mm[0];
                obj.RegLong = mm[1];
                NavigationService.Navigate(new Uri("/DRPMap.xaml?method=ShowRegionLocationOnTheMap", UriKind.Relative));
            }
        }

        /** Logo tap actions **/
        public void GoHome(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/DRPPanoramaPage1.xaml", UriKind.Relative));

        }

       
    }
}
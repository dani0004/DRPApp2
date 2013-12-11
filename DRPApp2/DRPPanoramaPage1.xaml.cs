using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Microsoft.Devices;


using DRPApp2.Model;
using Windows.ApplicationModel.DataTransfer;


using TweetSharp.Model;
using TweetSharp.Serialization;
using Microsoft.Phone.Net.NetworkInformation;


namespace DRPApp2
{
    public partial class DRPPanoramaPage1 : PhoneApplicationPage
    {
        //Popup customMessageBox = new Popup();
        Canvas canvas = null;
        private RegionData regionData;

      
        private TweetViewModel _model;
        private DataTransferManager _manager;

        //end added for messaging section

        public DRPPanoramaPage1()
        {
            InitializeComponent();

          
            //get the json for item 2
            regionData = new RegionData();

            //get teh twitter json for item3
            LoadNewsLine();

          
        }


        public void Contact_tap(object sender, EventArgs e)
        {
            try
            {

                NavigationService.Navigate(new Uri("/ContactPage.xaml", UriKind.Relative));

            }
            catch (Exception ee) { System.Diagnostics.Debug.WriteLine(ee.Message); }
        }
       

        /* contact tap */

        public void Picker_tap1(object o, EventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/DRPPivotPage.xaml", UriKind.Relative));
               
            }
            catch (Exception ee) { }
        }
    /*    public void SearchTextBox_GotFocus(object o, EventArgs e)
        {
            try
            {

            }
            catch (Exception ee) { }
        }
        public void SearchTextBox_KeyDown(object o, EventArgs e)
        {
            try
            {

            }
            catch (Exception ee) { }
        }*/

        /* contact tap */

        public void SendSMS_tap(object o, EventArgs e)
        {
            try
            {
                SmsComposeTask stask = new SmsComposeTask();
                stask.To = "613 2305468";
                stask.Body = "todays meetg will be at Arnprior";
                stask.Show();

            }
            catch (Exception ee) { }
        }
        /*
         * LoadNewsLine
         * 
         */
        public void LoadNewsLine()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                //Obtain keys by registering your app on https://dev.twitter.com/apps 
                var service = new TweetSharp.TwitterService("mC10oTbqBv00OXucdKeNbw", "Gbdpb2zUTKQyuR2zx5xrUAPaKvXnWrgiroaJX5a1Is");
                service.AuthenticateWith("807390408-LAmqs9FJ1pbfmEspWL3QGBiEuQXMFpzYWWa4kmSE", "fNQphiPs9zhAkLJu2HiGALrBiHmmbQypdLRM4oHZ4PdtN");

                //ScreenName is the profile name of the twitter user. 
                service.ListTweetsOnUserTimeline(new TweetSharp.ListTweetsOnUserTimelineOptions() { ScreenName = "Wendy-Anne Daniel@dani00043" }, (ts, rep) =>
                {
                    if (rep.StatusCode == HttpStatusCode.OK)
                    {
                        //bind 
                        this.Dispatcher.BeginInvoke(() => { tweetList.ItemsSource = ts; });
                    }
                });
            }
            else
            {

                MessageBox.Show("Please check your internet connestion.");
            }
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
            if (ic.Text.Trim() == "twitter")
            {
                NavigationService.Navigate(new Uri("/SendMessage.xaml", UriKind.Relative));
            }
        }

        /* show custom message bx from the toolkit */

        private void ShowCustomMessage(string message)
        {
            CustomMessageBox box = new CustomMessageBox()
            {
                Caption = "Informational",
                Message = message,
                LeftButtonContent = "ok",
                RightButtonContent = "cancel",
                //Content = textBox    
            };

            box.Dismissed += (s, boxEventArgs) =>
            {
            };

            box.Show();
        }
        /* Tap event for list items in item 2 */
        private void LLS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            ContactData.CategoryItem item = ((FrameworkElement)e.OriginalSource).DataContext as ContactData.CategoryItem;
            ContactData.selectedItem = item; // save the category item selected here
            //  if (item != null) // if fast-clicking, it is possible to get here with nothing selected.  Ignore
            //need to go to specific region
            NavigationService.Navigate(new Uri("/DRPPivotPage.xaml", UriKind.Relative));
        }
       

  

    }//end class
}//end namespace


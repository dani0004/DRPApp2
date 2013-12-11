using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace DRPApp2
{
     

    public partial class ContactPage : PhoneApplicationPage
    {
        private ContactData contactData;
        private string phnum;
        private string em;
        public ContactPage()
        {
            InitializeComponent();
            try
            {
                contactData = new ContactData();

                

              //  ContactLLS.DataContext = ContactData.contactJsonData;
            }
            catch (Exception exe) { System.Diagnostics.Debug.WriteLine(exe.Message); }
        }
        /************* Tap Handlers************************/
        /* contact tap */
        /* the lat and long of the current user are passed to the App object 
         * The url navigated to has a method call
         * */
        public void CardGeo_tap(object sender, EventArgs e)
        {
            try
            {
                //get the geo
                Button bb = (Button)sender;
               
               string aa=bb.Tag.ToString();
               var mm=aa.Split(',');
               
              
               var obj = App.Current as App;
               obj.Lat = mm[0];
               obj.Plong = mm[1];


               NavigationService.Navigate(new Uri("/DRPMap.xaml?method=ShowPersonLocationOnTheMap", UriKind.Relative));
           
               
               

            }
            catch (Exception ee) { }
        }
       
        //tap handler for phone
        public void ContactPhone(Object sender, EventArgs e)
        {
            try
            {
                //get the number from the sender
               
                    //ShowCustomMessage("Call to make a reservation?");
                Button bb = (Button)sender;
                 phnum=contactData.GetCurrentPerson(bb.Tag.ToString()).Phone;
                 em = contactData.GetCurrentPerson(bb.Tag.ToString()).Email;
                
                PhoneCallTask phoneTask = new PhoneCallTask();
                phoneTask.DisplayName = "display name";
                phoneTask.PhoneNumber = phnum;
                phoneTask.Show();
            }
            catch (UnauthorizedAccessException uae)
            {
                System.Diagnostics.Debug.WriteLine(uae.Message);
            }
        }
        //tap handler for email
        public void ContactEmail(Object o, EventArgs e)
        {

            //ShowCustomMessage("Reserve by email?");
            EmailComposeTask emailComposer = new EmailComposeTask();
            string emhref = "mailto:" + em;
            emailComposer.To = "<a href='"+emhref+"' target='_blank' rel='nofollow' >"+em+"</a>";
            emailComposer.Subject = "request for supplies";
            emailComposer.Body = "Need sandbags at site A";
            emailComposer.Show();
        }

        /* Tap event for list items */
        private void LLS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ContactData.CategoryItem item = ((FrameworkElement)e.OriginalSource).DataContext as ContactData.CategoryItem;
            ContactData.selectedItem = item; // save the category item selected here
          //  if (item != null) // if fast-clicking, it is possible to get here with nothing selected.  Ignore
          //      NavigationService.Navigate(new Uri("/menuDetail.xaml", UriKind.Relative));
            //need to go to phone or text page
        }

        /** App bar menu actions **/
        public void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton ic = (ApplicationBarIconButton)sender;

           
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

        /** Logo tap actions **/
        public void GoHome(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/DRPPanoramaPage1.xaml", UriKind.Relative));

        }

    
    }
}
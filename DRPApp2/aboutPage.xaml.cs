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
    public partial class aboutPage : PhoneApplicationPage
    {
        public aboutPage()
        {
            InitializeComponent();
        }

        public void ContactUs(Object o, RoutedEventArgs e)
        {
            EmailComposeTask emailComposer = new EmailComposeTask();
            emailComposer.To = "<a href='mailto:dani0004@algonquinlive.com' target='_blank' rel='nofollow' >dani0004@algonquinlive.com</a>";
            emailComposer.Subject = "problem with web page";
            emailComposer.Body = "We cannot see the xxx";
            emailComposer.Show();
        }
        public void CallUs(Object o, RoutedEventArgs e)
        {
            try
            {
                PhoneCallTask phoneTask = new PhoneCallTask();
                phoneTask.DisplayName = "display name";
                phoneTask.PhoneNumber = "555-5555";
                phoneTask.Show();
            }
            catch (UnauthorizedAccessException uae)
            {
                System.Diagnostics.Debug.WriteLine(uae.Message);
            }
        }
    }
}
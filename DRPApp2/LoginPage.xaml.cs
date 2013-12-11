

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
//using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.IsolatedStorage;
//using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using DRPApp2.Resources;
using System.Windows.Data;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
//susing System.Windows.Media.Imaging;
using System.ComponentModel;
//using Newtonsoft.Json.Linq.JArray;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;

/* login uses a local json file to store data in lieu of a sqlite db
 * In the real case  login would do a local certificate login */
namespace DRPApp2
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public static LoginResponse loginData = null;
        const string MyDirectory = "Assets";
        readonly string _offlineDataFile = Path.Combine(MyDirectory, "drplogin.json");

        private bool fromFile = false;
         private JObject loginTree;
         private string fname = "drplogin.json";
         string data;
        

        public LoginPage()
        {
            initializeTiles();
            var obj = App.Current as App;
            //user is already logged in - does not work because the app class 
            //is not persistent
            //login will always be the gate as the external tile points to it
           // if (obj.isLoggedIn)
             //   NavigationService.Navigate(new Uri("/DRPPanoramaPage1.xaml", UriKind.Relative));
            InitializeComponent();
            ReadFromFile();
            
            
           

        }
        /* contact tap */

        public void Login_tap(object o, EventArgs e)
        {
            Boolean loggedin = false;
            Boolean useridFound = false;
            Boolean passFound = false;
            string msg1=" "; 
            string msg2="Password not found";
            //authenticate user to keyring on local storage

            Boolean validated = ValidateLogin();
            if (validated)
            {
                JsonTextReader reader = new JsonTextReader(new StringReader(data));
                while (reader.Read())
                {
                    if (reader.Value != null){
                        // if((reader.Value).Equals("Userid", StringComparison.Ordinal))
                        if (String.Equals(reader.Value.ToString(),"Userid"))
                        {
                            reader.Read();
                           
                        }
                        if (String.Equals(reader.Value.ToString(),useridVal.Text.Trim()))
                       // if (reader.Value.ToString() == useridVal)
                            useridFound = true;
                        else
                            msg1 = "userid not found";
                   
                    if(String.Equals((reader.Value.ToString()),"Password")){
                              reader.Read();
                            
                   }
                    if (String.Equals((reader.Value.ToString()),passVal.Password.Trim()))
                    {
                        passFound = true;
                        msg2 = " ";
                        break;
                    }
                  }//end if not null

                }//end while
                if (passFound && useridFound)
                {
                    var obj = App.Current as App;
                    obj.isLoggedIn = true;
                    NavigationService.Navigate(new Uri("/DRPPanoramaPage1.xaml", UriKind.Relative));
                   
                }
                else
                {
                    ShowCustomMessage("Login failed, please try again " + msg1 + msg2);
                    var obj = App.Current as App;
                    obj.isLoggedIn = false;
                }
            }//end validated
        }//end function

        private Boolean ValidateLogin()
        {
           
            string message = " ";
            Boolean validated = true;
            try
            {
                
                if (useridVal.Text.Length == 0)
                {
                     message="Userid canot be blank";
                    ShowCustomMessage(message);
                    validated = false;
                   // return validated;
                }
                if (passVal.Password.Length == 0)
                {
                     message = "Password must be entered";
                    ShowCustomMessage(message);
                    validated = false;
                    //return validated;
                }
                
               
               
            }
            catch(Exception e){}
            return validated;
        }
        /* contact tap */

        public void Cancel_tap(object o, EventArgs e)
        {
            try
            {
                //close the app

                //NavigationService.Navigate(new Uri("/DRPPanoramaPage2.xaml", UriKind.Relative));
            }
            catch (Exception ee) { }
        }

        /* the function BaseGotFocus
        *reinitializes the state of the view
       * @param - Object sender an event object
        * @param - RoutedEventArgs the event that was generated
       * **/
        public void BaseGotFocus(object sender, RoutedEventArgs e)
        {
            //prepopulated for testing purposes
            try
            {
                useridVal.Text = "aboone";
                passVal.Password = "123456";

                useridVal.Focus();
            }
            catch (Exception ee)
            {
            }
        }

       
        /* the function ReadFromfile
        * Opens the file and creates a json object
        * */
        private void ReadFromFile2()
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();



            using (var fileStream = new IsolatedStorageFileStream(_offlineDataFile, FileMode.Open, store))
            {
                using (var stream = new StreamReader(fileStream))
                {
                   
                     data = stream.ReadToEnd();
                   // loginData = JsonConvert.DeserializeObject<LoginResponse>(data);
                     loginTree = JsonConvert.DeserializeObject<JObject>(data);
                    
                    }
                
            }

        }
        /* read file from storage folder for app */
        async public void ReadFromFile()
        {



            try
            {
                string path = fname;
                //System.Diagnostics.Debug.WriteLine(path);
                StorageFolder sfolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                // System.Diagnostics.Debug.WriteLine("got storage folder");
                StorageFolder subfolder = await sfolder.GetFolderAsync("Assets");
                // System.Diagnostics.Debug.WriteLine("got sub folder");
                var _file = await subfolder.GetFileAsync(path);
                //System.Diagnostics.Debug.WriteLine("gotfile");


                //System.Diagnostics.Debug.WriteLine("got stream");
                using (StreamReader r = new StreamReader(await _file.OpenStreamForReadAsync()))
                {
                     data = r.ReadToEnd();
                    if (!string.IsNullOrEmpty(data))
                    {
                        //loginTree = JsonConvert.DeserializeObject<JObject>(data);
                        loginData = JsonConvert.DeserializeObject<LoginResponse>(data);
                       
                    }
                }
            }
            catch (FileNotFoundException ee) { System.Diagnostics.Debug.WriteLine("file not found"); }
            catch (Newtonsoft.Json.JsonReaderException e) { System.Diagnostics.Debug.WriteLine("parsing error" + e.LineNumber + " " + e.Message); }
        }//end loadMenuData fn

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

        [DataContract]
        public class LoginResponse
        {

            public ObservableCollection<CategoryItem> Items { get; set; }


        }
        public class CategoryItem
        {

            public String Userid { get; set; }


            public String Password { get; set; }


            public String Certificate { get; set; }


        }
        /** App bar menu actions **/
        public void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton ic = (ApplicationBarIconButton)sender;

            if (ic.Text.Trim() == "login")
            {
                Login_tap(sender, e);
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

        /******LIVE TILES SECTION ********/
        public void initializeTiles()
        {
            // find the tile object for the application tile that using "flip" contains string in it.  
            ShellTile oTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("flip".ToString()));
            if (oTile != null && oTile.NavigationUri.ToString().Contains("flip"))
            {
                FlipTileData oFliptile = new FlipTileData();
                oFliptile.Title = "Region - Centre 144 E";
                oFliptile.Count = 11;
                oFliptile.BackTitle = "Trending News Centre 144 E";
                oFliptile.BackContent = "Evacuation to schools complete";
                oFliptile.WideBackContent = "back of the wide tile";
                oFliptile.SmallBackgroundImage = new Uri("Assets/Tiles/Flip/cycle_5.png", UriKind.Relative);
                oFliptile.BackgroundImage = new Uri("Assets/Tiles/Flip/cycle_1.png", UriKind.Relative);
                oFliptile.WideBackgroundImage = new Uri("Assets/Tiles/Flip/cycle_7691b7336.png", UriKind.Relative);
                oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/Flip/cycle_2.png", UriKind.Relative);

                // oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/Flip/flip691by336.png", UriKind.Relative);
                oFliptile.WideBackBackgroundImage = new Uri("/Assets/Tiles/Flip/flip691by336.png", UriKind.Relative);
                // oTile.Update(oFliptile); MessageBox.Show("Flip Tile Data successfully update.");

               
            }
            else
            {                // once it is created flip tile  
                Uri tileUri = new Uri("/LoginPage.xaml?tile=flip", UriKind.Relative);
                ShellTileData tileData = this.CreateFlipTileData();
                ShellTile.Create(tileUri, tileData, true);
            }//end else

        }//end shell tile

        private ShellTileData CreateFlipTileData()
        {

            return new FlipTileData()
            {
                Title = "Centre 144 SW",
                BackTitle = "Telecomm trending",
                BackContent = "Base stations are inaccessible in south",
                WideBackContent = "Hydro stations close to remote BS in southern Ontario are inaccessible ",
                Count = 8,
                SmallBackgroundImage = new Uri("/Assets/Tiles/Flip/cycle_4.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/Flip/cycle_6.png", UriKind.Relative),
                WideBackgroundImage = new Uri("/Assets/Tiles/Flip/flip691by691.png", UriKind.Relative),
            };
        }//end createflipTileData
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Media;
using System.Globalization;
//using System.Convert;
using System.Windows;


namespace DRPApp2
{
    class RegionComposite
    {
        public static RegionCompResponse regionCompJsonData = null;
        public static CategoryItem selectedItem;
       // public static ColourConverter colourConverter;

       

        private string jsonLocation = "http://dani0004.site40.net/drpregcomposite5.json";

        const string MyDirectory = "offline";
        readonly string _offlineDataFile = Path.Combine(MyDirectory, "drpregcomposite5.json");

        private bool fromFile = false;
        private CurrentCompRegion currentCompRegion;

         public RegionComposite()
         {
             if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
             {
                 // network is NOT ok - reads the last good file saved in the app storage
                 ReadFromFile();
                 MapRegions();
             }
             else
             {
                 RetrieveRemoteData();
             }
         }//end construtor

         [DataContract]
         public class RegionCompResponse
         {
             [System.Runtime.Serialization.DataMember(Name = "Org")]
             public String Org { get; set; }

             [System.Runtime.Serialization.DataMember(Name = "Website")]
             public String Website { get; set; }

             [System.Runtime.Serialization.DataMember(Name = "Regions")]
             public ObservableCollection<CategoryItem> Regions { get; set; }


         }

       

         public class CategoryItem
         {
            private string status;
          /*  public  object CBackground
            {

                get
                {
                    int value;
                    bool aa = int.TryParse(status, out value);
                    //GREEN
                    if (value == 1) return new SolidColorBrush(Color.FromArgb(255, 7, 166, 45));
                    //RED
                    if (value == 2) return new SolidColorBrush(Color.FromArgb(255, 230, 5, 10));
                    //YELLOW
                    if (value == 3) return new SolidColorBrush(Color.FromArgb(255, 189, 211, 3));

                    // Color default
                    return new SolidColorBrush(Colors.Gray);
                }
                set
                {
                   
                }
            }*/
         
             public String Regionid { get; set; }
             public String Geo { get; set; }
             public String Geolat { get; set; }
            public String Geolong { get; set; }
             public String CDescr1 { get; set; }
             public String CStatus1 { get; set; }
             public String CDescr2 { get; set; }
             public String CStatus2 { get; set; }
             public String CDescr3 { get; set; }
             public String CStatus3 { get; set; }
             public String CDescr4 { get; set; }
             public String CStatus4 { get; set; }
             public String CDescr5 { get; set; }
             public String CStatus5 { get; set; }
             public String CDescr6 { get; set; }
             public String CStatus6 { get; set; }

             public String TDescr1 { get; set; }
             public String TStatus1 { get; set; }
             public String TDescr2 { get; set; }
             public String TStatus2 { get; set; }
             public String TDescr3 { get; set; }
             public String TStatus3 { get; set; }
             public String TDescr4 { get; set; }
             public String TStatus4 { get; set; }
             public String TDescr5 { get; set; }
             public String TStatus5 { get; set; }
             public String TDescr6 { get; set; }
             public String TStatus6 { get; set; }

             public String HDescr1 { get; set; }
             public String HStatus1 { get; set; }
             public String HDescr2 { get; set; }
             public String HStatus2 { get; set; }
             public String HDescr3 { get; set; }
             public String HStatus3 { get; set; }
             public String HDescr4 { get; set; }
             public String HStatus4 { get; set; }
             public String HDescr5 { get; set; }
             public String HStatus5 { get; set; }
             public String HDescr6 { get; set; }
             public String HStatus6 { get; set; }
             


             
             public String Thumbnail { get; set; }


         }

         public class CurrentCompRegion
         {
             public String Regionid { get; set; }
             public String Geo { get; set; }
          //   public String Geolat { get; set; }
          //   public String Geolong { get; set; }
             public String CDescr1 { get; set; }
             public String CStatus1 { get; set; }
             public String CDescr2 { get; set; }
             public String CStatus2 { get; set; }
             public String CDescr3 { get; set; }
             public String CStatus3 { get; set; }
             public String CDescr4 { get; set; }
             public String CStatus4 { get; set; }
             public String CDescr5 { get; set; }
             public String CStatus5 { get; set; }
             public String CDescr6 { get; set; }
             public String CStatus6 { get; set; }

             public String TDescr1 { get; set; }
             public String TStatus1 { get; set; }
             public String TDescr2 { get; set; }
             public String TStatus2 { get; set; }
             public String TDescr3 { get; set; }
             public String TStatus3 { get; set; }
             public String TDescr4 { get; set; }
             public String TStatus4 { get; set; }
             public String TDescr5 { get; set; }
             public String TStatus5 { get; set; }
             public String TDescr6 { get; set; }
             public String TStatus6 { get; set; }

             public String HDescr1 { get; set; }
             public String HStatus1 { get; set; }
             public String HDescr2 { get; set; }
             public String HStatus2 { get; set; }
             public String HDescr3 { get; set; }
             public String HStatus3 { get; set; }
             public String HDescr4 { get; set; }
             public String HStatus4 { get; set; }
             public String HDescr5 { get; set; }
             public String HStatus5 { get; set; }
             public String HDescr6 { get; set; }
             public String HStatus6 { get; set; }
             

         }


        


         //web client can do sync or async download
         //retrieve json serialized string
         public void RetrieveRemoteData()
         {
             //check if the network is available
             if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
             {
                 // network is ok use locally stored data
                 WebClient webClient = new WebClient();
                 webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                 webClient.DownloadStringAsync(new Uri(jsonLocation));
             }
             else
                 ReadFromFile();




         }
         /* event listener  for json download*/

         private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
         {
             string json = e.Result;
             try
             {
                 if (!string.IsNullOrEmpty(json))
                 {
                     regionCompJsonData = JsonConvert.DeserializeObject<RegionCompResponse>(json);

                     WriteToFile(json);
                 }
                 else
                 {
                     ReadFromFile();
                 }

                 MapRegions();

             }
             catch (FileNotFoundException eee) { System.Diagnostics.Debug.WriteLine("file not found"); }
             catch (Newtonsoft.Json.JsonReaderException ee) { System.Diagnostics.Debug.WriteLine("parsing error" + ee.LineNumber + " " + ee.Message); }
             catch (Exception exe) { System.Diagnostics.Debug.WriteLine(exe.Message); }
         }//end loadMenuData fn

         private void MapRegions()
         {
             //get the root frame for the application
             var frame = App.Current.RootVisual as PhoneApplicationFrame;
             //frame is the container for all the app pages
             //the page is already in the frame
             var regionPage = frame.Content as PhoneApplicationPage;

             LongListSelector ll = (LongListSelector)(regionPage.FindName("RegionAllLLS"));
             LongListSelector ll2 = (LongListSelector)(regionPage.FindName("RegionAllLLS2"));
             LongListSelector ll3 = (LongListSelector)(regionPage.FindName("RegionAllLLS3"));

             ll.ItemsSource = regionCompJsonData.Regions;
             ll2.ItemsSource = regionCompJsonData.Regions;
             ll3.ItemsSource = regionCompJsonData.Regions;
         }

         /* the function Writetofile
           * Writes the serialized json string to a local file
           * This file is used to extract data if the service is not available
           * @param String- the json serialized string that was downloaded
           * */
         private void WriteToFile(String json)
         {
             var store = IsolatedStorageFile.GetUserStoreForApplication();
             if (!store.DirectoryExists(MyDirectory))
             {
                 store.CreateDirectory(MyDirectory);
             }


             using (var fileStream = new IsolatedStorageFileStream(_offlineDataFile, FileMode.Create, store))
             {
                 using (var stream = new StreamWriter(fileStream))
                 {
                     stream.Write(json);
                 }
             }

         }

         /* the function ReadFromfile
         * Opens the file and creates a json object
         * */
         private void ReadFromFile()
         {
             var store = IsolatedStorageFile.GetUserStoreForApplication();



             using (var fileStream = new IsolatedStorageFileStream(_offlineDataFile, FileMode.Open, store))
             {
                 using (var stream = new StreamReader(fileStream))
                 {
                     var data = stream.ReadToEnd();
                     regionCompJsonData = JsonConvert.DeserializeObject<RegionCompResponse>(data);
                 }
             }

         }

         //functions to get data out of the json
         public CurrentCompRegion GetCurrentCompRegion(String chosenRegionid)
         {
             int count = regionCompJsonData.Regions.Count;
             //string phonenum=" ";
             currentCompRegion = new CurrentCompRegion();
             for (int i = 0; i < count; i++)
             {
                 var item = regionCompJsonData.Regions[i];
                 if (String.Equals(item.Regionid, chosenRegionid))
                 {
                     // currentRegion.Phone = item.Phone;
                     // currentRegion.Email = item.Email;

                     break;
                 }//end if

             }//end for

             return currentCompRegion;

         }//end fn

       

    }

   
}//end namespace

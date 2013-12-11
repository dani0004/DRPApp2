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


namespace DRPApp2
{
    class RegionData
    {
        public static RegionResponse regionJsonData = null;
        public static CategoryItem selectedItem;

        private string jsonLocation = "http://dani0004.site40.net/drpregions3.json";

        const string MyDirectory = "offline";
        readonly string _offlineDataFile = Path.Combine(MyDirectory, "drpregions3.json");

        private bool fromFile = false;
        private CurrentRegion currentRegion;

          public RegionData()
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
          public class RegionResponse
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

              public String Regionid { get; set; }
              public String Descr { get; set; }


              public String CommunityCentres { get; set; }


              public String Hospitals { get; set; }
              public String PolicePosts { get; set; }
              public String Volunteers { get; set; }
        //      public String Geolat { get; set; }
       //       public String Geolong { get; set; }
        //      public String Geo { get; set; }

              public String Thumbnail { get; set; }


          }

          public class CurrentRegion
          {
              public String Regionid { get; set; }
              public String Descr { get; set; }


              public String CommunityCentres { get; set; }


              public String Hospitals { get; set; }
              public String PolicePosts { get; set; }
              public String Volunteers { get; set; }
         //     public String Geolat { get; set; }
        //      public String Geolong { get; set; }
        //      public String Geo { get; set; }

              public String Thumbnail { get; set; }
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
                      regionJsonData = JsonConvert.DeserializeObject<RegionResponse>(json);

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

              LongListSelector ll = (LongListSelector)(regionPage.FindName("RegionLLS"));

              ll.ItemsSource = regionJsonData.Regions;
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
                      regionJsonData = JsonConvert.DeserializeObject<RegionResponse>(data);
                  }
              }

          }

          //functions to get data out of the json
          public CurrentRegion GetCurrentRegion(String chosenRegionid)
          {
              int count = regionJsonData.Regions.Count;
              //string phonenum=" ";
              currentRegion = new CurrentRegion();
              for (int i = 0; i < count; i++)
              {
                  var item = regionJsonData.Regions[i];
                  if (String.Equals(item.Regionid, chosenRegionid))
                  {
                     // currentRegion.Phone = item.Phone;
                     // currentRegion.Email = item.Email;

                      break;
                  }//end if

              }//end for

              return currentRegion;

          }//end fn

       

   

    }//end class
}//end pkg

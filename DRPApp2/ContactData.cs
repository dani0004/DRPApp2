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
    
    class ContactData
    {
         public static ContactResponse contactJsonData = null;
         public static CategoryItem selectedItem;

         private string jsonLocation = "http://dani0004.site40.net/drppeople2.json";

         const string MyDirectory = "offline";
         readonly string _offlineDataFile = Path.Combine(MyDirectory, "drppeople2.json");

         private bool fromFile = false;
         private CurrentPerson currentPerson;

        /*
         * lat:45.37288789104629;long:-75.78080228396074 - Ambleside
         * lat:45.38373995576555;long:-75.80482360913959 - Hotel d
         * lat:45.36908168349915;long:-75.77397543944025 - Moore
         **/

         public ContactData()
         {
             if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
             {
                 // network is NOT ok - reads the last good file saved in the app storage
                 ReadFromFile();
                 MapPeople();
             }
             else
             {
                 RetrieveRemoteData();
             }
         }//end construtor


     [DataContract]
        public class ContactResponse
        {
            [System.Runtime.Serialization.DataMember(Name = "Org")]
            public String Org { get; set; }

           [System.Runtime.Serialization.DataMember(Name = "Website")]
            public String Website { get; set; }

          [System.Runtime.Serialization.DataMember(Name = "People")]  
          public ObservableCollection<CategoryItem> People { get; set; }


        }

      public class CategoryItem
        {
           
            public String Name { get; set; }
            public String Userid { get; set; }

          
            public String Title { get; set; }

          
            public String Unit { get; set; }
            public String Phone { get; set; }
            public String Email { get; set; }
            public String Geolat { get; set; }
            public String Geolong { get; set; }
            public String Geo { get; set; }

            public String Thumbnail { get; set; }

     
        }

      public class CurrentPerson
      {
          public String Name { get; set; }
          public String Userid { get; set; }


          public String Title { get; set; }


          public String Unit { get; set; }
          public String Phone { get; set; }
          public String Email { get; set; }
          public String Geolat { get; set; }
          public String Geolong { get; set; }
          public String Geo { get; set; }

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
                 contactJsonData = JsonConvert.DeserializeObject<ContactResponse>(json);

                  WriteToFile(json);
              }
              else
              {
                  ReadFromFile();
              }

              MapPeople();

          }
          catch (FileNotFoundException eee) { System.Diagnostics.Debug.WriteLine("file not found"); }
          catch (Newtonsoft.Json.JsonReaderException ee) { System.Diagnostics.Debug.WriteLine("parsing error" + ee.LineNumber + " " + ee.Message); }
          catch (Exception exe) { System.Diagnostics.Debug.WriteLine(exe.Message); }
      }//end loadMenuData fn

      private void MapPeople()
      {
          //get the root frame for the application
          var frame = App.Current.RootVisual as PhoneApplicationFrame;
          //frame is the container for all the app pages
          //the page is already in the frame
          var peoplePage = frame.Content as PhoneApplicationPage;

          LongListSelector ll = (LongListSelector)(peoplePage.FindName("ContactLLS"));

          ll.ItemsSource = contactJsonData.People;
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
                 contactJsonData = JsonConvert.DeserializeObject<ContactResponse>(data);
              }
          }

      }

        //functions to get data out of the json
        public CurrentPerson GetCurrentPerson(String chosenUserid){
            int count=contactJsonData.People.Count;
            //string phonenum=" ";
            currentPerson = new CurrentPerson();
            for(int i=0;i<count;i++){
                var item=contactJsonData.People[i];
                if(String.Equals(item.Userid,chosenUserid))
                {
                    currentPerson.Phone = item.Phone;
                    currentPerson.Email = item.Email;
                    
                    break;
                }//end if

            }//end for

            return currentPerson;
            
        }//end fn

       

   

}//end class
}//end pkg

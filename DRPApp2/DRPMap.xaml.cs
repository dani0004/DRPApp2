using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using Microsoft.Phone.Maps.Services;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DRPApp2
{
    public partial class DRPMap : PhoneApplicationPage
    {
        GeocodeQuery Item1geocodequery = null;
        RouteQuery Item1Query = null;
        List<GeoCoordinate> Item1Coordinates;

        GeocodeQuery Item2geocodequery = null;
        RouteQuery Item2Query = null;
        List<GeoCoordinate> Item2Coordinates;

        Grid MyGrid;

        public DRPMap()
        {
            InitializeComponent();

            Item1Coordinates = new List<GeoCoordinate>();
            Item2Coordinates = new List<GeoCoordinate>();

            var obj = App.Current as App;
            string lat = obj.Lat;
            string plong = obj.Plong;

       
            createMap();
            

        }

        private void createMap()
        {
          
            AddRegionBoundariesToMap();
        }

        private void Reset()
        {
           // Center="45.37288789104629, -75.78080228396074"

            Double d1 = Convert.ToDouble("45.37288789104629");
            Double d2 = Convert.ToDouble("-75.78080228396074");
            GeoCoordinate gc = new GeoCoordinate(d1, d2);
            DRPMap1.Center = gc;
            DRPMap1.ZoomLevel = 13;
        }

  
        //this method called directly when item is tapped
        //on the Regions Composite list in the pivot page
        public void ShowRegionLocationOnTheMap(int regid)
        {
            try
            {
                var obj = App.Current as App;
                string lat; string plong;
                switch (regid)
                {
                    case 1:
                     lat = obj.RegLat1;
                     plong = obj.RegLong1;
                    break;
                    case 2:
                     lat = obj.RegLat2;
                     plong = obj.RegLong2;
                    break;
                    case 3:
                    lat = obj.RegLat3;
                    plong = obj.RegLong3;
                    break;
                    case 4:    
                     lat = obj.RegLat4;
                     plong = obj.RegLong4;
                    break;
                    case 5:
                     lat = obj.RegLat5;
                     plong = obj.RegLong5;
                    break;
                    case 6:
                     lat = obj.RegLat6;
                     plong = obj.RegLong6;
                    break;
                    default:
                     lat = obj.RegLat;
                     plong = obj.RegLong;
                     break;

                }
               

                Double d1 = Convert.ToDouble(lat);
                Double d2 = Convert.ToDouble(plong);
                GeoCoordinate gc = new GeoCoordinate(d1, d2);
                DRPMap1.Center = gc;
                DRPMap1.ZoomLevel = 14;
              

            }
            catch (InvalidOperationException ne) { System.Diagnostics.Debug.WriteLine(ne.Message); }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); }
        }
        /* overloaded function that will use the default condition 
         * This is when the region location is invoked from a list item in the pivot page
         * Region geo will be stored values in the local context as they will not change
         */
        public void ShowRegionLocationOnTheMap()
        {
            ShowRegionLocationOnTheMap(7);
        }

        //getting my coordinates
        private void ShowPersonLocationOnTheMap()
        {
            try
            {
                var obj = App.Current as App;
                string lat = obj.Lat;
                string plong = obj.Plong;

                Double d1 = Convert.ToDouble(lat);
                Double d2 = Convert.ToDouble(plong);
                AddImageToMap(d1, d2);
                GeoCoordinate gc = new GeoCoordinate(d1, d2);
                DRPMap1.Center = gc;
               
            }
            catch (InvalidOperationException ne) { System.Diagnostics.Debug.WriteLine(ne.Message); }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); }






        }
        //getting my coordinates
     /*   private async void ShowPersonLocationOnTheMap2()
        {
            try
            {
                // Get my current location.
                Geolocator pGeolocator = new Geolocator();
                Geoposition pGeoposition = await pGeolocator.GetGeopositionAsync();


                // System.Device.Location.GeoCoordinate pGeocoordinate = new System.Device.Location.GeoCoordinate(d1, d2); ;
                Geocoordinate pGeocoordinate = pGeoposition.Coordinate;
                GeoCoordinate pGeoCoordinate =
               CoordinateConverter.ConvertGeocoordinate(pGeocoordinate);

                // Make person current location the center of the Map.
                this.DRPMap1.Center = pGeoCoordinate;
                this.DRPMap1.ZoomLevel = 13;

                //mark position
                // Create a small circle to mark the current location.
                Ellipse myCircle = new Ellipse();
                myCircle.Fill = new SolidColorBrush(Colors.Blue);
                myCircle.Height = 20;
                myCircle.Width = 20;
                myCircle.Opacity = 50;

                // Create a MapOverlay to contain the circle.
                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = myCircle;
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = pGeoCoordinate;

                // Create a MapLayer to contain the MapOverlay.
                MapLayer myLocationLayer = new MapLayer();
                myLocationLayer.Add(myLocationOverlay);

                // Add the MapLayer to the Map.
                DRPMap1.Layers.Add(myLocationLayer);
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); }





        }*/


          protected override void OnNavigatedTo(NavigationEventArgs e)
          {   
           
              string msg; 
            /*  if (NavigationContext.QueryString.TryGetValue("lat", out msg))
                  lat = msg;
              if (NavigationContext.QueryString.TryGetValue("plong", out msg))
                  plong = msg;*/
              if (NavigationContext.QueryString.TryGetValue("method", out msg))
              {
                  var name = msg;
                  if (name.Equals("ShowPersonLocationOnTheMap"))
                  {
                      ShowPersonLocationOnTheMap();
    
                  }
                  if (name.Equals("ShowRegionLocationOnTheMap"))
                      ShowRegionLocationOnTheMap();
                  if (name.Equals("Reset"))
                      Reset();  
              }
            
          }

       

        private void CreateGridForRegions()
        {
            //Creating a Grid element.

            MyGrid = new Grid();
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.Background = new SolidColorBrush(Colors.Transparent);

            //Creating a Rectangle
            Rectangle MyRectangle = new Rectangle();

            MyRectangle.Fill = new SolidColorBrush(Colors.Black);
            MyRectangle.Height = 20;
            MyRectangle.Width = 20;
            MyRectangle.SetValue(Grid.RowProperty, 0);
            MyRectangle.SetValue(Grid.ColumnProperty, 0);

            //Adding the Rectangle to the Grid
            MyGrid.Children.Add(MyRectangle);


        }
        public void AddImageToMap(Double lat, Double plng)
        {
            MapOverlay imageLayer = new MapOverlay();
            // MapLayer imageLayer = new MapLayer();

            Image image = new Image();
            //Define the URI location of the image
           
            //image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/stickfig.png", UriKind.Relative));
             image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/ic_map9.png", UriKind.Relative));
            //Define the image display properties
            image.Opacity = 1.0;
            image.Width = 36;
            image.Height = 36;
            // image.Stretch = System.Windows.Media.Stretch.None;
            //The map location to place the image at
            imageLayer.GeoCoordinate = new GeoCoordinate(lat, plng);
            imageLayer.PositionOrigin = new Point(0, 0.5);

            // imageLayer.AddChild(image, location, position);
            MapLayer MyLayer = new MapLayer();
            imageLayer.Content = image;
            MyLayer.Add(imageLayer);

            DRPMap1.Layers.Add(MyLayer);
        }
      
        /* add the region defining boundaries **/
        public void AddRegionBoundariesToMap()
        {
            MapOverlay imageLayer1, imageLayer2, imageLayer3, imageLayer4, imageLayer5, imageLayer6;
            // MapLayer imageLayer = new MapLayer();
            List<Image> regBoundaries = new List<Image>();
            List<MapOverlay> regoverlays = new List<MapOverlay>();
            MapLayer RegLayer = new MapLayer();
          
            Image image1 = new Image();
            regBoundaries.Add(image1);
            Image image2 = new Image();
            regBoundaries.Add(image2);
            Image image3 = new Image();
            regBoundaries.Add(image3);
            Image image4 = new Image();
            regBoundaries.Add(image4);
            Image image5 = new Image();
            regBoundaries.Add(image5);
            Image image6 = new Image();
            regBoundaries.Add(image6);
            int j = 0;
            string uri = " ";
            foreach (Image key in regBoundaries)
            {
                if (j == 0)
                {
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbwest144.png", UriKind.Relative));
                   // key.Tap += new EventHandler<GestureEventArgs>(GestureListener_Tap); - ASK ANTONIO

                }
                if (j == 1)
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbwest144sub.png", UriKind.Relative));
                if (j == 2)
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbcentre144east.png", UriKind.Relative));
                if (j == 3)
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbcentre144hub.png", UriKind.Relative));
                if (j == 4)
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbcen144swest.png", UriKind.Relative));
                if (j == 5)
                    key.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/rbsouth144.png", UriKind.Relative));
                key.Opacity = 1.0;
                key.Stretch = System.Windows.Media.Stretch.None;
                j++;
               
               
            }

            imageLayer1 = new MapOverlay();
            regoverlays.Add(imageLayer1);
            imageLayer2 = new MapOverlay();
            regoverlays.Add(imageLayer2);
            imageLayer3 = new MapOverlay();
            regoverlays.Add(imageLayer3);
            imageLayer4 = new MapOverlay();
            regoverlays.Add(imageLayer4);
            imageLayer5 = new MapOverlay();
            regoverlays.Add(imageLayer5);
            imageLayer6 = new MapOverlay();
            regoverlays.Add(imageLayer6);
            int i = 0;
            //using hard-coded geo for now
            var obj = App.Current as App;
            string lat; string plong;
            lat = obj.RegLat1;
            plong = obj.RegLong1;
            Double d1 = Convert.ToDouble(lat);
            Double d2 = Convert.ToDouble(plong);
            GeoCoordinate gc = new GeoCoordinate(d1, d2);
            foreach (MapOverlay key in regoverlays)
            {
                //hawkesbury
                if (i == 0) { lat = obj.RegLat1; plong = obj.RegLong1; }
               
                //kemptville
                if (i == 1) { lat = obj.RegLat2; plong = obj.RegLong2; }
                    
                //cassleman
                if (i == 2) { lat = obj.RegLat3; plong = obj.RegLong3; }
                   
                 //ambleside
                if (i == 3) { lat = obj.RegLat4; plong = obj.RegLong4; }
                   
                //kanata
                if (i == 4) { lat = obj.RegLat5; plong = obj.RegLong5; }

                //carleton place
                if (i == 5) { lat = obj.RegLat6; plong = obj.RegLong6; }
                   
      
                Double d11 = Convert.ToDouble(lat);
                Double d21 = Convert.ToDouble(plong);
                key.GeoCoordinate =  new GeoCoordinate(d11, d21);

                key.PositionOrigin = new Point(0, 0.5);
                key.Content = regBoundaries[i];
                RegLayer.Add(key);

                i++;


            }

         

            DRPMap1.Layers.Add(RegLayer);
            DRPMap1.ZoomLevel = 13;
        }

        private void GestureListener_Tap(object sender, GestureEventArgs e)
        {
            MessageBox.Show("I Am Tapped");
        }

        public void getRegionMap(object sender, EventArgs e)
        {
           Button bb = (Button)sender;

  
            if (bb.Name=="but1" )
            {
                ShowRegionLocationOnTheMap(3);
            }
            if (bb.Name == "but2")
            {
                ShowRegionLocationOnTheMap(4);
            }
            if (bb.Name == "but3")
            {
                ShowRegionLocationOnTheMap(5);
            }
            if (bb.Name == "but4")
            {
                ShowRegionLocationOnTheMap(2);
            }
            if (bb.Name == "but5")
            {
                ShowRegionLocationOnTheMap(6);
            }
            if (bb.Name == "but6")
            {
                ShowRegionLocationOnTheMap(1);
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
                NavigationService.Navigate(new Uri("/DRPMap.xaml?method=reset", UriKind.Relative));
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


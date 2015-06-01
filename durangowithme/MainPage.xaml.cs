using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPreview.Kinect;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238



namespace durangowithme
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.GetDefault();
            irReader = sensor.InfraredFrameSource.OpenReader();
            FrameDescription fd = sensor.InfraredFrameSource.FrameDescription;
            irData = new ushort[ud.LengthInPixels];
            irDataConverted = new byte[fd.LengthInPixels * 4];
            irBitmap = new WriteableBitmap(fd.Width, fd.Height);
            image.Source = irBitmap;
            bodies = new Body[6];
        }
    }
}

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
using Windows.UI.Xaml.Shapes;
using Windows.UI;
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
            msfr = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Infrared);
            msfr.MultiSourceFrameArrived += msfr_MultiSourceFrameArrived;
            sensor.Open();
        }

        void msfr_MultiSourceFrameArrived(MultiSourceFrameReader sender, MultiSourceFrameArrivedEventArgs args)
        {
            using(MultiSourceFrame msf = args.FrameReference.AcquireFrame())
            {
                if(msf != null)
                {
                    using(BodyFrame bodyFrame = msf.BodyFrameReference.AcquireFrame())
                    {
                        using(InfraredFrame irFrame = msf.InfraredFrameReference.AcquireFrame())
                        {
                            if(bodyFrame != null && irFrame != null)
                            {
                                irFrame.CopyFrameDataToArray(irData);
                                for(int i = 0; i < irData.Length; i++)
                                {
                                    byte intensity = (byte)(irData[i] >> 8);
                                    irDataConverted[i * 4] = intensity;
                                    irDataConverted[i * 4 + 1] = intensity;
                                    irDataConverted[i * 4 + 2] = intensity;
                                    irDataConverted[i * 4 + 3] = 255;
                                }

                                irDataConverted.CopyTo(irBitmap.PixelBuffer);
                                irBitmap.Invalidate();
                            }

                        }
                    }
                }
            }
        }

        bodyFrame.GetAndRefreshBodyData(bodies);
        bodyCanvas.Children.Clear();

        foreach (Body body in bodies)
        {
            if(Body.IsTracked)
    {
         
    }
        }
    }
}

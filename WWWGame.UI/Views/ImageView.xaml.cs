﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WWWGame.UI.Helpers;
using WWWGame.UI.ViewModel;

namespace WWWGame.UI.Views
{
    public partial class ImageView : PhoneApplicationPage
    {
        private double m_Zoom = 1;
        private double m_Width = 0;
        private double m_Height = 0;
        public ImageView()
        {
            InitializeComponent();
        }

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
            image.Width = Application.Current.Host.Content.ActualWidth;//image.ActualWidth;
            image.Height = Application.Current.Host.Content.ActualHeight;//image.ActualHeight;
            m_Width = image.Width;
            m_Height = image.Height;

            //System.Windows.Size dimensions = ResolutionHelper.ScreenResolution;

            // Initaialy we put Stretch to None in XAML part, so we can read image ActualWidth i ActualHeight (otherwise values are strange)
            // After that we set Stretch to UniformToFill in order to be able to resize image
            image.Stretch = Stretch.Uniform;
            viewport.Bounds = new Rect(0, 0, Application.Current.Host.Content.ActualWidth, Application.Current.Host.Content.ActualHeight);
        }


        private void viewport_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            if (e.PinchManipulation != null)
            {

                double newWidth, newHieght;


                if (m_Width < m_Height)  // box new size between image size and viewport actual size
                {
                    newHieght = m_Height * m_Zoom * e.PinchManipulation.CumulativeScale;
                    newHieght = Math.Max(viewport.ActualHeight, newHieght);
                    newHieght = Math.Min(newHieght, m_Height);
                    newWidth = newHieght * m_Width / m_Height;
                }
                else
                {
                    newWidth = m_Width * m_Zoom * e.PinchManipulation.CumulativeScale;
                    newWidth = Math.Max(viewport.ActualWidth, newWidth);
                    newWidth = Math.Min(newWidth, m_Width);
                    newHieght = newWidth * m_Height / m_Width;
                }


                if (newWidth < m_Width && newHieght < m_Height)
                {
                    // Tells image positione in viewport (offset)
                    MatrixTransform transform = image.TransformToVisual(viewport) as MatrixTransform;
                    // Calculate center of pinch gesture on image (not screen)
                    Point pinchCenterOnImage = transform.Transform(e.PinchManipulation.Original.Center);
                    // Calculate relative point (0-1) of pinch center in image
                    Point relativeCenter = new Point(e.PinchManipulation.Original.Center.X / image.Width, e.PinchManipulation.Original.Center.Y / image.Height);
                    // Calculate and set new origin point of viewport
                    Point newOriginPoint = new Point(relativeCenter.X * newWidth - pinchCenterOnImage.X, relativeCenter.Y * newHieght - pinchCenterOnImage.Y);
                    viewport.SetViewportOrigin(newOriginPoint);
                }

                image.Width = newWidth;
                image.Height = newHieght;

                // Set new view port bound
                viewport.Bounds = new Rect(0, 0, newWidth, newHieght);
            }
        }

        private void viewport_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            m_Zoom = image.Width / m_Width;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameterValue = NavigationContext.QueryString["param1"];

            int imgId;
            if (!Int32.TryParse(parameterValue, out imgId))
            {
                throw new NotImplementedException();
            }

            (this.DataContext as ImageViewModel).Id = imgId;
        }
    }
}
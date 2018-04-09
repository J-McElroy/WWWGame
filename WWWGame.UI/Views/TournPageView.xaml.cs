using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WWWGame.UI.ViewModel;

namespace WWWGame.UI.Views
{
    public partial class TournPageView : PhoneApplicationPage
    {
        public TournPageView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SlideTransition transition = new SlideTransition();
            transition.Mode = SlideTransitionMode.SlideRightFadeIn;
            PhoneApplicationPage page = (PhoneApplicationPage)((PhoneApplicationFrame)Application.Current.RootVisual).Content;
            ITransition trans = transition.GetTransition(page);
            trans.Completed += delegate
            {
                trans.Stop();
            };
            trans.Begin();

            if (e.NavigationMode == NavigationMode.Back)
            {
                var id = (this.DataContext as TournViewModel).TournId;
                DataContext = null;  
                DataContext = new TournViewModel();
                (this.DataContext as TournViewModel).TournId = id;
                return;
            }
            string parameterValue = NavigationContext.QueryString["param1"];

            int tournId;
            if (!Int32.TryParse(parameterValue, out tournId))
            {
                throw new NotImplementedException();
            }

            (this.DataContext as TournViewModel).TournId = tournId;
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            if(slider == null)
                return;
            double roundedValue = Math.Round(slider.Value, 0);
            //Check if it is a rounded value
            if (slider.Value != roundedValue)
                slider.Value = roundedValue;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //this.DataContext = null;
            base.OnBackKeyPress(e);
        }
    }
}
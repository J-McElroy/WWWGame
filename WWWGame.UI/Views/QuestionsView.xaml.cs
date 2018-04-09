using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WWWGame.UI.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace WWWGame.UI.Views
{
    public partial class QuestionsView : PhoneApplicationPage
    {
        private bool _isNewPageInstance;

        public QuestionsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                DataContext = State["ViewModel"];
                return;
            }
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

            string parameterValue = NavigationContext.QueryString["param1"];

            int tournId;
            if (!Int32.TryParse(parameterValue, out tournId))
            {
                throw new NotImplementedException();
            }
            parameterValue = NavigationContext.QueryString["param2"];

            int questionNum;
            if (!Int32.TryParse(parameterValue, out questionNum))
            {
                throw new NotImplementedException();
            }
            (this.DataContext as QuestionsViewModel).TournId = tournId;
            (this.DataContext as QuestionsViewModel).SelectedQuestion = questionNum - 1;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (window.IsOpen)
            {
                return;
            }
            var id = (this.DataContext as QuestionsViewModel).TournId;
            this.DataContext = null;
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // If this is a back navigation, the page will be discarded, so there
            // is no need to save state.
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                // Save the ViewModel variable in the page's State dictionary.
                State["ViewModel"] = this.DataContext;
            }
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            var img = sender as Image;
            if(img == null)
                return;
            
            panZoom.Source = img.Source;
            window.IsOpen = true;
        }

        private void CopyButton_OnClick(object sender, EventArgs e)
        {
            var context = (this.DataContext as QuestionsViewModel);
            if(context == null)
                return;

            context.CopyText();

            SystemTray.ProgressIndicator = new ProgressIndicator();
            SystemTray.ProgressIndicator.Text = "Текст вопроса скопирован в буфер";
            SystemTray.ProgressIndicator.IsIndeterminate = false;
            SystemTray.ProgressIndicator.IsVisible = true;

            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            timer.Tick += (o, args) =>
            {
                SystemTray.ProgressIndicator.IsVisible = false;
                var t = (o as DispatcherTimer);
                if (t != null)
                {
                    t.Stop();
                }
            };
            timer.Start();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WWWGame.UI.Resources;
using WWWGame.UI.ViewModel;
using Microsoft.Phone.Tasks;

namespace WWWGame.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!App.ViewModel.IsDataLoaded)
            //{
            //    App.ViewModel.LoadData();
            //}
        }

        protected async override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = (((Pivot.SelectedItem as PivotItem).Content as UserControl).DataContext as IReturnable).GoBack();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<NavigateToPageMessage>
                (
                    this,
                    (action) => ReceiveMessage(action)
                );
        }

        private object ReceiveMessage(NavigateToPageMessage action)
        {
            var page = string.Format("/Views/{0}View.xaml", action.PageName);

            //TODO: ParamBuilder
            if (action.Param != null)
            {
                var parameters = action.Param as int[];
                if (parameters != null && parameters.Count()>0)
                {
                    page += string.Format("?param1={0}", parameters[0]);
                    for (int i = 1; i < parameters.Count(); i++)
                    {
                        page += string.Format("&param{1}={0}", parameters[i], i+1);
                    }
                }
            }

            if (action.PageName == "Main")
            {
                page = "/MainPage.xaml";
            }


            NavigationService.Navigate(
               new System.Uri(page,
                     System.UriKind.Relative));
            return null;
        }

        private void RateApplication_OnClick(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void AboutApp_OnClick(object sender, EventArgs e)
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage() { PageName = "About" });
        }
    }
}
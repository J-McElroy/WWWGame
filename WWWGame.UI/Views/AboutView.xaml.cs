using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace WWWGame.UI.Views
{
    public partial class AboutView : PhoneApplicationPage
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void GoToSiteClick(object sender, MouseButtonEventArgs e)
        {
            var webBrowserTask = new WebBrowserTask { Uri = new Uri("http://db.chgk.info/", UriKind.Absolute) };

            webBrowserTask.Show();
        }

        private void SendMailClick(object sender, MouseButtonEventArgs e)
        {
            var emailComposeTask = new EmailComposeTask
            {
                Subject = "Приложение \"Что? Где? Когда?\"",
                To = "wp.www.game@gmail.com"
            };

            emailComposeTask.Show();
        }
    }
}
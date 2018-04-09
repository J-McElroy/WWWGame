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

namespace WWWGame.UI
{
    public partial class RetryControl : UserControl
    {
        public static readonly DependencyProperty RetryCommandProperty =
            DependencyProperty.Register(
                "RetryCommand", typeof(ICommand),
                typeof(RetryControl), null
                );

        public ICommand RetryCommand
        {
            get { return (ICommand)GetValue(RetryCommandProperty); }
            set { SetValue(RetryCommandProperty, value); }
        }
        public RetryControl()
        {
            InitializeComponent();
        }
    }
}

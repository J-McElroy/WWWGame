using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WWWGame.UI.Model;
using WWWGame.UI.ViewModel;

namespace WWWGame.UI
{
    public partial class LeveledTournsPage : PhoneApplicationPage
    {
        public LeveledTournsPage()
        {
            InitializeComponent();
        }

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as LongListSelector;
            if (list.SelectedItem == null)
                return;

            var vm = list.DataContext as AllTournsViewModel;
            int id = (list.SelectedItem as TournRow).Id;
            vm.OpenCommand.Execute(id);

            // Reset selected item to null
            list.SelectedItem = null;
        }
    }
}
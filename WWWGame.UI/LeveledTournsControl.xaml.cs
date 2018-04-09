using System;
using System.Collections.Generic;
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
    public partial class LeveledTournsControl : UserControl
    {
        public static readonly DependencyProperty IsLocalDataProperty =
            DependencyProperty.Register(
                "IsLocalData", typeof (Boolean),
                typeof (LeveledTournsControl), null
                );
        public bool IsLocalData
        {
            get { return (bool)GetValue(IsLocalDataProperty); }
            set { SetValue(IsLocalDataProperty, value); }
        }


        public LeveledTournsControl()
        {
            InitializeComponent();
        }

        private void LongListSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as LongListSelector;
            if (list.SelectedItem == null)
                return;

            var vm = list.DataContext as IOpenedCommand;
            int id = (list.SelectedItem as TournRow).Id;
            vm.OpenCommand.Execute(id);

            // Reset selected item to null
            list.SelectedItem = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    public abstract class BaseTournViewModel : ViewModelBase
    {
        protected List<TournRow> _tourns;

        public List<TournRow> Tourns
        {
            get
            {
                return _tourns;
            }
            set
            {
                Set("Tourns", ref _tourns, value);
                RaisePropertyChanged("EmptyVisibility");
            }
        }
        public Visibility ListVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility RetryVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public Visibility EmptyVisibility
        {
            get { return (Tourns == null || Tourns.Count == 0) ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}

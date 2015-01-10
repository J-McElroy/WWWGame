using System.Collections.Generic;
using GalaSoft.MvvmLight;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private AllTournsViewModel _allTourns;
        private LoadedTournsViewModel _loadedTourns;
        private LastTournsViewModel _lastTourns;
        private bool _isBusy;
        private string _message;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            App.ViewModel = this;
            AllTourns = new AllTournsViewModel();
            LoadedTourns = new LoadedTournsViewModel();
            LastTourns = new LastTournsViewModel();
        }
        
        public AllTournsViewModel AllTourns
        {
            get
            {
                return _allTourns;
            }
            set
            {
                Set("AllTourns", ref _allTourns, value);
            }
        }

        public LoadedTournsViewModel LoadedTourns
        {
            get
            {
                return _loadedTourns;
            }
            set
            {
                Set("LoadedTourns", ref _loadedTourns, value);
            }
        }

        public LastTournsViewModel LastTourns
        {
            get
            {
                return _lastTourns;
            }
            set
            {
                Set("LastTourns", ref _lastTourns, value);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set("IsBusy", ref _isBusy, value); }
        }

        public string Message
        {
            get { return _message; }
            set { Set("Message", ref _message, value); }
        }
    }
}
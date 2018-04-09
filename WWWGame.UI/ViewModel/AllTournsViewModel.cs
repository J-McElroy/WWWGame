using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using WWWGame.DataAccessLibrary;
using WWWGame.LogicLayer;
using WWWGame.SourceParser;
using WWWGame.UI.Helpers;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    public class AllTournsViewModel : BaseTournViewModel, IOpenedCommand, IReturnable
    {
        //private List<TournRow> _tourns;
        private readonly Stack<int?> _path = new Stack<int?>();
        private int? _currentTourn;
        private readonly ITournsAgent _agent = new WebTournsAgent(new LocalDbTournsAgent(new TournsRepository()), new TournsRepository(),
            new XmlTournParser(), new XmlTournListParser(), new CachedRootParser(), new ContentLoader());

        private bool _isNetworkError;
        private Visibility _listVisibility;
        private Visibility _retryVisibility;
        private bool _isBusy;

        public AllTournsViewModel()
        {
            IsNetworkError = false;
            App.ViewModel.Message = "Загрузка";
            Load();
        }

        private void Load()
        {
            
            OpenCommand = new RelayCommand<int>(Open);
            RetryCommand = new RelayCommand(Reload);
            //RaisePropertyChanged("RetryCommand");
            Open(-1);
        }

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

        private bool IsNetworkError
        {
            get { return _isNetworkError; }
            set
            {
                _isNetworkError = value;
                RaisePropertyChanged("EmptyVisibility");
                if (value)
                {
                    Set("ListVisibility", ref _listVisibility, Visibility.Collapsed);
                    Set("RetryVisibility", ref _retryVisibility, Visibility.Visible);
                }
                else
                {
                    Set("ListVisibility", ref _listVisibility, Visibility.Visible);
                    Set("RetryVisibility", ref _retryVisibility, Visibility.Collapsed);
                }
            }
        }

        public new Visibility ListVisibility
        {
            get { return _listVisibility; }
        }

        public new Visibility RetryVisibility
        {
            get { return _retryVisibility; }
        }

        public new Visibility EmptyVisibility
        {
            get { return (Tourns == null || Tourns.Count == 0) && !IsNetworkError ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set("IsBusy", ref _isBusy, value); }
        }

        public ICommand OpenCommand { get; private set; }

        public ICommand RetryCommand { get; private set; }

        public async void Open(int id)
        {
            //App.SetProgressIndicator(true);
            SetBusy(true);
            int? param = id;
            if (id == -1)
                param = null;
            
            
            try
            {
                Tourns =
                    (await _agent.GetTournsAsync(param)).Select(Mapper.Map<LogicLayer.Model.Tourn, TournRow>).ToList();

                if (_currentTourn != param)
                {
                    _path.Push(_currentTourn);
                    _currentTourn = param;
                }

                IsNetworkError = false;
            }
            catch (HttpRequestException)
            {
                _currentTourn = param;
                IsNetworkError = true;
            }
            catch(WebException)
            {
                _currentTourn = param;
                IsNetworkError = true;
            }
            catch (NotTournListException)
            {
                Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage() { PageName = "TournPage", Param = new int[]{id} });
            }

            SetBusy(false);

            //App.SetProgressIndicator(false);
        }

        public bool GoBack()
        {
            if (_path.Count == 0)
                return false;

            _currentTourn = _path.Pop();

            Tourns = _agent.GetTourns(_currentTourn).Select(Mapper.Map<LogicLayer.Model.Tourn, TournRow>).ToList();
            return true;
        }

        private void Reload()
        {
            var tournId = _currentTourn.HasValue ? _currentTourn.Value : -1;
            Open(tournId);
        }

        private void SetBusy(bool isBusy)
        {
            //GlobalLoading.Instance.IsLoading = isBusy;
            IsBusy = isBusy;
        }
    }
}

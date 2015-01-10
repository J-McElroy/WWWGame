using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WWWGame.DataAccessLibrary;
using WWWGame.LogicLayer;
using WWWGame.SourceParser;
using WWWGame.UI.Helpers;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    public class TournViewModel : ViewModelBase
    {
        private int _tournId;
        private readonly ITournsAgent _agent = new WebTournsAgent(new LocalDbTournsAgent(new TournsRepository()), new TournsRepository(),
            new XmlTournParser(), new XmlTournListParser(), new HtmlRootParser(), new ContentLoader());

        private Tourn _tourn;
        private string _message;
        private bool _isBusy;

        public TournViewModel()
        {
            Message = "Загрузка";
            GoToQuestionsPageCommand =
                new RelayCommand(
                    delegate
                    {
                        SetBusy(true);
                        Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage()
                        {
                            PageName = "Questions",
                            Param = new int[] {TournId, Tourn.LastQuestion}
                        });
                        SetBusy(false);
                    });
            
        }

        public int TournId
        {
            get { return _tournId; }
            set
            {
                Set("TournId", ref _tournId, value);
                LoadTourn(TournId);
            }
        }

        public Tourn Tourn
        {
            get { return _tourn; }
            set
            {
                Set("Tourn", ref _tourn, value);
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

        public RelayCommand GoToQuestionsPageCommand { get; private set; }

        private async void LoadTourn(int tournId)
        {
            SetBusy(true);
            Tourn = Mapper.Map<LogicLayer.Model.Tourn,Tourn>(await _agent.GetTournAsync(tournId));
            //await Task.Delay(1000);
            SetBusy(false);
        }

        private void SetBusy(bool isBusy)
        {
            //GlobalLoading.Instance.IsLoading = isBusy;
            IsBusy = isBusy;
        }
    }
}

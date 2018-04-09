using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WWWGame.DataAccessLibrary;
using WWWGame.LogicLayer;
using WWWGame.SourceParser;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    public class LoadedTournsViewModel : BaseTournViewModel, IOpenedCommand, IReturnable
    {
        //private List<TournRow> _tourns;
        private readonly Stack<int?> _path = new Stack<int?>();
        private int? _currentTourn;
        private readonly ITournsAgent _agent = new LocalDbTournsAgent(new TournsRepository());

        public LoadedTournsViewModel()
        {
            Load();
        }

        private void Load()
        {
            OpenCommand = new RelayCommand<int>(Open);
            Open(-1);
        }

        public ICommand OpenCommand { get; private set; }

        public async void Open(int id)
        {
            int? param = id;
            if (id == -1)
                param = null;
            if (_tourns != null)
            {
                var tourn = _tourns.FirstOrDefault(el => el.Id == id);
                if (tourn != null && !tourn.HasChildTourns)
                {
                    Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage()
                    {
                        PageName = "TournPage",
                        Param = new int[] {id}
                    });
                    return;
                }
            }

            if (_currentTourn != param)
            {
                _path.Push(_currentTourn);
                _currentTourn = param;
            }

            Tourns = (await _agent.GetTournsAsync(param)).Select(Mapper.Map<LogicLayer.Model.Tourn, TournRow>).ToList();
        }

        public bool GoBack()
        {
            if (_path.Count == 0)
                return false;

            _currentTourn = _path.Pop();

            Tourns = _agent.GetTourns(_currentTourn).Select(Mapper.Map<LogicLayer.Model.Tourn, TournRow>).ToList();
            return true;
        }
    }
}

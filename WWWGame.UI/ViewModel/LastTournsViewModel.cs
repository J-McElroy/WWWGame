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
using WWWGame.UI.Model;
using Tourn = WWWGame.LogicLayer.Model.Tourn;

namespace WWWGame.UI.ViewModel
{
    public class LastTournsViewModel : BaseTournViewModel, IOpenedCommand
    {
        private readonly ITournsAgent _agent = new LocalDbTournsAgent(new TournsRepository());

        public LastTournsViewModel()
        {
            Load();
            OpenCommand = new RelayCommand<int>(Open);
        }

        private void Open(int tournId)
        {
            Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage()
            {
                PageName = "TournPage",
                Param = new int[] { tournId }
            });
        }

        private void Load()
        {
            Tourns = _agent.GetLastTourns(10).OrderByDescending(el => el.LastOpened).Select(Mapper.Map<Tourn,TournRow>).ToList();
        }

        public ICommand OpenCommand { get; private set; }
    }
}

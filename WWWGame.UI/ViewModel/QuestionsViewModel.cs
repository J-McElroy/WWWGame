using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using WWWGame.DataAccessLibrary;
using WWWGame.LogicLayer;
using WWWGame.UI.Helpers;
using WWWGame.UI.Model;

namespace WWWGame.UI.ViewModel
{
    public class QuestionsViewModel : ViewModelBase
    {
        private List<Question> _questions;
        private IQuestionsAgent _agent = new QuestionsAgent(new TournsRepository());
        private ITournsAgent _tournAgent = new LocalDbTournsAgent(new TournsRepository());
        private int _tournId;
        private int _selectedQuestion;
        private bool _isBusy;
        private string _message;
        private int? _selTmp;
        private Tourn _tourn;
        private string _tournTitle;

        public QuestionsViewModel()
        {
            Message = "Загрузка";
        }

        public int TournId
        {
            get { return _tournId; }
            set
            {
                Set("TournId", ref _tournId, value);
                LoadQuestions(TournId);
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

        public string TournTitle
        {
            get { return _tournTitle; }
            set
            {
                Set("TournTitle", ref _tournTitle, value);
            }
        }

        public List<Question> Questions
        {
            get { return _questions; }
            set
            {
                Set("Questions", ref _questions, value);
            }
        }

        public int SelectedQuestion
        {
            get
            {
                if (_selectedQuestion < 0)
                    return 0;
                return _selectedQuestion;
            }
            set
            {
                Set("SelectedQuestion", ref _selectedQuestion, value);
                if (!_selTmp.HasValue)
                {
                    _selTmp = value;
                }
                _agent.SetLastQuestion(TournId, value + 1);
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


        private async void LoadQuestions(int tournId)
        {
            
            SetBusy(true);
            //var selected = SelectedQuestion;
            Questions = (await _agent.GetQuestions(tournId)).Select(Mapper.Map<LogicLayer.Model.Question, Question>).ToList();
            if (_selTmp != null) SelectedQuestion = _selTmp.Value;
            SetBusy(false);
        }


        private async void LoadTourn(int tournId)
        {
            Tourn = Mapper.Map<LogicLayer.Model.Tourn, Tourn>(await _tournAgent.GetTournAsync(tournId));
            TournTitle = Tourn.Title;
        }

        private void SetBusy(bool isBusy)
        {
            //GlobalLoading.Instance.IsLoading = isBusy;
            IsBusy = isBusy;
        }

        public void CopyText()
        {
            try
            {
                Clipboard.SetText(Questions[SelectedQuestion].Text);
            }
            catch
            {}
        }
    }
}

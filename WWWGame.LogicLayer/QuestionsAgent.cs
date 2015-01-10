using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Repositories;

namespace WWWGame.LogicLayer
{
    public class QuestionsAgent : IQuestionsAgent
    {
        private readonly ITournsRepository _repository;

        public QuestionsAgent(ITournsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        public Task<IEnumerable<Question>> GetQuestions(int tournId)
        {
            return Task.Run(()=>_repository.GetQuestions(tournId));
        }

        public void SetLastQuestion(int tournId, int questionNumber)
        {
            _repository.SetLastQuestion(tournId, questionNumber);
        }
}
}
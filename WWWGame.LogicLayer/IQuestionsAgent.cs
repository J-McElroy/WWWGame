using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer
{
    public interface IQuestionsAgent
    {
        Task<IEnumerable<Question>> GetQuestions(int tournId);
        void SetLastQuestion(int tournId, int questionNumber);
    }
}

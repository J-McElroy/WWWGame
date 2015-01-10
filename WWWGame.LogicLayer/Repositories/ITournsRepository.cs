using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer.Repositories
{
    public interface ITournsRepository
    {
        IEnumerable<Tourn> GetTournsByParent(int? parentId);
        IEnumerable<Tourn> GetLastTourns(int count);
        IEnumerable<Tourn> GetAllTourns();
        void SaveTourns(IEnumerable<Tourn> tourns, int? parentId);
        void SaveQuestions(IEnumerable<Question> questions, int parentId);
        IEnumerable<Question> GetQuestions(int tournId);
        string GetUrl(int? parentId);
        Tourn GetTourn(int id);
        void SetLastQuestion(int tournId, int questionNum);
        byte[] GetImage(int imgId);
    }
}

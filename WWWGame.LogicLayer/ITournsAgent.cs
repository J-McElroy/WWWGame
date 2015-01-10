using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer
{
    public interface ITournsAgent
    {
        IEnumerable<Tourn> GetAllTourns();
        IEnumerable<Tourn> GetLastTourns(int count);
        IEnumerable<Tourn> GetTourns(int? parentId);
        Task<IEnumerable<Tourn>> GetTournsAsync(int? parentId);
        Tourn GetTourn(int tournId);
        Task<Tourn> GetTournAsync(int tournId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer.Parser
{
    public interface IRootParser
    {
        Task<IEnumerable<Tourn>> GetTourns(string url);
    }
}

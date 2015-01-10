using System.Collections.Generic;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer.Parser
{
    public interface ITournListParser
    {
        IEnumerable<Tourn> GetTourns(string xml);
    }
}

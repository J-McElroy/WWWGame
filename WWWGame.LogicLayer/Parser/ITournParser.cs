using System.Collections.Generic;
using WWWGame.LogicLayer.Model;

namespace WWWGame.LogicLayer.Parser
{
    public interface ITournParser
    {
        IEnumerable<Question> GetQuestions(string xml);
    }
}

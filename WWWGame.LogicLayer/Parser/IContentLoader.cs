using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.LogicLayer.Parser
{
    public interface IContentLoader
    {
        Task<string> GetXml(string url);
        Task<byte[]> GetFile(string url);
    }
}

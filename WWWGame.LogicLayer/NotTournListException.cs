using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.LogicLayer
{
    public class NotTournListException : Exception
    {
        public NotTournListException(string message) : base(message)
        { }
    }
}

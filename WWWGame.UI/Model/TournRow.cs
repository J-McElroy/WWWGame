using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.UI.Model
{
    public class TournRow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Loaded { get; set; }
        public DateTime Played { get; set; }
        public DateTime LastOpened { get; set; }
        public TournType Type { get; set; }
        public int QuestionsCount { get; set; }
        public bool HasChildTourns { get; set; }
    }
}

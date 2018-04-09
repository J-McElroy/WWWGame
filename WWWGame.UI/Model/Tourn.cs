using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.UI.Model
{
    public class Tourn
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Played { get; set; }
        public int QuestionsCount { get; set; }
        public int LastQuestion { get; set; }
        public string Url { get; set; }
        public List<Question> Questions { get; set; }
    }
}

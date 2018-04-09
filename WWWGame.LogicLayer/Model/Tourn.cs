using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.LogicLayer.Model
{
    public class Tourn
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string SourceId { get; set; }
        public string SourceParentId { get; set; }
        public int QuestionsCount { get; set; }
        public string Type { get; set; }
        public string TextId { get; set; }
        public DateTime? Played { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastOpened { get; set; }
        public int? LastQuestion { get; set; }
        public DateTime? Loaded { get; set; }
        public bool HasChildTourns { get; set; }
    }
}

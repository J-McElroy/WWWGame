using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.DataAccessLibrary.Model
{
    [Table(Name = "Tourns")]
    public class Tourn
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(CanBeNull = true)]
        public int? ParentId { get; set; }
        [Column]
        public string Title { get; set; }
        [Column]
        public string Url { get; set; }
        [Column]
        public string SourceId { get; set; }
        [Column]
        public string SourceParentId { get; set; }
        [Column]
        public int QuestionsCount { get; set; }
        [Column]
        public string Type { get; set; }
        [Column]
        public string TextId { get; set; }
        [Column(CanBeNull = true)]
        public DateTime? Played { get; set; }
        [Column(CanBeNull = true)]
        public DateTime? Created { get; set; }
        [Column(CanBeNull = true)]
        public DateTime? LastOpened { get; set; }
        [Column(CanBeNull = true)]
        public DateTime? Loaded { get; set; }
        [Column(CanBeNull = true)]
        public int? LastQuestion { get; set; }
        [Column]
        public bool HasChildTourns { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.DataAccessLibrary.Model
{
    [Table(Name = "Questions")]
    class Question
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int TournId { get; set; }
        [Column]
        public string SourceId { get; set; }
        [Column]
        public string SourceParentId { get; set; }
        [Column]
        public string Type { get; set; }
        [Column]
        public int Number { get; set; }
        [Column]
        public int TypeNum { get; set; }
        [Column]
        public string Text { get; set; }
        [Column]
        public string Answer { get; set; }
        [Column]
        public string Authors { get; set; }
        [Column]
        public string Sources { get; set; }
        [Column]
        public string Comments { get; set; }
        [Column]
        public string AdditionalText { get; set; }
    }
}

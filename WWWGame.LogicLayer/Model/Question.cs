using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWGame.LogicLayer.Model
{
    public class Question
    {
        public int Id { get; set; }
        public int TournId { get; set; }
        public string SourceId { get; set; }
        public string SourceParentId { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }
        public int TypeNum { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string Authors { get; set; }
        public string Sources { get; set; }
        public string Comments { get; set; }
        public string AdditionalText { get; set; }
        public List<Image> TextImages { get; set; }
        public List<Image> AnswerImages { get; set; }
        public List<Image> CommentImages { get; set; }
    }
}

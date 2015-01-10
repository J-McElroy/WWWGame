using System.Collections.Generic;

namespace WWWGame.UI.Model
{
    public class Question
    {
        private List<Image> _textImages;
        public int Number { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string Authors { get; set; }
        public string Sources { get; set; }
        public string Comments { get; set; }

        public List<Image> TextImages
        {
            get { return _textImages; }
            set { _textImages = value; }
        }

        public List<Image> AnswerImages { get; set; }
        public List<Image> CommentImages { get; set; }
        public string AdditionalText { get; set; }
        public string Title { get { return string.Format("Вопрос {0}", Number); } }
    }
}
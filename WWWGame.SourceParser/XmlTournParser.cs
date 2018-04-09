using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Parser;

namespace WWWGame.SourceParser
{
    public class XmlTournParser : ITournParser
    {
        public IEnumerable<Question> GetQuestions(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            var entries = from e in doc.Descendants("question")
                          select new Question()
                          {
                              SourceId = (string)e.Element("Id"),
                              Type = (string)e.Element("Type"),
                              Number = (int)e.Element("Number"),
                              TypeNum = (int)e.Element("TypeNum"),
                              Text = RemoveSlashN((string)e.Element("Question")),
                              SourceParentId = (string)e.Element("ParentId"),
                              Answer = RemoveSlashN((string)e.Element("Answer")),
                              Authors = RemoveSlashN((string)e.Element("Authors")),
                              Sources = RemoveSlashN((string)e.Element("Sources")),
                              Comments = RemoveSlashN((string)e.Element("Comments"))
                          };

            return entries;
        }

        private string RemoveSlashN(string str)
        {
            return str.Replace("\n", " ");
        }
    }
}
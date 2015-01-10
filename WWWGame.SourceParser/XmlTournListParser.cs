using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WWWGame.LogicLayer;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Parser;

namespace WWWGame.SourceParser
{
    public class XmlTournListParser : ITournListParser
    {
        public IEnumerable<Tourn> GetTourns(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            var tmp = from e in doc.Descendants("tour")
                select new
                {
                    SourceId = (string)e.Element("Id"),
                    Type = (string)e.Element("Type"),
                    Title = (string)e.Element("Title"),
                    TextId = (string)e.Element("TextId"),
                    Created = (string)e.Element("CreatedAt"),
                    SourceParentId = (string)e.Element("ParentId"),
                    Played = (string)e.Element("PlayedAt"),
                    QuestionsCount = (int)e.Element("QuestionsNum")
                };

            var entries = new List<Tourn>();

            if (!tmp.Any())
            {
                throw new NotTournListException("Empty tourn list");
            }

            foreach (var entry in tmp)
            {
                DateTime created;
                var isCreatedParse = DateTime.TryParse(entry.Created, out created);
                DateTime played;
                var isPlayedParse = DateTime.TryParse(entry.Played, out played);

                entries.Add(new Tourn()
                {
                    SourceId = entry.SourceId,
                    Type = entry.Type,
                    Title = entry.Title,
                    TextId = entry.TextId,
                    Created = isCreatedParse?created:(DateTime?)null,
                    SourceParentId = entry.SourceParentId,
                    Played = isPlayedParse ? played : (DateTime?)null,
                    QuestionsCount = entry.QuestionsCount
                });
            }

            return entries;
        }
    }
}
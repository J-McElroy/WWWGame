using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Parser;
using WWWGame.LogicLayer.Repositories;

namespace WWWGame.LogicLayer
{
    public class WebTournsAgent : ITournsAgent
    {
        const string BaseUrl = @"http://db.chgk.info";
        private const string BasePicUrl = @"http://db.chgk.info/images/db/";
        private readonly ITournsAgent _agent;
        private readonly ITournsRepository _tournsRepository;
        private readonly ITournParser _tournParser;
        private readonly ITournListParser _tournListParser;
        private readonly IRootParser _rootParser;
        private readonly IContentLoader _loader;

        public WebTournsAgent(ITournsAgent agent, ITournsRepository tournsRepository, ITournParser tournParser, ITournListParser tournListParser,
            IRootParser rootParser, IContentLoader loader)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }
            _agent = agent;
            _tournsRepository = tournsRepository;
            _tournParser = tournParser;
            _tournListParser = tournListParser;
            _rootParser = rootParser;
            _loader = loader;
        }

        public IEnumerable<Tourn> GetAllTourns()
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Tourn> GetLastTourns(int count)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Tourn> GetTourns(int? parentId)
        {
            return _agent.GetTourns(parentId);
        }

        public async Task<IEnumerable<Tourn>> GetTournsAsync(int? parentId)
        {
            List<Tourn> tourns;
            if (!parentId.HasValue)
            {
                tourns = (await _rootParser.GetTourns(BaseUrl + @"/tour")).ToList();
            }
            else
            {
                var url = _tournsRepository.GetUrl(parentId);
                var xml = await _loader.GetXml(BaseUrl + @"/tour/" + url);
                tourns = _tournListParser.GetTourns(xml).ToList();
            }
            foreach (var tourn in tourns)
            {
                tourn.ParentId = parentId;
                tourn.HasChildTourns = true;
                //tourn.Loaded = DateTime.Now;
                if (string.IsNullOrEmpty(tourn.SourceId))
                {
                    tourn.SourceId = tourn.Url;
                }
            }
            _tournsRepository.SaveTourns(tourns, parentId);

            return _agent.GetTourns(parentId);
        }

        public Tourn GetTourn(int tournId)
        {
            return _agent.GetTourn(tournId);
        }

        public async Task<Tourn> GetTournAsync(int tournId)
        {
            var tourn = _tournsRepository.GetTourn(tournId);
            if (!tourn.Loaded.HasValue)
            {
                var url = _tournsRepository.GetUrl(tournId);
                var xml = await _loader.GetXml(BaseUrl + @"/tour/" + url);
                List<Question> questions = _tournParser.GetQuestions(xml).ToList();
                foreach (var question in questions)
                {
                    await ProcessQuestion(question);
                }
                _tournsRepository.SaveQuestions(questions, tournId);
            }

            return tourn;
        }

        private async Task ProcessQuestion(Question question)
        {
            question.TextImages = await GetImages(question.Text);
            question.Text = RemoveImageLinks(question.Text);
            question.AdditionalText = GetAdditionalText(question.Text); //TODO: Может ли быть картинка?
            question.Text = RemoveAdditionalText(question.Text);
            question.CommentImages = await GetImages(question.Comments);
            question.Comments = RemoveImageLinks(question.Comments);
            question.AnswerImages = await GetImages(question.Answer);
            question.Answer = RemoveImageLinks(question.Answer);
        }
        
        private string RemoveImageLinks(string text)
        {
            Regex regex = new Regex("\\(pic: [a-zA-Z0-9.]+\\)");
            Match match = regex.Match(text);


            while (match.Success)
            {
                var subStr = match.Value;
                var val = subStr.Substring(6, subStr.Length - 7);
                int index = text.IndexOf(subStr);
                text = (index < 0)
                    ? text
                    : text.Remove(index, subStr.Length).Trim();
                match = match.NextMatch();
            }
            return text;
        }

        private string GetAdditionalText(string text)
        {
            Regex regex = new Regex("<раздатка>(.*)</раздатка>");
            var v = regex.Matches(text);
            var result = new StringBuilder();
            if (v.Count > 0)
            {
                foreach (Match match in v)
                {
                    result.AppendLine(match.Groups[1].ToString());
                }
            }
            //text = regex.Replace(text, "");
            return result.ToString();//todo:
        }

        private string RemoveAdditionalText(string text)
        {
            Regex regex = new Regex("<раздатка>(.*)</раздатка>");
            return regex.Replace(text, "");
        }

        private async Task<List<Image>> GetImages(string text)
        {
            Regex regex = new Regex("\\(pic: [a-zA-Z0-9.]+\\)");
            Match match = regex.Match(text);

            var images = new List<Image>();

            while (match.Success)
            {
                var subStr = match.Value;
                var val = subStr.Substring(6, subStr.Length - 7);
                var img = await _loader.GetFile(BasePicUrl + val);
                images.Add(new Image(){Data = img});
                //int index = text.IndexOf(subStr);
                //text = (index < 0)
                //    ? text
                //    : text.Remove(index, subStr.Length).Trim();
                match = match.NextMatch();
            }
            return images;
        }
    }
}
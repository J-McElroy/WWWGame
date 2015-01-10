using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper;
using WWWGame.DataAccessLibrary.Model;
using WWWGame.LogicLayer.Repositories;
using Image = WWWGame.DataAccessLibrary.Model.Image;
using Question = WWWGame.LogicLayer.Model.Question;
using Tourn = WWWGame.LogicLayer.Model.Tourn;

namespace WWWGame.DataAccessLibrary
{
    public class TournsRepository : ITournsRepository
    {
        static TournsRepository()
        {
            Mapper.CreateMap<Model.Tourn, Tourn>();
            Mapper.CreateMap<Tourn, Model.Tourn>();

            Mapper.CreateMap<Model.Question, Question>();
            Mapper.CreateMap<Question, Model.Question>();
        }
        public TournsRepository()
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                if (db.DatabaseExists() == false)
                {
                    //Create the database
                    db.CreateDatabase();
                }
                //db.DeleteDatabase();
            }
        }

        public IEnumerable<Tourn> GetTournsByParent(int? parentId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                IEnumerable<Tourn> tourns;
                if (parentId.HasValue)
                {
                    tourns = from tournItem in db.TournItems
                        where tournItem.ParentId == parentId
                        select Mapper.Map<Model.Tourn, Tourn>(tournItem);
                }
                else
                {
                    tourns = from tournItem in db.TournItems
                             where !tournItem.ParentId.HasValue
                             select Mapper.Map<Model.Tourn, Tourn>(tournItem);
                }

                return tourns.ToList();
            }
        }

        public IEnumerable<Tourn> GetLastTourns(int count)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var tourns = (from tournItem in db.TournItems
                              where tournItem.LastOpened != null
                              orderby tournItem.LastOpened descending
                              select tournItem).Take(count).Select(el => Mapper.Map<Model.Tourn, Tourn>(el));

                var res = tourns.ToList();

                foreach (var tourn in res)
                {
                    if (tourn.Type == "Т")
                    {
                        var parent = db.TournItems.FirstOrDefault(el => el.Id == tourn.ParentId);
                        if (parent == null)
                        {
                            throw new Exception("");
                        }
                        tourn.Title = string.Format("{0} ({1})", parent.Title, tourn.Title);
                    }
                }

                return res;
            }
        }

        public IEnumerable<Tourn> GetAllTourns()
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var tourns = (from tournItem in db.TournItems
                              orderby tournItem.Loaded
                              where tournItem.HasChildTourns == false
                              select tournItem).Select(el => Mapper.Map<Model.Tourn, Tourn>(el));
                var res = tourns.ToList();

                foreach (var tourn in res)
                {
                    if (tourn.Type == "Т")
                    {
                        var parent = db.TournItems.FirstOrDefault(el => el.Id == tourn.ParentId);
                        if (parent == null)
                        {
                            throw new Exception("");
                        }
                        tourn.Title = string.Format("{0} ({1})", parent.Title, tourn.Title);
                    }
                }

                return tourns.ToList();
            }
        }

        public void SaveTourns(IEnumerable<Tourn> tourns, int? parentId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                if (parentId.HasValue)
                {
                    var parent = db.TournItems.First(el => el.Id == parentId);
                    parent.HasChildTourns = true;
                }
                foreach (var tourn in tourns)
                {
                    var existTourn = db.TournItems.FirstOrDefault(el => el.SourceId == tourn.SourceId);
                    if (existTourn == null)
                    {
                        var t = Mapper.Map<Tourn, Model.Tourn>(tourn);
                        db.TournItems.InsertOnSubmit(t);
                    }
                    else
                    {
                        UpdateItem(existTourn, tourn, new List<string> { "Id", "ParentId", "LastOpened", "LastQuestion", "Loaded", "HasChildTourns" });
                    }
                }
                db.SubmitChanges();
            }
        }

        public void SaveQuestions(IEnumerable<Question> questions, int parentId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var qs = questions.ToList();
                foreach (var question in qs)
                {
                    var q = Mapper.Map<Question, Model.Question>(question);
                    q.TournId = parentId;
                    db.QuestionItems.InsertOnSubmit(q);
                    db.SubmitChanges();
                    if (question.TextImages != null)
                    {
                        foreach (var image in question.TextImages)
                        {

                            var img = new Image {Data = image.Data, QuestionId = q.Id, Type = ImageType.Question};
                            db.ImageItems.InsertOnSubmit(img);

                        }
                    }
                    if (question.CommentImages != null)
                    {
                        foreach (var image in question.CommentImages)
                        {
                            var img = new Image { Data = image.Data, QuestionId = q.Id, Type = ImageType.Comment };
                            db.ImageItems.InsertOnSubmit(img);
                        }
                    }
                    if (question.AnswerImages != null)
                    {
                        foreach (var image in question.AnswerImages)
                        {
                            var img = new Image { Data = image.Data, QuestionId = q.Id, Type = ImageType.Answer };
                            db.ImageItems.InsertOnSubmit(img);
                        }
                    }
                    db.SubmitChanges();
                }
                var t = db.TournItems.FirstOrDefault(el => el.Id == parentId);
                t.HasChildTourns = false;
                t.Loaded = DateTime.Now;
                db.SubmitChanges();
            }
            //throw new NotImplementedException();
        }

        public IEnumerable<Question> GetQuestions(int tournId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var questions =
                    db.QuestionItems.Where(el => el.TournId == tournId)
                        .ToList();

                var res = new List<Question>();

                foreach (var question in questions)
                {
                    var q = Mapper.Map<Model.Question, Question>(question);
                    q.TextImages =
                        db.ImageItems.Where(el => el.QuestionId == question.Id && el.Type == ImageType.Question)
                            .Select(el => new LogicLayer.Model.Image(){ Data = el.Data, Id = el.Id})
                            .ToList();
                    q.CommentImages =
                        db.ImageItems.Where(el => el.QuestionId == question.Id && el.Type == ImageType.Comment)
                            .Select(el => new LogicLayer.Model.Image() { Data = el.Data, Id = el.Id })
                            .ToList();
                    q.AnswerImages =
                        db.ImageItems.Where(el => el.QuestionId == question.Id && el.Type == ImageType.Answer)
                            .Select(el => new LogicLayer.Model.Image() { Data = el.Data, Id = el.Id })
                            .ToList();
                    res.Add(q);
                }

                var tourn = db.TournItems.FirstOrDefault(el => el.Id == tournId);
                tourn.LastOpened = DateTime.Now;
                db.SubmitChanges();

                return res;
            }
        }

        public string GetUrl(int? parentId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var t = db.TournItems.First(el => el.Id == parentId);
                if (string.IsNullOrEmpty(t.TextId))
                {
                    return t.Url.Substring(6);
                }
                return t.TextId;
            }
        }

        public Tourn GetTourn(int id)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var t = db.TournItems.First(el => el.Id == id);
                return Mapper.Map<Model.Tourn, Tourn>(t);
            }
        }

        public void SetLastQuestion(int tournId, int questionNum)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var tourn = db.TournItems.FirstOrDefault(el => el.Id == tournId);
                tourn.LastQuestion = questionNum;
                db.SubmitChanges();
            }
        }

        public byte[] GetImage(int imgId)
        {
            using (var db = new TournsDataContext(TournsDataContext.DbConnectionString))
            {
                var img = db.ImageItems.FirstOrDefault(el => el.Id == imgId);
                if(img==null)
                    return null;
                return img.Data;
            }
        }

        private void UpdateItem<T, TU>(T existItem, TU item, List<string> expectProps)
        {
            var props = existItem.GetType().GetProperties();
            var anProps = item.GetType().GetProperties();

            foreach (var propertyInfo in props)
            {
                if(expectProps.Contains(propertyInfo.Name))
                    continue;

                var prop = anProps.First(el => el.Name == propertyInfo.Name);

                var nValue = prop.GetValue(item);
                propertyInfo.SetValue(existItem, nValue);
            }
        }
    }
}

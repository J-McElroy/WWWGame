using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Parser;

namespace WWWGame.SourceParser
{
    public class CachedRootParser : IRootParser
    {
        public Task<IEnumerable<Tourn>> GetTourns(string url)
        {
            IEnumerable<Tourn> rootTourns = new List<Tourn>()
            {
                new Tourn()
                {
                    Url = @"/tour/AUTHORS",
                    Title = @"Авторские вопросы"
                },
                new Tourn()
                {
                    Url = @"/tour/INTER",
                    Title = @"Чемпионаты разных стран и международные турниры"
                },
                new Tourn()
                {
                    Url = @"/tour/SINHR",
                    Title = @"Синхронные турниры"
                },
                new Tourn()
                {
                    Url = @"/tour/NEPOLN",
                    Title = @"Турниры для команд в неполном составе"
                },
                new Tourn()
                {
                    Url = @"/tour/REGION",
                    Title = @"Региональные турниры"
                },
                new Tourn()
                {
                    Url = @"/tour/INET",
                    Title = @"Игры в Интернете"
                },
                new Tourn()
                {
                    Url = @"/tour/R100",
                    Title = @"Конкурс ""Рейтинг-100"" газеты ""Игра"""
                },
                new Tourn()
                {
                    Url = @"/tour/TELE",
                    Title = @"Телеигры"
                },
                new Tourn()
                {
                    Url = @"/tour/TREN",
                    Title = @"Тренировки разных клубов/команд"
                },
                new Tourn()
                {
                    Url = @"/tour/TEMA",
                    Title = @"Тематические турниры"
                },
                new Tourn()
                {
                    Url = @"/tour/ERUDITK",
                    Title = @"Эрудитки"
                },
                new Tourn()
                {
                    Url = @"/tour/EF",
                    Title = @"Эрудит-футбол"
                },
                new Tourn()
                {
                    Url = @"/tour/BESKR",
                    Title = @"Бескрылки"
                },
                new Tourn()
                {
                    Url = @"/tour/SVOYAK",
                    Title = @"Своя игра"
                }
            };

            return Task.FromResult(rootTourns);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Repositories;

namespace WWWGame.LogicLayer
{
    public class LocalDbTournsAgent : ITournsAgent
    {
        private readonly ITournsRepository _repository;

        public LocalDbTournsAgent(ITournsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        public IEnumerable<Tourn> GetAllTourns()
        {
            return _repository.GetAllTourns();
        }

        public IEnumerable<Tourn> GetLastTourns(int count)
        {
            return _repository.GetLastTourns(count);
        }

        public IEnumerable<Tourn> GetTourns(int? parentId)
        {
            return _repository.GetTournsByParent(parentId);
        }

        public async Task<IEnumerable<Tourn>> GetTournsAsync(int? parentId)
        {
            return GetTourns(parentId);
        }

        public Tourn GetTourn(int tournId)
        {
            return _repository.GetTourn(tournId);
        }

        public async Task<Tourn> GetTournAsync(int tournId)
        {
            return GetTourn(tournId);
        }
    }
}
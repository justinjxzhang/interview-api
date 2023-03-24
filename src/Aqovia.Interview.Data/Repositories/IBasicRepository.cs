using Aqovia.Interview.Data.Models;

namespace Aqovia.Interview.Data.Repositories
{
    public interface IBasicRepository<TK, TV> where TV: IBasicModel<TK> {
        public bool TryGetById(TK id, out TV? value);
        public IEnumerable<TV> Get();
        public TV Update(TK id, TV value);
        public TV Create(TV value);
        public void Delete(TK id);
    }
}
using Aqovia.Interview.Data.Models;
using Aqovia.Interview.Data.Repositories;

namespace Aqovia.Interview.Data.InMemory.Repositories
{
    public class BasicInMemoryRepository<TV>
        : IBasicRepository<Guid, TV>
        where TV: IBasicModel<Guid>
    {
        private readonly List<TV> _datastore;
        public BasicInMemoryRepository() {
            this._datastore = new List<TV>();
        }

        public BasicInMemoryRepository(List<TV> seed) {
            this._datastore = seed;
        }

        public TV Create(TV value)
        {
            Guid newGuid;
            do {
                newGuid = Guid.NewGuid();
            } while (this._datastore.Any(x => x.Id == newGuid));

            value.Id = newGuid;
            this._datastore.Add(value);
            return value;
        }

        public void Delete(Guid id)
        {
            this._datastore.RemoveAll(x => x.Id == id);
        }

        public IEnumerable<TV> Get()
        {
            return this._datastore.ToList();
        }

        public bool TryGetById(Guid id, out TV? value)
        {
            value = this._datastore.FirstOrDefault(x => x.Id.Equals(id));
            return value == null;
        }
        public TV Update(Guid id, TV value)
        {
            var itemIndex = this._datastore.FindIndex(x => x.Id.Equals(id));
            if (itemIndex < 0)
                throw new KeyNotFoundException($"Item with ID ${id} not found");
            value.Id = id;
            this._datastore[itemIndex] = value;
            
            return value;
        }
    }
}
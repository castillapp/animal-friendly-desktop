using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockPersistencia.Data
{
    public abstract class BaseData<Key, ModelType> where ModelType : BaseModel
    {
        private Dictionary<Key, ModelType> rows = new Dictionary<Key, ModelType>();
        protected static Key CurrentId { get; private set; }
        protected abstract Func<Key, Key> IncreaseId { get; }
        protected abstract Func<Key, ModelType, ModelType> SetKeyToRow { get; }
        protected abstract Func<ModelType, ModelType> Clone { get; }

        private Key GetNextId()
        {
            var id = IncreaseId(CurrentId);
            CurrentId = id;
            return CurrentId;
        }

        public BaseData()
        {
            rows = new Dictionary<Key, ModelType>();
            InitData();
        }

        protected abstract void InitData();

        public void Insert(ModelType row)
        {
            var id = GetNextId();
            row = SetKeyToRow(id, row);
            rows.Add(id, row);
        }

        public ModelType Get(Key key)
        {
            var item = rows[key];
            return Clone(item);
        }

        public void Delete(Key key)
        {
            rows.Remove(key);
        }

        public List<ModelType> ListAll()
        {
            return rows.Values.Select<ModelType, ModelType>(f => Clone(f)).ToList();
        }

        public void Update(Key key, ModelType row)
        {
            rows[key] = row;
        }
    }
}

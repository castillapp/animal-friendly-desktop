using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockPersistencia.Data
{
    public abstract class BaseData<Key,ModelType> where ModelType : BaseModel
    {
        private static Dictionary<Key, ModelType> rows;

        public BaseData()
        {
            rows = new Dictionary<Key, ModelType>();
            InitData();
        }

        protected abstract void InitData();

        public void Insert(Key key, ModelType row)
        {
            rows.Add(key, row);
        }

        public ModelType Get(Key key)
        {
            return rows[key];
        }

        public void Delete(Key key)
        {
            rows.Remove(key);
        }

        public List<ModelType> ListAll()
        {
            return rows.Values.ToList();
        }

        public void Update(Key key,ModelType row)
        {
            var item = Get(key);
            item = row;
        }
    }
}

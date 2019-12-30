using MyEverNote.Common;
using MyEverNote.DataAccessLayer;
using MyEverNote.DataAccessLayer.Abstract;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class Repository<T>:RepositoryBase,IRepository<T> where T:class // class dışından bir nesnenin tanımlanmamasını istedik
    {
        //private DatabaseContext db = new DatabaseContext(); // db ile bundan sonraki bütün tablolarıma erişebilmeyi sağladım. RepositoryBase i miras aldıktan sonra bu ifadeye gerek kalmadı.

        private DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = db.Set<T>();
        }
        public List<T> List()
        {
            //return db.Set<T>().ToList();    // verimi listelemiş oldum.
            return _objectSet.ToList();    // verimi listelemiş oldum.

        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();

            //db.Categories.Where(x => x.Id == 1).ToList();  yukarıdaki where kelimesiyle eşdeğer bir sorgu

        }

        public T Find(Expression<Func<T, bool>> where)  // liste haline gelmeden arama yapılıcak o yüzden List i kaldırdım.
        {
            return _objectSet.FirstOrDefault(where);

        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is BaseEntity)
            {
                // BaseEntity türüne bu obj türüne casting işlemi yaparak dönüştürüyorum.
                BaseEntity o = obj as BaseEntity;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                // o.ModifiedUserName = "system";
                o.ModifiedUserName = App.common.GetCurrentUserName();
            }
            return Save();
        }

        public int Update(T obj)
        {

            if (obj is BaseEntity)
            {
                // BaseEntity türüne bu obj türüne casting işlemi yaparak dönüştürüyorum.
                BaseEntity o = obj as BaseEntity;
                DateTime now = DateTime.Now;
                o.ModifiedOn = now;
                o.ModifiedUserName = App.common.GetCurrentUserName();
            }
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();

        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public IQueryable<T> ListQueryable()
        {

            // Sonuçlar geldikten sonra filtreleme yapıyor tolist yapınca. Yük C# a düşer.
            // Sonuçlar direkt filtrelenerek geliyor. C# ın yükü azalır SQL daha çok işlem yapar.
            return _objectSet.AsQueryable<T>();
        }
    }
}

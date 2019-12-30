using MyEverNote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        // Singleton patern için yapıyoruz. Db yi tek bir yapıya indirmek için.
        protected static DatabaseContext db;

        // lock nesnesi için

        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (db==null)
            {
                lock (_lockSync)    //kilit true ise
                {
                    if (db==null)
                    {
                        db = new DatabaseContext();
                    }
                }
            }
        }
    }
}

using NHibernate;
using System;

namespace OwinConsoleSample.DAL
{
    public class Repository : IRepository
    {
        public object Save<T>(T tt)
        {
            object obj = null;

            using (ISession session = NHibernateSessionFactory.Instance.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        obj = session.Save(tt);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return obj;
        }
    }
}

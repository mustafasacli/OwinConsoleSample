using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using OwinConsoleSample.Types;
using System;
using System.IO;

namespace OwinConsoleSample.DAL
{
    internal class NHibernateSessionFactory
    {
        private static NHibernateSessionFactory pVal=null;
        private static object objLock = new object();
        
        ISessionFactory sessionFactory = null;
        private object objLock2 = new object();

        public static NHibernateSessionFactory Instance
        {
            get
            {
                if (pVal == null)
                {
                    lock (objLock)
                    {
                        if (pVal == null)
                        {
                            pVal = new NHibernateSessionFactory();
                        }
                    }
                }

                return pVal;
            }
        }

        private NHibernateSessionFactory() { }
                 
        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (objLock2)
                    {
                        if (sessionFactory == null)
                        {
                            sessionFactory = OpenSessionFact();
                        }
                    }
                }

                return sessionFactory;
            }
        }

        public static ISessionFactory OpenSessionFact()
        {
            var configuration = new Configuration();

            // for nhibernate configuration files
            var configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hibernate.cfg.xml");
            //var configurationPath = HttpContext.Current.Server.MapPath(@"~\hibernate.cfg.xml");
            configuration.Configure(configurationPath);

            // for entity config files
            //var employeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Employee.hbm.xml");
            //configuration.AddFile(employeeConfigurationFile);
            var serializer = new HbmSerializer() { Validate = true };

            using (var stream = serializer.Serialize(typeof(ServiceDeviceData)))
            {
                configuration.AddInputStream(stream, typeof(ServiceDeviceData).Name);
            }

            ISessionFactory sesFactory = configuration.BuildSessionFactory();
            return sesFactory;
            //return sessionFactory.OpenSession();
        }
    }
}

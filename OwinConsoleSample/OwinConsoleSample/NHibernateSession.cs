using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using OwinConsoleSample.Types;

namespace OwinConsoleSample
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"hibernate.cfg.xml");
            //var configurationPath = HttpContext.Current.Server.MapPath(@"~\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            //var employeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Employee.hbm.xml");
            //configuration.AddFile(employeeConfigurationFile);
            var serializer = new HbmSerializer() { Validate = true };

            using (var stream = serializer.Serialize(typeof(ServiceDeviceData)))
            {
                configuration.AddInputStream(stream, typeof(ServiceDeviceData).Name);
            }

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}

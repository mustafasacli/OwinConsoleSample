using NHibernate;
using OwinConsoleSample.DAL;
using OwinConsoleSample.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace OwinConsoleSample
{
    [RoutePrefix("Devdata")]
    public class DevDataController : BaseApiController
    {
        Repository repo = null;//new Repository();

        public DevDataController() { repo = new Repository(); }


        [ResponseType(typeof(int))]
        [AcceptVerbs("GET", "POST")]//, System.Net.Http.Formatting.FormDataCollection
        public IHttpActionResult Post(string deviceCode, string data)
        {
            int rs = 0;

            try
            {
                ServiceDeviceData sdd = new ServiceDeviceData
                {
                    DeviceCode = deviceCode,
                    DeviceData = data,
                    DeviceAdress = this.RequestAddress,
                    ResponseAdress = this.ResponseAddress,
                    //CreationTime = DateTime.Now
                };

                //using (ISession session = NHibernateSession.OpenSession())
                //{
                //    using (ITransaction transaction = session.BeginTransaction())
                //{
                sdd.CreationTime = DateTime.Now;
                // session.Save(sdd);
                object o = repo.Save(sdd);
                rs = sdd.Id;
                //        transaction.Commit();
                //    }
                //}

            }
            catch (Exception e)
            {
                try
                {
                    if (rs < 1)
                        rs = -1;

                }
                catch { }
            }

            return Ok(rs);
        }

        [AcceptVerbs("GET")]
        [Route("Info")]
        public IHttpActionResult GetInfo()
        {
            return Ok(GetInfoArray());
        }
    }
}

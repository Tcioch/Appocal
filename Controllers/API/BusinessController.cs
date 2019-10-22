using Appocal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Appocal.Controllers.API
{
    public class BusinessController : ApiController
    {
        private ApplicationDbContext _contex;
        public BusinessController()
        {
            _contex = new ApplicationDbContext();
        }
        public IHttpActionResult GetServices(string businessName)
        {
            var services = _contex.Users.Include(u => u.Business.Services).Single(u => u.UserName == businessName).Business.Services.Where(s => s.Active == true);
            services = services.OrderBy(s => s.Name);
            return Ok(services);
        }

        public IHttpActionResult GetService(string id)
        {
            int serviceId = int.Parse(id);
            var service = _contex.Services.SingleOrDefault(s => s.Id == serviceId);
            return Ok(service);
        }
    }
}
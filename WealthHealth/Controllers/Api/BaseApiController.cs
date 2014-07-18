using System.Web.Http;
using WealthHealth.Data.Contracts;

namespace WealthHealth.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected readonly IDbUow dbUow;

        public BaseApiController(IDbUow dbUow)
        {
            this.dbUow = dbUow;
        }
    }
}
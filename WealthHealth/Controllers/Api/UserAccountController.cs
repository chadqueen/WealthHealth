using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using WealthHealth.Data.Contracts;
using WealthHealth.Models;

namespace WealthHealth.Controllers.Api
{
    [RoutePrefix("api/UserAccount")]
    public class UserAccountController : BaseApiController
    {
        public UserAccountController(IDbUow dbUow): base(dbUow)
        {
        }

        // POST api/UserAccount/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel registerBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await dbUow.ApplicationUsers
                .RegisterUser(
                    registerBindingModel.UserName, 
                    registerBindingModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbUow.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
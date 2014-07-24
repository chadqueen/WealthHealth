using System.Web.Mvc;

namespace WealthHealth.Controllers
{
    public class WealthHealthController : Controller
    {
        // GET: WealthHealth
        public ActionResult Index()
        {
            ViewBag.AppUrlBase = "/wealthhealth/";
            ViewBag.AppName = "WealthHealth";
            return View();
        }
    }
}
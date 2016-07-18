using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.Home;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using Platform.Process;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class HomeController : WdControllerBase
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();

            var rates = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelCleanessList();

            foreach (var rate in rates)
            {
                model.HotelCleanessList.Add(new HotelCleaness() { HotelName = rate.Key, CleanessRate = rate.Value });
            }

            model.NoData = rates.Count(obj => obj.Value == CleanessRateResult.NoData);

            model.Faild = rates.Count(obj => obj.Value == CleanessRateResult.Fail);

            model.Worse = rates.Count(obj => obj.Value == CleanessRateResult.Worse);

            model.Qualified = rates.Count(obj => obj.Value == CleanessRateResult.Qualified);

            model.Good = rates.Count(obj => obj.Value == CleanessRateResult.Good);

            return View(model);
        }
    }
}
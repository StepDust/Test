using System.Web.Mvc;

namespace EBuy.Areas.Algorithm
{
    public class AlgorithmAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Algorithm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Algorithm_default",
                "Algorithm/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
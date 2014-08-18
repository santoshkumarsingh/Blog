using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HtmlInputsInMvc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult AddComment(string comment)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(comment, "<script>.*?</script>")) {
                throw new HttpException(500, "Potantially malicious Request.QueryString detected.");
            }
            else {
                ViewBag.Message = comment;
            }
            return View("Index");
        }
	}
}
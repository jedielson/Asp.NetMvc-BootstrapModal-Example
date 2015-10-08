namespace ModalPopUpBootstrap.Controllers
{
    using System.Web.Mvc;

    using MvcModalDialog.Models;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Dialog1()
        {
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult Dialog1(DialogModel model)
        {
            return this.ProcessDialog(model, 1, "Great, your answer is correct!");
        }

        public ActionResult Dialog2()
        {
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult Dialog2(DialogModel model)
        {
            return this.ProcessDialog(model, 2);
        }

        public ActionResult Dialog3()
        {
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult Dialog3(DialogModel model)
        {
            return this.ProcessDialog(model, 3);
        }

        ActionResult ProcessDialog(DialogModel model, int answer)
        {
            return this.ProcessDialog(model, answer, null);
        }

        ActionResult ProcessDialog(DialogModel model, int answer, string message)
        {
            if (this.ModelState.IsValid)
            {
                var data = new { id = 1, valor = "Ronaldo" };
                var jsonResponse = new ModalDialogExtensions.JsonDialogResult("jediInput", data);
                if (model.Value == answer)
                    return this.DialogResult(message, jsonResponse);  // Close dialog via DialogResult call
                else
                    this.ModelState.AddModelError("", string.Format("Invalid input value. The correct value is {0}", answer));
            }

            return this.PartialView(model);
        }
    }
}

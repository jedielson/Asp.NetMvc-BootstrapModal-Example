namespace ModalPopUpBootstrap
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    /// <summary>
    /// Cabeçalho teste
    /// </summary>
    public static class ModalDialogExtensions
    {
        /// <summary>
        /// The modal dialog action link.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString ModalDialogActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string dialogTitle, RouteValueDictionary routeValues)
        {
            var dialogDivId = Guid.NewGuid().ToString();
            return ajaxHelper.ActionLink(
                    linkText, 
                    actionName,
                    routeValues: routeValues,
                    ajaxOptions: new AjaxOptions
                    {
                        UpdateTargetId = dialogDivId,
                        InsertionMode = InsertionMode.Replace,
                        HttpMethod = "GET",
                        OnBegin = string.Format(CultureInfo.InvariantCulture, "prepareModalDialog('{0}')", dialogDivId),
                        OnFailure = string.Format(CultureInfo.InvariantCulture, "clearModalDialog('{0}');alert('Ajax call failed')", dialogDivId),
                        OnSuccess = string.Format(CultureInfo.InvariantCulture, "openModalDialog('{0}', '{1}')", dialogDivId, dialogTitle)
                    });
        }

        /// <summary>
        /// The modal dialog action link.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <returns>The <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString ModalDialogActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string dialogTitle)
        {
            var dialogDivId = Guid.NewGuid().ToString();
            return ajaxHelper.ActionLink(
                    linkText, 
                    actionName, 
                    routeValues: null,
                    ajaxOptions: new AjaxOptions
                    {
                        UpdateTargetId = dialogDivId,
                        InsertionMode = InsertionMode.Replace,
                        HttpMethod = "GET",
                        OnBegin = string.Format(CultureInfo.InvariantCulture, "prepareModalDialog('{0}')", dialogDivId),
                        OnFailure = string.Format(CultureInfo.InvariantCulture, "clearModalDialog('{0}');alert('Ajax call failed')", dialogDivId),
                        OnSuccess = string.Format(CultureInfo.InvariantCulture, "openModalDialog('{0}', '{1}')", dialogDivId, dialogTitle)
                    });
        }

        /// <summary>
        /// The modal dialog action link.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="controllerName">The controller name.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <returns>The <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString ModalDialogActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, string dialogTitle)
        {
            var dialogDivId = Guid.NewGuid().ToString();
            return ajaxHelper.ActionLink(
                    linkText, 
                    actionName, 
                    controllerName, 
                    routeValues: null,
                    ajaxOptions: new AjaxOptions
                    {
                        UpdateTargetId = dialogDivId,
                        InsertionMode = InsertionMode.Replace,
                        HttpMethod = "GET",
                        OnBegin = "prepareModalDialog()",
                        OnSuccess = "openModalDialog('" + dialogTitle + "')"
                    });
        }

        /// <summary>
        /// The begin modal dialog form.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <returns>The <see cref="MvcForm"/>.</returns>
        public static MvcForm BeginModalDialogForm(this AjaxHelper ajaxHelper)
        {
            // ReSharper disable once Mvc.ActionNotResolved
            return ajaxHelper.BeginForm(new AjaxOptions
            {
                HttpMethod = "POST"
            });
        }

        /// <summary>
        /// The dialog result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public static ActionResult DialogResult(this Controller controller)
        {
            return DialogResult(controller, string.Empty);
        }

        /// <summary>
        /// The dialog result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public static ActionResult DialogResult(this Controller controller, string message)
        {
            return new DialogActionResult(message);
        }

        /// <summary>
        /// Classe DialogActionResult
        /// </summary>
        internal sealed class DialogActionResult : ActionResult
        {
            /// <summary>
            /// Inicializa um <see cref="DialogActionResult"/>
            /// </summary>
            /// <param name="message">Uma mensagem</param>
            public DialogActionResult(string message)
            {
                this.Message = message ?? string.Empty;
            }

            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// The execute result.
            /// </summary>
            /// <param name="context">
            /// The context.
            /// </param>
            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.Write(string.Format("<div data-dialog-close='true' data-dialog-result='{0}' />", this.Message));
            }
        }
    }
}
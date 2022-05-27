using Administrator.Models.ViewModels;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator.Services
{
    public static class AlertService
    {
        public enum AlertType
        {
            Success,
            Error,
            Warning,
            Info
        }
        public static void ShowAlert(Page page, AlertType type, SweetAlertModel sweetAlert)
        {
            if (page.Master.FindControl("Script") is ContentPlaceHolder control)
            {
                LiteralControl literal = new LiteralControl
                {
                    Text = CreateAlert(type, sweetAlert)
                };
                control.Controls.Add(literal);
            }

        }

        private static string CreateAlert(AlertType type, SweetAlertModel sweetAlert)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<script>$(document).ready(function () {Swal.fire({");

            switch (type)
            {
                case AlertType.Success:
                    sb.Append("icon: 'success',");
                    break;
                case AlertType.Error:
                    sb.Append("icon: 'error',");
                    break;
                case AlertType.Warning:
                    sb.Append("icon: 'warning',");
                    break;
                case AlertType.Info:
                    sb.Append("icon: 'info',");
                    break;
                default:
                    
                    break;
            }

            if (sweetAlert.Title != null)
            {
                sb.Append($"title: '{sweetAlert.Title}',");
            }
            if (sweetAlert.Text != null)
            {
                sb.Append($"text: '{sweetAlert.Text}',");
            }
            if (sweetAlert.Html != null)
            {
                sb.Append($"html: '{sweetAlert.Html}',");
            }
            if (sweetAlert.Position != null)
            {
                sb.Append($"position: '{sweetAlert.Position}',");
            }
            if (sweetAlert.ShowCloseButton)
            {
                sb.Append($"showCloseButton: {sweetAlert.ShowCloseButton},");
            }
            if (sweetAlert.ShowCancelButton)
            {
                sb.Append($"showCancelButton: {sweetAlert.ShowCancelButton},");
            }
            if (sweetAlert.ShowConfirmButton)
            {
                sb.Append($"showConfirmButton: {sweetAlert.ShowConfirmButton},");
            }
            if (sweetAlert.Timer != null)
            {
                sb.Append($"timer: {sweetAlert.Timer},");
            }

            sb.Append("})});</script>");

            return sb.ToString();
        }
    }
}
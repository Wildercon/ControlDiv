

using ControlDiv.Shared.Responses;

namespace ControlDiv.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}

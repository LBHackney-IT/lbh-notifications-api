using LbhNotificationsApi.V1.Boundary.Requests;

namespace LbhNotificationsApi.V1.Controllers.Validators.Interfaces
{
    public interface IEmailRequestValidator
    {
        bool ValidateEmailRequest(EmailNotificationRequest request);
    }
}

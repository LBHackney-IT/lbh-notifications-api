using System.Threading.Tasks;
using LbhNotificationsApi.V1.Boundary.Requests;

namespace LbhNotificationsApi.V1.UseCase.Interfaces
{
    public interface ISendEmailNotificationUseCase
    {
        public void Execute(EmailNotificationRequest request);
    }
}

using System.Threading.Tasks;

namespace Rikkonbi.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
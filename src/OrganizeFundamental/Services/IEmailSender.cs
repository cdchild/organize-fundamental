using System.Threading.Tasks;

namespace OrganizeFundamental.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}

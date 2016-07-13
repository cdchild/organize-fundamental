using System.Threading.Tasks;

namespace OrganizeFundamental.Services
{
	public interface ISmsSender
	{
		Task SendSmsAsync(string number, string message);
	}
}

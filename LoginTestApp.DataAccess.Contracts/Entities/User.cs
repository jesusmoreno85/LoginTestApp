
namespace LoginTestApp.DataAccess.Contracts.Entities
{
	public class User : EntityBase
	{
		public string Alias { get; set; }

		public string Password { get; set; }

		public string PasswordRecoveryClue { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public bool IsActive { get; set; }
	}
}
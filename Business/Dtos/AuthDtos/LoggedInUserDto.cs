namespace Business.Dtos.AuthDtos
{
	public class LoggedInUserDto
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}

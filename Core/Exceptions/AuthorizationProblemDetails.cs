using Newtonsoft.Json;

namespace Core.Exceptions
{
	public class AuthorizationProblemDetails : ExceptionDetails
	{
		public override string ToString() => JsonConvert.SerializeObject(this);
	}

}

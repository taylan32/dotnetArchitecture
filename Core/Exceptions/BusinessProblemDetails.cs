using Newtonsoft.Json;

namespace Core.Exceptions
{
	public class BusinessProblemDetails : ExceptionDetails
	{
		public override string ToString() => JsonConvert.SerializeObject(this);
	}

}

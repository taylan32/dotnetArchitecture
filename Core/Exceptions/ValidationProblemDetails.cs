using Newtonsoft.Json;

namespace Core.Exceptions
{
	public class ValidationProblemDetails : ExceptionDetails
	{
		public object? Errors { get; set; }
		public override string ToString() => JsonConvert.SerializeObject(this);
	}

}

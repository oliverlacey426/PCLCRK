// MeloongCore

namespace MeloongCore
{
	public class HttpRequestCodeException(string message, HttpStatusCode statusCode, string? response) : HttpRequestException(message)
	{
		public new HttpStatusCode StatusCode { get; } = statusCode;
		public string? Response { get; } = response;
	}
}

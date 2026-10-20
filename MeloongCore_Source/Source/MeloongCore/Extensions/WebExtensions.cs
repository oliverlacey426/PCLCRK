// MeloongCore.Extensions

namespace MeloongCore.Extensions
{
	public static class WebExtensions
	{
		public static HttpRequestMessage Clone(this HttpRequestMessage request)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(request.Method, request.RequestUri)
			{
				Content = request.Content.Clone(),
				Version = request.Version
			};
			foreach (KeyValuePair<string, object> property in request.Properties)
			{
				httpRequestMessage.Properties.Add(property);
			}
			foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
			{
				httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
			return httpRequestMessage;
		}
		public static async Task<HttpRequestMessage> CloneAsync(this HttpRequestMessage request)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(request.Method, request.RequestUri);
			HttpRequestMessage httpRequestMessage2 = httpRequestMessage;
			httpRequestMessage2.Content = await request.Content.CloneAsync().NoCapture();
			httpRequestMessage.Version = request.Version;
			HttpRequestMessage httpRequestMessage3 = httpRequestMessage;
			foreach (KeyValuePair<string, object> property in request.Properties)
			{
				httpRequestMessage3.Properties.Add(property);
			}
			foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
			{
				httpRequestMessage3.Headers.TryAddWithoutValidation(header.Key, header.Value);
			}
			return httpRequestMessage3;
		}
		public static HttpContent? Clone(this HttpContent? content)
		{
			if (content == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			content.CopyToAsync(memoryStream).Wait();
			memoryStream.Position = 0L;
			StreamContent streamContent = new StreamContent(memoryStream);
			foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
			{
				streamContent.Headers.Add(header.Key, header.Value);
			}
			return streamContent;
		}
		public static async Task<HttpContent?> CloneAsync(this HttpContent? content)
		{
			if (content == null)
			{
				return null;
			}
			MemoryStream ms = new MemoryStream();
			await content.CopyToAsync(ms).NoCapture();
			ms.Position = 0L;
			StreamContent streamContent = new StreamContent(ms);
			foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
			{
				streamContent.Headers.Add(header.Key, header.Value);
			}
			return streamContent;
		}
	}
}

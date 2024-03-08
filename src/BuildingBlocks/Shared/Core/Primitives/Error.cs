using System.Text.Json.Serialization;

namespace Shared.Core.Primitives
{
    public sealed class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonPropertyName("code")]
        public string Code { get; }

        [JsonPropertyName("message")]
        public string Message { get; }
    }
}

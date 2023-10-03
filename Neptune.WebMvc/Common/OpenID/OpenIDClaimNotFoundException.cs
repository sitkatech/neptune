using System.Runtime.Serialization;

namespace Neptune.WebMvc.Common.OpenID;

public class OpenIDClaimNotFoundException : Exception
{
    public OpenIDClaimNotFoundException(string message) : base(message) { }
    public OpenIDClaimNotFoundException() { }
    public OpenIDClaimNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    protected OpenIDClaimNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
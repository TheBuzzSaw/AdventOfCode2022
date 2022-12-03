using System;
using System.Runtime.Serialization;

[Serializable]
public class AdventException : Exception
{
    public AdventException() { }
    public AdventException(string message) : base(message) { }
    public AdventException(string message, Exception inner) : base(message, inner) { }
    protected AdventException(
        SerializationInfo info,
        StreamingContext context) : base(info, context) { }
}
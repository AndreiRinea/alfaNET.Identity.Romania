using System.ComponentModel;
using Newtonsoft.Json;

namespace alfaNET.Identity.Romania.Tests.Cnp.Serialization;

internal class MockJsonReader(JsonToken tokenType, object? value) : JsonReader
{
    public override bool Read()
    {
        throw new InvalidOperationException();
    }

    public override JsonToken TokenType { get; } = tokenType;
    public override object? Value { get; } = value;
}
using Newtonsoft.Json;

namespace alfaNET.Identity.Romania.Tests.Cnp.Serialization;

internal class MockJsonWriter : JsonWriter
{
    public int WriteNullCalled { get; private set; }
    public long? WriteLongValue { get; private set; }

    public override void Flush()
    {
    }

    public override void WriteNull()
    {
        WriteNullCalled++;
    }

    public override void WriteValue(long value)
    {
        WriteLongValue = value;
    }
}
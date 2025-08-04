using alfaNET.Identity.Romania.Cnp;
using alfaNET.Identity.Romania.Cnp.Serialization;
using Newtonsoft.Json;

namespace alfaNET.Identity.Romania.Tests.Cnp.Serialization;

public class PersonalNumericCodeJsonConverterTest
{
    private const long Code1 = 1800101420010;
    
    private readonly PersonalNumericCodeJsonConverter _systemUnderTest = new();
    private readonly PersonalNumericCode _personalNumericCode1 = new(Code1);
    private readonly JsonSerializer _jsonSerializer = JsonSerializer.CreateDefault();
    private readonly MockJsonWriter _jsonWriter = new();
    
    [Fact]
    public void WriteJson_WritesNull_ForNullInput()
    {
        _systemUnderTest.WriteJson(_jsonWriter, null, _jsonSerializer);
        Assert.Equal(1, _jsonWriter.WriteNullCalled);
    }
    
    [Fact]
    public void WriteJson_WritesLongValue_ForNonNullInput()
    {
        _systemUnderTest.WriteJson(_jsonWriter, _personalNumericCode1, _jsonSerializer);
        Assert.Equal(Code1, _jsonWriter.WriteLongValue);
    }

    [Fact]
    public void ReadJson_ReadsNull_ForNullInput()
    {
        var jsonReader = new MockJsonReader(JsonToken.Null, null);
        var personalNumericCode = _systemUnderTest.ReadJson(jsonReader, typeof(long), null, false, _jsonSerializer);
        Assert.Null(personalNumericCode);
    }

    [Fact]
    public void ReadJson_ReadsCnp_ForLongValue()
    {
        var jsonReader = new MockJsonReader(JsonToken.Integer, Code1);
        var personalNumericCode = _systemUnderTest.ReadJson(jsonReader, typeof(long), null, false, _jsonSerializer);
        Assert.Equal(_personalNumericCode1, personalNumericCode);
    }

    [Fact]
    public void ReadJson_Throws_ForOtherValue()
    {
        var jsonReader = new MockJsonReader(JsonToken.String, Code1.ToString());
        Assert.Throws<JsonSerializationException>(() =>
        {
            _systemUnderTest.ReadJson(jsonReader, typeof(long), null, false, _jsonSerializer);
        });
    }
}
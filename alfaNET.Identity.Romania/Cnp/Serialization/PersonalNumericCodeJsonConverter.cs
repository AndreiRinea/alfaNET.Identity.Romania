using System;
using Newtonsoft.Json;

namespace alfaNET.Identity.Romania.Cnp.Serialization
{
    public class PersonalNumericCodeJsonConverter : JsonConverter<PersonalNumericCode>
    {
        public override void WriteJson(JsonWriter writer, PersonalNumericCode value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var cnpInt64 = value.GetValueAsLong();
                writer.WriteValue(cnpInt64);
            }
        }

        public override PersonalNumericCode ReadJson(JsonReader reader, Type objectType,
            PersonalNumericCode existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.Integer:
                    var cnpInt64 = Convert.ToInt64(reader.Value);
                    var personalNumericCode = new PersonalNumericCode(cnpInt64);
                    return personalNumericCode;
                default:
                    throw new JsonSerializationException($"Unexpected token parsing {nameof(PersonalNumericCode)}.");
            }
        }
    }
}
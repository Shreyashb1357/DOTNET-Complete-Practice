using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Protobuf.WellKnownTypes;

namespace ServerApp.Resources.Converter
{
    public class TimestampConverter : JsonConverter<Timestamp>
    {
        public override Timestamp Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();

            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new JsonException("Date value cannot be null or empty");
            }

            var date = DateTime.ParseExact(dateString,"dd-MM-yyyy",CultureInfo.InvariantCulture);

            return Timestamp.FromDateTime(date.ToUniversalTime());
        }

        public override void Write(Utf8JsonWriter writer,Timestamp value,JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToDateTime().ToString("dd-MM-yyyy"));
        }
    }
}

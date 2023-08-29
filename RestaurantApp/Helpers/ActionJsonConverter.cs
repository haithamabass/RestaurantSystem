using System.Text.Json;
using System.Text.Json.Serialization;


namespace RestaurantApp.Helpers
{
    public class ActionJsonConverter : JsonConverter<Action>
    {
        public override Action Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the method name from the JSON.
            string methodName = reader.GetString();

            Action delegateInstance = new Action(() => { });

            // Set the delegate's method name.
            //delegateInstance.Method = methodName;

            return delegateInstance;
        }

        public override void Write(Utf8JsonWriter writer, Action value, JsonSerializerOptions options)
        {
            // Get the method name from the delegate instance.
            string methodName = value.Method.Name;

            // Write the method name to the JSON.
            writer.WriteStringValue(methodName);
        }
    }
}

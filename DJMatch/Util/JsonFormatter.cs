using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DJMatch.Util
{
    public class JsonNetFormatter : MediaTypeFormatter
   {
       private JsonSerializerSettings _jsonSerializerSettings;
    
       public JsonNetFormatter(JsonSerializerSettings jsonSerializerSettings)
       {
           _jsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings();
    
           // Fill out the mediatype and encoding we support
           SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
           Encoding = new UTF8Encoding(false, true);
       }

        public UTF8Encoding Encoding { get; private set; }

        public override bool CanReadType(Type type)
       {
    
           return true;
       }
    
       public override bool CanWriteType(Type type)
       {
           return true;
       }
         
       // public Task<object> OnReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext)
       //{
       //    // Create a serializer
       //    JsonSerializer serializer = JsonSerializer.Create(_jsonSerializerSettings);
    
       //    // Create task reading the content
       //    return Task.Factory.StartNew(() =>
       //    {
       //        using (StreamReader streamReader = new StreamReader(stream, Encoding))
       //        {
       //            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
       //            {
       //                return serializer.Deserialize(jsonTextReader, type);
       //            }
       //        }
       //    });
       //}
    
       //protected Task OnWriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext, TransportContext transportContext)
       //{
       //    // Create a serializer
       //    JsonSerializer serializer = JsonSerializer.Create(_jsonSerializerSettings);
    
       //    // Create task writing the serialized content
       //    return Task.Factory.StartNew(() =>
       //    {
       //        using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(stream, Encoding)) { CloseOutput = false })
       //        {
       //            serializer.Serialize(jsonTextWriter, value);
       //            jsonTextWriter.Flush();
       //        }
       //    });
       //}
   }
}
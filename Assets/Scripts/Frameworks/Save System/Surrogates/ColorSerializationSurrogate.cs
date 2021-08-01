using System.Runtime.Serialization;
using UnityEngine;

namespace Ninito.UsualSuspects.SaveSystem.Surrogates
{
    public class ColorSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var color = (Color) obj;

            info.AddValue(name: "r", value: color.r);
            info.AddValue(name: "g", value: color.g);
            info.AddValue(name: "b", value: color.b);
            info.AddValue(name: "a", value: color.a);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var color = (Color) obj;

            color.r = (float) info.GetValue(name: "r", type: typeof(float));
            color.g = (float) info.GetValue(name: "g", type: typeof(float));
            color.b = (float) info.GetValue(name: "b", type: typeof(float));
            color.a = (float) info.GetValue(name: "a", type: typeof(float));

            obj = color;
            return obj;
        }
    }
}
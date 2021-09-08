using System.Runtime.Serialization;
using UnityEngine;

namespace Ninito.UsualSuspects.SaveSystem.Surrogates
{
    public class Vector2SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector2 = (Vector2) obj;
            info.AddValue(name: "x", value: vector2.x);
            info.AddValue(name: "y", value: vector2.y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var vector2 = (Vector2) obj;

            vector2.x = (float) info.GetValue(name: "x", type: typeof(float));
            vector2.y = (float) info.GetValue(name: "y", type: typeof(float));

            obj = vector2;
            return obj;
        }
    }
}
using System.Runtime.Serialization;
using UnityEngine;

namespace ManyTools.UnityExtended.SaveSystem.Surrogates
{
    public class Vector4SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector4) obj;
            info.AddValue(name: "x", value: vector3.x);
            info.AddValue(name: "y", value: vector3.y);
            info.AddValue(name: "z", value: vector3.z);
            info.AddValue(name: "w", value: vector3.w);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var vector4 = (Vector4) obj;

            vector4.x = (float) info.GetValue(name: "x", type: typeof(float));
            vector4.y = (float) info.GetValue(name: "y", type: typeof(float));
            vector4.z = (float) info.GetValue(name: "z", type: typeof(float));
            vector4.w = (float) info.GetValue(name: "w", type: typeof(float));

            obj = vector4;
            return obj;
        }
    }
}
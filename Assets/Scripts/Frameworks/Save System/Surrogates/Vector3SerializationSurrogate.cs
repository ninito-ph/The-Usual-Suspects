using System.Runtime.Serialization;
using UnityEngine;

namespace ManyTools.UnityExtended.SaveSystem.Surrogates
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector3) obj;

            info.AddValue(name: "x", value: vector3.x);
            info.AddValue(name: "y", value: vector3.y);
            info.AddValue(name: "z", value: vector3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var vector3 = (Vector3) obj;

            vector3.x = (float) info.GetValue(name: "x", type: typeof(float));
            vector3.y = (float) info.GetValue(name: "y", type: typeof(float));
            vector3.z = (float) info.GetValue(name: "z", type: typeof(float));

            obj = vector3;
            return obj;
        }
    }
}
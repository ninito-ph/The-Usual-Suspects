using System.Runtime.Serialization;
using UnityEngine;

namespace Ninito.UsualSuspects.SaveSystem.Surrogates
{
    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var quaternion = (Quaternion) obj;

            info.AddValue(name: "x", value: quaternion.x);
            info.AddValue(name: "y", value: quaternion.y);
            info.AddValue(name: "z", value: quaternion.z);
            info.AddValue(name: "w", value: quaternion.w);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            var quaternion = (Quaternion) obj;

            quaternion.x = (float) info.GetValue(name: "x", type: typeof(float));
            quaternion.y = (float) info.GetValue(name: "y", type: typeof(float));
            quaternion.z = (float) info.GetValue(name: "z", type: typeof(float));
            quaternion.w = (float) info.GetValue(name: "w", type: typeof(float));

            obj = quaternion;
            return obj;
        }
    }
}
using UnityEngine;

namespace com.daxode.imgui
{
    struct UnityObjRef<T> where T : UnityEngine.Object
    {
        public int InstanceID;
        public T Value => (T)Resources.InstanceIDToObject(InstanceID);
        public static implicit operator T(UnityObjRef<T> r) => r.Value;
        public static implicit operator UnityObjRef<T>(T v) => new() { InstanceID = v.GetInstanceID() };
    }
}
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace com.daxode.imgui
{
    public struct UnityObjRef<T> where T : UnityEngine.Object
    {
        System.IntPtr m_Data;
        public T Value => (T)Resources.InstanceIDToObject(UnsafeUtility.As<System.IntPtr, int>(ref m_Data));
        public static implicit operator T(UnityObjRef<T> r) => r.Value;
        public static implicit operator UnityObjRef<T>(T v)
        {
            var instanceID = v.GetInstanceID();
            return new UnityObjRef<T> { m_Data = UnsafeUtility.As<int, System.IntPtr>(ref instanceID) };
        }
        
        public bool IsValid => m_Data != System.IntPtr.Zero;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Huatuo
{
    public enum ByteEnum : byte
    {
    }
    
    public enum IntEnum : byte
    {
    }

    public static class HuatuoHelper
    {
        [DllImport("GameAssembly.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int il2cpp_huatuo_register_valuetype_shared_inst(IntPtr obj);

        private static List<Type> RefTypes()
        {
            var types = new List<Type>
            {
                typeof(Dictionary<_shared_size_2, ByteEnum>)
            };

            return types;
        }

        public static void Init()
        {
            Debug.Log("Huatuo.HuatuoHelper.Init Start");
            var bytes = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "Assembly-CSharp.dll"));
            Assembly.Load(bytes);
            Debug.Log("Huatuo.HuatuoHelper.Init End");

            foreach (var type in shared_value_types.types) RegisterValuetypeSharedInst(type);
        }

        private static void RegisterValuetypeSharedInst(Type t)
        {
            var size = il2cpp_huatuo_register_valuetype_shared_inst(t.TypeHandle.Value);
            Debug.Log(t + ": " + size);
        }
    }
}
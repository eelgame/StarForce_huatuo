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
        [DllImport("GameAssembly.dll", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "il2cpp_huatuo_register_valuetype_shared_inst")]
        private static extern int il2cpp_huatuo_register_valuetype_shared_inst_win(IntPtr obj);


        [DllImport("libil2cpp.so", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "il2cpp_huatuo_register_valuetype_shared_inst")]
        private static extern int il2cpp_huatuo_register_valuetype_shared_inst_android(IntPtr obj);

        private static List<Type> RefTypes()
        {
            var types = new List<Type>
            {
                typeof(Dictionary<_shared_size_2, ByteEnum>)
            };

            return types;
        }

        public static int Init()
        {
            // return 0; // aot模式运行
            Debug.Log("Huatuo.HuatuoHelper.Init Start");
            
            BetterStreamingAssets.Initialize();

            var bytes = BetterStreamingAssets.ReadAllBytes("Assembly-CSharp.dll");

            Assembly.Load(bytes);
            Debug.Log("Huatuo.HuatuoHelper.Init End");

            foreach (var type in shared_value_types.types) RegisterValuetypeSharedInst(type);

            return 1; // 热更模式运行
        }

        private static void RegisterValuetypeSharedInst(Type t)
        {
            var size = 0;
            if (Application.platform == RuntimePlatform.Android)
                size = il2cpp_huatuo_register_valuetype_shared_inst_android(t.TypeHandle.Value);
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
                size = il2cpp_huatuo_register_valuetype_shared_inst_win(t.TypeHandle.Value);
            else
                throw new NotSupportedException();
            Debug.Log(t + ": " + size);
        }
    }
}
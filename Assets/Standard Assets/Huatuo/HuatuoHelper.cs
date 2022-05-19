using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Huatuo
{
    public interface IInterpreterValueType
    {
        
    }

    public enum ByteEnum : byte
    {
    }

    public enum IntEnum : byte
    {
    }

    public static class HuatuoHelper
    {

        public static bool IsInterpreterType(this Type t)
        {
            return t.Module.MetadataToken >= 1 << 26;
        }

        private static List<Type> RefTypes()
        {
            var types = new List<Type>
            {
                typeof(Dictionary<_shared_size_2, ByteEnum>),
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
            var size = IL2CPP.il2cpp_huatuo_register_valuetype_shared_inst(t.TypeHandle.Value);
            Debug.Log(t + ": " + size);
        }
    }
}
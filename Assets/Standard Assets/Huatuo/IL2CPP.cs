using System;
using System.Runtime.InteropServices;

namespace Huatuo
{
    public class IL2CPP
    {
#if UNITY_ANDROID
        public const string DLLNAME = "libil2cpp.so";
#else
        public const string DLLNAME = "GameAssembly.dll";
#endif

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int il2cpp_huatuo_register_valuetype_shared_inst(IntPtr obj);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr il2cpp_class_get_method_ptr_from_name(IntPtr type,
            [MarshalAs(UnmanagedType.LPStr)] string name, int argsCount);
    }
}
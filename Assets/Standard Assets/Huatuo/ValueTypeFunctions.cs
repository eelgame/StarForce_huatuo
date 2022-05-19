using System;

namespace Huatuo
{
    public delegate bool Equals(object obj);
    public delegate int GetHashCode();
    public delegate string ToString();
    public class ValueTypeFunctions
    {
        public IntPtr _huatuo_Equals_ftn_;
        public IntPtr _huatuo_GetHashCode_ftn_;
        public IntPtr _huatuo_ToString_ftn_;

        public static ValueTypeFunctions Setup(Type type)
        {
            if (type.IsInterpreterType())
            {
                var typeHandle = type.TypeHandle;
                var functions = new ValueTypeFunctions();
                functions._huatuo_Equals_ftn_ =
                    IL2CPP.il2cpp_class_get_method_ptr_from_name(typeHandle.Value, "Equals", 1);
                functions._huatuo_GetHashCode_ftn_ =
                    IL2CPP.il2cpp_class_get_method_ptr_from_name(typeHandle.Value, "GetHashCode", 0);
                functions._huatuo_ToString_ftn_ =
                    IL2CPP.il2cpp_class_get_method_ptr_from_name(typeHandle.Value, "ToString", 0);

                return functions;
            }

            return null;
        }
    }
}
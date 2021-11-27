using System.Globalization;

namespace SharpMenu.NativeHelpers
{
    internal unsafe class Api
    {
        private static delegate* unmanaged<FunctionIndex, void*> get_function_pointer;

        internal static void Init(string[] args)
        {
            var get_function_pointerString = args[0];
            get_function_pointer = (delegate* unmanaged<FunctionIndex, void*>)Convert.ToUInt64(get_function_pointerString, CultureInfo.InvariantCulture);
            GetFunctionPointers();
        }

        private static void GetFunctionPointers()
        {
            typeid_hash_code = (delegate* unmanaged<nuint, nuint>)get_function_pointer(FunctionIndex.typeid_hash_code);
            get_random_class_ptr = (delegate* unmanaged<nuint>)get_function_pointer(FunctionIndex.get_random_class_ptr);

            var class_ptr = get_random_class_ptr();

            var class_hash_code = typeid_hash_code(class_ptr);

            Console.WriteLine(class_hash_code);
        }

        private enum FunctionIndex
        {
            typeid_hash_code,
            get_random_class_ptr
        }

        internal static delegate* unmanaged<nuint, nuint> typeid_hash_code;
        internal static delegate* unmanaged<nuint> get_random_class_ptr;
    }
}

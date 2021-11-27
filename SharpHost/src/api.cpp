#include "api.hpp"

namespace sharp_host::api
{
    void* get_function_pointer(FunctionIndex api_enum)
    {
        switch (api_enum)
        {
        case FunctionIndex::typeid_hash_code:
            return &api_typeid_hash_code;
            break;
        case FunctionIndex::get_random_class_ptr:
            return &api_get_random_class_ptr;
            break;
        }

        return nullptr;
    }

    size_t api_typeid_hash_code(rtti_dummy* class_ptr)
    {
        return typeid(*class_ptr).hash_code();
    }

    uintptr_t api_get_random_class_ptr()
    {
        return (uintptr_t)new random_class();
    }
}

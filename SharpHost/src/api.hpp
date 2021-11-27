#pragma once

namespace sharp_host::api
{
    enum FunctionIndex
    {
        typeid_hash_code,
        get_random_class_ptr
    };

    void* get_function_pointer(FunctionIndex api_enum);

    class rtti_dummy
    {
    public:
        virtual void do_nothing() { }
    };
    size_t api_typeid_hash_code(rtti_dummy* class_ptr);

    class random_class
    {
    public:
        virtual void do_aaaaa() { }
    };
    uintptr_t api_get_random_class_ptr();
}

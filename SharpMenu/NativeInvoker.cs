using SharpMenu.Rage.Natives;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct NativeCallContext
    {
        void* ReturnValue;
        uint ArgCount;
        void* Args;
        int DataCount;
        fixed uint Data[48];

        fixed ulong ReturnStack[10];
        fixed ulong ArgStack[100];

        public NativeCallContext()
        {
            fixed (void* returnValuePtr = &ReturnStack[0])
                ReturnValue = returnValuePtr;

            fixed (void* argsPtr = &ArgStack[0])
                Args = argsPtr;

            DataCount = 0;
            ArgCount = 0;
        }

        internal void Reset()
        {
            ArgCount = 0;
            DataCount = 0;
        }

        internal void PushArg<T>(T arg)
            where T : unmanaged
        {
            UInt64* _args = (UInt64*)Args;
            UInt64* ptrToArgInArray = _args + ArgCount++;
            *ptrToArgInArray = Unsafe.As<T, UInt64>(ref arg);
        }

        internal T* GetReturnValue<T>()
            where T : unmanaged
        {
            return (T*)ReturnValue;
        }

        internal static delegate* unmanaged<NativeCallContext*, void> FixVectors;
    }

    internal static unsafe class NativeInvoker
    {
        private static NativeCallContext _callContext;

        private static unsafe readonly Dictionary<scrNativeHash, UIntPtr> NativeHashToNativeHandler = new();

        internal static void CacheHandlers()
        {
            foreach (var (oldHash, newHash) in Crossmap.OldToNewNativeHash)
            {
                var handler = scrNativeRegistrationTable.GetNativeHandler(newHash);

                NativeHashToNativeHandler.Add(oldHash, (UIntPtr)handler);
            }
        }

        private static void BeginCall()
        {
            _callContext.Reset();
        }

        private static void EndCall(ulong hash)
        {
            if (NativeHashToNativeHandler.TryGetValue(hash, out var nativeHandler))
            {
                fixed (NativeCallContext* callContext = &_callContext)
                {
                    ReturnAddressSpoofer.Spoof(callContext, (void*)nativeHandler);

                    NativeCallContext.FixVectors(callContext);
                }
            }
        }

        private static void PushArg<T>(T arg)
            where T : unmanaged
        {
            _callContext.PushArg(arg);
        }

        private static T GetReturnValue<T>()
            where T : unmanaged
        {
            return *_callContext.GetReturnValue<T>();
        }

        internal static void Invoke<T1>(scrNativeHash hash, T1 arg1)
            where T1 : unmanaged
        {
            BeginCall();

            PushArg(arg1);

            EndCall(hash);
        }

        internal static void Invoke<T1, T2>(scrNativeHash hash, T1 arg1, T2 arg2)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);

            EndCall(hash);
        }

        internal static void Invoke<T1, T2, T3>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);

            EndCall(hash);
        }

        internal static void Invoke<T1, T2, T3, T4>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);

            EndCall(hash);
        }

        internal static void Invoke<T1, T2, T3, T4, T5>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);

            EndCall(hash);
        }

        internal static void Invoke<T1, T2, T3, T4, T5, T6>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);

            EndCall(hash);
        }

        internal static TReturn Invoke<TReturn, T1>(scrNativeHash hash, T1 arg1)
            where TReturn : unmanaged
            where T1 : unmanaged
        {
            Invoke(hash, arg1);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn, T1, T2>(scrNativeHash hash, T1 arg1, T2 arg2)
            where TReturn : unmanaged
            where T1 : unmanaged
            where T2 : unmanaged
        {
            Invoke(hash, arg1, arg2);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn, T1, T2, T3>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3)
            where TReturn : unmanaged
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn, T1, T2, T3, T4>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TReturn : unmanaged
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn, T1, T2, T3, T4, T5>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TReturn : unmanaged
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn, T1, T2, T3, T4, T5, T6>(scrNativeHash hash, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            where TReturn : unmanaged
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6);

            return GetReturnValue<TReturn>();
        }
    }
}

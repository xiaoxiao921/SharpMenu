using SharpMenu.Rage.Natives;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpMenu
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct NativeCallContext
    {
        [FieldOffset(0)]
        void* ReturnValue;

        [FieldOffset(8)]
        uint ArgCount;

        [FieldOffset(16)]
        void* Args;

        [FieldOffset(24)]
        int DataCount;

        [FieldOffset(28)]
        fixed uint Data[48];

        public NativeCallContext()
        {
            ReturnValue = NativeMemory.Alloc(8 * 10);
            Args = NativeMemory.Alloc(8 * 100);

            DataCount = 0;
            ArgCount = 0;
        }

        internal void Reset()
        {
            DataCount = 0;
            ArgCount = 0;
        }

        internal void PushArg<T>(T arg)
            where T : unmanaged
        {
            ulong* _args = (ulong*)Args;
            ulong* ptrToArgInArray = _args + ArgCount++;
            *ptrToArgInArray = Unsafe.As<T, ulong>(ref arg);
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
        private static NativeCallContext _callContext = new();

        private static unsafe readonly Dictionary<scrNativeHash, UIntPtr> OldNativeHashToNativeHandler = new();

        internal static void CacheHandlers()
        {
            int i = 0;
            foreach (var (oldHash, newHash) in Crossmap.OldToNewNativeHash)
            {
                //Log.Info($"{oldHash} -> {newHash}");

                i++;
                if (i == 7000)
                    break;

                if (!OldNativeHashToNativeHandler.ContainsKey(oldHash))
                    OldNativeHashToNativeHandler.Add(oldHash, (UIntPtr)scrNativeRegistrationTable.GetNativeHandler(newHash));
            }
        }

        private static void BeginCall()
        {
            _callContext.Reset();
        }

        private static void EndCall(ulong hash)
        {
            if (OldNativeHashToNativeHandler.TryGetValue(hash, out var nativeHandler))
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

        internal static void Invoke(scrNativeHash hash)
        {
            BeginCall();

            EndCall(hash);
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

        internal static TReturn Invoke<TReturn>(scrNativeHash hash)
            where TReturn : unmanaged
        {
            Invoke(hash);

            return GetReturnValue<TReturn>();
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

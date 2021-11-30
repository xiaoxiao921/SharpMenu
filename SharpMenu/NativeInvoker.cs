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

        internal static void Invoke(scrNativeHash hash, ulong arg1)
        {
            BeginCall();

            PushArg(arg1);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6)
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

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);
            PushArg(arg28);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);
            PushArg(arg28);
            PushArg(arg29);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);
            PushArg(arg28);
            PushArg(arg29);
            PushArg(arg30);

            EndCall(hash);
        }

        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30, ulong arg31)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);
            PushArg(arg28);
            PushArg(arg29);
            PushArg(arg30);
            PushArg(arg31);

            EndCall(hash);
        }


        internal static void Invoke(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30, ulong arg31, ulong arg32)
        {
            BeginCall();

            PushArg(arg1);
            PushArg(arg2);
            PushArg(arg3);
            PushArg(arg4);
            PushArg(arg5);
            PushArg(arg6);
            PushArg(arg7);
            PushArg(arg8);
            PushArg(arg9);
            PushArg(arg10);
            PushArg(arg11);
            PushArg(arg12);
            PushArg(arg13);
            PushArg(arg14);
            PushArg(arg15);
            PushArg(arg16);
            PushArg(arg17);
            PushArg(arg18);
            PushArg(arg19);
            PushArg(arg20);
            PushArg(arg21);
            PushArg(arg22);
            PushArg(arg23);
            PushArg(arg24);
            PushArg(arg25);
            PushArg(arg26);
            PushArg(arg27);
            PushArg(arg28);
            PushArg(arg29);
            PushArg(arg30);
            PushArg(arg31);
            PushArg(arg32);

            EndCall(hash);
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash)
            where TReturn : unmanaged
        {
            Invoke(hash);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27, arg28);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27, arg28, arg29);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27, arg28, arg29, arg30);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30, ulong arg31)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27, arg28, arg29, arg30, arg31);

            return GetReturnValue<TReturn>();
        }

        internal static TReturn Invoke<TReturn>(scrNativeHash hash, ulong arg1, ulong arg2, ulong arg3, ulong arg4, ulong arg5, ulong arg6, ulong arg7, ulong arg8, ulong arg9, ulong arg10, ulong arg11, ulong arg12, ulong arg13, ulong arg14, ulong arg15, ulong arg16, ulong arg17, ulong arg18, ulong arg19, ulong arg20, ulong arg21, ulong arg22, ulong arg23, ulong arg24, ulong arg25, ulong arg26, ulong arg27, ulong arg28, ulong arg29, ulong arg30, ulong arg31, ulong arg32)
            where TReturn : unmanaged
        {
            Invoke(hash, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, arg17, arg18, arg19, arg20, arg21, arg22, arg23, arg24, arg25, arg26, arg27, arg28, arg29, arg30, arg31, arg32);

            return GetReturnValue<TReturn>();
        }
    }
}

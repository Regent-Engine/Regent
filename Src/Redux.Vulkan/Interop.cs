using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Vulkan
{
    public static unsafe class Interop
    {
        public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr pointer)
        {
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(pointer);
        }

        public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate @delegate)
        {
            return Marshal.GetFunctionPointerForDelegate(@delegate);
        }

        public static unsafe string GetString(byte* ptr)
        {
            return Vortice.Vulkan.Interop.GetString(ptr);
        }

        public static int GetMaxByteCount(string value)
        {
            return value is null ? 0 : Encoding.UTF8.GetMaxByteCount(value.Length + 1);
        }

        public static IntPtr Alloc(int byteCount)
        {
            if (byteCount is 0)
                return IntPtr.Zero;

            return Marshal.AllocHGlobal(byteCount);
        }


        public static IntPtr AllocToPointer(string value)
        {
            if (value is null)
            {
                return IntPtr.Zero;
            }

            // Get max number of bytes the string may need.
            int maxSize = GetMaxByteCount(value);

            // Allocate unmanaged memory.
            IntPtr managedPtr = Alloc(maxSize);
            byte* ptr = (byte*)managedPtr;

            // Encode to utf-8, null-terminate and write to unmanaged memory.
            int actualNumberOfBytesWritten;
            fixed (char* ch = value)
            {
                actualNumberOfBytesWritten = Encoding.UTF8.GetBytes(ch, value.Length, ptr, maxSize);
            }

            ptr[actualNumberOfBytesWritten] = 0;

            // Return pointer to the beginning of unmanaged memory.
            return managedPtr;
        }


        public static unsafe byte* ToPointer(string value)
        {
            return (byte*)(void*)AllocToPointer(value);
        }
    }
}

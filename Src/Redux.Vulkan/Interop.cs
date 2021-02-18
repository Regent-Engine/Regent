/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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




        public static void Read<T>(IntPtr srcPointer, ref T value)
        {
            Unsafe.Copy(ref value, srcPointer.ToPointer());
        }

        public static void Write<T>(IntPtr dstPointer, ref T value)
        {
            Unsafe.Copy(dstPointer.ToPointer(), ref value);
        }

        public static void Write<T>(void* dstPointer, ref T value)
        {
            Unsafe.Copy(dstPointer, ref value);
        }

        public static void CopyBlocks<T>(void* dstPointer, T[] values)
        {
            if (values is null || values.Length is 0)
            {
                return;
            }

            int stride = SizeOf<T>();
            uint size = (uint)(stride * values.Length);
            void* srcPtr = AsPointer(ref values[0]);
            Unsafe.CopyBlock(dstPointer, srcPtr, size);
        }

        public static int SizeOf<T>(params T[] values)
        {
            return Unsafe.SizeOf<T>() * values.Length;
        }

        public static int SizeOf<T>()
        {
            return Unsafe.SizeOf<T>();
        }


        public static void* AsPointer<T>(ref T value)
        {
            return Unsafe.AsPointer(ref value);
        }
        public static void Write<T>(IntPtr dstPointer, T[] values)
        {
            if (values is null || values.Length is 0)
            {
                return;
            }

            int stride = SizeOf<T>();
            uint size = (uint)(stride * values.Length);
            void* srcPtr = Unsafe.AsPointer(ref values[0]);
            Unsafe.CopyBlock(dstPointer.ToPointer(), srcPtr, size);
        }


        public static void Read<T>(IntPtr srcPointer, T[] values)
        {
            int stride = SizeOf<T>();
            long size = stride * values.Length;
            void* dstPtr = Unsafe.AsPointer(ref values[0]);
            System.Buffer.MemoryCopy(srcPointer.ToPointer(), dstPtr, size, size);
        }

        public static void CopyMemory(object uploadMemory, IntPtr dataPointer, object sizeInBytes)
        {
            throw new NotImplementedException();
        }
    }
}

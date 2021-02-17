using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Regent.Vulkan
{
    public static class Interop
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
    }
}

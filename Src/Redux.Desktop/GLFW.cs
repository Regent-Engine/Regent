using Silk.NET.GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Desktop
{
	internal static unsafe partial class GLFW
	{

		internal delegate IntPtr glfwGetWin32Window(WindowHandle* window);
		internal delegate IntPtr glfwGetCocoaWindow(WindowHandle* window);

		internal delegate IntPtr glfwGetX11Window(WindowHandle* window);
		internal delegate IntPtr glfwGetX11Display();

		internal delegate IntPtr glfwGetWaylandWindow(WindowHandle* window);
		internal delegate IntPtr glfwGetWaylandDisplay();

	}
}

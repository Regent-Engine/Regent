﻿/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Vortice.Vulkan;
using static Vortice.Vulkan.Vulkan;
using static Vortice.Vulkan.VkUtils;
using Redux.Core;
using Vortice.Mathematics;

namespace Redux.Vulkan
{
    // Provided by VK_KHR_xlib_surface
    internal unsafe struct VkXlibSurfaceCreateInfoKHR
    {
        internal VkStructureType sType;
        internal void* pNext;
        internal uint flags;
        internal IntPtr* display;
        internal IntPtr window;
    }


    internal struct wl_display { }
    internal struct wl_surface { }

    internal unsafe struct VkWaylandSurfaceCreateInfoKHR
    {
        internal VkStructureType sType;
        internal void* pNext;
        internal uint flags;
        internal wl_display* display;
        internal wl_surface* surface;
    }

    public unsafe class SwapChain : GraphicsResource, IDisposable
    {
        private delegate VkResult PFN_vkCreateWin32SurfaceKHRDelegate(VkInstance instance, VkWin32SurfaceCreateInfoKHR* createInfo, VkAllocationCallbacks* allocator, VkSurfaceKHR* surface);
        private delegate VkResult PFN_vkCreateXlibSurfaceKHR(VkInstance instance, VkXlibSurfaceCreateInfoKHR* pCreateInfo, VkAllocationCallbacks* pAllocator, VkSurfaceKHR* pSurface);
        private delegate VkResult PFN_vkCreateWaylandSurfaceKHR(VkInstance instance, VkWaylandSurfaceCreateInfoKHR* pCreateInfo, VkAllocationCallbacks* pAllocator, VkSurfaceKHR* pSurface);

        internal VkSurfaceKHR surface;
        internal VkFormat color_format;
        internal VkColorSpaceKHR color_space;
        internal VkSwapchainKHR handle;
        internal VkImage[] images;
        internal VkImageView[] swapChain_image_views;





        public SwapChain(Device device, PresentationParameters parameters) : base(device)
        {
            Parameters = parameters;

            SwapchainSource = parameters.SwapchainSource;

            surface = CreateSurface();

            CreateSwapChain();


            CreateBackBuffers();


            DepthStencil = new Image(device, new ImageDescription
            {
                Flags = TextureFlags.DepthStencil,
                Usage = GraphicsResourceUsage.Default,
                Width = Parameters.Width,
                Height = Parameters.Height,
            });
        }

        public PresentationParameters Parameters { get; set; }
        public PixelFormat ColorFormat { get; private set; }
        public Image DepthStencil { get; private set; }
        public SwapchainSource SwapchainSource { get; set; }



        private unsafe void CreateBackBuffers()
        {

            swapChain_image_views = new VkImageView[images.Length];

            for (int i = 0; i < images.Length; i++)
            {

                VkImageViewCreateInfo image_view_info = new VkImageViewCreateInfo()
                {
                    sType = VkStructureType.ImageViewCreateInfo,
                    pNext = null,
                    flags = VkImageViewCreateFlags.None,
                    components = VkComponentMapping.Identity, // TODO: VkComponentMapping
                    image = images[i],
                    viewType = VkImageViewType.Image2D,
                    format = color_format,
                    subresourceRange = new VkImageSubresourceRange()
                    {
                        aspectMask = VkImageAspectFlags.Color,
                        baseMipLevel = 0,
                        levelCount = 1,
                        baseArrayLayer = 0,
                        layerCount = 1,
                    }
                };

                vkCreateImageView(NativeDevice.handle, &image_view_info, null, out swapChain_image_views[i]);
            }
        }



        internal VkSurfaceKHR CreateSurface()
        {
            VkInstance instance = NativeDevice.NativeAdapter.instance;
            VkSurfaceKHR surface = 0;

            PFN_vkCreateWin32SurfaceKHRDelegate vkCreateWin32SurfaceKHR = NativeDevice.GetInstanceProcAddr<PFN_vkCreateWin32SurfaceKHRDelegate>("vkCreateWin32SurfaceKHR");
            PFN_vkCreateXlibSurfaceKHR vkCreateXlibSurfaceKHR = NativeDevice.GetInstanceProcAddr<PFN_vkCreateXlibSurfaceKHR>("vkCreateXlibSurfaceKHR");
            PFN_vkCreateWaylandSurfaceKHR vkCreateWaylandSurfaceKHR = NativeDevice.GetInstanceProcAddr<PFN_vkCreateWaylandSurfaceKHR>("vkCreateWaylandSurfaceKHR");


            //if (SwapchainSource is WindowSwapchainSource sourcewin)
            //{
            //    surface = new VkSurfaceKHR(sourcewin.Surface);
            //}


            if (SwapchainSource is Win32SwapchainSource sourcewin32 && NativeDevice.NativeAdapter.SupportsWin32Surface)
            {
                VkWin32SurfaceCreateInfoKHR Win32SurfaceCreateInfo = new()
                {
                    sType = VkStructureType.Win32SurfaceCreateInfoKHR,
                    pNext = null,
                    flags = VkWin32SurfaceCreateFlagsKHR.None,
                    hinstance = sourcewin32.Hinstance,
                    hwnd = sourcewin32.Hwnd,
                };

                vkCreateWin32SurfaceKHR(instance, &Win32SurfaceCreateInfo, null, &surface);
            }



            if (SwapchainSource is XlibSwapchainSource xlibsource) // TODO: xlibsource
            {

                VkXlibSurfaceCreateInfoKHR XlibSurfaceCreateInfo = new()
                {
                    sType = VkStructureType.XlibSurfaceCreateInfoKHR,
                    pNext = null,
                    flags = 0,
                    display = (IntPtr*)xlibsource.Display,
                    window = xlibsource.Window
                };

                vkCreateXlibSurfaceKHR(instance, &XlibSurfaceCreateInfo, null, &surface);
            }

            if (SwapchainSource is WaylandSwapchainSource Waylandsource) // TODO: CreateWayland
            {

                VkWaylandSurfaceCreateInfoKHR XlibSurfaceCreateInfo = new()
                {
                    sType = VkStructureType.WaylandSurfaceCreateInfoKHR,
                    pNext = null,
                    flags = 0,
                    display = (wl_display*)Waylandsource.Display, // TODO: wl_display ?
                    surface = (wl_surface*)Waylandsource.Surface  // TODO: wl_surface ?
                };

                vkCreateWaylandSurfaceKHR(instance, &XlibSurfaceCreateInfo, null, &surface);
            }


            return surface;


        }



        public void CreateSwapChain()
        {

            VkPhysicalDevice PhysicalDevice = NativeDevice.NativeAdapter.handle;

            int width = Parameters.Width;

            int height = Parameters.Height;

            bool vsync = false;


            // Get available queue family properties
            uint queueCount;
            vkGetPhysicalDeviceQueueFamilyProperties(PhysicalDevice, &queueCount, null);
            VkQueueFamilyProperties* queueProps = stackalloc VkQueueFamilyProperties[(int)queueCount];
            vkGetPhysicalDeviceQueueFamilyProperties(PhysicalDevice, &queueCount, queueProps);



            // Iterate over each queue to learn whether it supports presenting:
            // Find a queue with present support
            // Will be used to present the swap chain Images to the windowing system
            VkBool32* supportsPresent = stackalloc VkBool32[(int)queueCount];
            for (uint i = 0; i < queueCount; i++)
            {
                vkGetPhysicalDeviceSurfaceSupportKHR(PhysicalDevice, i, surface, out supportsPresent[i]);
            }



            // Search for a graphics and a present queue in the array of queue
            // families, try to find one that supports both
            uint graphicsQueueNodeIndex = uint.MaxValue;
            uint presentQueueNodeIndex = uint.MaxValue;


            for (uint i = 0; i < queueCount; i++)
            {
                if ((queueProps[i].queueFlags & VkQueueFlags.Graphics) != 0)
                {
                    if (graphicsQueueNodeIndex is uint.MaxValue)
                    {
                        graphicsQueueNodeIndex = i;
                    }

                    if (supportsPresent[i] == true)
                    {
                        graphicsQueueNodeIndex = i;
                        presentQueueNodeIndex = i;
                        break;
                    }
                }
            }

            if (presentQueueNodeIndex is uint.MaxValue)
            {
                // If there's no queue that supports both present and graphics
                // try to find a separate present queue
                for (uint i = 0; i < queueCount; ++i)
                {
                    if (supportsPresent[i] == true)
                    {
                        presentQueueNodeIndex = i;
                        break;
                    }
                }
            }

            // Exit if either a graphics or a presenting queue hasn't been found
            if (graphicsQueueNodeIndex is uint.MaxValue || presentQueueNodeIndex is uint.MaxValue)
            {
                throw new InvalidOperationException("Could not find a graphics and/or presenting queue!");
            }


            // TODO : Add support for separate graphics and presenting queue
            if (graphicsQueueNodeIndex != presentQueueNodeIndex)
            {
                throw new InvalidOperationException("Separate graphics and presenting queues are not supported yet!");
            }




            // Get list of supported Surface formats
            uint formatCount;
            vkGetPhysicalDeviceSurfaceFormatsKHR(PhysicalDevice, surface, &formatCount, null);
            VkSurfaceFormatKHR* surfaceFormats = stackalloc VkSurfaceFormatKHR[(int)formatCount];
            vkGetPhysicalDeviceSurfaceFormatsKHR(PhysicalDevice, surface, &formatCount, surfaceFormats);



            // If the Surface format list only includes one entry with VK_FORMAT_UNDEFINED,
            // there is no preferered format, so we assume VK_FORMAT_B8G8R8A8_UNORM
            if ((formatCount is 1) && (surfaceFormats[0].format is VkFormat.Undefined))
            {
                color_format = VkFormat.B8G8R8A8UNorm;
                color_space = surfaceFormats[0].colorSpace;
            }
            else
            {
                // iterate over the list of available Surface format and
                // check for the presence of VK_FORMAT_B8G8R8A8_UNORM
                bool found_B8G8R8A8_UNORM = false;


                // TODO: VkSurfaceFormatKHR -> stackalloc
                List<VkSurfaceFormatKHR> Formats = new List<VkSurfaceFormatKHR>();

                for (int i = 0; i < formatCount; i++)
                {
                    Formats.Add(surfaceFormats[i]);
                }

                foreach (VkSurfaceFormatKHR surfaceFormat in Formats)
                {
                    if (surfaceFormat.format is VkFormat.B8G8R8A8UNorm)
                    {
                        color_format = surfaceFormat.format;
                        color_space = surfaceFormat.colorSpace;
                        found_B8G8R8A8_UNORM = true;
                        break;
                    }
                }

                // in case VK_FORMAT_B8G8R8A8_UNORM is not available
                // select the first available color format
                if (!found_B8G8R8A8_UNORM)
                {
                    color_format = surfaceFormats[0].format;
                    color_space = surfaceFormats[0].colorSpace;
                }
            }


            // Get physical Device Surface properties and formats
            vkGetPhysicalDeviceSurfaceCapabilitiesKHR(PhysicalDevice, surface, out VkSurfaceCapabilitiesKHR surfCaps);



            // Get available present modes
            uint presentModeCount;
            vkGetPhysicalDeviceSurfacePresentModesKHR(PhysicalDevice, surface, &presentModeCount, null);
            VkPresentModeKHR* presentModes = stackalloc VkPresentModeKHR[(int)presentModeCount];
            vkGetPhysicalDeviceSurfacePresentModesKHR(PhysicalDevice, surface, &presentModeCount, presentModes);


            Size swapchainExtent = default;
            // If width (and height) equals the special value 0xFFFFFFFF, the size of the Surface will be set by the swapchain
            if (surfCaps.currentExtent.Width == unchecked(-1))
            {
                // If the Surface size is undefined, the size is set to
                // the size of the Images requested.

                swapchainExtent = new(width, height);
            }
            else
            {
                // If the Surface size is defined, the swap chain size must match
                swapchainExtent = surfCaps.currentExtent;
                width = (int)surfCaps.currentExtent.Width;
                height = (int)surfCaps.currentExtent.Height;
            }


            // Select a present mode for the swapchain

            // The VK_PRESENT_MODE_FIFO_KHR mode must always be present as per spec
            // This mode waits for the vertical blank ("v-sync")
            VkPresentModeKHR swapchainPresentMode = VkPresentModeKHR.Fifo;

            // If v-sync is not requested, try to find a mailbox mode
            // It's the lowest latency non-tearing present mode available
            if (!vsync)
            {
                for (uint i = 0; i < presentModeCount; i++)
                {
                    if (presentModes[i] is VkPresentModeKHR.Mailbox)
                    {
                        swapchainPresentMode = VkPresentModeKHR.Mailbox;
                        break;
                    }
                    if ((swapchainPresentMode is not VkPresentModeKHR.Mailbox) && (presentModes[i] is VkPresentModeKHR.Immediate))
                    {
                        swapchainPresentMode = VkPresentModeKHR.Immediate;
                    }
                }
            }

            // Determine the number of Images
            uint desiredNumberOfSwapchainImages = surfCaps.minImageCount + 1;
            if ((surfCaps.maxImageCount > 0) && (desiredNumberOfSwapchainImages > surfCaps.maxImageCount))
            {
                desiredNumberOfSwapchainImages = surfCaps.maxImageCount;
            }


            // Find the transformation of the Surface
            VkSurfaceTransformFlagsKHR preTransform;
            if ((surfCaps.supportedTransforms & VkSurfaceTransformFlagsKHR.Identity) is not 0)
            {
                // We prefer a non-rotated transform
                preTransform = VkSurfaceTransformFlagsKHR.Identity;
            }
            else
            {
                preTransform = surfCaps.currentTransform;
            }



            // Find a supported composite alpha format (not all devices support alpha opaque)
            VkCompositeAlphaFlagsKHR compositeAlpha = VkCompositeAlphaFlagsKHR.Opaque; //VK_COMPOSITE_ALPHA_OPAQUE_BIT_KHR;

            // Simply select the first composite alpha format available
            VkCompositeAlphaFlagsKHR[] compositeAlphaFlags = new[]
            {
                VkCompositeAlphaFlagsKHR.Opaque,
                VkCompositeAlphaFlagsKHR.PreMultiplied,
                VkCompositeAlphaFlagsKHR.PostMultiplied,
                VkCompositeAlphaFlagsKHR.Inherit,
            };

            foreach (VkCompositeAlphaFlagsKHR compositeAlphaFlag in compositeAlphaFlags)
            {
                if ((surfCaps.supportedCompositeAlpha & compositeAlphaFlag) is not 0)
                {
                    compositeAlpha = compositeAlphaFlag;
                    break;
                }
            }


            //uint* extent = stackalloc uint[2];
            //extent[0] = swapchainExtent.Width + 1;
            //extent[1] = swapchainExtent.Height + 1;

            //VkExtent2D imageExtent = *(VkExtent2D*)&extent;



            VkSwapchainKHR oldSwapchain = handle;

            VkSwapchainCreateInfoKHR swapchain_info = new()
            {
                sType = VkStructureType.SwapchainCreateInfoKHR,
                pNext = null,
                
                surface = surface,
                minImageCount = desiredNumberOfSwapchainImages,
                imageFormat = color_format,
                imageColorSpace = color_space,
                imageExtent = new(swapchainExtent.Width, swapchainExtent.Height)  /*imageExtent*/,

                imageUsage = VkImageUsageFlags.ColorAttachment,
                preTransform = preTransform,
                imageArrayLayers = 1,
                imageSharingMode = VkSharingMode.Exclusive,
                queueFamilyIndexCount = 0,
                pQueueFamilyIndices = null,
                presentMode = swapchainPresentMode,
                oldSwapchain = oldSwapchain,

                // Setting clipped to VK_TRUE allows the implementation to discard rendering outside of the Surface area
                clipped = true,
                compositeAlpha = compositeAlpha,
            };


            // Enable transfer source on swap chain images if supported
            if ((surfCaps.supportedUsageFlags & VkImageUsageFlags.TransferSrc) is not 0)
            {
                swapchain_info.imageUsage |= VkImageUsageFlags.TransferSrc;
            }

            // Enable transfer destination on swap chain images if supported
            if ((surfCaps.supportedUsageFlags & VkImageUsageFlags.TransferDst) is not 0)
            {
                swapchain_info.imageUsage |= VkImageUsageFlags.TransferDst;
            }



            vkCreateSwapchainKHR(NativeDevice.handle, &swapchain_info, null, out handle).CheckResult();



            // If an existing swap chain is re-created, destroy the old swap chain
            // This also cleans up all the presentable images
            if (oldSwapchain != VkSwapchainKHR.Null)
            {
                vkDestroySwapchainKHR(NativeDevice.handle, oldSwapchain, null);
            }




            uint imageCount;
            vkGetSwapchainImagesKHR(NativeDevice.handle, handle, &imageCount, null);
            images = new VkImage[imageCount];

            fixed (VkImage* images_ptr = images)
            {
                vkGetSwapchainImagesKHR(NativeDevice.handle, handle, &imageCount, images_ptr);
            }
        }




        public void Present()
        {
            VkSemaphore Semaphore = NativeDevice.render_finished_semaphore;
            VkSwapchainKHR swapchain = handle;
            CommandBuffer commandBuffer = NativeDevice.NativeCommand;
            uint imageIndex = commandBuffer.imageIndex;

            VkPresentInfoKHR present_info = new()
            {
                sType = VkStructureType.PresentInfoKHR,
                pNext = null,
                pResults = null,
                waitSemaphoreCount = 1,
                pWaitSemaphores = &Semaphore,
                swapchainCount = 1,
                pSwapchains = &swapchain,
                pImageIndices = &imageIndex /*Interop.AllocToPointer(ref commandBuffer.imageIndex)*/,
            };


            vkQueuePresentKHR(NativeDevice.command_queue, &present_info);
        }

        public void Dispose()
        {
            vkDestroySwapchainKHR(NativeDevice.handle, handle, null);
            vkDestroySurfaceKHR(NativeDevice.NativeAdapter.instance, surface, null);
        }
    }

}

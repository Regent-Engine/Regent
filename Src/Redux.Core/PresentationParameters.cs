using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Core
{
    public class PresentationParameters
    {

        public PresentationParameters(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public SwapchainSource? SwapchainSource { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}

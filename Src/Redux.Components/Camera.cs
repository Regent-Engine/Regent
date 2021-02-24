using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Components
{
    public class Camera : Component, ICamera
    {
        public double Angle
        {
            get;
            set;
        }
    }
}
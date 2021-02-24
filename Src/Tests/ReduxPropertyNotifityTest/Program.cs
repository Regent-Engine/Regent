/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using Redux;
using Redux.Data;
using System;

namespace ReduxPropertyNotifityTest
{
    class Program
    {
        static void Main(string[] args) 
        {
            TestObject obj = new();
            TestObject.TitleProperty.Changed += (s, e) =>
            {
                if (s.Equals(obj))
                {
                    Console.WriteLine($"Title Changed to {obj.Title}");
                }
            };
            while (true)
            {
                var R = new Random();

                if(R.NextDouble() >= 0.5)
                {
                    obj.Title = "title rx";
                }
                else
                {
                    obj.Title = "title random";
                }
            }
        }
    }

    public class TestObject : ReduxObject
    {
        public string Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly ReduxProperty<string> TitleProperty 
            = ReduxPropertyRegister.Register(nameof(Title), "Hello World!");
    }
}

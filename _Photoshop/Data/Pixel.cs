﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public struct Pixel
    {
        public Pixel(double red, double green, double blue)
        {
            this.b = blue;
            this.g = green;
            this.r = red;
        }
        public double Check(double value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentException();
            }
            return value;
        }
        double r;
        public double Red
        {
            get { return r; }

            set
            {
                r = Check(value);
            }

        }
        double b;
        public double Blue
        {
            get { return b; }
            set
            {
                b = Check(value);
            }
        }
        double g;
        public double Green
        {
            get { return g; }
            set
            {
                g = Check(value);
            }
        }
        /// <summary>
        /// Обрезает значение канала если значение фильтра менше 0 или больше 1
        /// </summary>
        /// <param name="value"> длина канала </param>
        /// <returns></returns>
        public static double Trim(double value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 1)
            {
                return 1;
            }
            return value;
        }

        public static Pixel operator *(Pixel pixel, double num)
        {
            return new Pixel(
                        Pixel.Trim(pixel.Red * num),
                        Pixel.Trim(pixel.Green * num),
                        Pixel.Trim(pixel.Blue * num)
                        );
        }

        public static Pixel operator *(double num, Pixel pixel)
        {
            return pixel * num;
        }
    }
}

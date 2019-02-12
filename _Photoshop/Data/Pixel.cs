using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public class Pixel
    {
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
            if (value<0)
            {
                return 0;
            }
            if (value>1)
            {
                return 1;
            }
            return value;
        }
    }
}

using System;

namespace MyPhotoshop
{
	public class Photo
	{
		public readonly int width;

        public readonly int height;
		public readonly Pixel [,] data;

        public Photo(int valWidth, int valHeight)
        {
            width = valWidth;
            height = valHeight;
            data = new Pixel[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    data[i, j] = new Pixel();
                }
            }
        }
	}
}


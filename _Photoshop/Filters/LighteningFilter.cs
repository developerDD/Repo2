using System;

namespace MyPhotoshop
{
	public class LighteningFilter : IFilter
	{
		public ParameterInfo[] GetParameters()
		{
			return new []
			{
				new ParameterInfo { Name="Коэффициент", MaxValue=10, MinValue=0, Increment=0.1, DefaultValue=1 }
				
			};
		}
		
		public override string ToString ()
		{
			return "Осветление/затемнение";
		}
		
		public Photo Process(Photo original, double[] parameters)
		{
			var result=new Photo(original.width,original.height);
			//result.width=original.width;
			//result.height=original.height;
			//result.data=new Pixel[result.width,result.height];
			
			for (int x=0;x<result.width;x++)
				for (int y=0;y<result.height;y++)
                {
                 result.data[x, y].Red = Pixel.Trim(original.data[x, y].Red * parameters[0]);
                 result.data[x, y].Blue = Pixel.Trim(original.data[x, y].Blue * parameters[0]);
                 result.data[x, y].Green = Pixel.Trim(original.data[x, y].Green * parameters[0]);
                }
						
			return result;
		}
	}
}


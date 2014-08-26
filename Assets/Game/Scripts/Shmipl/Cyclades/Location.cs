using System;
using System.Collections;
using System.Collections.Generic;

namespace Shmipl.FrmWrk.Library
{
	public struct Coords
	{
		public int x;
		public int y;

		public int width { get { return x+1; } }
		public int height { get { return y+1; } }

        public Coords(long x, long y): this ((int)x, (int)y) {
        }

        public Coords(int x, int y)
		{
			this.x = x; 
			this.y = y;
		}

		public int Sqrt()
		{
			return width * height;
		}

		public int ToIndex(int width)
		{
			return x + y * width;
		}

		public bool Contain(Coords coord)
		{
			return new Coords (0, 0) <= coord && coord <= this;
		}

		public bool Contain(int x, int y)
		{
			return Contain(new Coords(x, y));
		}

		public bool NotContain(Coords coord)
		{
			return !Contain (coord);
		}

		public bool NotContain(int x, int y)
		{
			return NotContain(new Coords(x, y));
		}

		public int Min()
		{
			return System.Math.Min (x, y);
		}

		public int Max()
		{
			return System.Math.Max (x, y);
		}

		/*static*/
		public static Coords Distance(Coords c1, Coords c2) //расстояние
		{
			return new Coords (System.Math.Abs(c1.x - c2.x), System.Math.Abs(c1.y - c2.y));
		}

		public static Coords operator-(Coords c1, Coords c2) 
		{
			return new Coords ((c1.x - c2.x), (c1.y - c2.y));
		}

		public static Coords operator+(Coords c1, Coords c2) 
		{
			return new Coords (c1.x + c2.x, c1.y + c2.y);
		}

		public static bool operator<(Coords c1, Coords c2)
		{
			return c1.x < c2.x && c1.y < c2.y;
		}

		public static bool operator>(Coords c1, Coords c2)
		{
			return c1.x > c2.x && c1.y > c2.y;
		}

		public static bool operator<=(Coords c1, Coords c2)
		{
			return c1.x <= c2.x && c1.y <=c2.y;
		}

		public static bool operator>=(Coords c1, Coords c2)
		{
			return c1.x >= c2.x && c1.y >= c2.y;
		}

		public static bool operator==(Coords c1, Coords c2)
		{
			return c1.x == c2.x && c1.y == c2.y;
		}

		public static bool operator!=(Coords c1, Coords c2)
		{
			return c1.x != c2.x && c1.y != c2.y;
		}

		public override string ToString()
		{
			return String.Format ("coords({0}, {1})", x, y);
		}

        public override int GetHashCode() {
            return x * 1000 + y; //TODO
        }

        public override bool Equals(object obj) {
            if (!(obj is Coords))
                return false;
            return x == ((Coords)obj).x && y == ((Coords)obj).y;
        }
	}

	public class Location
	{
		public Location ()
		{
		}
	}

	public class QLocation: Location 
	{
		class QLocationCoordEnumerable: IEnumerable
		{
			Coords zero;
			Coords size;
			public QLocationCoordEnumerable(Coords size)
				:this(new Coords(0, 0), size)
			{
			}

			public QLocationCoordEnumerable(Coords zero, Coords size)
			{
				this.size = size;
				this.zero = zero;
			}

			public IEnumerator GetEnumerator()
			{
				for (int y = zero.y; y <= size.y; ++y) {
					for (int x = zero.y; x <= size.x; ++x) {
						yield return new Coords (x, y);
					}
				}
			}
		}

		public static IEnumerable GetCoords(Coords size)
		{
			return new QLocationCoordEnumerable(size);
		}

		public static IEnumerable GetCoords(int size_x, int size_y)
		{
			return GetCoords (new Coords(size_x, size_y));
		}

		public static IEnumerable GetCoords(Coords zero, Coords size)
		{
			return new QLocationCoordEnumerable(zero, size);
		}

		public static IEnumerable GetCoords(int zero_x, int zero_y, int size_x, int size_y)
		{
			return GetCoords (new Coords(zero_x, zero_y), new Coords(size_x, size_y));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoWa.Game
{
	public struct Vector2
	{
		public float X;
		public float Y;

		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static Vector2 Parse(string s)
		{
			string[] splits = s.Split('/');
			if(splits.Length == 2)
			{
				float sx;
				float sy;
				if(float.TryParse(splits[0], out sx) && float.TryParse(splits[1], out sy))
				{
					return new Vector2(sx, sy);
				}
			}
			throw new Exception("Illegal string for Vector2! => " + s);
		}

		public static bool TryParse(string s, out Vector2 result)
		{
			string[] splits = s.Split('/');
			if (splits.Length == 2)
			{
				float sx;
				float sy;
				if (float.TryParse(splits[0], out sx) && float.TryParse(splits[1], out sy))
				{
					result = new Vector2(sx, sy);
					return true;
				}
			}
			result = new Vector2();
			return false;
		}
	
		public static Vector2 Compare(Vector2 v1, Vector2 v2)
		{
			Vector2 diff = new Vector2();
			diff.X = v1.X - v2.X;
			diff.Y = v1.Y - v2.Y;
			return diff;
		}

		public static float Distance(Vector2 v1, Vector2 v2)
		{
			float dx = Math.Abs(v1.X - v2.X);
			float dy = Math.Abs(v1.Y - v2.Y);
			return dx + dy;
		}
	}

	public struct Vector3
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3 Parse(string s)
		{
			string[] splits = s.Split('/');
			if (splits.Length == 3)
			{
				float sx;
				float sy;
				float sz;
				if (float.TryParse(splits[0], out sx) && float.TryParse(splits[1], out sy) & float.TryParse(splits[2], out sz))
				{
					return new Vector3(sx, sy, sz);
				}
			}
			throw new Exception("Illegal string for Vector2! => " + s);
		}

		public static bool TryParse(string s, out Vector3 result)
		{
			string[] splits = s.Split('/');
			if (splits.Length == 3)
			{
				float sx;
				float sy;
				float sz;
				if (float.TryParse(splits[0], out sx) && float.TryParse(splits[1], out sy) && float.TryParse(splits[2], out sz))
				{
					result = new Vector3(sx, sy, sz);
					return true;
				}
			}
			result = new Vector3();
			return false;
		}

		public static Vector3 Compare(Vector3 v1, Vector3 v2)
		{
			Vector3 diff = new Vector3();
			diff.X = v1.X - v2.X;
			diff.Y = v1.Y - v2.Y;
			diff.Z = v1.Z - v2.Z;
			return diff;
		}

		public static float Distance(Vector3 v1, Vector3 v2)
		{
			float dx = Math.Abs(v1.X - v2.X);
			float dy = Math.Abs(v1.Y - v2.Y);
			float dz = Math.Abs(v1.Z - v2.Z);
			return dx + dy + dz;
		}
	}
}

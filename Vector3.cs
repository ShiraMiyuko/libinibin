namespace Dargon.Plugins.InibinTroybin
{
	/// <summary>
	/// Struct containing three values
	/// </summary>
	/// <typeparam name="T">Data type</typeparam>
	public class Vector3<T>
	{
		public T X { get; set; }
		public T Y { get; set; }
		public T Z { get; set; }

		/// <summary>
		/// Struct containing three values
		/// </summary>
		/// <param name="x">First value</param>
		/// <param name="y">Second value</param>
		/// <param name="z">Third value</param>
		public Vector3(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>{X, Y, Z}</returns>
		public override string ToString()
		{
			return "{" + X + ", " + Y + ", " + Z + "}";
		}
	}
}
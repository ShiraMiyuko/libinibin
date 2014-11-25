namespace Dargon.Plugins.InibinTroybin
{
	/// <summary>
	/// Struct containing two values
	/// </summary>
	/// <typeparam name="T">Data type</typeparam>
	public class Vector2<T>
	{
		public T X { get; set; }
		public T Y { get; set; }

		/// <summary>
		/// Struct containing two values
		/// </summary>
		/// <param name="x">First value</param>
		/// <param name="y">Second value</param>
		public Vector2(T x, T y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Returns a string representation
		/// </summary>
		/// <returns>{X, Y}</returns>
		public override string ToString()
		{
			return "{" + X + ", " + Y + "}";
		}
	}
}
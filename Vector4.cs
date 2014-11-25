namespace Dargon.LeagueOfLegends {
   /// <summary>
   /// Struct containing four values
   /// </summary>
   /// <typeparam name="T">Data type</typeparam>
   public class Vector4<T> {
      public T X { get; set; }
      public T Y { get; set; }
      public T Z { get; set; }
      public T W { get; set; }

      /// <summary>
      /// Struct containing four values
      /// </summary>
      /// <param name="x">First value</param>
      /// <param name="y">Second value</param>
      /// <param name="z">Third value</param>
      /// <param name="w">Fourth value</param>
      public Vector4(T x, T y, T z, T w) {
         X = x;
         Y = y;
         Z = z;
         W = w;
      }

      /// <summary>
      /// Returns a string representation
      /// </summary>
      /// <returns>{X, Y, Z, W}</returns>
      public override string ToString() {
         return "{" + X + ", " + Y + ", " + Z + ", " + W + "}";
      }
   }
}
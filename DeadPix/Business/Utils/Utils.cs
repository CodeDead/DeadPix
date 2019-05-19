using System;
using System.Windows.Media;

namespace DeadPix.Business.Utils
{
    /// <summary>
    /// Internal sealed class that contains helper methods
    /// </summary>
    internal static class Utils
    {
        #region Variables
        private static readonly Random Rnd = new Random();
        #endregion

        /// <summary>
        /// Generate a new random color
        /// </summary>
        /// <returns>A randomly generated color</returns>
        internal static Color GenerateColor()
        {
            return Color.FromRgb((byte)Rnd.Next(1, 255), (byte)Rnd.Next(1, 255), (byte)Rnd.Next(1, 255));
        }
    }
}

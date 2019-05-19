using System.Windows.Media;

namespace DeadPix.Business.Events
{
    /// <summary>
    /// Delegate that can be called when a color has changed
    /// </summary>
    /// <param name="color">The new color</param>
    internal delegate void ColorChangedEvent(Color color);
}

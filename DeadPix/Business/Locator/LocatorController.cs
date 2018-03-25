using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DeadPix.Business.Locator
{
    internal class LocatorController
    {
        private Color _currentColor;
        private int _interval;

        internal LocatorController(int interval)
        {
            _interval = interval;
        }
    }
}

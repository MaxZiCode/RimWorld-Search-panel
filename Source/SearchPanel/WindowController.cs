using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class WindowController
    {
        private readonly Window window;

        public WindowController(Window window)
        {
            this.window = window;
        }

        public bool IsWindowOpened => Find.WindowStack.IsOpen(window.GetType());

        public void ToggleWindow()
        {
            if (!IsWindowOpened)
            {
                Find.WindowStack.Add(window);
            }
            else
            {
                Find.WindowStack.TryRemove(window, false);
            }
        }
    }
}
using System;

namespace Essentials.Events
{
    public interface IEventHandler
    {
        protected void EventHandler(object sender, EventArgs e);
    }
}

using System;

namespace Essentials.Events
{
    public interface IEventHandler
    {
        protected void Handler(object sender, EventArgs e);
    }
}

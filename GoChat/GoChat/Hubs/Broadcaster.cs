using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GoChat.Hubs
{
    public class Broadcaster : Hub<IBroadcaster>
    {

    }


    // Client side methods to be invoked by Broadcaster Hub
    public interface IBroadcaster
    {
    }
}

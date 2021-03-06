﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Hubs
{
    public class TopicHub: Hub
    {
        public void SendTopicUpdated(int topicId)
        {
            Clients.All.SendAsync("UpdateTopic", topicId);
        }
    }
}

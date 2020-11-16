using Lnrpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.ViewModels
{
    public class CloseChannelVM
    {
        public ChannelPoint ChannelPoint { get; set; }

        public string DeliveryAddress { get; set; }

        public bool IsForcedClose { get; set; }

        public long SatPerByte { get; set; }

        public int TargetConf { get; set; }
    }
}

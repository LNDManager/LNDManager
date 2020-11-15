using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.ViewModels
{
    public class OpenChannelVM
    {
        [Required]
        public string PubKey { get; set; }

        [Required]
        public long FundingAmount { get; set; }

        public int TargetConf { get; set; }

        public long Fee { get; set; }
    }
}

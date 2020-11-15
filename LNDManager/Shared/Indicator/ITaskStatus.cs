using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.Shared.Indicator
{
    public interface ITaskStatus : IDisposable
    {
        double? ProgressValue { get; set; }

        double? ProgressMax { get; set; }

        string Maintext { get; set; }

        string Subtext { get; set; }

        Exception Exception { get; set; }

        void DismissException();
    }
}

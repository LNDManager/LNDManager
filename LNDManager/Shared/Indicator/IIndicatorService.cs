using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.Shared.Indicator
{
    public interface IIndicatorService
    {
        Task StartTaskAsync(Func<ITaskStatus, Task> action, string context = "", string maintext = null, string subtext = null);

        void SubscribeToTaskProgressChanged(string context, Func<ITaskStatus, Task> action);

        void UnsubscribeFromTaskProgressChanged(string context, Func<ITaskStatus, Task> action);

        IndicatorOptions Options { get; }
    }
}

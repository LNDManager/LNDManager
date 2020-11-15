using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.Shared.Indicator
{
    public class IndicatorOptions
    {
        public Type IndicatorTemplate { get; set; } = typeof(DefaultTemplate);

        public IndicatorChildContentHideModes ChildContentHideMode { get; set; } = IndicatorChildContentHideModes.CssDisplayNone;
    }
}

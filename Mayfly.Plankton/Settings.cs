﻿using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Plankton
{
    public class Settings : Wild.Settings
    {
        public Settings()
            : base(UserSettings.ReaderSettings)
        { }

        protected override void SaveSettings()
        { }
    }
}

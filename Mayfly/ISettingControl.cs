using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly
{
    public interface ISettingControl
    {
        void LoadSettings();

        void SaveSettings();
    }
}

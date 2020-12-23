using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heart_Rate_Analyser
{
    class VerticalProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams orientation = base.CreateParams;
                orientation.Style |= 0x04;
                return orientation;
            }
        }
    }
}

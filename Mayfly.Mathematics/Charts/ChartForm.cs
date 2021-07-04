using System.Windows.Forms;

namespace Mayfly.Mathematics.Charts
{
    public partial class ChartForm : Form
    {
        #region Constructors

        public ChartForm()
        {
            InitializeComponent();
            StatChart.Clear();
        }

        public ChartForm(string xTitle, string yTitle)
        {
            InitializeComponent();

            StatChart.AxisXTitle = xTitle;
            StatChart.AxisYTitle = yTitle;
            StatChart.Clear();
        }

        public ChartForm(string xTitle) : this(xTitle, Resources.Interface.DefaultYTitle)
        {    }

        #endregion
    }
}

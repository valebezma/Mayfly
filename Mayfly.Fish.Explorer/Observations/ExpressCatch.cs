using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public partial class ExpressCatch : Form
    {
        public Wild.Survey Data { get; set; }

        public Surveys.ActionRow ActionRow { get; set; }

        public TextAndImageCell GridCell { get; set; }



        public ExpressCatch(Surveys.ActionRow actionRow)
        {
            InitializeComponent();
        
            ActionRow = actionRow;
            Data = new Data(Fish.UserSettings.SpeciesIndex, Fish.UserSettings.SamplersIndex);
            Text = String.Format("{0} - {1}", ActionRow.GetShortDescription(), Text);
        }

        public ExpressCatch(TextAndImageCell gridCell)
            : this(gridCell.Tag as Surveys.ActionRow)
        {
            this.GridCell = gridCell;
            this.SetFriendlyDesktopLocation(gridCell);
        }
    }
}

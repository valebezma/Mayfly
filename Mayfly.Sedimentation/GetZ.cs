using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Sedimentation
{
    public partial class GetZ : Form
    {
        public double Z { get; private set; }

        public double Fi
        {
            get
            {
                try { return (double)numericUpDownFi.Value; }
                catch { return double.NaN; }
            }
        }

        public double Velocity
        {
            get
            {
                try { return Convert.ToDouble(textBoxVelocity.Text); }
                catch { return double.NaN; }
            }
        }

        public double TurbulentWidth
        {
            get
            {
                try { return Convert.ToDouble(textBoxTurbulentWidth.Text); }
                catch { return double.NaN; }
            }
        }

        double Obstruction
        {
            get
            {
                return this.TurbulentWidth / this.Width;
            }
        }



        public GetZ()
        {
            InitializeComponent();
        }

        public GetZ(SedimentProject.ProjectRow project) : this()
        {
            if (!project.IsVelocityNull()) textBoxVelocity.Text = project.Velocity.ToString();
            if (!project.IsWidthNull()) textBoxWidth.Text = project.Width.ToString();
            if (!project.IsTurbulentWidthNull()) textBoxTurbulentWidth.Text = project.TurbulentWidth.ToString();
            if (!project.IsFiNull()) numericUpDownFi.Value = (decimal)project.Fi;

            UpdateValues();
        }



        private void UpdateValues()
        {
            textBoxObstruction.Text = this.Obstruction.ToString("N3");
            this.Z = 100 * Service.SoilEntrainmentNomogram(this.Velocity, this.Obstruction, this.Fi, SandSize.Fine);
            textBoxZ.Text = this.Z.ToString("N1");
        }



        private void value_Changed(object sender, EventArgs e)
        {
            UpdateValues();
        }
    }
}

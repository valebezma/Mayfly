using Meta.Numerics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mayfly.Wild.Controls
{
    public partial class SizeGroup : UserControl
    {
        /// <summary>
        /// Length interval in millimetres
        /// </summary>
        public Interval LengthInterval {
            get;
            set;
        }

        public double LeftEndpoint { get { return LengthInterval.LeftEndpoint; } }

        public double RightEndpoint { get { return LengthInterval.RightEndpoint; } }

        /// <summary>
        /// Count of fishes
        /// </summary>
        public int Count {
            get {
                return (int)Numeric.Value;
            }

            set {
                Numeric.Value = value;
            }
        }

        public new bool Enabled {
            get {
                return Numeric.Enabled;
            }

            set {
                Numeric.Enabled = value;
            }
        }

        /// <summary>
        /// Size of small ticks on scale
        /// </summary>
        public static int SmallTickSize {
            get {
                return 6;
            }
        }

        /// <summary>
        /// Size og big ticks on scale
        /// </summary>
        public static int ClassTickSize {
            get {
                return 15;
            }
        }

        /// <summary>
        /// Count of ticks on scale
        /// </summary>
        public static int Ticks {
            get {
                return 10;
            }
        }

        public override Font Font {
            set {
                label1.Font = value;
            }
        }

        /// <summary>
        /// Default size of new SizeGroup item
        /// </summary>
        internal static Size ClassSize {
            get {
                return new Size(80, 35);
            }
        }

        internal static Rectangle NumericField {
            get {
                return new Rectangle(32, 0, 47, 20);
            }
        }

        public event EventHandler ValueChanged;



        private SizeGroup() {
            InitializeComponent();
            Size = ClassSize;
        }

        public SizeGroup(double length, double interval)
            : this() {
            LengthInterval = Interval.FromEndpointAndWidth(length, interval);
        }



        public override string ToString() {
            return "Length: " + LengthInterval.ToString() + "; Bounds: " + Bounds.ToString();
        }

        public SizeGroup Copy(int length) {
            SizeGroup result = new SizeGroup(length, LengthInterval.Width);
            result.ValueChanged += ValueChanged;
            result.Count = 0;
            return result;
        }



        private void sizeGroup_Load(object sender, EventArgs e) {
            label1.Text = LeftEndpoint.ToString();
        }

        private void sizeGroup_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;

            int tickWidth = Width / Ticks;

            graphics.DrawLine(Pens.Black,
                0 + 2 * tickWidth, Height - ClassTickSize,
                0 + 2 * tickWidth, Height);

            graphics.DrawLine(Pens.Black,
                tickWidth * (Ticks / 2 + 2), Height - ClassTickSize + 4,
                tickWidth * (Ticks / 2 + 2), Height);

            for (int i = 0; i < Ticks; i++) {
                graphics.DrawLine(Pens.Black,
                    i * tickWidth, Height - SmallTickSize,
                    i * tickWidth, Height);
            }
        }

        private void numeric_ValueChanged(object sender, EventArgs e) {
            if (ValueChanged != null) {
                ValueChanged.Invoke(sender, e);
            }
        }
    }
}

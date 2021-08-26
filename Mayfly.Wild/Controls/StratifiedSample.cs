using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Wild.Controls
{
    public partial class StratifiedSample : UserControl
    {
        #region Properties

        public int ParkingLot
        {
            get { return parkingLot; }

            set
            {
                parkingLot = value;
                Layout();
            }
        }

        public static int DefaultStart = 50;

        public static int DefaultEnd = 100;

        public double Interval
        {
            get
            {
                return interval; 
            }

            set 
            {
                interval = value; 
            }
        }

        public double Start 
        {
            get
            {
                double result = End;

                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    if (sizeGroup.LeftEndpoint < result)
                    {
                        result = sizeGroup.LeftEndpoint;
                    }
                }

                return result;
            }

            set
            {
                if (value < Start)
                {
                    while (value < Start)
                    {
                        AddLeft();
                    }
                }

                while (value > End)
                {
                    AddRight();
                }

                if (value > Start)
                {
                    while (value > Start)
                    {
                        RemoveLeft();
                    }
                }

                Properties.UpdateValues(); 
            }
        }

        public double End 
        {
            get
            {
                double result = 0;
                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    if (sizeGroup.LeftEndpoint > result)
                    {
                        result = sizeGroup.LeftEndpoint;
                    }
                }
                return result;
            }

            set
            {
                if (value > End)
                {
                    while (value > End)
                    {
                        AddRight();
                    }
                }

                while (value < Start)
                {
                    AddLeft();
                }


                if (value < End)
                {
                    while (value < End)
                    {
                        RemoveRight();
                    }
                }

                Properties.UpdateValues(); 
            }
        }

        public int TotalCount
        {
            get
            {
                int result = 0;

                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    result += sizeGroup.Count;
                }

                return result;
            }
        }

        public double FirstValue
        {
            get
            {
                for (int i = 0; i < SizeGroups.Count; i++)
                {
                    if (SizeGroups[i].Count > 0)
                    {
                        return SizeGroups[i].LeftEndpoint;
                    }
                }

                return Start;
            }
        }

        public double LastValue
        {
            get
            {
                for (int i = SizeGroups.Count - 1; i > 0; i--)
                {
                    if (SizeGroups[i].Count > 0)
                    {
                        return SizeGroups[i].LeftEndpoint;
                    }
                }
                return End;
            }
        }

        public new bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;

                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    sizeGroup.Enabled = value;
                }
            }
        }

        public List<SizeGroup> SizeGroups
        {
            get
            {
                List<SizeGroup> result = new List<SizeGroup>();
                foreach (Control control in Controls)
                {
                    if (control is SizeGroup)
                    {
                        result.Add((SizeGroup)control);
                    }
                }
                return result;
            }
        }

        public bool IsChanged = false;

        public override Font Font 
        {
            get
            {
                return font;
            }

            set
            {
                font = value;

                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    sizeGroup.Font = value;
                }

                label1.Font = value;
            }
        }

        public event EventHandler ValueChanged;

        public StratifiedSampleProperties Properties
        {
            get;
            set;
        }

        #endregion

        private bool enabled;

        private int parkingLot;

        private double interval = 10;

        private Font font = SystemFonts.DefaultFont;

        private Graphics AddButton;



        public StratifiedSample()
        {
            InitializeComponent();

            Properties = new StratifiedSampleProperties(this);

            ParkingLot = 15;
            Enabled = true;
            Reset();

            IsChanged = false;
            RefreshTerminal();
        }



        #region Methods

        private void Clear()
        {
            while (SizeGroups.Count > 0)
            {
                Controls.Remove(FindCounter(Start));
            }
        }

        public void Reset()
        {
            Reset(DefaultStart, DefaultEnd, false);
        }

        public void Reset(double start, double end)
        {
            Reset(start, end, false);
        }

        public void Reset(double start, double end, bool dropValues)
        {
            if (start > end) 
                throw new ArgumentException("Beginning should be less than end.");

            Clear();

            End = end;
            Start = start;

            foreach (SizeGroup sizeGroup in SizeGroups)
            {
                sizeGroup.Enabled = Enabled;

                if (dropValues)
                {
                    sizeGroup.Count = 0;
                }
            }

            if (ValueChanged != null)
            {
                foreach (SizeGroup sizeGroup in SizeGroups)
                {
                    sizeGroup.ValueChanged += new EventHandler(ValueChanged);
                }
            }

            Refresh();
        }

        public SizeGroup FindCounter(double length)
        {
            foreach (SizeGroup sizeGroup in SizeGroups)
            {
                if (sizeGroup.LeftEndpoint == length)
                {
                    return sizeGroup;
                }
            }
            return null;
        }

        private SizeGroup NewSizeGroup(double length)
        {
            SizeGroup result = new SizeGroup(length, this.Interval);
            result.Font = Font;
            result.Enabled = Enabled;
            if (ValueChanged != null)
            {
                result.ValueChanged += ValueChanged;
            }
            return result;
        }

        private void MoveTo(Control control, int x)
        {
            control.SetBounds(x, 0, control.Width, control.Height); 
        }

        public void AddRight()
        {
            if (SizeGroups.Count == 0)
            {
                Controls.Add(NewSizeGroup(0));
            }
            else
            {
                Controls.Add(NewSizeGroup(End + Interval));
            }       
        }

        public void AddLeft()
        {
            if (SizeGroups.Count == 0)
            {
                Controls.Add(NewSizeGroup(0));
            }
            else
            {
                Controls.Add(NewSizeGroup(Start - Interval));
            }
        }

        public void RemoveRight()
        {
            foreach (SizeGroup sizeGroup in SizeGroups)
            {
                if (sizeGroup.LeftEndpoint == End)
                {
                    Controls.Remove(sizeGroup);
                    break;
                }
            }

            Layout();
        }

        public void RemoveLeft()
        {
            double start = Start;
            foreach (SizeGroup sizeGroup in SizeGroups)
            {
                if (sizeGroup.LeftEndpoint == start)
                {
                    Controls.Remove(sizeGroup);
                }
            }

            Layout();
        }

        private void ShowLeft()
        {
                AddButton = panelLeft.CreateGraphics();
                AddButton.FillRectangle(SystemBrushes.ControlLight, new Rectangle(-1, 0, 8, 20));
                AddButton.DrawRectangle(SystemPens.ControlDark, new Rectangle(-1, 0, 8, 20));
        }

        private void RefreshTerminal()
        {
            label1.Text = (End + Interval).ToString();

            Graphics graphics = Terminal.CreateGraphics();
            //graphics.Clear(BackColor);

            int tickWidth = SizeGroup.ClassSize.Width / SizeGroup.Ticks;

            graphics.DrawLine(Pens.Black, 0 + 2 * tickWidth, 
                SizeGroup.ClassSize.Height - SizeGroup.ClassTickSize, 
                0 + 2 * tickWidth, 
                SizeGroup.ClassSize.Height);

            for (int i = 0; i < 3; i++)
            {
                graphics.DrawLine(Pens.Black, i * tickWidth, 
                    SizeGroup.ClassSize.Height - SizeGroup.SmallTickSize,
                    i * tickWidth,
                    SizeGroup.ClassSize.Height);
            }
        }

        private new void Layout()
        {
            foreach (SizeGroup sizeGroup in SizeGroups)
            {
                MoveTo(sizeGroup, panelLeft.Bounds.X + ParkingLot + (int)((sizeGroup.LeftEndpoint - Start) /
                    Interval) * SizeGroup.ClassSize.Width);
            }

            MoveTo(Terminal, panelLeft.Bounds.X + ParkingLot + (int)((End + Interval - Start) / 
                Interval) * SizeGroup.ClassSize.Width);

            RefreshTerminal();
        }

        #endregion

        #region Interface logics

        private void controlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is SizeGroup)
            {
                IsChanged = true;
            }
            Layout();
        }

        private void controlRemoved(object sender, ControlEventArgs e)
        {
            IsChanged = true;
        }


        private void panelLeft_MouseEnter(object sender, EventArgs e)
        {
            if (Enabled && Start > 0)
            {
                ShowLeft();
            }
        }

        private void panelLeft_MouseLeave(object sender, EventArgs e)
        {
            if (Enabled)
            {
                if (AddButton != null)
                {
                    AddButton.Clear(BackColor);
                }
            }
        }

        private void panelLeft_Click(object sender, EventArgs e)
        {
            if (Enabled && Start > 0)
            {
                AddLeft();
                AddButton.Clear(BackColor);
                ShowLeft();
            }
        }


        private void Terminal_MouseEnter(object sender, EventArgs e)
        {
            if (Enabled)
            {
                AddButton = Terminal.CreateGraphics();
                AddButton.FillRectangle(SystemBrushes.ControlLight, SizeGroup.NumericField);
                AddButton.FillRectangles(SystemBrushes.ControlDark, new Rectangle[] {
                new Rectangle(42, 8, 12, 4),
                new Rectangle(46, 4, 4, 12)});
                AddButton.DrawRectangle(SystemPens.ControlDark, SizeGroup.NumericField);
            }
        }

        private void Terminal_MouseLeave(object sender, EventArgs e)
        {
            if (Enabled)
            {
                AddButton.Clear(BackColor);
                RefreshTerminal();
            }
        }

        private void Terminal_MouseDown(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                AddButton = Terminal.CreateGraphics();
                AddButton.FillRectangle(SystemBrushes.ControlDark, SizeGroup.NumericField);
            }
        }

        private void Terminal_MouseUp(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                AddButton = Terminal.CreateGraphics();
                AddButton.FillRectangle(SystemBrushes.ControlLight, SizeGroup.NumericField);
                AddButton.DrawRectangle(SystemPens.ControlDark, SizeGroup.NumericField);
            }
        }

        private void Terminal_Paint(object sender, PaintEventArgs e)
        {
            RefreshTerminal();
        }

        private void Terminal_Click(object sender, EventArgs e)
        {
            if (Enabled)
            {
                AddRight();
            }
        }

        #endregion
    }
}

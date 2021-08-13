using Mayfly.Extensions;
using System;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;

namespace Mayfly.Controls
{
    public class NumberBox : System.Windows.Forms.TextBox
    {
        string format;

        [Localizable(true)]
        public string Format 
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
                Text = Value.ToString(value);
            }
        }

        public double Value 
        {
            get 
            {
                try { return double.Parse(this.Text); }
                catch { return double.NaN; }
            }

            set 
            {
                if (double.IsNaN(value)) {
                    this.Text = Constants.Null;
                }
                else
                {
                    this.Text = value.ToString(format);
                }
            }
        }

        public int Precision 
        {
            get
            {
                int point = Text.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                return (point == -1) ? (0) : (Text.Length - point - 1);
            }
        }

        public double Increment 
        {
            get
            {
                return Math.Pow(10, -Precision);
            }
        }



        public NumberBox()
            : base()
        {
            TextAlign = HorizontalAlignment.Right;
        }



        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (!double.IsNaN(Value))
            {
                format = "N" + Precision;
            }
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e) 
        {
            base.OnKeyPress(e);


            if (e.KeyChar.ToString() == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
            {
                e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }

            InputVariant inputVariant = this.AllowInput(e.KeyChar);

            if (inputVariant != InputVariant.Allow)
            {
                ToolTip toolTip = new ToolTip();
                string instruction = string.Empty;
                switch (inputVariant)
                {
                    case InputVariant.DecimalRepeat:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputDecimalRepeat;
                        instruction = String.Format(Mayfly.Resources.Interface.InputDecimalRepeatInstruction, e.KeyChar);
                        break;
                    case InputVariant.NotNumber:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputNotNumber;
                        instruction = String.Format(Mayfly.Resources.Interface.InputNotNumberInstruction, e.KeyChar);
                        break;
                    case InputVariant.Other:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputOther;
                        instruction = String.Format(Mayfly.Resources.Interface.InputOtherInstruction, e.KeyChar);
                        break;
                }
                toolTip.Show(instruction, this, this.Width / 2, this.Height, 1500);
                Mayfly.Service.PlaySound(Resources.Sounds.StandardSound);
            }

            e.Handled = inputVariant != InputVariant.Allow;


        }

        public InputVariant AllowInput(char symbol) 
        {
            if (symbol == (char)Keys.Back)
            {
                return InputVariant.Allow;
            }


            // If symbol is decimal separator
            if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol) ||
                CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol))
            {
                // If separator is already in value
                if (this.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                    this.Text.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
                {
                    // If separator is in selected part
                    if (this.SelectedText.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                            this.SelectedText.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
                    {
                        return InputVariant.Allow;
                    }
                    else
                    {
                        return InputVariant.DecimalRepeat;
                    }
                }
                else // If it is first instance of separator
                {
                    return InputVariant.Allow;
                }
            }
            else // If symbol is digit
            {
                if (Constants.Numbers.Contains(symbol))
                {
                    return InputVariant.Allow;
                }
                else
                {
                    return InputVariant.NotNumber;
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    Value += Increment;
                    break;

                case Keys.Down:
                    Value -= Increment;
                    break;

                default:
                    base.OnPreviewKeyDown(e);
                    break;
            }
        }
    }
}
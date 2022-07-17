using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public class NumberBox : System.Windows.Forms.TextBox
    {
        string format;
        //double numberValue;
        double impossibleValue = -1;
        double minimum = 0;
        private EventHandler valueChanged;

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get; set; }

        [Localizable(true)]
        public string Format {
            get {
                return format;
            }

            set {
                format = value;
                base.Text = Value == impossibleValue ? string.Empty : Value.ToString(format);
            }
        }

        [Browsable(true), DefaultValue(-1)]
        public double Value {

            get {
                return string.IsNullOrWhiteSpace(base.Text) ? impossibleValue : double.Parse(base.Text);
            }

            set {
                base.Text = value == impossibleValue ? string.Empty : value.ToString();
            }
        }

        [Browsable(true), DefaultValue(0)]
        public double Minimum {

            get { return minimum; }
            set {
                minimum = value;
                impossibleValue = value - 1;
            }
        }

        [Browsable(true), DefaultValue(100)]
        public double Maximum {

            get;
            set;
        }

        public int Precision {
            get {
                int point = base.Text.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                return (point == -1) ? (0) : (base.Text.Length - point - 1);
            }
        }

        public double Increment {
            get {
                return Math.Pow(10, -Precision);
            }
        }

        public bool IsSet {
            get {
                return !string.IsNullOrWhiteSpace(base.Text);
            }
        }

        public event EventHandler ValueChanged {
            add {
                valueChanged += value;
            }
            remove {
                valueChanged -= value;
            }
        }



        public NumberBox()
            : base() {
            TextAlign = HorizontalAlignment.Right;
            Value = impossibleValue;
        }



        protected override void OnTextChanged(EventArgs e) {

            base.OnTextChanged(e);

            if (string.IsNullOrEmpty(base.Text)) {
                Value = impossibleValue;
            } else if (Value > Maximum) {
                Value = Maximum;
            } else if (Value < Minimum) {
                Value = Minimum;
            }

            if (valueChanged != null) valueChanged.Invoke(this, EventArgs.Empty);

            format = "N" + Precision;
        }



        protected override void OnKeyPress(KeyPressEventArgs e) {


            if (e.KeyChar.ToString() == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator) {
                e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }

            InputVariant inputVariant = AllowInput(e.KeyChar);

            if (inputVariant == InputVariant.Allow) {

            } else {

                switch (inputVariant) {
                    case InputVariant.DecimalRepeat:
                        this.NotifyInstantly(Resources.Interface.InputDecimalRepeatInstruction, e.KeyChar);
                        break;
                    case InputVariant.NotNumber:
                        this.NotifyInstantly(Resources.Interface.InputNotNumberInstruction, e.KeyChar);
                        break;
                    case InputVariant.Other:
                        this.NotifyInstantly(Resources.Interface.InputOtherInstruction, e.KeyChar);
                        break;
                }

                Service.PlaySound(Resources.Sounds.StandardSound);
            }

            e.Handled = inputVariant != InputVariant.Allow;
        }

        public InputVariant AllowInput(char symbol) {

            if (symbol == (char)Keys.Back) {
                return InputVariant.Allow;
            }

            // If symbol is decimal separator
            if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol) ||
                CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol)) {
                // If separator is already in value
                if (base.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                    base.Text.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)) {
                    // If separator is in selected part
                    if (this.SelectedText.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                            this.SelectedText.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)) {
                        return InputVariant.Allow;
                    } else {
                        return InputVariant.DecimalRepeat;
                    }
                } else // If it is first instance of separator
                  {
                    return InputVariant.Allow;
                }
            } else // If symbol is digit
              {
                if (Constants.Numbers.Contains(symbol)) {
                    return InputVariant.Allow;
                } else {
                    return InputVariant.NotNumber;
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e) {

            switch (e.KeyData) {
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Mayfly.Controls
{
    public class SpreadSheetIconTextBoxColumn : DataGridViewTextBoxColumn
    {
        private Image imageValue;
        private Size imageSize;

        public SpreadSheetIconTextBoxColumn()
        {
            this.CellTemplate = new TextAndImageCell();
        }

        public override object Clone()
        {
            SpreadSheetIconTextBoxColumn c = base.Clone() as SpreadSheetIconTextBoxColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                     inheritedPadding.Top, inheritedPadding.Right,
                     inheritedPadding.Bottom);
                    }
                }
            }
        }
        private TextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as TextAndImageCell; }
        }
        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }

    public class TextAndImageCell : DataGridViewTextBoxCell
    {
        private Image imageValue;
        private Size imageSize;
        private DataGridViewCellStyle style;

        public override object Clone()
        {
            TextAndImageCell c = base.Clone() as TextAndImageCell;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get
            {
                if (this.OwningColumn == null || this.OwningTextAndImageColumn == null)
                {
                    return imageValue;
                }
                else if (this.imageValue != null)
                {
                    return this.imageValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Image;
                }
            }

            set
            {
                if (value == null)
                {
                    imageValue = value;
                    return;
                }

                style = this.DataGridView == null ? this.OwningRow.DefaultCellStyle : this.DataGridView.Columns[this.ColumnIndex].DefaultCellStyle;
                Padding padding = style.Padding;

                if (this.imageValue != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;
                }

                switch (style.Alignment)
                {
                    case DataGridViewContentAlignment.BottomRight:
                    case DataGridViewContentAlignment.MiddleRight:
                    case DataGridViewContentAlignment.TopRight:
                        this.Style.Padding = new Padding(imageSize.Width + 2 * padding.Left, padding.Top, padding.Right, padding.Bottom);
                        break;
                    default:
                        this.Style.Padding = new Padding(padding.Left, padding.Top, padding.Right * 2 + imageSize.Width, padding.Bottom);
                        break;
                }

                if (this.DataGridView != null) this.DataGridView.Refresh();
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
            Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            if (this.Image != null)
            {
                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();

                graphics.SetClip(cellBounds);

                Point imageLocation = new Point();
                Padding padding = style.Padding;

                switch (this.InheritedStyle.Alignment)
                {
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                        imageLocation = new Point(cellBounds.Width - padding.Right - imageSize.Width, cellBounds.Height - imageSize.Height - padding.Bottom);
                        break;
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                        imageLocation = new Point(cellBounds.Width - padding.Right - imageSize.Width, cellBounds.Height / 2 - imageSize.Height / 2);
                        break;
                    case DataGridViewContentAlignment.TopLeft:
                    case DataGridViewContentAlignment.TopCenter:
                        imageLocation = new Point(cellBounds.Width - padding.Right - imageSize.Width, padding.Top);
                        break;
                    case DataGridViewContentAlignment.BottomRight:
                        imageLocation = new Point(padding.Left, cellBounds.Height - imageSize.Height - padding.Bottom);
                        break;
                    case DataGridViewContentAlignment.MiddleRight:
                        imageLocation = new Point(padding.Left, cellBounds.Height / 2 - imageSize.Height / 2);
                        break;
                    case DataGridViewContentAlignment.TopRight:
                        imageLocation = new Point(padding.Left, padding.Top);
                        break;
                }

                imageLocation.Offset(cellBounds.Location);
                
                graphics.DrawImageUnscaled(this.Image, imageLocation);

                graphics.EndContainer(container);
            }
        }

        private SpreadSheetIconTextBoxColumn OwningTextAndImageColumn
        {
            get { return this.OwningColumn as SpreadSheetIconTextBoxColumn; }
        }
    }
}
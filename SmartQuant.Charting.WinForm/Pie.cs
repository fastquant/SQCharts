using System;
using System.Collections;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Pie : IDrawable
    {
        private Color[] palette;

        public string Name { get; set; }

        public string Title { get; set; }

        public bool ToolTipEnabled { get; set; }

        public string ToolTipFormat { get; set; }

        public ArrayList Pieces { get; private set; }

        public bool EnableContour { get; set; }

        public Color ContourColor { get; set; }

        public int Gap { get; set; }

        public string Format { get; set; }

        public Pie()
            : this(null, null)
        {
        }

        public Pie(string name)
            : this(name, null)
        {
        }

        public Pie(string name, string title)
        {
            Name = Name;
            Title = title;
            Pieces = new ArrayList();
            EnableContour = true;
            ContourColor = Colors.Gray;
            Gap = 0;
            Format = "F1";
            palette = new Color[0];
        }


        public void Add(double weight)
        {
            Pieces.Add(new TPieItem(weight, "", ColorUtils.Empty()));
        }

        public void Add(double weight, string text, Color color)
        {
            Pieces.Add(new TPieItem(weight, text, color));
        }

        public void Add(double weight, string text)
        {
            Pieces.Add(new TPieItem(weight, text, ColorUtils.Empty()));
        }

        public virtual void Draw(string option)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw()
        {
            Draw("");
        }

        public virtual void Paint(Pad pad, double xMin, double xMax, double yMin, double yMax)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double x, double y)
        {
            return null;
        }
    }
}


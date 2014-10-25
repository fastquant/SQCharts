using System;
using System.Drawing;


#if GTK
using Compatibility.Gtk;
#else
using Compatibility.WinForm;
#endif

namespace SmartQuant.Charting.Draw3D
{
    public class TView
    {
        private double scaleZ = 1.0;

        public int Left { get; private set; }

        public int Top { get; private set; }

        public int H { get; private set; }

        public TVec3 o { get; private set; }

        public TVec3 Lx { get; private set; }

        public TVec3 Ly { get; private set; }

        public TVec3 Lz { get; private set; }

        public double ScaleZ
        {
            get
            {
                return this.scaleZ;
            }
            set
            {
                this.scaleZ = value < 0 ? 1 : value;
            }
        }

        public TView()
        {
            this.SetProjectionSpecial(-2.0, Math.PI / 6.0);
        }

        public static TView View(Pad pad)
        {
            throw new NotImplementedException();
        }

        public void SetProjectionOrthogonal(double AngleXY, double ViewAngle)
        {
            throw new NotImplementedException();
        }

        public void SetProjectionSpecial(double angleXY, double viewAngle)
        {
            throw new NotImplementedException();
        }

        public void CalculateAxes(Pad pad, int left, int top, int h)
        {
            throw new NotImplementedException();
        }

        public void PaintAxisGridAndTicks(Graphics g, Axis a, bool marks, TVec3 o, TVec3 o_, TVec3 l)
        {
            throw new NotImplementedException();
        }

        public void PaintAxes(Graphics g, Pad pad, int left, int top, int h)
        {
            throw new NotImplementedException();
        }

        public void PaintAxes(Pad pad, int left, int top, int h)
        {
            throw new NotImplementedException();
        }
    }
}

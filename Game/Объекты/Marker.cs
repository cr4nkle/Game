using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Объекты
{
    class Marker:BaseObject
    {
        public Marker(float x, float y, float angle) : base(x, y, angle)
        {
        }
        public override void Render(Graphics g)
        {
            g.DrawLine(new Pen(Color.Red, 2), 0, -15, 0, 15);
            g.DrawLine(new Pen(Color.Red, 2), -15, 0, 15, 0);
            g.DrawEllipse(new Pen(Color.Red, 2), -10, -10, 20, 20);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-10, -10, 20, 20);
            return path;
        }
    }
}

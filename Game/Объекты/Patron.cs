using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Объекты
{
    class Patron:BaseObject
    {
        public Action<Patron> WantsRemove;
        public int count = 200;
        public Patron(float x, float y, float angle) : base(x, y, angle)
        {
        }

        
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Brown), 0, -5, 25, 10);
            g.FillRectangle(new SolidBrush(Color.Gold), -15, -5, 30, 10);
           
            if (count > 0)
            {
                g.DrawString(
                    $"{count}%",
                    new Font("Verdana", 6), 
                    new SolidBrush(Color.Indigo), 
                    10, 10 
                    );
                count--;
            }
            else
            {
                WantsRemove?.Invoke(this);
            }

        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, -5, 25, 10);
            return path;
        }

    }
}

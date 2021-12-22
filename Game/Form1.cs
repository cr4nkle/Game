﻿using Game.Объекты;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        Patron patron;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;

        public Form1()
        {
            InitializeComponent();
            Random randX = new Random();
            Random randY = new Random();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);          
            
            
            var numb = 0;
            
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            

            player.OnPatronOverlap += (n) =>
            {
                patron = null;
                objects.Remove(n);
                
                numb++;
                textBox1.Text = numb.ToString();
                patron = new Patron(pbMain.Width / 2 + randX.Next(-400, 400), pbMain.Height / 2 + randY.Next(-300,300), 0);
                objects.Add(patron);
            };

            if(patron == null)
            {
                patron = new Patron(pbMain.Width / 2 + randX.Next(-400, 400), pbMain.Height / 2 + randY.Next(-300, 300), 0);
                patron.WantsRemove += (p) =>
                {
                    p.X = pbMain.Width / 2 + randX.Next(-150, 550);
                    p.Y = pbMain.Height / 2 + randY.Next(-20, 375);
                    p.count = 100;
                };
            }
            objects.Add(patron);
            objects.Add(player);
            objects.Add(marker);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

            foreach(var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); 
                    obj.Overlap(player);
                }                
            }

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }            
        }

        public void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            
            player.X += player.vX;
            player.Y += player.vY;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {                       
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); 
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}

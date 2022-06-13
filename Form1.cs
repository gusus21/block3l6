using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3block
{
    public partial class Form1 : Form
    {
        Label[] lb = new Label[6];
        TextBox[] tb = new TextBox[6];
        Graphics g;
        PointF[] pt = new PointF[3];
        PointF[] ptFi = new PointF[3];
        static PointF pt0;
        Pen p1 = new Pen(Color.Black);
        float fii;
        float coeff;
        int numClick;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            coeff = 0;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            coeff = 0;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            tb[2 * numClick].Text = e.X.ToString();
            tb[2 * numClick + 1].Text = e.Y.ToString();
            numClick += 1;
            if (numClick == 3)
                numClick = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                lb[i] = new Label();
                lb[i].Location = new Point(10, 45 + 30 * i);
                lb[i].Size = new Size(25, 25);
                if (i % 2 == 0)
                    lb[i].Text = "x" + (i / 2 + 1);
                else
                    lb[i].Text = "y" + (i / 2 + 1);
                lb[i].TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(lb[i]);

                tb[i] = new TextBox();
                tb[i].Location = new Point(40, 45 + 30 * i);
                tb[i].Size = new Size(35, 25);
                tb[i].TextAlign = HorizontalAlignment.Center;
                tb[i].ReadOnly = true;
                Controls.Add(tb[i]);
            }
            numClick = 0;
            g = panel1.CreateGraphics();
        }

        static PointF Triangle_Top(float fi, PointF ptt)
        {
            float dx = ptt.X - pt0.X;
            float dy = ptt.Y - pt0.Y;
            float dxn = dx * (float)Math.Cos(fi) - dy * (float)Math.Sin(fi);
            float dyn = dx * (float)Math.Sin(fi) + dy * (float)Math.Cos(fi);
            PointF pt = new PointF(pt0.X + dxn, pt0.Y + dyn);
            return pt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            for (int i = 0; i < 3; i++)
                pt[i] = new PointF(float.Parse(tb[2 * i].Text), float.Parse(tb[2 * i + 1].Text));
            g.DrawPolygon(p1, pt);
            pt0 = new PointF((pt[0].X + pt[1].X + pt[2].X) / 3.0f, (pt[0].Y + pt[1].Y + pt[2].Y) / 3.0f);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            fii = coeff * (float)Math.PI;
            for (int i1 = 0; i1 < 3; i1++)
            {
                ptFi[i1] = Triangle_Top(fii, pt[i1]);
            }
            g.DrawPolygon(p1, ptFi);
            coeff += 0.05f;
            if (coeff == 2)
                coeff = 0;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            tb[2 * numClick].Text = e.X.ToString();
            tb[2 * numClick + 1].Text = e.Y.ToString();
            numClick += 1;
            if (numClick == 3)
                numClick = 0;
        }
    }
}

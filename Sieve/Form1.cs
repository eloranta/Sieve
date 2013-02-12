using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sieve
{
    public partial class Form1 : Form
    {
        List<Integer> list = new List<Integer>();
        const int maxState = 40;
        int prime = 2;
        int current = 2;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            list.Add(new Integer(1, Color.White));

            for (int i = 2; i <= maxState; ++i)
                list.Add(new Integer(i, Color.LightGray));

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(tick);
            timer.Start();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            foreach (Integer i in list)
                i.Draw(graphics);
            graphics.Dispose();
        }
        private int FindFirstFree()
        {
            foreach (Integer i in list)
                if (i.color == Color.LightGray)
                    return i.value;
            return -1;
        }
        public void Clear()
        {
            for (int i = 2; i <= maxState; ++i)
                list[i - 1].color = Color.LightGray;
            prime = 2;
            current = prime;

        }
        private void tick(object sender, System.EventArgs e)
        {
            if (current == prime)
                list[current-1].SetColor(Color.Red);
            else
                list[current - 1].SetColor(Color.Wheat);

            current += prime;
            if (current > maxState)
            {
                prime = FindFirstFree();
                current = prime;
                if (prime == -1)
                    Clear();
            }
            this.Invalidate();  // Optimize?
        }
    }
    public class Integer
    {
        public int value;
        public Color color;
        const int dx = 25;
        const int dy = 25;
        const int border = 5;
        const int margin = 5;
        public Integer(int value, Color color)
        {
            this.value = value;
            this.color = color;
        }
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(color);
            int x = margin + border + ((value - 1) % 10) * (dx + border);
            int y = margin + border + ((value - 1) / 10) * (dy + border);
            graphics.FillRectangle(brush, new Rectangle(x, y, dx, dy));

            if (value > 1)
            {
                Font font = new Font("Arial", 10);
                SolidBrush brush2 = new SolidBrush(Color.Black);
                graphics.DrawString(value.ToString(), font, brush2, x, y + 5);
                font.Dispose();
                brush2.Dispose();
            }
            brush.Dispose();
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}

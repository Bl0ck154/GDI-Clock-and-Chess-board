using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI__Clock_and_Chess
{
	public partial class Clock : Form
	{
		Bitmap bmp;
		Point center;
		private Point mouseOffset;
		private bool isMouseDown = false;
		public Clock()
		{
			InitializeComponent();
			Load += Form1_Load;
			bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
			pictureBox1.ContextMenuStrip = contextMenuStrip1;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			SetClientSizeCore(300, 300);
			System.Drawing.Drawing2D.GraphicsPath Form_Path = new System.Drawing.Drawing2D.GraphicsPath();
			Form_Path.AddEllipse(0, 0, this.Width, this.Height);
			Region = new Region(Form_Path);

			CenterToScreen();

			center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

			Timer timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += Timer_Tick;
			timer.Start();
			Timer_Tick(timer, null);

			KeyUp += Form1_KeyUp; // выход из программы по Esc

			pictureBox1.MouseDown += Form1_MouseDown;
			pictureBox1.MouseMove += Form1_MouseMove;
			pictureBox1.MouseUp += Form1_MouseUp;
		}
		Point ArrowCalc(Point center, int value, int R)
		{
			return new Point((int)(center.X + R * Math.Cos(-Math.PI / 2 + Math.PI * value / 30)),
				(int)(center.Y + R * Math.Sin(-Math.PI / 2 + Math.PI * value / 30)));
		}
		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}
		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			int xOffset;
			int yOffset;

			if (e.Button == MouseButtons.Left)
			{
				xOffset = -e.X - SystemInformation.FrameBorderSize.Width +4;
				yOffset = -e.Y - SystemInformation.FrameBorderSize.Height +4;
				mouseOffset = new Point(xOffset, yOffset);
				isMouseDown = true;
			}
		}
		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
			{
				Point mousePos = Control.MousePosition;
				mousePos.Offset(mouseOffset.X, mouseOffset.Y);
				Location = mousePos;
			}
		}
		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
			}
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			DateTime currentTime = DateTime.Now;
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Honeydew);
				//g.DrawEllipse(Pens.Black, new Rectangle(center, new Size(10,10)));
				g.FillEllipse(Brushes.Black, center.X-5, center.Y-5, 10, 10);
				for (int i = 1; i <= 60; i++)
				{
					g.DrawLine(new Pen(Color.Black, i % 5 == 0 ? 4 : 1), ArrowCalc(center, i, 140), ArrowCalc(center, i, 144));
					if(i % 5 == 0)
					{
						g.DrawString($"{i/5}", new Font("", 10), Brushes.Black, ArrowCalc(center, i, 130).X-10, ArrowCalc(center, i, 130).Y-10);
					}
				}

				g.DrawLine(new Pen(Color.Black), center, ArrowCalc(center, currentTime.Second, 130));

				g.DrawLine(new Pen(Color.Black, 3), center, ArrowCalc(center, currentTime.Minute, 115));

				g.DrawLine(new Pen(Color.Black, 7), center, ArrowCalc(center, (currentTime.Hour > 12 ? currentTime.Hour-12 : currentTime.Hour)*5, 90));

				g.DrawString($"{currentTime.ToLongTimeString()}", new Font("", 15), Brushes.DodgerBlue, 100,220);
			}
			
			pictureBox1.Image = bmp;
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void поверхВсехОконToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;
		}
	}
}

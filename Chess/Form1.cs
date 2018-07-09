using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			Paint += Form1_Paint;
			Resize += Form1_Resize;

			Load += Form1_Load;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			CenterToScreen();
			ClientSize = new Size(256, 256);
			Form1_Resize(sender, e);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (Size.Height != Size.Width)
			{
				Size = new Size(Size.Width, Size.Width);
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			ClientSize = new Size(256, 256);

			const int field = 8; // размер поля
			int side = ClientSize.Width / field;

			bool toogle = false;
			Rectangle tmp;
			for (int i = 0; i < field; i++)
			{
				toogle = !toogle;
				for (int j = 0; j < field; j++)
				{
					tmp = new Rectangle(0 + side * i, 0 + side * j, side, side);
					//			g.DrawRectangle(i % 2 == 0 && j % 2 == 0 ? Pens.Black : Pens.White, tmp);
					g.FillRectangle((toogle = !toogle)? Brushes.Black : Brushes.White, tmp);
					
				}
			}
			g.Dispose();
		}
	}
}

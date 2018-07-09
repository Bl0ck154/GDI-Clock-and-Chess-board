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
	public partial class Chess : Form
	{
		const int field = 8; // размер поля
		List<PictureBox> cells;
		Stack<Control> hided;
		public Chess()
		{
			InitializeComponent();

			Resize += Form1_Resize;
			
			ResizeRedraw = true;

			Shown += Form1_Shown;

			cells = new List<PictureBox>();

			hided = new Stack<Control>();

			unhideLastToolStripMenuItem.Enabled = false;

			ContextMenuStrip = contextMenuStrip2;
			contextMenuStrip2.Items.Add(unhideLastToolStripMenuItem);
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			SetClientSizeCore(300, 300);
			Form1_Resize(sender, e);
			CenterToScreen();
		}
		private void Form1_Resize(object sender, EventArgs e)
		{
			if (ClientSize.Height != ClientSize.Width)
			{
				ClientSize = new Size(ClientSize.Width, ClientSize.Width);
			}
		}
		enum Chessmen
		{
			rook,
			knight,
			bishop,
			queen,
			king,
			pawn
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.Clear(Color.Chocolate);

			int side = ClientSize.Height / field;

			bool toogle = false;
			Rectangle tmp;
			Bitmap tmp_img = new Bitmap(side, side);
			PictureBox pb;
			for (int i = 0; i < field; i++)
			{
				toogle = !toogle;
				for (int j = 0; j < field; j++)
				{
					tmp = new Rectangle(0 + side * j, 0 + side * i, side, side);

					g.FillRectangle((toogle = !toogle) ? Brushes.Chocolate : Brushes.BurlyWood, tmp);
					
					if (cells.Count < field * field)
					{
						pb = new PictureBox();
						cells.Add(pb);
						if (i > 1 && i < field - 1 - 1)
							continue;

						Controls.Add(pb);
						pb.ContextMenuStrip = contextMenuStrip1;
						pb.SizeMode = PictureBoxSizeMode.StretchImage;
						tmp_img = Image.FromFile("chess//" +
							(i==1||field-1-i==1 ? Chessmen.pawn : 
							(j < 5 ? (Chessmen)j : (Chessmen)field-1-j)) + 
							(i < 2 ? "B":"W") + ".png") as Bitmap;
						tmp_img.MakeTransparent();
						
						pb.Image = tmp_img;
						pb.BackColor = Color.Transparent;
					}
					else
						pb = cells[i*field + j];

					pb.Location = tmp.Location;
					pb.ClientSize = tmp.Size;
					
				}
			}
			g.Dispose();
		}

		private void testToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = sender as ToolStripItem;
			if (menuItem != null)
			{
				ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
				if (owner != null)
				{
					Control sourceControl = owner.SourceControl;
					sourceControl.Hide();
					hided.Push(sourceControl);
					unhideLastToolStripMenuItem.Enabled = true;
				}
			}
		}

		private void unhideLastToolStripMenuItem_Click(object sender, EventArgs e)
		{
			hided.Pop().Show();
			if(hided.Count == 0)
				unhideLastToolStripMenuItem.Enabled = false;
		}
	}
}

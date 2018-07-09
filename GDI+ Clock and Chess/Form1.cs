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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			Load += Form1_Load;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			CenterToScreen();

			Graphics g = CreateGraphics();

			Timer timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += Timer_Tick;
			timer.Start();

			KeyUp += Form1_KeyUp; // выход из программы по Esc
			SizeChanged += Form1_SizeChanged;
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			DateTime currentTime = DateTime.Now;
			Text = currentTime.ToLongTimeString();
		}
	}
}

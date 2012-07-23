//=================================================================
// progress.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004, 2005, 2006  FlexRadio Systems
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// You may contact us via email at: sales@flex-radio.com.
// Paper mail may be sent to: 
//    FlexRadio Systems
//    12100 Technology Blvd.
//    Austin, TX 78727
//    USA
//=================================================================

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PowerSDR
{
	public class Progress : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private float percent_done;

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ButtonTS btnAbort;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor and Destructor

		public Progress(string s)
		{
            this.AutoScaleMode = AutoScaleMode.Inherit;
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = (float)(8.25 / ratio);
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;
            this.PerformAutoScale();
            this.PerformLayout();

			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.Text = s;
			percent_done = 0.0f;
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Progress));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAbort = new System.Windows.Forms.ButtonTS();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(16, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 24);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnAbort
            // 
            this.btnAbort.Image = null;
            this.btnAbort.Location = new System.Drawing.Point(240, 16);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // Progress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(330, 56);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "progress";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Progress_Closing);
            this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		private delegate void Invoker(float f);

		public void SetPercent(float f)
		{
			percent_done = f * 100;;
			panel1.Invalidate();
		}

		#endregion

		#region Event Handlers

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			this.Visible = false;
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int bar = (int)Math.Floor(panel1.Width * percent_done / 100);
			if(bar == 0) return;
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			LinearGradientBrush b = new LinearGradientBrush(
				new Rectangle(0, 0, bar, panel1.Height),
				Color.Green,
				Color.Lime,
				LinearGradientMode.Horizontal);
			
			g.FillRectangle(b, 0, 0, bar, panel1.Height);

			string s = percent_done.ToString("f1") + "%";
			SolidBrush b2 = new SolidBrush(Color.Black);
			Font f = new Font("Microsoft Sans Serif", 10);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			g.DrawString(s,	f, b2, new RectangleF(0, 0, panel1.Width, panel1.Height), sf);
		}

		private void Progress_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		#endregion		
	}
}

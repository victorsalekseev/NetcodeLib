using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Netcode.Graph.DChart;

namespace NPlotDemo
{
	/// <summary>
	/// Summary description for Tests.
	/// </summary>
	public class AxisTestsForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AxisTestsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.Paint += new PaintEventHandler(Tests_Paint);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// AxisTestsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 342);
			this.Name = "AxisTestsForm";
			this.Text = "Tests";

		}
		#endregion

		private void Tests_Paint(object sender, PaintEventArgs e)
		{
			System.Drawing.Rectangle boundingBox;

            Netcode.Graph.DChart.LinearAxis a = new LinearAxis(0, 10);
			a.Draw( e.Graphics, new Point(10,10), new Point(10, 200), out boundingBox );

			a.Reversed = true;
			a.Draw( e.Graphics, new Point(40,10), new Point(40, 200), out boundingBox );

			a.SmallTickSize = 0;
			a.Draw( e.Graphics, new Point(70,10), new Point(70, 200), out boundingBox );

			a.LargeTickStep = 2.5;
			a.Draw( e.Graphics, new Point(100,10), new Point(100,200), out boundingBox );

			a.NumberOfSmallTicks = 5;
			a.SmallTickSize = 2;
			a.Draw( e.Graphics, new Point(130,10), new Point(130,200), out boundingBox );

			a.AxisColor = Color.Cyan;
			a.Draw( e.Graphics, new Point(160,10), new Point(160,200), out boundingBox );

			a.TickTextColor= Color.Cyan;
			a.Draw( e.Graphics, new Point(190,10), new Point(190,200), out boundingBox );

			a.TickTextBrush = Brushes.Black;
			a.AxisPen = Pens.Black;
			a.Draw( e.Graphics, new Point(220,10), new Point(280,200), out boundingBox );

			a.WorldMax = 100000;
			a.WorldMin = -3;
			a.LargeTickStep = double.NaN;
			a.Draw( e.Graphics, new Point(310,10), new Point(310,200), out boundingBox );

			a.NumberFormat = "{0:0.0E+0}";
			a.Draw( e.Graphics, new Point(360,10), new Point(360,200), out boundingBox );
		}
	}
}

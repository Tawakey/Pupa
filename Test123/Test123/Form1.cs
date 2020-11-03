using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Input;
using System.Globalization;
using netDxf;
using netDxf.Entities;

namespace Test123
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        List<System.Drawing.Point> points = new List<System.Drawing.Point>();
        
         
        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var FCode = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "dxf files (*.dxf)|*.dxf|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    DxfDocument dxfLoad = DxfDocument.Load(filePath);




                    Read_DXF(dxfLoad);
                }
            }

        }


        int convert(double value, double From1, double From2, double To1, double To2)
        {
            return Convert.ToInt32((value - From1) / (From2 - From1) * (To2 - To1) + To1);
        }
        private void Read_DXF(DxfDocument dxf)
        {
            foreach (netDxf.Entities.LwPolyline pl in dxf.LwPolylines)
            {
                points.Clear();
                foreach (netDxf.Entities.LwPolylineVertex vert in pl.Vertexes)
                {
                    if (vert.Bulge == 0)
                    {
                        points.Add(new System.Drawing.Point(convert(vert.Position.X, -1300, 1000, 0, pictureBox1.Size.Width), pictureBox1.Size.Height - convert(vert.Position.Y, 0, 1300, 0, pictureBox1.Size.Height)));
                    }
                    else
                    {
                        Console.WriteLine(vert.Bulge);
                        Console.WriteLine(vert.Position.X);
                        Console.WriteLine(vert.Position.Y);
                    }
                    
                }
                Draw(points);
            }
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void Draw(List<System.Drawing.Point> points)
        {
            Graphics g = pictureBox1.CreateGraphics();

            int end = points.ToArray().Length - 1;
            for (int i = 0; i < end; i++){
                g.DrawLine(new Pen(Brushes.Red, 4), points[i], points[i + 1]);
            }
        }
    }
}

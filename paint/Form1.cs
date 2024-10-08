using System;
using System.Drawing;
using System.Windows.Forms;

namespace paint
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point startPoint;
        private Point endPoint;
        private Color currentColor = Color.Black;
        private readonly Bitmap canvasBitmap;

        public Form1()
        {
            InitializeComponent();
            // ������� Bitmap ��� �������� �������
            canvasBitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
            pictureBoxCanvas.Image = canvasBitmap;
        }

        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
            endPoint = e.Location; // ��������� �������� �����
        }

        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location; // ��������� �������� �����
                pictureBoxCanvas.Invalidate(); // ��������� �����, ����� ���������� ������, ������� �� ���������� ����������
            }
        }
        
        private void pictureBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;

                using (Graphics g = Graphics.FromImage(canvasBitmap))
                {
                    DrawShape(g, startPoint, endPoint);
                }
                pictureBoxCanvas.Invalidate(); // ��������� ����� ��� ����������� ������������ ������
            }
        }

        private void DrawShape(Graphics g, Point start, Point end)
        {
            Pen pen = new Pen(currentColor, (float)numericUpDownLineWidth.Value);

            // ���������� ��������� ��� ������
            switch (comboBoxShapes.SelectedItem.ToString())
            {
                case "Line":
                    g.DrawLine(pen, start, end);
                    break;
                case "Rectangle":
                    g.DrawRectangle(pen, GetRectangle(start, end));
                    break;
                case "Triangle":
                    g.DrawPolygon(pen, GetTriangle(start, end));
                    break;
            }
        }

        private Rectangle GetRectangle(Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);
            return new Rectangle(x, y, width, height);
        }

        private Point[] GetTriangle(Point start, Point end)
        {
            // �������� ������� ������������
            Point top = new Point((start.X + end.X) / 2, start.Y);
            Point left = new Point(start.X, end.Y);
            Point right = new Point(end.X, end.Y);
            return new Point[] { top, left, right };
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // ������� Bitmap � ��������� PictureBox
            using (Graphics g = Graphics.FromImage(canvasBitmap))
            {
                g.Clear(Color.White);
            }
            pictureBoxCanvas.Invalidate(); // ������� PictureBox
        }

        // ���������� ������ Paint ��� ����������� ������������ �����
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(canvasBitmap, Point.Empty);
        }
    }
}


//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace paint
//{
//    public partial class Form1 : Form
//    {
//        private bool isDrawing = false;
//        private Point startPoint;
//        private Point endPoint;
//        private Color currentColor = Color.Black;
//        private readonly Bitmap canvasBitmap;

//        public Form1()
//        {
//            InitializeComponent();
//            // ������� Bitmap ��� �������� �������
//            canvasBitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
//            pictureBoxCanvas.Image = canvasBitmap;
//        }

//        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
//        {
//            isDrawing = true;
//            startPoint = e.Location;
//            endPoint = e.Location; // ��������� �������� �����
//        }

//        //private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
//        //{
//        //    if (isDrawing)
//        //    {
//        //        endPoint = e.Location; // ��������� �������� �����
//        //        pictureBoxCanvas.Invalidate(); // ��������� �����, ����� ���������� ������, ������� �� ���������� ����������
//        //    }
//        //}

//        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (isDrawing)
//            {
//                using (Graphics g = Graphics.FromImage(canvasBitmap))
//                {
//                    //// ������� ����� ����� ���������� ����� ����� ��� ������
//                    g.Clear(Color.White);
//                    g.DrawImage(canvasBitmap, Point.Empty); // ��������������� ��� ������������ ������

//                    if (comboBoxShapes.SelectedItem.ToString() == "Curve")
//                    {
//                        // ������ ������
//                        DrawShape(g, startPoint, e.Location);
//                    }
//                    else
//                    {
//                        // ������ ������ (�����, ��������, ������������)
//                        DrawShape(g, startPoint, e.Location);
//                    }
//                }
//                pictureBoxCanvas.Invalidate(); // ��������� PictureBox
//            }
//        }

//        private void pictureBoxCanvas_MouseUp(object sender, MouseEventArgs e)
//        {
//            if (isDrawing)
//            {
//                isDrawing = false;

//                using (Graphics g = Graphics.FromImage(canvasBitmap))
//                {
//                    DrawShape(g, startPoint, endPoint);
//                }
//                pictureBoxCanvas.Invalidate(); // ��������� ����� ��� ����������� ������������ ������
//            }
//        }



//        private void DrawShape(Graphics g, Point start, Point end)
//        {
//            Pen pen = new Pen(currentColor, (float)numericUpDownLineWidth.Value);

//            // ���������� ��������� ��� ������
//            switch (comboBoxShapes.SelectedItem.ToString())
//            {
//                case "Line":
//                    g.DrawLine(pen, start, end);
//                    break;
//                case "Rectangle":
//                    g.DrawRectangle(pen, GetRectangle(start, end));
//                    break;
//                case "Triangle":
//                    g.DrawPolygon(pen, GetTriangle(start, end));
//                    break;
//            }
//        }

//        private void DrawTemporaryShape(Graphics g, Point start, Point end)
//        {
//            Pen pen = new Pen(currentColor, (float)numericUpDownLineWidth.Value);

//            // ������ ������, ������� �� ������ �������
//            switch (comboBoxShapes.SelectedItem.ToString())
//            {
//                case "Line":
//                    g.DrawLine(pen, start, end);
//                    break;
//                case "Rectangle":
//                    g.DrawRectangle(pen, GetRectangle(start, end));
//                    break;
//                case "Triangle":
//                    g.DrawPolygon(pen, GetTriangle(start, end));
//                    break;
//            }
//        }

//        private Rectangle GetRectangle(Point start, Point end)
//        {
//            int x = Math.Min(start.X, end.X);
//            int y = Math.Min(start.Y, end.Y);
//            int width = Math.Abs(start.X - end.X);
//            int height = Math.Abs(start.Y - end.Y);
//            return new Rectangle(x, y, width, height);
//        }

//        private Point[] GetTriangle(Point start, Point end)
//        {
//            // �������� ������� ������������
//            Point top = new Point((start.X + end.X) / 2, start.Y);
//            Point left = new Point(start.X, end.Y);
//            Point right = new Point(end.X, end.Y);
//            return new Point[] { top, left, right };
//        }

//        private void buttonColor_Click(object sender, EventArgs e)
//        {
//            using (ColorDialog colorDialog = new ColorDialog())
//            {
//                if (colorDialog.ShowDialog() == DialogResult.OK)
//                {
//                    currentColor = colorDialog.Color;
//                }
//            }
//        }

//        private void buttonClear_Click(object sender, EventArgs e)
//        {
//            // ������� Bitmap � ��������� PictureBox
//            using (Graphics g = Graphics.FromImage(canvasBitmap))
//            {
//                g.Clear(Color.White);
//            }
//            pictureBoxCanvas.Invalidate(); // ������� PictureBox
//        }

//        // ���������� ������ Paint ��� ����������� ������������ �����
//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);
//            e.Graphics.DrawImage(canvasBitmap, Point.Empty);

//            if (isDrawing)
//            {
//                using (Graphics g = e.Graphics)
//                {
//                    DrawTemporaryShape(g, startPoint, endPoint);
//                }
//            }
//        }
//    }
//}

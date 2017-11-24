using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer

        private int _numpts = 0;
        private int _numlines = 0;
        private bool _gooddata = false;
        private int[,] _lines;
        private double[,] _vertices;
        private double[,] _scrnpts;
        private double[,] _tnet = new double[4, 4];  //your main transformation matrix
        private bool _rotating = false;
        private Thread _thread;
        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (201711) (George & Steve)";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9, 22);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Transformer_FormClosing);
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Transformer_ClientSizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);

            if (_gooddata)
            {
                _scrnpts = Transformations.MatrixMultiply(_vertices, _tnet);

                //now draw the lines

                for (int i = 0; i < _numlines; i++)
                {
                    grfx.DrawLine(pen, (int)_scrnpts[_lines[i, 0], 0], (int)_scrnpts[_lines[i, 0], 1],
                        (int)_scrnpts[_lines[i, 1], 0], (int)_scrnpts[_lines[i, 1], 1]);
                }
            } // end of gooddata block
        } // end of OnPaint

        private void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            _gooddata = GetNewData();
            RestoreInitialImage();
        }

        private void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        private void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        private void RestoreInitialImage()
        {
            //create the screen coordinates:
            // scrnpts = vertices*ctrans
            if (_gooddata)
            {
                Transformations.SetIdentity(_tnet, 4, 4);
                _tnet = Transformations.TranslateToOrigin(_vertices, _tnet);
                _tnet = Transformations.ReflectOnYAxis(_tnet);
                _tnet = Transformations.ScaleToIntial(_vertices, _tnet, this.ClientRectangle.Height);

                _tnet = Transformations.TranslateToCenter(_tnet, this.ClientRectangle.Width, this.ClientRectangle.Height);

                Transformations.PrintMatrix(_tnet);
                Invalidate();
            }
        } // end of RestoreInitialImage

        private bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Points";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                coorddata.RemoveAt(coorddata.Count - 1);
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            _scrnpts = new double[_numpts, 4];
            Transformations.SetIdentity(_tnet, 4, 4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        private void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            _vertices = new double[coorddata.Count, 4];
            _numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                if (text[0].Equals("-1")) break;
                _vertices[_numpts, 0] = double.Parse(text[0]);
                _vertices[_numpts, 1] = double.Parse(text[1]);
                _vertices[_numpts, 2] = double.Parse(text[2]);
                _vertices[_numpts, 3] = 1.0d;
                _numpts++;
            }
        }// end of DecodeCoords

        private void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            _lines = new int[linesdata.Count, 2];
            _numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                _lines[_numlines, 0] = int.Parse(text[0]);
                if (_lines[_numlines, 0] < 0) break;
                _lines[_numlines, 1] = int.Parse(text[1]);
                _numlines++;
            }
        } // end of DecodeLines



        private void Transformer_Load(object sender, System.EventArgs e)
        {
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if(_rotating)
            {
                _rotating = false;
                _thread.Abort();
                _thread.Join();
                _thread = null;
            }
            if (e.Button == transleftbtn)
            {
                _tnet = Transformations.Translate(_tnet, -75, 0, 0);
                Refresh();
            }
            if (e.Button == transrightbtn)
            {
                _tnet = Transformations.Translate(_tnet, +75, 0, 0);
                Refresh();
            }
            if (e.Button == transupbtn)
            {
                _tnet = Transformations.Translate(_tnet, 0, -35, 0);
                Refresh();
            }
            if (e.Button == transdownbtn)
            {
                _tnet = Transformations.Translate(_tnet, 0, +35, 0);
                Refresh();
            }
            if (e.Button == scaleupbtn)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.ScaleUniform(_scrnpts, _tnet, 1.10);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }
            if (e.Button == scaledownbtn)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.ScaleUniform(_scrnpts, _tnet, 0.90);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }
            if (e.Button == rotxby1btn)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnX(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }
            if (e.Button == rotyby1btn)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnY(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();       
            }
            if (e.Button == rotzby1btn)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnZ(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }

            if (e.Button == rotxbtn)
            {
                _rotating = true;
                _thread = new Thread(new ThreadStart(RotateXAsync));
                _thread.Start();
            }
            if (e.Button == rotybtn)
            {
                _rotating = true;
                _thread = new Thread(new ThreadStart(RotateYAsync));
                _thread.Start();
            }

            if (e.Button == rotzbtn)
            {
                _rotating = true;
                _thread = new Thread(new ThreadStart(RotateZAsync));
                _thread.Start();
            }

            if (e.Button == shearleftbtn)
            {
                _tnet = Transformations.TranslateBaselineToXaxis(_vertices, _scrnpts, _tnet);
                _tnet = Transformations.ShearHorizontal(_tnet, 0.10);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }

            if (e.Button == shearrightbtn)
            {
                _tnet = Transformations.TranslateBaselineToXaxis(_vertices, _scrnpts, _tnet);
                _tnet = Transformations.ShearHorizontal(_tnet, -0.10);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Refresh();
            }

            if (e.Button == resetbtn)
            {
                RestoreInitialImage();
            }

            if (e.Button == exitbtn)
            {
                Close();
            }
        }

        private void RotateXAsync()
        {
            while (_rotating)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnX(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Thread.Sleep(5);
                CallRefreshToMainThread();
            }
        }

        private void RotateYAsync()
        {
            while (_rotating)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnY(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Thread.Sleep(5);
                CallRefreshToMainThread();
            }
        }

        private void RotateZAsync()
        {
            while (_rotating)
            {
                _tnet = Transformations.TranslateToOrigin(_scrnpts, _tnet);
                _tnet = Transformations.RotateOnZ(_tnet, .05);
                _tnet = Transformations.Translate(_tnet, Transformations.PreviousXPos, Transformations.PreviousYPos, Transformations.PreviousZPos);
                Thread.Sleep(5);
                CallRefreshToMainThread();
            }
        }

        private void CallRefreshToMainThread()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new ThreadStart(this.CallRefreshToMainThread));
            }
            else
            {
                Refresh();
            }
        }

        private void Transformer_ClientSizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Transformer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _rotating = false;
            if (_thread != null)
            {
                _thread.Abort();
                _thread.Join();
            }
        }
    }
}
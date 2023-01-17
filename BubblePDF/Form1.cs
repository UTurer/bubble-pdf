using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EAG_TeknikResimDamgalama
{
    public partial class Form1 : Form
    {   
        private class Stamp
        {
            public float X = -1;
            public float Y = -1;
            //public string name = @"";
            public string text = @"";
            public float margin = -1;
            public float size = -1;
            public int number = -1;
            public string borderShape = @"";
            public float borderWidth = -1;
            public System.Drawing.Font font = null;
            public System.Drawing.Color borderColor = System.Drawing.Color.Empty;
            public System.Drawing.Color fontColor = System.Drawing.Color.Empty;
            public Int32 ID = -1;
            public System.Windows.Forms.TreeNode node = null;
            public System.Drawing.PointF p1 = System.Drawing.Point.Empty;
            public System.Drawing.PointF p2 = System.Drawing.Point.Empty;
            public bool lineCommitted = false;
        }
        
        private System.Collections.Generic.List<Stamp> list1 = null;

        private string openFile = @"";
        private byte insertMode = 0;
        private Form2 frmPreferences = null;
        private bool textBox1_Valid = false;
        private bool textBox2_Valid = false;
        private bool textBox3_Valid = false;
        private bool textBox4_Valid = false;
        private bool textBox5_Valid = false;

        public Form1()
        {
              InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
             //bunu true yapınca focus alan kontrol değil form yakalıyor basılan keyi
            this.KeyPreview = true;

            closeToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            frmPreferences = new Form2();

            this.Size = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form1Size;
            splitContainer1.SplitterDistance = EAG_TeknikResimDamgalama.Properties.Settings.Default.splitContainer1SplitterDistance;
            splitContainer2.SplitterDistance = EAG_TeknikResimDamgalama.Properties.Settings.Default.splitContainer2SplitterDistance;
            frmPreferences.stampFont = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox1Font;
            frmPreferences.stampFontColor = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox1Color;
            frmPreferences.comboBox1SelectedIndex = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2comboBox1SelectedIndex;
            frmPreferences.stampBorderColor = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2panel1BackColor;
            frmPreferences.stampBorderWidth = System.Convert.ToSingle(EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox3Text);
            frmPreferences.stampMargin = System.Convert.ToSingle(EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox2Text);
            frmPreferences.stampSelectionColor = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2panel2BackColor;
            frmPreferences.form2Size = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2Size;
            frmPreferences.splitContainer1SplitterDistance = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2splitContainer1SplitterDistance;
            frmPreferences.insertKey = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2InsertKey;
            frmPreferences.deleteKey = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2DeleteKey;
            frmPreferences.noText = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2NoText;
            frmPreferences.noShape = EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2NoShape;

            frmPreferences.InitializeForm();
            splitContainer2.Panel2.Enabled = false;

            importLayoutToolStripMenuItem.Enabled = false;
            exportLayoutToolStripMenuItem.Enabled = false;


            string nl = System.Environment.NewLine;
            string msg = "A required dll could not be found in the following locations:" + nl;

            //search for pdfium in some default locations
            string dllPath = EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath;
            bool dllLoaded = false;
            try
            {
                Patagames.Pdf.Net.PdfCommon.Initialize(null, dllPath, null);
                EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath = dllPath;
                dllLoaded = true;
            }
            catch(Exception ex)
            {
                msg = msg + dllPath + nl;
                dllLoaded = false;
            }

            if (dllLoaded == false)
            {
                dllPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Pdfium.NET.SDK\x86\pdfium.dll";
            }
            try
            {
                Patagames.Pdf.Net.PdfCommon.Initialize(null, dllPath, null);
                EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath = dllPath;
                dllLoaded = true;
            }
            catch (Exception ex)
            {
                msg = msg + dllPath + nl;
                dllLoaded = false;
            }
           
            if (dllLoaded == false)
            {
                dllPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\Pdfium.NET.SDK\x86\pdfium.dll";

                try
                {
                    Patagames.Pdf.Net.PdfCommon.Initialize(null, dllPath, null);
                    EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath = dllPath;
                    dllLoaded = true;
                }
                catch(Exception ex)
                {
                    msg = msg + dllPath + nl;
                    dllLoaded = false;
                }
            }

            if (dllLoaded == false)
            {
                dllPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Pdfium.NET.SDK\x86\pdfium.dll";

                try
                {
                    Patagames.Pdf.Net.PdfCommon.Initialize(null, dllPath, null);
                    EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath = dllPath;
                    dllLoaded = true;
                }
                catch (Exception ex)
                {
                    msg = msg + dllPath + nl;
                    dllLoaded = false;
                }
            }

            if (dllLoaded == false)
            {
                System.Windows.Forms.DialogResult result1 = System.Windows.Forms.MessageBox.Show(msg + "Could not find pdfium.dll file. Please locate to file to continue.", "WARNING!", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (result1 != System.Windows.Forms.DialogResult.OK)
                {
                    System.Windows.Forms.Application.Exit();
                }

                System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
                dialog1.Filter = "Dll File|*.dll";
                if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    System.Windows.Forms.Application.Exit();
                }

                dllPath = dialog1.FileName;

                try
                {
                    Patagames.Pdf.Net.PdfCommon.Initialize(null, dllPath, null);
                    EAG_TeknikResimDamgalama.Properties.Settings.Default.PdfiumDllPath = dllPath;
                    dllLoaded = true;
                }
                catch (Exception ex)
                {
                    dllLoaded = false;
                }
            }

            if (dllLoaded == false)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "PDF Files|*.pdf|All Files|*.*";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
                return;
            try
            {
                pdfViewer1.LoadDocument(openFileDialog1.FileName);
                this.Text = openFileDialog1.FileName;
                this.openFile = openFileDialog1.FileName;
                this.list1 = new System.Collections.Generic.List<Stamp>();
                closeToolStripMenuItem.Enabled = true;
                //saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                list1.Clear();
                treeView1.Nodes.Clear();
                splitContainer2.Panel2.Enabled = false;
            }
            catch(System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
        }

        private int funGetNextAvailableNumber()
        {
            System.Collections.Generic.List<int> numbers = new System.Collections.Generic.List<int>();

            for (int i = 0; i < this.list1.Count; i++)
            {
                numbers.Add(this.list1[i].number);
            }

            int nextAvailable = Enumerable.Range(1, Int32.MaxValue).Except(numbers).First();
            return (nextAvailable);
        }

        private Int32 funGetNextAvailableID()
        {
            System.Collections.Generic.List<Int32> IDs = new System.Collections.Generic.List<Int32>();

            for (int i = 0; i < this.list1.Count; i++)
            {
                IDs.Add(this.list1[i].ID);
            }

            Int32 nextAvailable = Enumerable.Range(0, Int32.MaxValue).Except(IDs).First();
            return (nextAvailable);
        }

        private void funSortList()
        {
            this.list1.Sort(delegate (Stamp s1, Stamp s2) { return s1.number.CompareTo(s2.number); });

            for (int i = 0; i < this.list1.Count; i++)
            {
                Stamp stamp = this.list1[i];
            }
        }

        private void funApplyChangesToPDF()
        {
            if (list1 != null)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    Stamp stamp = list1[i];

                    float margin = 10;

                    // Convert from PDF coords to WinForm coords.
                    System.Drawing.PointF p2m = new System.Drawing.PointF(stamp.p2.X, stamp.Y + (stamp.Y - stamp.p2.Y));
                    System.Drawing.PointF p1m = new System.Drawing.PointF(stamp.p1.X, stamp.Y + (stamp.Y - stamp.p1.Y));

                    System.Drawing.RectangleF rectf1 = new System.Drawing.RectangleF(stamp.X - stamp.size / 2, stamp.Y - stamp.size / 2, stamp.size, stamp.size);
                    System.Drawing.RectangleF rectf1_m = new System.Drawing.RectangleF(stamp.X - stamp.size / 2, stamp.Y - stamp.size / 2, stamp.size, stamp.size);

                    if (p2m.X < rectf1.Left)
                    {
                        rectf1_m.X = p2m.X;
                        rectf1_m.Width = rectf1_m.Width + (rectf1.Left - p2m.X);
                    }
                    if (p2m.X > rectf1.Right)
                    {
                        rectf1_m.Width = rectf1_m.Width + (p2m.X - rectf1.Right);
                    }
                    if(p2m.Y < rectf1.Top)
                    {
                        rectf1_m.Y = p2m.Y;
                        rectf1_m.Height = rectf1_m.Height + (rectf1.Top - p2m.Y);
                    }
                    if (p2m.Y > rectf1.Bottom)
                    {
                        rectf1_m.Height = rectf1_m.Height + (p2m.Y - rectf1.Bottom);
                    }

                    rectf1_m.X = rectf1_m.X - margin;
                    rectf1_m.Y = rectf1_m.Y - margin;
                    rectf1_m.Width = rectf1_m.Width + 2 * margin;
                    rectf1_m.Height = rectf1_m.Height + 2 * margin;
                    
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)System.Math.Ceiling(rectf1_m.Width), (int)System.Math.Ceiling(rectf1_m.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                    System.Drawing.PointF pStamp_m = new System.Drawing.PointF(stamp.X - rectf1_m.X, stamp.Y - rectf1_m.Y);
                    System.Drawing.PointF p2_m = new System.Drawing.PointF(p2m.X - rectf1_m.X, p2m.Y - rectf1_m.Y);
                    System.Drawing.PointF p1_m = new System.Drawing.PointF(p1m.X - rectf1_m.X, p1m.Y - rectf1_m.Y);
                    System.Drawing.RectangleF rectfStamp_m = new System.Drawing.RectangleF(pStamp_m.X-stamp.size/2, pStamp_m.Y-stamp.size/2, stamp.size,stamp.size);
                    System.Drawing.Rectangle rectStamp_m = new System.Drawing.Rectangle((int)System.Math.Round(rectfStamp_m.X), (int)System.Math.Round(rectfStamp_m.Y), (int)System.Math.Round(rectfStamp_m.Width), (int)System.Math.Round(rectfStamp_m.Height));
                    
                    if(frmPreferences.noShape == false)
                    {
                        System.Drawing.Pen pen = new System.Drawing.Pen(stamp.borderColor, stamp.borderWidth);
                        
                        if (stamp.borderShape == @"Circle")
                        {
                            g.DrawEllipse(pen, rectfStamp_m);
                        }
                        else if (stamp.borderShape == @"Square")
                        {
                            g.DrawRectangle(pen, rectStamp_m);
                        }
                        else if (stamp.borderShape == @"Triangle")
                        {

                        }

         
                        g.DrawLine(pen, p1_m, p2_m);
                    }

                    if(frmPreferences.noText == false)
                    {
                        System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                        sf.LineAlignment = System.Drawing.StringAlignment.Center;
                        sf.Alignment = System.Drawing.StringAlignment.Center;
                        g.DrawString(stamp.text, stamp.font, new System.Drawing.SolidBrush(stamp.fontColor), rectfStamp_m, sf);
                    }
                    
                    System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    IntPtr ptr = bmpData.Scan0;

                    Patagames.Pdf.Net.PdfBitmap pdfbitmap1 = new Patagames.Pdf.Net.PdfBitmap(bmp.Width, bmp.Height, Patagames.Pdf.Enums.BitmapFormats.FXDIB_Argb, ptr, bmpData.Stride);
                    Patagames.Pdf.Net.PdfImageObject imageObj = Patagames.Pdf.Net.PdfImageObject.Create(pdfViewer1.Document, pdfbitmap1, rectf1_m.X, stamp.Y - (rectf1_m.Bottom-stamp.Y));
                    pdfViewer1.Document.Pages[0].PageObjects.Add(imageObj);
                    bmp.UnlockBits(bmpData);
                }
                pdfViewer1.Document.Pages[0].GenerateContent();
                pdfViewer1.ClearRenderBuffer();
                pdfViewer1.Invalidate();
                list1.Clear();
                treeView1.Nodes.Clear();
                splitContainer2.Panel2.Enabled = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            funApplyChangesToPDF();
            try
            {
                pdfViewer1.Document.Save(this.openFile, Patagames.Pdf.Enums.SaveFlags.NoIncremental);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog1 = new System.Windows.Forms.SaveFileDialog();
            dialog1.Filter = "PDF Files|*.pdf|All Files|*.*";
            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            funApplyChangesToPDF();
            try
            {
                pdfViewer1.Document.Save(dialog1.FileName, Patagames.Pdf.Enums.SaveFlags.NoIncremental);
            }
            catch(System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            pdfViewer1.Invalidate();
            this.Text = dialog1.FileName;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list1.Clear();
            list1 = null;
            pdfViewer1.CloseDocument();
            openFile = @"";
            closeToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2.Enabled = false;
            this.Text = @"";
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences.Show();
        }

        private Stamp funFindStampByID(int ID)
        {
            if (list1 == null)
            {
                return (null);
            }

            Stamp result = null;
            for (int i=0;i<list1.Count;i++)
            {
                if(list1[i].ID == ID)
                {
                    result = list1[i];
                }
            }
            return (result);
        }

        private Stamp funFindStampByText(string text)
        {
            if (list1 == null)
            {
                return (null);
            }

            Stamp result = null;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].text == text)
                {
                    result = list1[i];
                }
            }
            return (result);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int id = (Int32)(e.Node.Tag);
            Stamp stamp = funFindStampByID(id);

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(frmPreferences.stampBorderShapes.Cast<Object>().ToArray());

            textBox1.Text = stamp.text;
            textBox1.Tag = stamp.font;
            textBox1.ForeColor = stamp.fontColor;

            comboBox1.SelectedItem = comboBox1.Items[comboBox1.Items.IndexOf(stamp.borderShape)]; ;
            panel1.BackColor = stamp.borderColor;
            textBox2.Text = stamp.borderWidth.ToString();
            textBox3.Text = stamp.margin.ToString();
            textBox4.Text = stamp.X.ToString();
            textBox5.Text = stamp.Y.ToString();


        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Following line is required to select a node with right click.
            treeView1.SelectedNode = e.Node;

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frmAbout = new Form3();
            frmAbout.Show();
        }

        private static Patagames.Pdf.Net.BasicTypes.PdfTypeArray ConvertContentsToArray(Patagames.Pdf.Net.PdfPage page)
        {
            //this function is directly taken from the following link, and then modified:
            //https://forum.patagames.com/posts/t590-GenerateContent-is-SLOW

            Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary pageDict = page.Dictionary;
            Patagames.Pdf.Net.BasicTypes.PdfIndirectList list = Patagames.Pdf.Net.BasicTypes.PdfIndirectList.FromPdfDocument(page.Document);

            if (!pageDict.ContainsKey("Contents"))
            {
                Patagames.Pdf.Net.BasicTypes.PdfTypeArray array = Patagames.Pdf.Net.BasicTypes.PdfTypeArray.Create();
                //Add array into list of indirect objects
                list.Add(array);
                //And set it as a contents of the page
                pageDict.SetIndirectAt("Contents", list, array);
                return array;
            }

            Patagames.Pdf.Net.BasicTypes.PdfTypeBase contents = pageDict["Contents"];

            //check the original content whether it's an array
            if (contents is Patagames.Pdf.Net.BasicTypes.PdfTypeArray)
            {
                return contents as Patagames.Pdf.Net.BasicTypes.PdfTypeArray;  //if contents is a array just return it
            } 
            else if (contents is Patagames.Pdf.Net.BasicTypes.PdfTypeIndirect)
            {
                if ((contents as Patagames.Pdf.Net.BasicTypes.PdfTypeIndirect).Direct is Patagames.Pdf.Net.BasicTypes.PdfTypeArray)
                {
                    return (contents as Patagames.Pdf.Net.BasicTypes.PdfTypeIndirect).Direct as Patagames.Pdf.Net.BasicTypes.PdfTypeArray; //if contents is a reference to array then return that array
                }
                else if ((contents as Patagames.Pdf.Net.BasicTypes.PdfTypeIndirect).Direct is Patagames.Pdf.Net.BasicTypes.PdfTypeStream)
                {
                    //if contents is a reference to a stream then create a new array and insert stream as a first element of array
                    Patagames.Pdf.Net.BasicTypes.PdfTypeArray array = Patagames.Pdf.Net.BasicTypes.PdfTypeArray.Create();
                    array.AddIndirect(list, (contents as Patagames.Pdf.Net.BasicTypes.PdfTypeIndirect).Direct);
                    //Add array into list of indirect objects
                    list.Add(array);
                    //And set it as a contents of the page
                    pageDict.SetIndirectAt("Contents", list, array);
                    return array;
                }
                else
                {
                    throw new Exception("Unexpected content type");
                } 
            }
            else if (contents is Patagames.Pdf.Net.BasicTypes.PdfTypeStream)
            {
                //if contents is a stream instead of reference to a stream then try to convert it to a reference then create a new array and insert stream as a first element of array
                list.Add(contents);
                Patagames.Pdf.Net.BasicTypes.PdfTypeArray array = Patagames.Pdf.Net.BasicTypes.PdfTypeArray.Create();
                array.AddIndirect(list, contents);
                //Add array into list of indirect objects
                list.Add(array);
                //And set it as a contents of the page
                pageDict.SetIndirectAt("Contents", list, array);
                return array;
            }
            else
            {
                throw new Exception("Unexpected content type");
            }   
        }

        private static Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary FindResource(Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary dict)
        {
            if (dict.ContainsKey("Resources"))
                return dict["Resources"].As<Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary>();

            if (dict.ContainsKey("Parent"))
                return FindResource(dict["Parent"].As<Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary>());

            dict["Resources"] = Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary.Create();
            return dict["Resources"].As<Patagames.Pdf.Net.BasicTypes.PdfTypeDictionary>();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stamp stamp = list1.Find(x => x.ID == (Int32)(treeView1.SelectedNode.Tag));
            list1.Remove(stamp);
            treeView1.Nodes.Remove(stamp.node);
            
            if(treeView1.Nodes.Count == 0)
            {
                splitContainer2.Panel2.Enabled = false;
            }
            pdfViewer1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Text = textBox1.Font.Name;
            System.Windows.Forms.FontDialog dialog1 = new System.Windows.Forms.FontDialog();

            dialog1.ShowColor = true;
            dialog1.ShowEffects = true;
            dialog1.Font = (System.Drawing.Font)textBox1.Tag;
            dialog1.Color = textBox1.ForeColor;

            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            textBox1.Tag = dialog1.Font;
            textBox1.ForeColor = dialog1.Color;

            Stamp stamp = list1.Find(x => x.ID == (Int32)(treeView1.SelectedNode.Tag));
            stamp.font = dialog1.Font;
            stamp.fontColor = dialog1.Color;

            float sizeInPix = stamp.font.SizeInPoints / 72 * 96;
            int sizeInDesignUnits = stamp.font.FontFamily.GetEmHeight(stamp.font.Style);
            int sizeAscentInDesignUnits = stamp.font.FontFamily.GetCellAscent(stamp.font.Style);
            int sizeDescentInDesignUnits = stamp.font.FontFamily.GetCellDescent(stamp.font.Style);
            float margin = stamp.margin;
            sizeInPix = ((float)sizeAscentInDesignUnits + (float)sizeDescentInDesignUnits) / (float)sizeInDesignUnits * sizeInPix + 2 * margin;
            float lineWidth = stamp.borderWidth;
            sizeInPix = sizeInPix + lineWidth;
            stamp.size = sizeInPix;

            float r = stamp.size / 2;
            double ang = System.Math.Atan2((double)(stamp.p2.Y - stamp.Y), (double)(stamp.p2.X - stamp.X));
            System.Drawing.PointF p1 = new System.Drawing.PointF(stamp.X + r * (float)System.Math.Cos(ang), stamp.Y + r * (float)System.Math.Sin(ang));
            stamp.p1 = p1;

            pdfViewer1.Invalidate();
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            //everything is allowed.
            textBox1_Valid = true;
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
            button2.Enabled = true;

            if (textBox1_Valid == false)
            {
                button2.Enabled = false;
                return;
            }
                
            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.text = textBox1.Text;
            stamp.node.Text = stamp.text;
            pdfViewer1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Stamp2 stamp = list2.Find(x => x.text == treeView1.SelectedNode.Text);
            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.borderShape = comboBox1.Items[comboBox1.SelectedIndex].ToString();

            pdfViewer1.Invalidate();
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog1 = new System.Windows.Forms.ColorDialog();
            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            panel1.BackColor = dialog1.Color;

            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.borderColor = dialog1.Color;
            pdfViewer1.Invalidate();
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            textBox2_Valid = false;
            try
            {
                int val = System.Convert.ToInt32(textBox2.Text);
                if(val<1)
                {
                    textBox2_Valid = false;
                }
                else
                {
                    textBox2_Valid = true;
                }
            }
            catch
            {
                textBox2_Valid = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
            button2.Enabled = true;

            if (textBox2_Valid == false)
            {
                button2.Enabled = false;
                return;
            }
                
            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.borderWidth = System.Convert.ToInt32(textBox2.Text);
            pdfViewer1.Invalidate();
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {

            textBox3_Valid = false;
            try
            {
                float val = System.Convert.ToSingle(textBox3.Text);
                if (val < 0)
                {
                    textBox3_Valid = false;
                }
                else
                {
                    textBox3_Valid = true;
                }
            }
            catch
            {
                textBox3_Valid = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
            button2.Enabled = true;

            if (textBox3_Valid == false)
            {
                button2.Enabled = false;
                return;
            }
                

            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.margin = System.Convert.ToSingle(textBox3.Text);

            float sizeInPix = stamp.font.SizeInPoints / 72 * 96;
            int sizeInDesignUnits = stamp.font.FontFamily.GetEmHeight(stamp.font.Style);
            int sizeAscentInDesignUnits = stamp.font.FontFamily.GetCellAscent(stamp.font.Style);
            int sizeDescentInDesignUnits = stamp.font.FontFamily.GetCellDescent(stamp.font.Style);
            float margin = stamp.margin;
            sizeInPix = ((float)sizeAscentInDesignUnits + (float)sizeDescentInDesignUnits) / (float)sizeInDesignUnits * sizeInPix + 2 * margin;
            float lineWidth = stamp.borderWidth;
            sizeInPix = sizeInPix + lineWidth;
            stamp.size = sizeInPix;

            float r = stamp.size / 2;
            double ang = System.Math.Atan2((double)(stamp.p2.Y - stamp.Y), (double)(stamp.p2.X - stamp.X));
            System.Drawing.PointF p1 = new System.Drawing.PointF(stamp.X + r * (float)System.Math.Cos(ang), stamp.Y + r * (float)System.Math.Sin(ang));
            stamp.p1 = p1;

            pdfViewer1.Invalidate();
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            textBox4_Valid = false;
            try
            {
                float val = System.Convert.ToSingle(textBox4.Text);
                textBox4_Valid = true;
            }
            catch
            {
                textBox4_Valid = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
            button2.Enabled = true;

            if (textBox4_Valid == false)
            {
                button2.Enabled = false;
                return;
            }
                

            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.X = System.Convert.ToSingle(textBox4.Text);
            pdfViewer1.Invalidate();
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            textBox5_Valid = false;
            try
            {
                float val = System.Convert.ToSingle(textBox5.Text);
                textBox5_Valid = true;
            }
            catch
            {
                textBox5_Valid = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
            button2.Enabled = true;

            if (textBox5_Valid == false)
            {
                button2.Enabled = false;
                return;
            }
                

            Stamp stamp = funFindStampByID((Int32)(treeView1.SelectedNode.Tag));
            stamp.Y = System.Convert.ToSingle(textBox5.Text);
            pdfViewer1.Invalidate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form1Size = this.Size;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.splitContainer1SplitterDistance = splitContainer1.SplitterDistance;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.splitContainer2SplitterDistance = splitContainer2.SplitterDistance;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox1Font = frmPreferences.stampFont;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox1Color = frmPreferences.stampFontColor;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2comboBox1SelectedIndex = frmPreferences.comboBox1SelectedIndex;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2panel1BackColor = frmPreferences.stampBorderColor;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox3Text = frmPreferences.stampBorderWidth.ToString();
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2textBox2Text = frmPreferences.stampMargin.ToString();
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2panel2BackColor = frmPreferences.stampSelectionColor;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2Size = frmPreferences.form2Size;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2splitContainer1SplitterDistance = frmPreferences.splitContainer1SplitterDistance;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2InsertKey = frmPreferences.insertKey;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2DeleteKey = frmPreferences.deleteKey;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2NoText = frmPreferences.noText;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Form2NoShape = frmPreferences.noShape;
            EAG_TeknikResimDamgalama.Properties.Settings.Default.Save();
        }

        private void exportLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog1 = new System.Windows.Forms.SaveFileDialog();
            dialog1.Filter = @"Stamp Layout File|*.stamp|All Files|*.*";
            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            using (System.IO.Stream stream = System.IO.File.Open(dialog1.FileName, System.IO.FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, list1);
                stream.Close();
            }
        }

        private void importLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.DialogResult result1 = System.Windows.Forms.MessageBox.Show(@"This will erase all current stamps. Do you want to continue?", "WARNING!", System.Windows.Forms.MessageBoxButtons.YesNo);
            //if (result1 == System.Windows.Forms.DialogResult.No)
            //    return;

            //System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
            //dialog1.Filter = @"Stamp Layout File|*.stamp|All Files|*.*";
            //if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //    return;

            //for (int i = 0; i < list2.Count; i++)
            //{
            //    Stamp stamp1 = list1[i];
            //    stamp1.page.PageObjects.Remove(stamp1.imgobj);
            //    stamp1.page.GenerateContent();
            //}

            //list1.Clear();
            //treeView1.Nodes.Clear();

            //using (System.IO.Stream stream = System.IO.File.Open(dialog1.FileName, System.IO.FileMode.Open))
            //{
            //    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                
            //    list1 = (System.Collections.Generic.List<Stamp>)binaryFormatter.Deserialize(stream);
                
            //}

            //for (int i = 0; i < list1.Count; i++)
            //{
            //    Stamp stamp1 = list1[i];
            //    DrawStamp(stamp1);
            //    System.Windows.Forms.TreeNode node1 = treeView1.Nodes.Add(stamp1.text);
            //    node1.Tag = stamp1.ID;

            //}
            //if (treeView1.Nodes.Count>0)
            //{
            //    splitContainer2.Panel2.Enabled = true;
            //}        
        }

        private void pdfViewer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pdfViewer1.Document == null)
                return;

            pdfViewer1.Invalidate();
        }

        private void pdfViewer1_Paint(object sender, PaintEventArgs e)
        {
            if (pdfViewer1.Document == null)
                return;

            System.Drawing.Rectangle rect1 = pdfViewer1.CalcActualRect(0);
            System.Drawing.Point p1_client = new System.Drawing.Point(rect1.Left, rect1.Top);
            System.Drawing.Point p2_client = new System.Drawing.Point(rect1.Right, rect1.Bottom);
            System.Drawing.PointF p1_page = pdfViewer1.ClientToPage(0,p1_client);
            System.Drawing.PointF p2_page = pdfViewer1.ClientToPage(0,p2_client);
            double diagonal_page = System.Math.Sqrt(System.Math.Pow(p1_page.X-p2_page.X,2)+ System.Math.Pow(p1_page.Y - p2_page.Y, 2));
            double diagonal_client = System.Math.Sqrt(System.Math.Pow(p1_client.X - p2_client.X, 2) + System.Math.Pow(p1_client.Y - p2_client.Y, 2));
            double scale = diagonal_client / diagonal_page;
           
            if (insertMode == 0) // draw preview of stamp
            {
                if (frmPreferences.noShape == false)
                {
                    System.Drawing.PointF p1 = pdfViewer1.PointToClient(System.Windows.Forms.Cursor.Position);
                    System.Drawing.Graphics g = e.Graphics;
                    float size = frmPreferences.stampSizeInPixels;
                    size = size * (float)scale;
                    float borderWidth = frmPreferences.stampBorderWidth;
                    borderWidth = borderWidth * (float)scale;

                    System.Drawing.Pen pen = new System.Drawing.Pen(frmPreferences.stampBorderColor, borderWidth);
                    g.DrawEllipse(pen, p1.X - size / 2, p1.Y - size / 2, size, size);
                }
                else
                {
                    System.Drawing.PointF p1 = pdfViewer1.PointToClient(System.Windows.Forms.Cursor.Position);
                    System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                    sf.LineAlignment = System.Drawing.StringAlignment.Center;
                    sf.Alignment = System.Drawing.StringAlignment.Center;
                    float size = frmPreferences.stampSizeInPixels;
                    size = size * (float)scale;
                    float borderWidth = frmPreferences.stampBorderWidth;
                    borderWidth = borderWidth * (float)scale;
                    
                    int num = funGetNextAvailableNumber();

                    
                    System.Drawing.RectangleF rectf = new System.Drawing.RectangleF(p1.X - size / 2, p1.Y - size / 2, size, size);
                    System.Drawing.Font scaledFont = new System.Drawing.Font(frmPreferences.stampFont.Name, frmPreferences.stampFont.Size * (float)scale, frmPreferences.stampFont.Style, frmPreferences.stampFont.Unit);

                    System.Drawing.Graphics g = e.Graphics;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    g.DrawString(num.ToString(), scaledFont, new System.Drawing.SolidBrush(frmPreferences.stampFontColor), rectf, sf);

                }


            }
            else if (insertMode == 1) //draw preview of line of stamp
            {
                Stamp stamp = unfinishedStamp;
                System.Drawing.PointF p1 = pdfViewer1.PointToClient(System.Windows.Forms.Cursor.Position);
                System.Drawing.PointF p2 = new System.Drawing.PointF(stamp.X, stamp.Y);
                System.Drawing.PointF p3 = pdfViewer1.PageToClient(0, p2);
                float size = frmPreferences.stampSizeInPixels;
                size = size * (float)scale;

                float r = size / 2;
                double ang = System.Math.Atan2((double)(p1.Y - p3.Y), (double)(p1.X - p3.X));
                System.Drawing.PointF p4 = new System.Drawing.PointF(p3.X + r * (float)System.Math.Cos(ang), p3.Y + r * (float)System.Math.Sin(ang));

                System.Drawing.Graphics g = e.Graphics;
                float borderWidth = frmPreferences.stampBorderWidth;
                borderWidth = borderWidth * (float)scale;

                System.Drawing.Pen pen = new System.Drawing.Pen(frmPreferences.stampBorderColor, borderWidth);
                g.DrawLine(pen, p4, p1); 
            }
                        
            if (list1 != null)
            {
                if (unfinishedStamp != null)
                {
                    list1.Add(unfinishedStamp);
                }
                for (int i = 0; i<list1.Count;i++)
                {
                    Stamp stamp = list1[i];
                    System.Drawing.PointF p2 = new System.Drawing.PointF(stamp.X, stamp.Y);
                    System.Drawing.PointF p3 = pdfViewer1.PageToClient(0, p2);

                    float size = stamp.size;
                    size = size * (float)scale;

                    float borderWidth = stamp.borderWidth;
                    borderWidth = borderWidth * (float)scale;

                    System.Drawing.Pen pen = new System.Drawing.Pen(stamp.borderColor, borderWidth);

                    System.Drawing.Graphics g = e.Graphics;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                    if (frmPreferences.noShape == false)
                    {
                        if (stamp.borderShape == @"Circle")
                        {
                            g.DrawEllipse(pen, p3.X - size / 2, p3.Y - size / 2, size, size);
                        }
                        else if (stamp.borderShape == @"Square")
                        {
                            g.DrawRectangle(pen, p3.X - size / 2, p3.Y - size / 2, size, size);
                        }
                        else if (stamp.borderShape == @"Triangle")
                        {

                        }

                        int son = list1.Count;
                        if (insertMode == 1)
                        {
                            son = list1.Count - 1;
                        }

                        if (i < son)
                        {
                            float r = size / 2;
                            System.Drawing.PointF p4 = pdfViewer1.PageToClient(0, stamp.p2);
                            double ang = System.Math.Atan2((double)(p4.Y - p3.Y), (double)(p4.X - p3.X));
                            System.Drawing.PointF p5 = new System.Drawing.PointF(p3.X + r * (float)System.Math.Cos(ang), p3.Y + r * (float)System.Math.Sin(ang));
                            g.DrawLine(pen, p5, p4);
                        }
                    }
                    
                    if (frmPreferences.noText == false)
                    {
                        System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                        sf.LineAlignment = System.Drawing.StringAlignment.Center;
                        sf.Alignment = System.Drawing.StringAlignment.Center;
                        System.Drawing.RectangleF rectf = new System.Drawing.RectangleF(p3.X - size / 2, p3.Y - size / 2, size, size);
                        System.Drawing.Font scaledFont = new System.Drawing.Font(stamp.font.Name, stamp.font.Size * (float)scale, stamp.font.Style, stamp.font.Unit);
                        g.DrawString(stamp.text, scaledFont, new System.Drawing.SolidBrush(stamp.fontColor), rectf, sf);
                    }

                }
                if (unfinishedStamp != null)
                {
                    list1.Remove(unfinishedStamp);
                }
            }
           
        }

        private Stamp unfinishedStamp = null;

        private void Form1_KeyUp(object sender, KeyEventArgs e) 
        {
            if (pdfViewer1.Document == null)
                return;

            if (e.KeyData == frmPreferences.insertKey)
            {
                if (insertMode == 0)
                {
                    System.Drawing.Point p1 = System.Windows.Forms.Cursor.Position;
                    System.Drawing.Point p2 = pdfViewer1.PointToClient(p1);
                    System.Drawing.PointF p3 = pdfViewer1.ClientToPage(0, p2);
                    //System.Drawing.Point p3 = new System.Drawing.Point(p2.X + pdfViewer1.HorizontalScroll.Value, p2.Y + pdfViewer1.VerticalScroll.Value);

                    Stamp stamp = new Stamp();
                    stamp.borderColor = frmPreferences.stampBorderColor;
                    stamp.borderShape = frmPreferences.stampBorderShape;
                    stamp.borderWidth = frmPreferences.stampBorderWidth;
                    stamp.font = frmPreferences.stampFont;
                    stamp.fontColor = frmPreferences.stampFontColor;
                    stamp.ID = funGetNextAvailableID();
                    stamp.margin = frmPreferences.stampMargin;
                    stamp.size = frmPreferences.stampSizeInPixels;
                    stamp.X = p3.X;
                    stamp.Y = p3.Y;
                    stamp.number = funGetNextAvailableNumber();
                    stamp.text = stamp.number.ToString();

                    unfinishedStamp = stamp;
                    
                    if(frmPreferences.noShape == true)
                    {
                        list1.Add(unfinishedStamp);

                        System.Windows.Forms.TreeNode node1 = treeView1.Nodes.Add(unfinishedStamp.text);
                        node1.Tag = unfinishedStamp.ID;
                        unfinishedStamp.node = node1;
                        treeView1.SelectedNode = node1;

                        splitContainer2.Panel2.Enabled = true;
                        unfinishedStamp = null;
                    }
                    else
                    {
                        insertMode = 1;
                    }
                    
                }
                else if (insertMode == 1)
                {
                    System.Drawing.PointF p2 = pdfViewer1.ClientToPage(0, pdfViewer1.PointToClient(System.Windows.Forms.Cursor.Position));

                    float r = unfinishedStamp.size / 2;
                    double ang = System.Math.Atan2((double)(p2.Y - unfinishedStamp.Y), (double)(p2.X - unfinishedStamp.X));
                    System.Drawing.PointF p1 = new System.Drawing.PointF(unfinishedStamp.X + r * (float)System.Math.Cos(ang), unfinishedStamp.Y + r * (float)System.Math.Sin(ang));

                    unfinishedStamp.p1 = p1;
                    unfinishedStamp.p2 = p2;

                    list1.Add(unfinishedStamp);
                   
                    System.Windows.Forms.TreeNode node1 = treeView1.Nodes.Add(unfinishedStamp.text);
                    node1.Tag = unfinishedStamp.ID;
                    unfinishedStamp.node = node1;
                    treeView1.SelectedNode = node1;

                    splitContainer2.Panel2.Enabled = true;
                    //richTextBox1.AppendText(stamp.X.ToString() + " " + stamp.Y.ToString()+"\n");

                    insertMode = 0;
                    unfinishedStamp = null;
                }

                pdfViewer1.Invalidate();

            }
            else if (e.KeyData == frmPreferences.deleteKey)
            {
                if (insertMode == 1)
                {
                    unfinishedStamp = null;
                    insertMode = 0;
                }
                else
                {
                    if (list1.Count > 0)
                    {
                        Stamp stamp = list1[list1.Count - 1];
                        treeView1.Nodes.RemoveAt(stamp.node.Index);
                        list1.RemoveAt(list1.Count - 1);
                        
                    }

                }
                if (list1.Count == 0)
                {
                    splitContainer2.Panel2.Enabled = false;
                }
                pdfViewer1.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            for (int i=0;i<list1.Count;i++)
            {
                Stamp stamp = list1[i];
                stamp.font = (System.Drawing.Font)textBox1.Tag;
                stamp.fontColor = textBox1.ForeColor;
                stamp.borderShape = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                stamp.borderColor = panel1.BackColor;
                stamp.borderWidth = System.Convert.ToInt32(textBox2.Text);
                stamp.margin = System.Convert.ToSingle(textBox3.Text);

                float sizeInPix = stamp.font.SizeInPoints / 72 * 96;
                int sizeInDesignUnits = stamp.font.FontFamily.GetEmHeight(stamp.font.Style);
                int sizeAscentInDesignUnits = stamp.font.FontFamily.GetCellAscent(stamp.font.Style);
                int sizeDescentInDesignUnits = stamp.font.FontFamily.GetCellDescent(stamp.font.Style);
                float margin = stamp.margin;
                sizeInPix = ((float)sizeAscentInDesignUnits + (float)sizeDescentInDesignUnits) / (float)sizeInDesignUnits * sizeInPix + 2 * margin;
                float lineWidth = stamp.borderWidth;
                sizeInPix = sizeInPix + lineWidth;
                stamp.size = sizeInPix;

                float r = stamp.size / 2;
                double ang = System.Math.Atan2((double)(stamp.p2.Y - stamp.Y), (double)(stamp.p2.X - stamp.X));
                System.Drawing.PointF p1 = new System.Drawing.PointF(stamp.X + r * (float)System.Math.Cos(ang), stamp.Y + r * (float)System.Math.Sin(ang));
                stamp.p1 = p1;
            }
            pdfViewer1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPreferences.stampFont = (System.Drawing.Font)textBox1.Tag;
            frmPreferences.stampFontColor = textBox1.ForeColor;
            frmPreferences.comboBox1SelectedIndex = comboBox1.SelectedIndex;
            frmPreferences.stampBorderColor = panel1.BackColor;
            frmPreferences.stampBorderWidth = (float)System.Convert.ToDouble(textBox2.Text);
            frmPreferences.stampMargin = (float)System.Convert.ToDouble(textBox3.Text);
            frmPreferences.InitializeForm();
        }

    }
}

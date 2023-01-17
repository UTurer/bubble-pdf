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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private bool formInitialized = false;
        public float stampSizeInPixels = -1;
        public string stampBorderShape = @"";
        public System.Drawing.Color stampBorderColor = System.Drawing.Color.Empty;
        public System.Drawing.Font stampFont = null;
        public System.Drawing.Color stampFontColor = System.Drawing.Color.Empty;
        public float stampBorderWidth = -1;
        public float stampMargin = -1;
        public System.Windows.Forms.ComboBox.ObjectCollection stampBorderShapes = null;
        public System.Drawing.Color stampSelectionColor = System.Drawing.Color.Empty;
        public int comboBox1SelectedIndex = -1;
        public System.Drawing.Size form2Size = System.Drawing.Size.Empty;
        public int splitContainer1SplitterDistance = -1;
        public System.Windows.Forms.Keys insertKey;
        public System.Windows.Forms.Keys deleteKey;
        public bool noText = false;
        public bool noShape = false;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void InitializeForm()
        {
            tabControl1.Visible = false;
            treeView1.SelectedNode = treeView1.Nodes[0];
            System.Drawing.FontFamily fontFamily = new System.Drawing.FontFamily(@"Tahoma");
            System.Drawing.Font font = new System.Drawing.Font(fontFamily, 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            textBox1.Tag = this.stampFont;
            textBox1.ForeColor = this.stampFontColor;
            textBox1.Text = @"123";
            comboBox1.SelectedIndex = this.comboBox1SelectedIndex;
            textBox2.Text = this.stampMargin.ToString();
            panel1.BackColor = this.stampBorderColor;
            textBox3.Text = this.stampBorderWidth.ToString();
            this.stampBorderShapes = comboBox1.Items;
            panel2.BackColor = this.stampSelectionColor;
            this.Size = this.form2Size;
            splitContainer1.SplitterDistance = this.splitContainer1SplitterDistance;
            label8.Text = this.insertKey.ToString();
            label9.Text = this.deleteKey.ToString();
            checkBox1.Checked = this.noText;
            checkBox2.Checked = this.noShape;

            this.formInitialized = true;
            funUpdatePictureBox();
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //show selected panel
            string name2 = e.Node.Name.Substring(4);
            string panelName = @"pnl" + name2;

            // hide all panels.
            System.Collections.Generic.List<System.Windows.Forms.Control> panelList = GetAllChildControls(this, typeof(System.Windows.Forms.Panel));
            foreach (System.Windows.Forms.Control c in panelList)
            {
                System.Windows.Forms.Panel p = (System.Windows.Forms.Panel)c;
                if (p.Name == panelName)
                {
                    p.Parent = splitContainer1.Panel2;
                    p.Visible = true;
                }
                else
                {
                    if (p.Name.Length > 3)
                        if (p.Name.Substring(0, 3) == @"pnl")
                        {
                            string name1 = p.Name.Substring(3);
                            string tabname1 = @"tab" + name1;
                            p.Parent = tabControl1.TabPages[tabname1];
                        }
                }
            }
        }

        private System.Collections.Generic.IEnumerable<System.Windows.Forms.Control> GetAllChildControls2(System.Windows.Forms.Control parentControl, System.Type type)
        {
            var controls = parentControl.Controls.Cast<System.Windows.Forms.Control>();

            return controls.SelectMany(ctrl => GetAllChildControls2(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private System.Collections.Generic.List<System.Windows.Forms.Control> GetAllChildControls(System.Windows.Forms.Control parentControl, System.Type type)
        {
            System.Collections.Generic.List<System.Windows.Forms.Control> controlList = new System.Collections.Generic.List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control c in parentControl.Controls)
            {
                controlList.AddRange(GetAllChildControls(c, type));
                if (c.GetType() == type)
                    controlList.Add(c);
            }
            return controlList;
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
            this.stampFont = dialog1.Font;
            this.stampFontColor = dialog1.Color;
            funUpdatePictureBox();
        }

        private void funUpdatePictureBox()
        {
            if (formInitialized == false)
                return;

            System.Drawing.Font font = (System.Drawing.Font)textBox1.Tag;
            float sizeInPix = font.SizeInPoints / 72 * 96;
            int sizeInDesignUnits = font.FontFamily.GetEmHeight(font.Style);
            int sizeAscentInDesignUnits = font.FontFamily.GetCellAscent(font.Style);
            int sizeDescentInDesignUnits = font.FontFamily.GetCellDescent(font.Style);
            float margin = System.Convert.ToSingle(textBox2.Text);
            sizeInPix = ((float)sizeAscentInDesignUnits + (float)sizeDescentInDesignUnits) / (float)sizeInDesignUnits * sizeInPix + 2 * margin;
            float lineWidth = System.Convert.ToSingle(textBox3.Text);
            sizeInPix = sizeInPix + lineWidth;
            this.stampSizeInPixels = sizeInPix;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)sizeInPix, (int)sizeInPix, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.RectangleF rectf = new System.Drawing.RectangleF(lineWidth / 2, lineWidth / 2, sizeInPix - lineWidth, sizeInPix - lineWidth);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);

            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            if(this.noShape == false)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    g.DrawEllipse(new System.Drawing.Pen(panel1.BackColor, lineWidth), rectf);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    g.DrawRectangle(new System.Drawing.Pen(panel1.BackColor, lineWidth), rectf.X, rectf.Y, rectf.Width, rectf.Height);
                }
                else if (comboBox1.SelectedIndex == 2)
                {

                }
            }
            
            if (this.noText == false)
            {
                System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                sf.Alignment = System.Drawing.StringAlignment.Center;
                g.DrawString(@"99", (System.Drawing.Font)textBox1.Tag, new System.Drawing.SolidBrush(textBox1.ForeColor), rectf, sf);
            }
            
            //g.Flush();
            pictureBox1.Image = bmp;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog dialog1 = new System.Windows.Forms.ColorDialog();
            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            panel1.BackColor = dialog1.Color;
            this.stampBorderColor = dialog1.Color;
            funUpdatePictureBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1SelectedIndex = comboBox1.SelectedIndex;
            this.stampBorderShape = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            funUpdatePictureBox();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.stampBorderWidth = System.Convert.ToSingle(textBox3.Text);
            funUpdatePictureBox();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.stampMargin = System.Convert.ToSingle(textBox2.Text);
            funUpdatePictureBox();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.form2Size = this.Size;
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {

            System.Windows.Forms.ColorDialog dialog1 = new System.Windows.Forms.ColorDialog();
            if (dialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            panel2.BackColor = dialog1.Color;
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            this.form2Size = this.Size;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.splitContainer1SplitterDistance = splitContainer1.SplitterDistance;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == @"Assign Key")
            {
                button2.Text = @"Cancel";
                button3.Text = @"Assign Key";
                this.KeyPreview = true;
            }
            else
            {
                button2.Text = @"Assign Key";
                this.KeyPreview = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == @"Assign Key")
            {
                button3.Text = @"Cancel";
                button2.Text = @"Assign Key";
                this.KeyPreview = true;
            }
            else
            {
                button3.Text = @"Assign Key";
                this.KeyPreview = false;
            }
        }
        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (button2.Text == @"Cancel")
            {
                label8.Text = e.KeyCode.ToString();
                this.insertKey = e.KeyCode;
                button2.Text = @"Assign Key";
            }
            if(button3.Text == @"Cancel")
            {
                this.deleteKey = e.KeyCode;
                label9.Text = e.KeyCode.ToString();
                button3.Text = @"Assign Key";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.noText = checkBox1.Checked;
            funUpdatePictureBox();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.noShape = checkBox2.Checked;
            funUpdatePictureBox();
        }
    }
}

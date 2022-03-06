using System;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class ColumnsEdtitor : Form
    {
        Form1 mainForm;
        Label lbWidth = new Label();
        public ColumnsEdtitor(Form1 fm)
        {
            InitializeComponent();
            mainForm = fm;
        }

        private void ColumnsEdtitor_Load(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnT in mainForm.MainListView.Columns)
            {
                if (columnT.Width > 1)
                    checkedListBox1.Items.Add(columnT.Text, true);
                else
                    checkedListBox1.Items.Add(columnT.Text, false);
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex >= 0)
            {
                label1.Width = mainForm.MainListView.Columns[checkedListBox1.SelectedIndex].Width;
                label1.Text = mainForm.MainListView.Columns[checkedListBox1.SelectedIndex].Text;
                lbWidth.Left = label1.Left + label1.Width + 2;
                lbWidth.Text = label1.Width.ToString() + " px";
                lbWidth.Top = label1.Top;
                lbWidth.MouseClick += ColumnWidthMouseMover;
                this.panel1.Controls.Add(lbWidth);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    if (mainForm.MainListView.Columns[i].Width < 5)
                        mainForm.MainListView.Columns[i].Width = 80;
                }
                else
                    mainForm.MainListView.Columns[i].Width = 0;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            ColumnWidthMouseMover(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            ColumnWidthMouseMover(sender, e);
        }
        void ColumnWidthMouseMover(object sender, EventArgs e)
        {
            label1.Width = Cursor.Position.X - this.Left - panel1.Left;
            if (checkedListBox1.SelectedIndex >= 0)
            {
                mainForm.MainListView.Columns[checkedListBox1.SelectedIndex].Width = label1.Width;
                lbWidth.Left = label1.Left + label1.Width + 2;
                lbWidth.Text = label1.Width.ToString() + " px";
                lbWidth.Top = label1.Top;
            }

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOverPointer();
        }
        void MouseOverPointer()
        {
            label1.Width = Cursor.Position.X - this.Left - panel1.Left;
            lbWidth.Left = label1.Left + label1.Width + 2;
            lbWidth.Text = label1.Width.ToString() + " px";
            lbWidth.Top = label1.Top;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOverPointer();
        }

        private void ColumnsEdtitor_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkedListBox1.SelectedIndex >= 0)
                label1.Width = mainForm.MainListView.Columns[checkedListBox1.SelectedIndex].Width;
            lbWidth.Left = label1.Left + label1.Width + 2;
            lbWidth.Text = label1.Width.ToString() + " px";
            lbWidth.Top = label1.Top;
        }
    }
}

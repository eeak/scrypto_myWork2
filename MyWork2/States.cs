using System;
using System.Data;
using System.Windows.Forms;

namespace MyWork2
{
    public partial class States : Form
    {
        Form1 mainForm;
        string id_client;
        Editor editorForm;
        public States(Form1 mf, string id_client, Editor ed1)
        {
            InitializeComponent();
            mainForm = mf;
            this.id_client = id_client;
            editorForm = ed1;
        }

        private void States_Load(object sender, EventArgs e)
        {
            mainForm.basa.StatesMapTable_Create();
            ListViewWriter(id_client);

        }
        public void ListViewWriter(string id_clent)
        {
            try
            {
                StockListView.Items.Clear();

                DataTable dtStates1;
                dtStates1 = mainForm.basa.StatesMapGiver(id_client);
                for (int i = 0; i < dtStates1.Rows.Count; i++)
                {
                    ListViewItem newitem = new ListViewItem((i + 1).ToString());
                    newitem.SubItems.Add(dtStates1.Rows[i].ItemArray[2].ToString());
                    newitem.SubItems.Add(dtStates1.Rows[i].ItemArray[3].ToString());
                    newitem.SubItems.Add(dtStates1.Rows[i].ItemArray[0].ToString());
                    StockListView.Items.Add(newitem);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        private void DeleteStateButton1_Click(object sender, EventArgs e)
        {
            if (StockListView.SelectedIndices.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить статус номер: " + StockListView.Items[StockListView.SelectedIndices[0]].SubItems[0].Text, "Вы уверены?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mainForm.basa.StatesMapDelete(StockListView.Items[StockListView.SelectedIndices[0]].SubItems[3].Text);
                    ListViewWriter(id_client);
                    editorForm.DynamicLabelMaker();
                }
            }
            else
                MessageBox.Show("Не выделено ни одной строки для удаления");
        }

        private void States_FormClosed(object sender, FormClosedEventArgs e)
        {
            editorForm.Enabled = true;
        }
    }
}

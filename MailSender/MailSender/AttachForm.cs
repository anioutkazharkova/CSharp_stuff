using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MailSender
{
    public delegate void DelAttach(Object sender);

    public partial class AttachForm : Form
    {
        List<string> attachItems;        
        Button btnDelete;
        int selectedItemIndex;
        MessageForm form;

        DelAttach del;

        public AttachForm(List<string> items,MessageForm form)
        {
            this.form = form;
           
            attachItems = new List<string>();
            foreach (string item in items)
            {
                FileInfo file = new FileInfo(item);

                attachItems.Add(file.Name);
            }
            InitializeComponent();

            selectedItemIndex = -1;
                      
            foreach (string attach in attachItems)
            {
                ListViewItem item = new ListViewItem(attach+"\r\n");
                
                //item.ImageIndex = 0;
                attachListView.Items.Add(item);               
                
               
            }
             btnDelete = new Button();
            btnDelete.Width = 20;
            btnDelete.Height = 20;
            btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            btnDelete.BackColor = Color.Transparent;
            btnDelete.Image = Resource.cross;
            attachListView.Controls.Add(btnDelete);
            btnDelete.Location = new Point(0, 0);
            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnDelete.Hide();

            attachListView.Columns[0].Width = attachListView.Width - 30;
            attachListView.Columns[1].Width = 30;
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            form.RemoveAttachment(selectedItemIndex);
            attachItems.RemoveAt(selectedItemIndex);
            attachListView.Items.RemoveAt(selectedItemIndex);
            btnDelete.Hide();
            if (del != null)
            {
                del(this);
            }
        }

        public void Register(DelAttach d)
        {
            del+=d;
        }

        

        private void attachListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Selected)
            {
                selectedItemIndex = e.ItemIndex;
                int y = e.Item.Bounds.Top;
                btnDelete.Location = new Point(attachListView.Columns[0].Width, y);
                btnDelete.Show();

            }
            else btnDelete.Hide();
        }

        private void attachListView_SizeChanged(object sender, EventArgs e)
        {
            attachListView.Columns[0].Width = attachListView.Width - 30;
            attachListView.Columns[1].Width = 30;
        }

       

        
    }
}

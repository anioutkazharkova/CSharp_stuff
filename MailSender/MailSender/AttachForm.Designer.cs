namespace MailSender
{
    partial class AttachForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachForm));
            this.attachListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.delImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // attachListView
            // 
            this.attachListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.attachListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attachListView.FullRowSelect = true;
            this.attachListView.GridLines = true;
            this.attachListView.Location = new System.Drawing.Point(12, 12);
            this.attachListView.MultiSelect = false;
            this.attachListView.Name = "attachListView";
            this.attachListView.Size = new System.Drawing.Size(357, 237);
            this.attachListView.TabIndex = 0;
            this.attachListView.UseCompatibleStateImageBehavior = false;
            this.attachListView.View = System.Windows.Forms.View.Details;
            this.attachListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.attachListView_ItemSelectionChanged);
            this.attachListView.SizeChanged += new System.EventHandler(this.attachListView_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item name";
            this.columnHeader1.Width = 322;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 30;
            // 
            // delImageList
            // 
            this.delImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("delImageList.ImageStream")));
            this.delImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.delImageList.Images.SetKeyName(0, "delete.png");
            // 
            // AttachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 261);
            this.Controls.Add(this.attachListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttachForm";
            this.Text = "Attachments";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView attachListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList delImageList;
        private System.Windows.Forms.ColumnHeader columnHeader2;


    }
}

namespace PsChamp.GeneralForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.layoutControlMatch = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMatch
            // 
            this.layoutControlMatch.AllowCustomization = false;
            this.layoutControlMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMatch.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControlMatch.Name = "layoutControlMatch";
            this.layoutControlMatch.Root = this.Root;
            this.layoutControlMatch.Size = new System.Drawing.Size(790, 418);
            this.layoutControlMatch.TabIndex = 0;
            this.layoutControlMatch.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(790, 418);
            this.Root.TextVisible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 418);
            this.Controls.Add(this.layoutControlMatch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "MainForm";
            this.Text = "www.championat.com";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMatch;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
    }
}


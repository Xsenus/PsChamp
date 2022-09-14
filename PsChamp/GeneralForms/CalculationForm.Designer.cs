
namespace PsChamp.GeneralForms
{
    partial class CalculationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculationForm));
            this.layoutControlMatch = new DevExpress.XtraLayout.LayoutControl();
            this.memoInfo = new DevExpress.XtraEditors.MemoEdit();
            this.btnCalculation = new DevExpress.XtraEditors.SimpleButton();
            this.txtN = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCalculation = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemN = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupTeamSecond = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupTeamFirst = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemMemo = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItemGrid = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMatch)).BeginInit();
            this.layoutControlMatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCalculation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTeamSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTeamFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMatch
            // 
            this.layoutControlMatch.AllowCustomization = false;
            this.layoutControlMatch.Controls.Add(this.memoInfo);
            this.layoutControlMatch.Controls.Add(this.btnCalculation);
            this.layoutControlMatch.Controls.Add(this.txtN);
            this.layoutControlMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMatch.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControlMatch.Name = "layoutControlMatch";
            this.layoutControlMatch.Root = this.Root;
            this.layoutControlMatch.Size = new System.Drawing.Size(983, 647);
            this.layoutControlMatch.TabIndex = 0;
            this.layoutControlMatch.Text = "layoutControl1";
            // 
            // memoInfo
            // 
            this.memoInfo.Location = new System.Drawing.Point(7, 33);
            this.memoInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.memoInfo.Name = "memoInfo";
            this.memoInfo.Properties.ReadOnly = true;
            this.memoInfo.Size = new System.Drawing.Size(969, 146);
            this.memoInfo.StyleController = this.layoutControlMatch;
            this.memoInfo.TabIndex = 10;
            // 
            // btnCalculation
            // 
            this.btnCalculation.Appearance.BackColor = System.Drawing.Color.PeachPuff;
            this.btnCalculation.Appearance.Options.UseBackColor = true;
            this.btnCalculation.Location = new System.Drawing.Point(7, 613);
            this.btnCalculation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(969, 27);
            this.btnCalculation.StyleController = this.layoutControlMatch;
            this.btnCalculation.TabIndex = 4;
            this.btnCalculation.Text = "Рассчитать";
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // txtN
            // 
            this.txtN.EditValue = "3";
            this.txtN.Location = new System.Drawing.Point(26, 7);
            this.txtN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtN.MaximumSize = new System.Drawing.Size(175, 0);
            this.txtN.MinimumSize = new System.Drawing.Size(146, 0);
            this.txtN.Name = "txtN";
            this.txtN.Properties.Appearance.Options.UseTextOptions = true;
            this.txtN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtN.Size = new System.Drawing.Size(175, 22);
            this.txtN.StyleController = this.layoutControlMatch;
            this.txtN.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCalculation,
            this.emptySpaceItemN,
            this.layoutControlItemN,
            this.layoutControlGroupTeamSecond,
            this.layoutControlGroupTeamFirst,
            this.layoutControlItemInfo,
            this.splitterItemMemo,
            this.splitterItemGrid});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(983, 647);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemCalculation
            // 
            this.layoutControlItemCalculation.Control = this.btnCalculation;
            this.layoutControlItemCalculation.Location = new System.Drawing.Point(0, 606);
            this.layoutControlItemCalculation.Name = "layoutControlItemCalculation";
            this.layoutControlItemCalculation.Size = new System.Drawing.Size(973, 31);
            this.layoutControlItemCalculation.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCalculation.TextVisible = false;
            // 
            // emptySpaceItemN
            // 
            this.emptySpaceItemN.AllowHotTrack = false;
            this.emptySpaceItemN.Location = new System.Drawing.Point(198, 0);
            this.emptySpaceItemN.Name = "emptySpaceItemN";
            this.emptySpaceItemN.Size = new System.Drawing.Size(775, 26);
            this.emptySpaceItemN.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemN
            // 
            this.layoutControlItemN.Control = this.txtN;
            this.layoutControlItemN.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemN.Name = "layoutControlItemN";
            this.layoutControlItemN.Size = new System.Drawing.Size(198, 26);
            this.layoutControlItemN.Text = "N:";
            this.layoutControlItemN.TextSize = new System.Drawing.Size(16, 17);
            // 
            // layoutControlGroupTeamSecond
            // 
            this.layoutControlGroupTeamSecond.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupTeamSecond.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupTeamSecond.AppearanceGroup.Options.UseTextOptions = true;
            this.layoutControlGroupTeamSecond.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlGroupTeamSecond.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupTeamSecond.Location = new System.Drawing.Point(490, 182);
            this.layoutControlGroupTeamSecond.Name = "layoutControlGroupTeamSecond";
            this.layoutControlGroupTeamSecond.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroupTeamSecond.Size = new System.Drawing.Size(483, 424);
            this.layoutControlGroupTeamSecond.Text = "Гости";
            // 
            // layoutControlGroupTeamFirst
            // 
            this.layoutControlGroupTeamFirst.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupTeamFirst.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupTeamFirst.AppearanceGroup.Options.UseTextOptions = true;
            this.layoutControlGroupTeamFirst.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlGroupTeamFirst.Location = new System.Drawing.Point(0, 182);
            this.layoutControlGroupTeamFirst.Name = "layoutControlGroupTeamFirst";
            this.layoutControlGroupTeamFirst.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroupTeamFirst.Size = new System.Drawing.Size(484, 424);
            this.layoutControlGroupTeamFirst.Text = "Хозяева";
            // 
            // layoutControlItemInfo
            // 
            this.layoutControlItemInfo.Control = this.memoInfo;
            this.layoutControlItemInfo.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemInfo.Name = "layoutControlItemInfo";
            this.layoutControlItemInfo.Size = new System.Drawing.Size(973, 150);
            this.layoutControlItemInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfo.TextVisible = false;
            // 
            // splitterItemMemo
            // 
            this.splitterItemMemo.AllowHotTrack = true;
            this.splitterItemMemo.Location = new System.Drawing.Point(0, 176);
            this.splitterItemMemo.Name = "splitterItemMemo";
            this.splitterItemMemo.Size = new System.Drawing.Size(973, 6);
            // 
            // splitterItemGrid
            // 
            this.splitterItemGrid.AllowHotTrack = true;
            this.splitterItemGrid.Location = new System.Drawing.Point(484, 182);
            this.splitterItemGrid.Name = "splitterItemGrid";
            this.splitterItemGrid.Size = new System.Drawing.Size(6, 424);
            // 
            // CalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 647);
            this.Controls.Add(this.layoutControlMatch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "CalculationForm";
            this.Text = "www.championat.com";
            this.Load += new System.EventHandler(this.CalculationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMatch)).EndInit();
            this.layoutControlMatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCalculation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTeamSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTeamFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMatch;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCalculation;
        private DevExpress.XtraEditors.TextEdit txtN;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemN;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCalculation;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemN;
        private DevExpress.XtraEditors.MemoEdit memoInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTeamFirst;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTeamSecond;
        private DevExpress.XtraLayout.SplitterItem splitterItemMemo;
        private DevExpress.XtraLayout.SplitterItem splitterItemGrid;
    }
}


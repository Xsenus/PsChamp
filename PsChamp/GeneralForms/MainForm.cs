using Core.Controllers;
using Core.Models;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PsChamp.Controls;
using System;
using System.Linq;

namespace PsChamp.GeneralForms
{
    public partial class MainForm : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();

        public MainForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var matchControl = default(MatchControl);
            var baseLayoutItem = layoutControlMatch.Items.FirstOrDefault(f => f.Text.Equals(nameof(matchControl)));
            if (baseLayoutItem is null)
            {
                matchControl = new MatchControl();
                matchControl.FocusedRowChangedEvent += MatchControl_FocusedRowChangedEvent;
                matchControl.SetUnitOfWork(_uof);
                var item = layoutControlMatch.AddItem(nameof(matchControl));
                item.Control = matchControl;
            }
            else
            {
                matchControl = (MatchControl)((LayoutControlItem)baseLayoutItem).Control;
            }

            matchControl.UpdateData(await MatchController.GetMatchesAsync(_uof));
        }

        private void MatchControl_FocusedRowChangedEvent(Match obj, int focusedRowHandle)
        {
            
        }
    }
}

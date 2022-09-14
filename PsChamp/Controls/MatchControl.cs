using Core.Controllers;
using Core.Models;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PsChamp.GeneralForms;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PsChamp.Controls
{
    public partial class MatchControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private List<Match> _listObj;
        private bool _isShowPopup = true;

        public delegate void FocusedRowChangedEventHandler(Match obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public MatchControl()
        {
            InitializeComponent();
            _listObj = new List<Match>();
        }
        
        public MatchControl(List<Match> listObj) : this()
        {
            this._listObj = listObj;
        }
        
        public void UpdateData(object listObj)
        {
            if (listObj is List<Match> list)
            {
                this._listObj = list;
                gridControl.DataSource = this._listObj;
            }
        }
        public void SetShowPopup(bool isShowPopup)
        {
            _isShowPopup = isShowPopup;
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        private void Control_Load(object sender, EventArgs e)
        {
            gridControl.GridControlSetup();
            UpdateData(_listObj);
            SetGridViewSettings();
        }

        public void SetGridViewSettings(bool isHideColumn = false)
        {
            gridView.ColumnSetup($"{nameof(Match.Oid)}", caption: "Номер", width: 100, isFixedWidth: true, isVisible: false);
            gridView.ColumnSetup($"{nameof(Match.Guid)}", caption: "Уникальный номер", width: 250, isFixedWidth: true, isVisible: false);
            gridView.ColumnSetup($"{nameof(Match.Position)}", caption: "Позиция", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center, isVisible: false);
            gridView.ColumnSetup($"{nameof(Match.Period)}", caption: "Период", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Match.Tour)}", caption: "Тур", width: 50, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Match.Date)}", caption: "Дата", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Match.TeamFirst)}", caption: "Команда 1", width: 150, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(Match.ScoreFirst)}", caption: "Счет 1 команды", width: 75, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Match.TeamSecond)}", caption: "Команда 2", width: 150, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(Match.ScoreSecond)}", caption: "Счет 2 команды", width: 75, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            if (isHideColumn)
            {
                foreach (GridColumn item in gridView.Columns)
                {
                    item.Visible = false;
                }
            }

            gridView.ColumnSetup($"{nameof(Match.ThisString)}", caption: "Матч", width: 375, isFixedWidth: true, isGridGroupCount: true);

            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFilterPanelMode: false);
            gridView.BestFitColumns();
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="renter">Арендатор.</param>
        /// <param name="contract">Договор.</param>
        private void OpenEditForm(Match obj)
        {
            //var form = new ContractEdit(obj);
            //form.FormClosed += ContractEditFormClosed;
            //form.XtraFormShow();
        }
        
        private void ContractEditFormClosed(object sender, FormClosedEventArgs e)
        {
            //if (sender is ContractEdit contractEdit)
            //{
            //    if (contractEdit.IsSave)
            //    {
            //        var currentContract = contractEdit?.Contract;
            //        if (currentContract != null)
            //        {
            //            var contract = _listObj.FirstOrDefault(f => f.Oid == currentContract.Oid);
            //            if (contract is null)
            //            {
            //                _listObj.Add(contractEdit.Contract);
            //                contract = contractEdit.Contract;
            //            }

            //            contract?.Reload();
            //            gridView.RefreshData();
            //            gridView.FocusedRowHandle = gridView.LocateByValue(nameof(Contract.Oid), contract?.Oid);
            //        }
            //    }
            //}
        }
        
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (e is DXMouseEventArgs ea)
            {
                if (sender is GridView gridView)
                {
                    var info = gridView.CalcHitInfo(ea.Location);
                    if (info.InRow)
                    {
                        if (gridView.GetRow(gridView.FocusedRowHandle) is Match obj)
                        {
                            OpenEditForm(obj);
                        }
                    }
                }
            }
        }
        
        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (_isShowPopup is false)
            {
                return;
            }

            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEdit.Enabled = false;
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnEdit.Enabled = true;
                        barBtnDel.Enabled = true;
                    }

                    barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
                    barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenEditForm(null);
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Match obj)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Match obj)
            {
                var text = $"Вы действительно хотите удалить матч: {obj}?";
                var caption = $"Удаление договора [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using (var uof = new UnitOfWork())
                    {
                        var match = await uof.GetObjectByKeyAsync<Match>(obj.Oid);
                        if (match != null)
                        {
                            match.Delete();
                            await uof.CommitTransactionAsync().ConfigureAwait(false);

                            _listObj.Remove(obj);
                            gridView.FocusedRowHandle = gridView.FocusedRowHandle - 1;
                            gridView.RefreshData();
                        }
                    }
                }
            }
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateData(await MatchController.GetMatchesAsync(_uof));
        }

        private void barCheckFindPanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsFindPanelVisible)
            {
                gridView.HideFindPanel();
            }
            else
            {
                gridView.ShowFindPanel();
            }
        }

        private void barCheckAutoFilterRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.OptionsView.ShowAutoFilterRow)
            {
                gridView.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                gridView.OptionsView.ShowAutoFilterRow = true;
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Match obj)
            {
                FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is Match match)
                {
                    if (_teamFirst?.Contains(match.Guid) is true)
                    {
                        e.Appearance.BackColor = Color.LightBlue;
                    }
                    else if (_teamSecond?.Contains(match.Guid) is true)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                    else if (match.ScoreFirst is null && match.ScoreSecond is null)
                    {
                        e.Appearance.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void barBtnGet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new HtmlParserForm();
            form.UpdateEvent += Form_UpdateEvent;
            form.FormClosing += Form_FormClosing;
            form.XtraFormShow();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form = Objects.GetRequiredObject<HtmlParserForm>(sender);
            if (form != null)
            {
                form.UpdateEvent -= Form_UpdateEvent;
            }
        }

        private async void Form_UpdateEvent(bool isUpdate)
        {
            UpdateData(await MatchController.GetMatchesAsync(_uof));
        }

        private void barBtnCalculation_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new CalculationForm();
            form.GridControlStyleRowEvent += Form_GridControlStyleRowEvent;
            form.FormClosing += CalculationForm_FormClosing1;
            form.XtraFormShow();
        }

        private IEnumerable<Guid> _teamFirst;
        private IEnumerable<Guid> _teamSecond;
        
        private void CalculationForm_FormClosing1(object sender, FormClosingEventArgs e)
        {
            var form = Objects.GetRequiredObject<CalculationForm>(sender);
            if (form != null)
            {
                form.GridControlStyleRowEvent -= Form_GridControlStyleRowEvent;
                _teamFirst = default;
                _teamSecond = default;
            }
        }

        private void Form_GridControlStyleRowEvent(object sender, IEnumerable<Guid> teamFirst, IEnumerable<Guid> teamSecond)
        {
            _teamFirst = teamFirst;
            _teamSecond = teamSecond;
            gridView?.RefreshData();
        }
    }
}

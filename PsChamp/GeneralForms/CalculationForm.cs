using Core.Controllers;
using Core.Models;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using PsChamp.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PsChamp.GeneralForms
{
    public partial class CalculationForm : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();
        private List<Match> matches = new List<Match>();

        public delegate void GridControlStyleRowEventHandler(object sender, IEnumerable<Guid> teamFirst, IEnumerable<Guid> teamSecond);
        public event GridControlStyleRowEventHandler GridControlStyleRowEvent;

        private MatchControl _teamFirstMatchControl;
        private MatchControl _teamSecondMatchControl;

        public CalculationForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
        }

        private async void CalculationForm_Load(object sender, EventArgs e)
        {
            matches = await MatchController.GetMatchesAsync(_uof);
            
            _teamFirstMatchControl = CreateMatchControl(_teamFirstMatchControl, layoutControlGroupTeamFirst);
            _teamSecondMatchControl = CreateMatchControl(_teamSecondMatchControl, layoutControlGroupTeamSecond);            
        }

        private MatchControl CreateMatchControl(MatchControl matchControl, LayoutControlGroup layoutControlGroup)
        {
            matchControl = default(MatchControl);
            var baseLayoutItem = layoutControlGroup.Items.FirstOrDefault(f => f.Text.Equals(nameof(matchControl)));
            if (baseLayoutItem is null)
            {
                matchControl = new MatchControl();
                matchControl.SetUnitOfWork(_uof);
                var item = layoutControlGroup.AddItem(nameof(matchControl));
                item.Control = matchControl;
            }
            else
            {
                matchControl = (MatchControl)((LayoutControlItem)baseLayoutItem).Control;
            }
            matchControl.SetGridViewSettings(true);
            matchControl.SetShowPopup(false);
            return matchControl;
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            AddTextToMemoEdit(text: "Начался процесс расчета...", isClear: true);

            var n = Objects.GetIntObject(txtN.EditValue);
            if (n == 0)
            {
                var text = "Необходимо указать константу N.";
                AddTextToMemoEdit(text: text);
                DevXtraMessageBox.ShowXtraMessageBox(text, txtN);
                return;
            }

            AddTextToMemoEdit(text: $"N: {n}");
            var count = matches.Count;
            AddTextToMemoEdit(text: $"Количество матчей: {count}");            

            var countTeamSecondWin = GetCountTeamSecondWin(n, count);
            var countTeamFirstWin = GetCountTeamFirstWin(n, count);
            
            AddTextToMemoEdit(text: $"Побед хозяев ({GetPercent(count, countTeamSecondWin)}%): {countTeamSecondWin} (N = {n})");
            _teamSecondMatchControl.UpdateData(teamSecondWins);

            AddTextToMemoEdit(text: $"Побед гостей ({GetPercent(count, countTeamFirstWin)}%): {countTeamFirstWin} (N = {n})");
            _teamFirstMatchControl.UpdateData(teamFirstWins);

            AddTextToMemoEdit(text: "Расчет окончен...");

            GridControlStyleRowEvent?.Invoke(this, teamSecondWins?.Select(s => s.Guid), teamFirstWins?.Select(s => s.Guid));
        }

        private decimal GetPercent(int count, int countWin)
        {
            var result = (decimal)countWin / (decimal)count * 100;            
            return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }

        private List<Match> teamFirstWins;
        private List<Match> teamSecondWins;

        private int GetCountTeamFirstWin(int n, int count)
        {
            var tempN = 0;
            var countTeamFirstWin = 0;
            teamSecondWins = new List<Match>();

            for (int i = 0; i < count; i++)
            {
                if (tempN == n)
                {
                    if (matches[i].ScoreFirst < matches[i].ScoreSecond)
                    {
                        teamSecondWins.Add(matches[i]);
                        countTeamFirstWin++;
                    }
                    tempN = 0;
                    continue;
                }

                if (matches[i].ScoreFirst > matches[i].ScoreSecond)
                {
                    tempN++;
                }
                else
                {
                    tempN = 0;
                }                
            }

            return countTeamFirstWin;
        }

        private int GetCountTeamSecondWin(int n, int count)
        {
            var tempN = 0;
            var countTeamSecondWin = 0;
            teamFirstWins = new List<Match>();

            for (int i = 0; i < count; i++)
            {
                if (tempN == n)
                {
                    if (matches[i].ScoreFirst > matches[i].ScoreSecond)
                    {
                        teamFirstWins.Add(matches[i]);
                        countTeamSecondWin++;
                    }
                    tempN = 0;
                    continue;
                }

                if (matches[i].ScoreFirst < matches[i].ScoreSecond)
                {
                    tempN++;
                }
                else
                {
                    tempN = 0;
                }
            }

            return countTeamSecondWin;
        }

        private void AddTextToMemoEdit(string text, bool isWrite = true, bool isClear = false)
        {
            if (isWrite is false)
            {
                return;
            }

            if (!IsHandleCreated)
            {
                return;
            }

            if (isClear)
            {
                memoInfo.EditValue = null;
            }

            Invoke((Action)delegate
            {
                memoInfo.MaskBox.AppendText($"[{DateTime.Now}] => {text}{Environment.NewLine}");
                memoInfo.SelectionStart = Int32.MaxValue;
                memoInfo.ScrollToCaret();
            });
        }
    }
}

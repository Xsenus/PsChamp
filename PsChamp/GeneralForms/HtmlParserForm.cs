using Core.Controllers;
using Core.Models;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Extensions.DevXpo;
using PulsLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsChamp.GeneralForms
{
    public partial class HtmlParserForm : XtraForm
    {
        private bool _isWrite => !checkIsWrite.Checked;        
        private UnitOfWork _uof = new UnitOfWork();
        
        public delegate void UpdateEventHandler(bool isUpdate);
        public event UpdateEventHandler UpdateEvent;

        public HtmlParserForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
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
                memoParserInfo.EditValue = null;
            }

            Invoke((Action)delegate
            {
                memoParserInfo.MaskBox.AppendText($"[{DateTime.Now}] => {text}{Environment.NewLine}");
                memoParserInfo.SelectionStart = Int32.MaxValue;
                memoParserInfo.ScrollToCaret();
            });
        }
        
        private async void btnGet_Click(object sender, EventArgs e)
        {
            AddTextToMemoEdit(text: "Начался процесс получение данных...", isClear: true);
                        
            var checkEdit = Objects.GetRequiredObject<bool>(checkIsAllPeriods.EditValue);
            if (checkEdit is true)
            {
                var periods = await PeriodController.GetPeriods(_uof);
                var periodsWithLinks = periods.Where(w => w.Link != null);

                foreach (var period in periodsWithLinks)
                {
                    await GetMatches(period.Link.Url, period.ToString());
                }
            }
            else
            {
                var link = Objects.GetRequiredObject<Link>(cmbUrl.EditValue);
                if (link is null && string.IsNullOrWhiteSpace(link.Url))
                {
                    var text = "Необходимо указать ссылку.";
                    AddTextToMemoEdit(text: text);
                    DevXtraMessageBox.ShowXtraMessageBox(text, cmbUrl);
                    return;
                }

                var period = await _uof.GetObjectByKeyFromValueAsync<Period>(cmbPeriod.EditValue);
                if (period is null || string.IsNullOrWhiteSpace(period.ToString()))
                {
                    var text = "Необходимо указать период.";
                    AddTextToMemoEdit(text: text);
                    DevXtraMessageBox.ShowXtraMessageBox(text, cmbPeriod);
                    return;
                }
                await GetMatches(link.Url, period.ToString());
            }

            UpdateEvent?.Invoke(true);

            AddTextToMemoEdit(text: "Успешно завершен импорт");
            DevXtraMessageBox.ShowXtraMessageBox("Успешно завершен импорт");
        }

        private async Task GetMatches(string url, string period)
        {
            AddTextToMemoEdit(text: $"Получение страницы по ссылке: {url}");
            var htmlDocument = await HtmlParserController.GetHtmlDocumentAsync(url);

            AddTextToMemoEdit(text: $"Получение записей из исходной HTML страницы...");
            var nodes = HtmlParserController.GetHtmlNodeCollection(htmlDocument, "/html/body/div/div/div/div/div/div/table/tbody/tr");
            if (nodes != null && nodes.Count > 0)
            {
                AddTextToMemoEdit(text: $"Получено объектов для обработки: {nodes.Count}");
            }

            using (var uof = new UnitOfWork())
            {
                var matches = await MatchController.GetMatchesAsync(uof);
                var teams = await TeamController.GetTeamsAsync(uof);

                var position = 1;
                
                foreach (var node in nodes)
                {
                    var str = node.InnerText?.Replace("  ", "")?.Replace("\n\n", "|")?.Replace("&nbsp", " ")?.Replace("\n", "|")?.Trim();
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        AddTextToMemoEdit(text: $"Получена строка для разбора объектов: {str}", isWrite: _isWrite);

                        var splits = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (splits != null && splits.Length > 0)
                        {
                            var _tour = splits[0]?.ToString()?.Trim();
                            if (string.IsNullOrWhiteSpace(_tour))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найден номер тура: {_tour ?? "NULL"}", isWrite: _isWrite);
                                continue;
                            }

                            var _date = splits[1]?.ToString()?.Replace(" ", "")?.Replace(";", " ")?.Trim();
                            if (!DateTime.TryParse(_date, out DateTime date))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдена дата проведения матча: {_date ?? "NULL"}", isWrite: _isWrite);
                                continue;
                            }

                            var _teamFirstName = splits[4]?.ToString()?.Trim();
                            if (string.IsNullOrWhiteSpace(_teamFirstName))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдена команда: {_teamFirstName ?? "NULL"}", isWrite: _isWrite);
                                continue;
                            }
                            var teamFirst = GetTeam(uof, teams, _teamFirstName);

                            var _teamSecondName = splits[6]?.ToString()?.Trim();
                            if (string.IsNullOrWhiteSpace(_teamSecondName))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдена команда: {_teamSecondName ?? "NULL"}", isWrite: _isWrite);
                                continue;
                            }
                            var teamSecond = GetTeam(uof, teams, _teamSecondName);

                            var _scores = splits[7]?.ToString()?.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (_scores is null || _scores.Length == 0)
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдена счет матча: {splits[7]?.ToString() ?? "NULL"}", isWrite: _isWrite);
                                continue;
                            }

                            var scoreFirst = default(int?);
                            var _scoreFirst = _scores[0]?.ToString()?.Trim();
                            if (!int.TryParse(_scoreFirst, out int scoreFirstInt))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдено количество очков первой команды: {_scoreFirst ?? "NULL"}", isWrite: _isWrite);
                            }
                            else
                            {
                                scoreFirst = scoreFirstInt;
                            }

                            var scoreSecond = default(int?);
                            var _scoreSecond = _scores[1]?.ToString()?.Trim();
                            if (!int.TryParse(_scoreSecond, out int scoreSecondInt))
                            {
                                AddTextToMemoEdit(text: $"В объекте не найдено количество очков второй команды: {_scoreSecond ?? "NULL"}", isWrite: _isWrite);
                            }
                            else
                            {
                                scoreSecond = scoreSecondInt;
                            }

                            var _match = MatchController.Create(period, _tour, date, teamFirst, scoreFirst, teamSecond, scoreSecond);
                            if (_match != null)
                            {
                                AddTextToMemoEdit(text: $"Сформирован новый матч: {_match}", isWrite: _isWrite);
                            }

                            var currentMatch = matches.FirstOrDefault(f => f != null && f.Equals(_match));
                            if (currentMatch is null)
                            {
                                var match = MatchController.Create(uof, _match);
                                match.Save();

                                matches.Add(match);
                                currentMatch = match;
                                AddTextToMemoEdit(text: $"Добавлена новая запись в базу данных: {match}");
                            }
                            else
                            {
                                AddTextToMemoEdit(text: $"Запись: {_match} - уже существует в базе данных.", isWrite: _isWrite);
                                AddTextToMemoEdit(text: $"Проверка итогового счета...", isWrite: _isWrite);

                                if ((currentMatch.ScoreFirst is null || currentMatch.ScoreSecond is null)
                                    && (_match.ScoreFirst != null && _match.ScoreSecond != null))
                                {
                                    currentMatch.UpdateScore(_match);
                                    currentMatch.Save();

                                    AddTextToMemoEdit(text: $"Обновлен счет матча: {currentMatch}");
                                }
                            }

                            currentMatch.UpdatePosition(position);
                        }
                    }

                    position++;
                }

                await uof.CommitChangesAsync();
            }
        }

        private static Team GetTeam(UnitOfWork uof, List<Team> teams, string name)
        {
            var team = teams.FirstOrDefault(f => f.Name == name);
            if (team is null)
            {
                team = TeamController.Create(uof, name);
                team.Save();

                teams.Add(team);
            }

            return team;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private async void cmbPeriod_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var comboBoxEdit = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (comboBoxEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    comboBoxEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Search)
                {
                    await GetUrlAndPeriodAsync();
                }
            }
        }
        
        private async Task GetUrlAndPeriodAsync(string url = "https://www.championat.com/football/_england.html", string host = "https://www.championat.com")
        {
            AddTextToMemoEdit(text: "Начался процесс получение данных...", true);
            
            AddTextToMemoEdit(text: $"Получение страницы по ссылке: {url}");
            var htmlDocument = await HtmlParserController.GetHtmlDocumentAsync(url);
            
            AddTextToMemoEdit(text: $"Получение записей из исходной HTML страницы...");
            var nodes = HtmlParserController.GetHtmlNodeCollection(htmlDocument, "//a[@class='seo-links__item']");
            if (nodes != null && nodes.Count > 0)
            {
                AddTextToMemoEdit(text: $"Получено объектов для обработки: {nodes.Count}");
            }

            var links = await LinkController.GetLinksAsync(_uof);
            var periods = await PeriodController.GetPeriods(_uof);

            foreach (var node in nodes)
            {
                var str = node.InnerText?.Trim();
                if (!string.IsNullOrWhiteSpace(str))
                {
                    AddTextToMemoEdit(text: $"Получена строка для разбора объектов: {str}", isWrite: _isWrite);

                    var splits = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splits != null && splits.Length > 0)
                    {
                        var strYear = splits[0]?.Trim();

                        if (!string.IsNullOrWhiteSpace(strYear))
                        {
                            AddTextToMemoEdit(text: $"Получена строка c датами: {strYear}", isWrite: _isWrite);
                            var splitsYear = strYear.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                            if (splitsYear != null && splitsYear.Length == 2)
                            {
                                if (!int.TryParse(splitsYear[0]?.ToString(), out int yearFirst))
                                {
                                    AddTextToMemoEdit(text: $"В объекте не найден год начала периода: {splitsYear[0]?.ToString() ?? "NULL"}", isWrite: _isWrite);
                                    continue;
                                }
                                AddTextToMemoEdit(text: $"Найден год начала периода: {yearFirst}");

                                if (!int.TryParse(splitsYear[1]?.ToString(), out int yearSecond))
                                {
                                    AddTextToMemoEdit(text: $"В объекте не найден год окончания периода: {splitsYear[1]?.ToString() ?? "NULL"}", isWrite: _isWrite);
                                    continue;
                                }
                                AddTextToMemoEdit(text: $"Найден год окончания периода: {yearSecond}", isWrite: _isWrite);

                                var _period = PeriodController.Create(yearFirst, yearSecond);
                                if (_period != null)
                                {
                                    AddTextToMemoEdit(text: $"Сформирован новый период: {_period}", isWrite: _isWrite);
                                }

                                var currentPeriod = periods.FirstOrDefault(f => f != null && f.Equals(_period));
                                if (currentPeriod is null)
                                {
                                    var period = PeriodController.Create(_uof, _period);
                                    period.Save();

                                    periods.Add(period);

                                    AddTextToMemoEdit(text: $"Добавлена новая запись в базу данных: {period}");
                                    currentPeriod = period;
                                }

                                var attributeHref = node.Attributes?.FirstOrDefault(f => f.Name != null && f.Name.Equals("href"));
                                if (attributeHref != null && !string.IsNullOrWhiteSpace(attributeHref.Value))
                                {
                                    var value = attributeHref.Value;
                                    AddTextToMemoEdit(text: $"Найдена ссылка в атрибуте объекта: {value}", isWrite: _isWrite);

                                    var href = $"{host}{value}calendar/";
                                    AddTextToMemoEdit(text: $"Получена полная ссылка: {href}", isWrite: _isWrite);

                                    var _link = LinkController.Create(href);
                                    if (_link != null)
                                    {
                                        AddTextToMemoEdit(text: $"Сформирована новая ссылка: {_link}", isWrite: _isWrite);
                                    }

                                    var currentLink = links.FirstOrDefault(f => f != null && f.Equals(_link));
                                    if (currentLink is null)
                                    {
                                        var link = LinkController.Create(_uof, _link);
                                        link.Save();

                                        links.Add(link);

                                        AddTextToMemoEdit(text: $"Добавлена новая запись в базу данных: {link}");
                                        currentLink = link;
                                    }

                                    currentPeriod.SetLink(currentLink);
                                    currentPeriod.Save();

                                    AddTextToMemoEdit(text: $"Для периода [{currentPeriod}] добавлена ссылка {currentLink}", isWrite: _isWrite);
                                }
                                else
                                {
                                    AddTextToMemoEdit(text: $"Не найдена ссылка в атрибуте объекта", isWrite: _isWrite);
                                }
                            }
                            else
                            {
                                AddTextToMemoEdit(text: $"Не получилось получить период из следующей строки: {strYear}", isWrite: _isWrite);
                            }
                        }
                    }
                }
            }

            await _uof.CommitChangesAsync();
            AddTextToMemoEdit(text: "Успешно завершен импорт");
            DevXtraMessageBox.ShowXtraMessageBox("Успешно завершен импорт");

            await FillingFormObjectsAsync();
        }

        private async void HtmlParserForm_Load(object sender, EventArgs e)
        {
            await FillingFormObjectsAsync();
        }

        private async Task FillingFormObjectsAsync()
        {
            await cmbUrl.AddItemsFromListObjectAsync<Link>(_uof);
            await cmbPeriod.AddItemsFromListObjectAsync<Period>(_uof);
            cmbPeriod.SelectedIndex = 0;
        }

        private async void cmbUrl_SelectedValueChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (comboBoxEdit != null)
            {
                if (comboBoxEdit.EditValue is Link link)
                {
                    var period = await PeriodController.GetPeriod(_uof, link);
                    if (period != null)
                    {
                        cmbPeriod.EditValue = period;
                    }
                }
            }
        }

        private void cmbPeriod_SelectedValueChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (comboBoxEdit != null)
            {
                if (comboBoxEdit.EditValue is Period period)
                {
                    if (period.Link != null)
                    {
                        cmbUrl.EditValue = period.Link;
                    }
                }
            }
        }

        private void checkIsAllPeriods_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = Objects.GetRequiredObject<CheckEdit>(sender);
            if (checkEdit != null)
            {
                cmbPeriod.Enabled = !checkEdit.Checked;
                cmbUrl.Enabled = !checkEdit.Checked;
            }
        }
    }
}
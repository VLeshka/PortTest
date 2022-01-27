using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Configuration;
using Microsoft.Win32;


namespace ComApplication
{    
        public partial class MainForm : Form
        {
            #region переменные и константы

            // показывает поступившие из порта данные
            public delegate void InserInListBoxAnswer(structPacketBytes dat); 

            // имя программы в регистре
            private const string progRegName = "PortTest";

            // название колонки время
            private const string nameColulumnTime = "Время";
            public string GetNameColulumnTime { get { return (nameColulumnTime); } }

            // название колонки время
            private const string nameColulumnHex = "Hex";
            public string GetNameColulumnHex { get { return (nameColulumnHex); } }

            // название колонки текст
            private const string nameColumnTextBase = "Текст"; // базовое название колонки с текстом, в консоли // к ней добавляется указание испоьзуемой кодировки текста
            public string GetNameColulumnText 
            {
                get
                {
                    try
                    {
                        return (nameColumnTextBase + " (кодовая страница " + OUnit.getCodePage.kodStranicaName + ")");
                    }
                    catch
                    {
                        return (nameColumnTextBase + " (кодовая страница некорректна)");
                    }
                }
            }

            // базовое "имя порта", когда никаких портов нет
            private const string basePortNameNo = "[НЕТ]"; 
            public string GetBasePortNameNo { get { return (basePortNameNo); } }

            // текст в командной строке по умолчанию // при этом режим отображения всегда  HEX            
            private const string texboxCommandTextDefault = "01 04 00 00 00 01";

            #endregion

            #region автоматически создаваемые методы

            public MainForm()
        {
            InitializeComponent();
            OUnit.HandlerEventAnswerMsg = new OUnit.EventAnswerMsg(EventAnswerAdd);
        }

            private void выходToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void buttonOpen_Click(object sender, EventArgs e)
            {
                string result_message = "";
				// если порт не открыт
                if (!OUnit.isOpenSerialPort) 
                {
                    // если открытие порта прошло успешно
					if (OUnit.OpenSerialPort(ref result_message)) 
                        buttonOpen.Text = "Закрыть serial-порт";
                    else
                    {
                        MessageBox.Show(result_message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        buttonOpen.Text = "Открыть serial-порт";
                    }
                }
                else 
                {
                    // если закрытие порта прошло успешно
					if (OUnit.CloseSerialPort(ref result_message)) 
                        buttonOpen.Text = "Открыть serial-порт";
                    else
                    {
                        MessageBox.Show(result_message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        buttonOpen.Text = "Закрыть serial-порт";
                    }
                }
                buttonSend.Enabled = OUnit.isOpenSerialPort;
                pictureBoxGreen.Visible = OUnit.isOpenSerialPort;
                pictureBoxRed.Visible = !pictureBoxGreen.Visible;
                настройкиToolStripMenuItem.Enabled = !OUnit.isOpenSerialPort;
                ToolStripMenuItemOptionsAnswer.Enabled = !OUnit.isOpenSerialPort;
                ToolStripMenuItemOptionsCommand.Enabled = !OUnit.isOpenSerialPort;
                buttonOptionsSerial.Enabled = !OUnit.isOpenSerialPort;
            }

            private void buttonSend_Click(object sender, EventArgs e)
            {
                if (textBoxComand.Text.Trim() == "")
                {
                    MessageBox.Show("Введите данные для отправки в порт", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                // проверка на корректность количества символов
                if (radioButtonHex.Checked)                    
                    if (!VerifyTextBoxComandHex())
                        return;
                if (checkBoxPeriod.Checked)
                {
                    // если надо периодически
                    if (timerSendCommand.Enabled)
                    {
                        buttonSend.Text = "Старт";
                        timerSendCommand.Enabled = false;
                        // textBoxPeriod
                        textBoxPeriod.Enabled = true;
                        checkBoxPeriod.Enabled = true;
                        return;
                    }
                    else
                    {                        
                        buttonSend.Text = "Стоп";
                        // textBoxPeriod
                        textBoxPeriod.Enabled = false;
                        checkBoxPeriod.Enabled = false;
                    }
                }
                // timerSendCommand
                timerSendCommand.Interval = Properties.Settings.Default.timerSendCommandInterval;
                timerSendCommand_Tick(this, null); // там же timerSendCommand.Enabled = true
            }

            private void ToolStripMenuAnswerItem_Click(object sender, EventArgs e)
            {
                listViewAnswer.Items.Clear();
            }

            private void MainForm_Resize(object sender, EventArgs e)
            {
                splitContainer1.Height = this.ClientSize.Height - 79;
                splitContainer1.Width = this.ClientSize.Width - 27;                                     
            }

            private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("ПортТест - программа для тестирования приборов," +
                                "\r\nподключённых через serial-порт к компьютеру." +
                                "\r\n                                  Версия 1.0" +
                                "\r\nРазработчик: Васильев А.В. vleshka@mail.ru" +
                                "\r\n                         Лиценция: FreeWare", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private void toolStripMenuItem1_Click(object sender, EventArgs e)
            {
                int kolColumns = (Properties.Settings.Default.isShowColumnTime ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnHex ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnText ? 1 : 0);
                if ((kolColumns == 0) | (listViewAnswer.Items.Count == 0))
                {
                    MessageBox.Show("Данные для сохранения отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SaveLogConsol(listViewAnswer, Properties.Settings.Default.fileAnswer);           
            }

            private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OptionsForm fo = new OptionsForm();
                fo.StartPosition = FormStartPosition.CenterScreen;
                fo.ShowInTaskbar = false;
                fo.ShowDialog(this);
            }

            private void toolStripMenuItemSaveAs_Click(object sender, EventArgs e)
            {
                int kolColumns = (Properties.Settings.Default.isShowColumnTime ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnHex ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnText ? 1 : 0);
                if ((kolColumns == 0) | (listViewAnswer.Items.Count == 0))
                {
                    MessageBox.Show("Данные для сохранения отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                saveFileDialog1.Title = "Сохранить в файл с текстовой кодировкой " + OUnit.getCodePage.kodStranicaName;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                SaveLogConsol(listViewAnswer, saveFileDialog1.FileName);        
            }

            private void настройкиToolStripMenuItem_MouseHover(object sender, EventArgs e)
            {
                настройкиToolStripMenuItem.AutoToolTip = false;
                if (OUnit.isOpenSerialPort)
                    настройкиToolStripMenuItem.ToolTipText = "для задания настроек, надо закрыть serial-порт";
                else
                    настройкиToolStripMenuItem.ToolTipText = "настройки";
                настройкиToolStripMenuItem.AutoToolTip = true;
            }

            private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                string s_temp = "";
                OUnit.CloseSerialPort(ref s_temp);
                // сохраняем настройки
                int indexColumnUse = 0;
                if (Properties.Settings.Default.isShowColumnTime)
                {
                    Properties.Settings.Default.consolCommandTimeWidth = listViewCommand.Columns[indexColumnUse].Width;
                    Properties.Settings.Default.consolAnswerTimeWidth = listViewAnswer.Columns[indexColumnUse].Width;
                    indexColumnUse++;
                }
                if (Properties.Settings.Default.isShowColumnHex)
                {
                    Properties.Settings.Default.consolCommandHexWidth = listViewCommand.Columns[indexColumnUse].Width;
                    Properties.Settings.Default.consolAnswerHexWidth = listViewAnswer.Columns[indexColumnUse].Width;
                    indexColumnUse++;
                }
                if (Properties.Settings.Default.isShowColumnText)
                {
                    Properties.Settings.Default.consolCommandTextWidth = listViewCommand.Columns[indexColumnUse].Width;
                    Properties.Settings.Default.consolAnswerTextWidth = listViewAnswer.Columns[indexColumnUse].Width;
                }
                Properties.Settings.Default.mainFormStartLeft = this.Left;
                Properties.Settings.Default.mainFormStartTop = this.Top;
                Properties.Settings.Default.mainFormWidth = this.Width;
                Properties.Settings.Default.mainFormHeight = this.Height;
            }

            public void textBoxComand_TextChanged(object sender, EventArgs e)
            {
                if (this.radioButtonHex.Checked)
                {
                    textBoxComand.TextChanged -= textBoxComand_TextChanged; // http:// www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
                    Assist.VerifyTextbox(textBoxComand,
                                         OUnit.getRazdByteChar,
                                         Properties.Settings.Default.isRazdByte,
                                         Properties.Settings.Default.isRazdHex8);
                    textBoxComand.TextChanged += textBoxComand_TextChanged;
                    labelCommandTextForm.Text = Assist.StringFromHexTextBoxCommandToText(textBoxComand.Text);
                }
            }

            private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
            {
                radioButtonText.CheckedChanged -= radioButtonText_CheckedChanged; // чтоб не вызывать лишний раз событие  radioButtonUtf8_CheckedChanged
                radioButtonText.Checked = !radioButtonHex.Checked;
                radioButtonText.CheckedChanged += radioButtonText_CheckedChanged;
                if (radioButtonHex.Checked)
                {
                    this.textBoxComand.TextChanged -= textBoxComand_TextChanged; // http:// www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
                    textBoxComand.Text = Assist.StringFromTextToFilteredHex(textBoxComand.Text);
                    Assist.VerifyTextbox(textBoxComand,
                                        OUnit.getRazdByteChar,
                                        Properties.Settings.Default.isRazdByte,
                                        Properties.Settings.Default.isRazdHex8); 
                    this.textBoxComand.TextChanged += textBoxComand_TextChanged;
                    labelCommandTextForm.Visible = Properties.Settings.Default.isDoubleCommandTextForm;
                }
            }

            private void radioButtonText_CheckedChanged(object sender, EventArgs e)
            {
                // проверка на корректность количества символов
                if (radioButtonText.Checked)
                    if (VerifyTextBoxComandHex())
                    {
                        this.textBoxComand.TextChanged -= textBoxComand_TextChanged; // чтоб не вызывать лишний раз событие textBoxComand_TextChanged
                        textBoxComand.Text = Assist.StringFromHexTextBoxCommandToText(textBoxComand.Text);
                        this.textBoxComand.TextChanged += textBoxComand_TextChanged;
                        // следим за radioButtonHex
                        radioButtonHex.CheckedChanged -= radioButtonHex_CheckedChanged; // чтоб не вызывать лишний раз событие radioButtonHex_CheckedChanged
                        radioButtonHex.Checked = !radioButtonText.Checked;
                        radioButtonHex.CheckedChanged += radioButtonHex_CheckedChanged;
                        labelCommandTextForm.Visible = false;
                    }
            }

            private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                listViewCommand.Items.Clear();
                OUnit.dataCommand.Clear();
            }

            private void groupBoxCommand_Resize(object sender, EventArgs e)
            {
                TextBoxCommand_PositionAndSize();
                checkBoxPeriod.Left = groupBoxCommand.ClientSize.Width - 244;
                textBoxPeriod.Left = groupBoxCommand.ClientSize.Width - 160;
                buttonSend.Left = groupBoxCommand.ClientSize.Width - 117;
                pictureBoxGreen.Left = groupBoxCommand.ClientSize.Width - 32;
                pictureBoxRed.Left = pictureBoxGreen.Left;
                listViewCommand.Height = groupBoxCommand.Height - 77;
            }

            private void groupBoxAnswer_Resize(object sender, EventArgs e)
            {
                listViewAnswer.Height = groupBoxAnswer.Height - 25;
            }

            private void textBoxPeriod_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    int cursPosFromEnd = this.textBoxPeriod.Text.Count() - this.textBoxPeriod.SelectionStart; // чтоб курсор не прыгал
                    string s_AsIs = "";
                    const string mask = "0123456789";
                    foreach (char c in textBoxPeriod.Text)
                        if (mask.IndexOf(c) != -1)
                            if (!((s_AsIs.Count() == 0) & (c == '0')))
                                s_AsIs += c;
                    this.textBoxPeriod.TextChanged -= textBoxPeriod_TextChanged; // чтоб не вызывать лишний раз событие textBoxPeriod_TextChanged
                    this.textBoxPeriod.Text = s_AsIs;
                    if (this.textBoxPeriod.Text.Count() - cursPosFromEnd >= 0)
                        this.textBoxPeriod.SelectionStart = this.textBoxPeriod.Text.Count() - cursPosFromEnd;
                    this.textBoxPeriod.TextChanged += textBoxPeriod_TextChanged;
                    Properties.Settings.Default.timerSendCommandInterval = Int32.Parse(s_AsIs);
                }
                catch
                {
                    this.textBoxPeriod.Text = "";
                    Properties.Settings.Default.timerSendCommandInterval = 0;
                }
            }

            private void checkBoxPeriod_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBoxPeriod.Checked)
                {
                    textBoxPeriod.Visible = true;
                    buttonSend.Text = "Старт";
                }
                else
                {
                    textBoxPeriod.Visible = false;
                    buttonSend.Text = "Отправить";
                }
            }

            private void timerSendCommand_Tick(object sender, EventArgs e)
            {
                timerSendCommand.Enabled = false;
                structPacketBytes structPaketSend = new structPacketBytes(new byte[0], true,"пакет ещё не послан");                
                // получаем hex-команду с учётом добавленного CRC-кода                
                if (radioButtonHex.Checked)
                    structPaketSend.dataBytes = Assist.BytesFromFilteredHex(Assist.GetFilteredHex(textBoxComand.Text));
                else
                    structPaketSend.dataBytes = Assist.BytesFromFilteredHex(Assist.StringFromTextToFilteredHex(textBoxComand.Text));
                if (Properties.Settings.Default.isAddCRC)
                    structPaketSend.dataBytes = Assist.GetBytesWithCRC16modbus(structPaketSend.dataBytes);
                // посылаем команду и показываем результат                
                if (!OUnit.WriteData(ref structPaketSend)) // если не получилось записать данные в порт
                {                                  
                    SplitAndAddSentPacketToListViewCommand(structPaketSend);
                    // MessageBox.Show(result_message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    SplitAndAddSentPacketToListViewCommand(structPaketSend);
                timerSendCommand.Enabled = (checkBoxPeriod.Checked);                    
            }

            private void buttonOptionsSerial_Click(object sender, EventArgs e)
            {
                OptionsForm fo = new OptionsForm();
                fo.StartPosition = FormStartPosition.CenterScreen;
                fo.ShowInTaskbar = false;
                fo.tabControlOptions.SelectedIndex = 1;
                fo.ShowDialog(this);
            }

            private void ToolStripMenuItemOptionsAnswer_Click(object sender, EventArgs e)
            {
                OptionsForm fo = new OptionsForm();
                fo.StartPosition = FormStartPosition.CenterScreen;
                fo.ShowInTaskbar = false; 
                fo.tabControlOptions.SelectedIndex = 2;
                fo.ShowDialog(this);
            }

            private void ToolStripMenuItemOptionsCommand_Click(object sender, EventArgs e)
            {
                OptionsForm fo = new OptionsForm();
                fo.StartPosition = FormStartPosition.CenterScreen;
                fo.ShowInTaskbar = false; 
                fo.tabControlOptions.SelectedIndex = 2;
                fo.ShowDialog(this);
            }

            private void ToolStripMenuItemShirinaColAnswer_Click(object sender, EventArgs e)
            {
                ShirinaColAnswerAuto();
            }

            private void ToolStripMenuItemShirinaColCommand_Click(object sender, EventArgs e)
            {
                ShirinaColCommandAuto();
            }

            private void checkBoxPeriod_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(checkBoxPeriod, "Если выбран, то данные в serial-порт отправляются непрерывно с заданным периодом (мс)");
            }
            
            private void radioButtonText_MouseHover(object sender, EventArgs e)
            {
                try
                {
                    toolTip1.SetToolTip(radioButtonText, "Представить данные как текст в кодировке " + OUnit.getCodePage.kodStranicaName);
                }
                catch
                {
                    toolTip1.SetToolTip(radioButtonText, "Представить команду как текст [кодировка некорректна!]");
                }
            }

            private void textBoxPeriod_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(textBoxPeriod, "Период, с которым данные отправляются в serial-порт (мс)");
            }

            private void buttonSend_MouseHover(object sender, EventArgs e)
            {
                if (checkBoxPeriod.Checked)
                    toolTip1.SetToolTip(buttonSend, "Старт непрерывной отправки данных в serial-порт с заданным периодом");
                else
                    toolTip1.SetToolTip(buttonSend, "Отправить данные в serial-порт");
            }

            private void buttonOpen_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(buttonOpen, "Открыть serial-порт");
            }

            private void buttonOptionsSerial_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(buttonOptionsSerial, "Открыть настройки serial-порта");
            }

            private void listViewCommand_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(listViewCommand, "Консоль отправленных данных");
            }

            private void listViewAnswer_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(listViewAnswer, "Консоль принятых данных");
            }
            
            private void ToolStripMenuItemSaveCommand_Click(object sender, EventArgs e)
            {
                int kolColumns = (Properties.Settings.Default.isShowColumnTime ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnHex ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnText ? 1 : 0);
                if ((kolColumns == 0) | (listViewCommand.Items.Count == 0))
                {
                    MessageBox.Show("Данные для сохранения отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SaveLogConsol(listViewCommand, Properties.Settings.Default.fileCommand);
            }

            private void ToolStripMenuItemSaveAsCommand_Click(object sender, EventArgs e)
            {
                int kolColumns = (Properties.Settings.Default.isShowColumnTime ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnHex ? 1 : 0) +
                  (Properties.Settings.Default.isShowColumnText ? 1 : 0);
                if ((kolColumns == 0) | (listViewCommand.Items.Count == 0))
                {
                    MessageBox.Show("Данные для сохранения отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                saveFileDialog1.Title = "Сохранить в файл с текстовой кодировкой " + OUnit.getCodePage.kodStranicaName; 
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                SaveLogConsol(listViewCommand, saveFileDialog1.FileName);
            }

            private void timerPortList_Tick(object sender, EventArgs e)
            {
                if (!Properties.Settings.Default.isCheckConditionSerialPort)
                {
                    comboBoxSerialName.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.WindowText);
                    return;
                }
                // проверка порта на пригодность
                if (!OUnit.TestSerialPort())
                    comboBoxSerialName.ForeColor = Color.Red;
                else
                    comboBoxSerialName.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.WindowText);
            }

            private void MainForm_Shown(object sender, EventArgs e)
            {                
                try
                {
                    // размер формы
                    this.Width = Properties.Settings.Default.mainFormWidth;
                    this.Height = Properties.Settings.Default.mainFormHeight;
                    if ((Properties.Settings.Default.mainFormStartLeft == 0) & (Properties.Settings.Default.mainFormStartTop == 0))
                        this.StartPosition = FormStartPosition.CenterScreen;
                    else
                    {
                        this.Left = Properties.Settings.Default.mainFormStartLeft;
                        this.Top = Properties.Settings.Default.mainFormStartTop;
                    }
                    MainForm_Resize(this, null);
                    groupBoxCommand_Resize(this, null);
                    groupBoxAnswer_Resize(this, null);
                    // учитываем показ ComboBoxSerialName
                    ComboBoxSerialName_ChangeVisible();
                    // связка radioButtonHex - textBoxComand
                    /*textBoxComand.Text = "";
                    if (radioButtonHex.Checked != Properties.Settings.Default.isRadioButtonHexChecked)
                    {
                        radioButtonHex.CheckedChanged -= radioButtonHex_CheckedChanged; // чтоб не вызывать лишний раз событие radioButtonHex_CheckedChanged
                        radioButtonHex.Checked = true;
                        radioButtonHex.CheckedChanged += radioButtonHex_CheckedChanged;
                    }
                    else
                    {
                        radioButtonText.CheckedChanged -= radioButtonText_CheckedChanged; // чтоб не вызывать лишний раз событие  radioButtonUtf8_CheckedChanged
                        radioButtonText.Checked = true;
                        radioButtonText.CheckedChanged += radioButtonText_CheckedChanged;
                    }*/
                    // labelCommandTextForm
                    labelCommandTextForm.Text = Assist.StringFromHexTextBoxCommandToText(textBoxComand.Text);
                    // checkBoxPeriod
                    checkBoxPeriod.Checked = Properties.Settings.Default.isCheckBoxPeriodChecked;
                    // textBoxPeriod
                    textBoxPeriod.Text = Properties.Settings.Default.timerSendCommandInterval.ToString();
                    textBoxPeriod.Visible = checkBoxPeriod.Checked;
                    // comboBoxSerialName // анализируем имя порта, сохранённое в настройках
                    comboBoxSerialName.Items.Clear();
                    if (Properties.Settings.Default.nameComPort == basePortNameNo)
                    {
                        string[] portNames = SerialPort.GetPortNames();
                        if (portNames.Count() > 0)
                            comboBoxSerialName.Items.Add(portNames[0]);
                        else
                            comboBoxSerialName.Items.Add(Properties.Settings.Default.nameComPort);
                    }
                    else
                        comboBoxSerialName.Items.Add(Properties.Settings.Default.nameComPort);
                    comboBoxSerialName.SelectedIndex = 0;
                    comboBoxSerialName.Visible = Properties.Settings.Default.isShowSetSerialPort;
                    // добавлять ли CRC
                    checkBoxIsAddCRC.Checked = Properties.Settings.Default.isAddCRC;
                    // позволять ли вводить текст
                    radioButtonHex.Visible = Properties.Settings.Default.isAllowEnterText;
                    radioButtonText.Visible = Properties.Settings.Default.isAllowEnterText;
                    TextBoxCommand_PositionAndSize();
                    // показывать текстовый вид команды при наборе hex-команды
                    labelCommandTextForm.Visible = Properties.Settings.Default.isDoubleCommandTextForm;
                    // проверяем порт
                    timerPortList_Tick(this, null);
                    // при первом запуске, показываем окно информации и настройки
                    if (IsFirstStartOnPCandMarkStart())
                    {
                        справкаToolStripMenuItem1_Click(this, null);
                        настройкиToolStripMenuItem_Click(this, null);                        
                    }
                    // открытие порта при запуске
                    if (Properties.Settings.Default.isAutoOpenSerial)
                        buttonOpen_Click(sender, null);
                }
                catch { }
                // ширина колонок в консолях
                try
                {
                    CheckConsolColumns();
                    int indexColumnUse = 0;
                    if (Properties.Settings.Default.isShowColumnTime)
                    {
                        listViewCommand.Columns[indexColumnUse].Width = Properties.Settings.Default.consolCommandTimeWidth;
                        listViewAnswer.Columns[indexColumnUse].Width = Properties.Settings.Default.consolAnswerTimeWidth;
                        indexColumnUse++;
                    }
                    if (Properties.Settings.Default.isShowColumnHex)
                    {
                        listViewCommand.Columns[indexColumnUse].Width = Properties.Settings.Default.consolCommandHexWidth;
                        listViewAnswer.Columns[indexColumnUse].Width = Properties.Settings.Default.consolAnswerHexWidth;
                        indexColumnUse++;
                    }
                    if (Properties.Settings.Default.isShowColumnText)
                    {
                        listViewCommand.Columns[indexColumnUse].Width = Properties.Settings.Default.consolCommandTextWidth;
                        listViewAnswer.Columns[indexColumnUse].Width = Properties.Settings.Default.consolAnswerTextWidth;
                    }
                }
                catch { }
            }

            private void comboBoxSerialName_SelectedIndexChanged(object sender, EventArgs e)
            {
                Properties.Settings.Default.nameComPort = comboBoxSerialName.Text;
            }

            private void comboBoxSerialName_DropDown(object sender, EventArgs e)
            {
                // имя порта
                int lastSelectedIndex = comboBoxSerialName.SelectedIndex;
                string findPortName;
                if (comboBoxSerialName.Items.Count > 0)
                    // перед очисткой запоминаем, какой порт ищем
				    findPortName = comboBoxSerialName.Text.Trim().ToLower(); 
                else
                    findPortName = Properties.Settings.Default.nameComPort.Trim().ToLower();
                string[] portNames = SerialPort.GetPortNames();
                comboBoxSerialName.Items.Clear();
                // если портов нет
                if (portNames.Count() == 0)
                {
                    comboBoxSerialName.Items.Add(basePortNameNo);
                    comboBoxSerialName.ForeColor = Color.Red;
                    comboBoxSerialName.SelectedIndex = 0;
                    comboBoxSerialName.Enabled = false;
                }
                // если есть порты
                comboBoxSerialName.Enabled = true;
                comboBoxSerialName.Items.AddRange(portNames);
                if (findPortName == basePortNameNo.Trim().ToLower())
                {
                    comboBoxSerialName.SelectedIndex = 0;
                    return;
                }
                // анализ того что забили                    
                for (int ii = 0; ii < comboBoxSerialName.Items.Count; ii++)
                    if (comboBoxSerialName.Items[ii].ToString().Trim().ToLower() == findPortName)
                    {
                        comboBoxSerialName.SelectedIndex = ii;
                        break;
                    }
            }

            private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Clipboard.SetData(DataFormats.Text, (object)textBoxComand.SelectedText);
                textBoxComand.SelectedText = "";
            }

            private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Clipboard.SetData(DataFormats.Text, (object)textBoxComand.SelectedText);
            }

            private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                textBoxComand.SelectedText = "";
                int lastSelectionStart = textBoxComand.SelectionStart;
                string textFromCpd = Clipboard.GetText(TextDataFormat.UnicodeText);
                textBoxComand.Text = textBoxComand.Text.Insert(textBoxComand.SelectionStart, textFromCpd);
                textBoxComand.SelectionStart = lastSelectionStart + textFromCpd.Length;
            }

            private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                textBoxComand.SelectedText = "";
            }

            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                this.linkLabelCRC.LinkVisited = true;
                System.Diagnostics.Process.Start("https:// yandex.ru/search/?text=расчет%20crc%20онлайн&lr=45&clid=1955454");
            }

            private void radioButtonHex_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(radioButtonHex, "Представить данные в hex-кодировке");
            }

            private void checkBoxIsAddCRC_CheckedChanged(object sender, EventArgs e)
            {
                Properties.Settings.Default.isAddCRC = checkBoxIsAddCRC.Checked;
            }

            private void поУмолчаниюToolStripMenuItem_Click(object sender, EventArgs e)
            {
                radioButtonHex.Checked = true;
                textBoxComand.Text = texboxCommandTextDefault;
            }

            private void labelCommandTextForm_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(labelCommandTextForm, "Текстовое представление набираемой hex-команды.\r\n" +
                                                          "        Следует иметь в виду при преобразовании " +
                                                          "\r\nhex-кода символа в его текстовое представление," +
                                                          "\r\nчто если hex-код принадлежит невидимому символу" +
                                                          "\r\nв таблице кодировки, то его символ отображаться" +
                                                          "\r\nне будет, а hex-код символа может быть потерян!");
            }

            private void textBoxComand_MouseHover(object sender, EventArgs e)
            {
                string mes;
                try
                {
                    mes = "Данные для отправки в serial-порт.\r\nКодовая страница " + OUnit.getCodePage + ".";
                }
                catch
                {
                    mes = "Данные для отправки в serial-порт.\r\nКодовая страница некорректна!";
                }
                // if (radioButtonHex.Checked) mes += " \r\nВводите hex-код правильно! Иначе при перевод в текст будет некорректен!";
                toolTip1.SetToolTip(textBoxComand, mes);
            }
            
            private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                StartInfoForm si = new StartInfoForm();
                si.StartPosition = FormStartPosition.CenterScreen;
                si.ShowInTaskbar = false;
                si.ShowDialog(this);
            }

            private void comboBoxSerialName_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(comboBoxSerialName, "Выбрать serial-порт," +
                                                        "\r\nпри этом все настройки serial-порта" +
                                                        "\r\nбудут как у предыдущего serial-порта");
            }

            private void linkLabelCRC_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(linkLabelCRC, "Добавлять в конец команды её CRC-16 код (Modbus)");
            }

            private void checkBoxIsAddCRC_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(checkBoxIsAddCRC, "Добавлять в конец команды её CRC-16 код (Modbus)");
            }

            #endregion

            #region процедуры и функции VLeshka

            // расположение кнопок открытие порта и настройки порта
            public void ComboBoxSerialName_ChangeVisible() { ComboBoxSerialName_ChangeVisible(Properties.Settings.Default.isShowSetSerialPort); }
            public void ComboBoxSerialName_ChangeVisible(bool isShowSetSerialPort)
            {
                comboBoxSerialName.Visible = isShowSetSerialPort;
                if (isShowSetSerialPort)
                {
                    buttonOptionsSerial.Left = 110;
                    buttonOpen.Left = 141;
                    checkBoxIsAddCRC.Left = 303;
                    linkLabelCRC.Left = 317;
                }
                else
                {
                    buttonOptionsSerial.Left = 18;
                    buttonOpen.Left = 49;
                    checkBoxIsAddCRC.Left = 211;
                    linkLabelCRC.Left = 225;
                }
            }

            // расположение и размер TextBoxCommand
            public void TextBoxCommand_PositionAndSize() { TextBoxCommand_PositionAndSize(Properties.Settings.Default.isAllowEnterText); }
            public void TextBoxCommand_PositionAndSize(bool isAllowText)
            {
                if (isAllowText)
                {
                    textBoxComand.Left = 140;
                    textBoxComand.Width = groupBoxCommand.ClientSize.Width - 392;
                }
                else
                {
                    textBoxComand.Left = 81;
                    textBoxComand.Width = groupBoxCommand.ClientSize.Width - 333;
                }
            }

            // вставляет в listViewCommand строку с данными byte[] dataCommand
            // isFirstLineOfData - первая ли строка данных (разбивается)
            // остальные описания переменных скопированы из CheckConsolColumns()
            // 1 вариант: запись с настройками, которые сохранёны
            // 2 вариант: запись с индвивидуальными настройками
            /*private void AddRowToListViewCommand(DateTime dt, bool isError, string message, byte[] dataCommand, bool isFirstLineOfData)
            {
                AddRowToListViewCommand(dt, isError, message, dataCommand, isFirstLineOfData,
                    Properties.Settings.Default.isShowColumnTime, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isShowTimeMS, Properties.Settings.Default.isTimeFormatPodpis,
                    Properties.Settings.Default.isShowColumnHex, Properties.Settings.Default.isRazdHex8, Properties.Settings.Default.kolBytesPerLineIndex, Properties.Settings.Default.isRazdByte, Properties.Settings.Default.razdByteIndex,
                    Properties.Settings.Default.isShowColumnText, Properties.Settings.Default.indexKodStranicaNomer);
            }*/
            private void AddRowToListViewCommand(DateTime dt, bool isError, string message, byte[] dataCommand, bool isFirstLineOfData,
                                    bool isShowTime, int timeFormatIndex, bool isTimeMS, bool isTimePodpis,
                                    bool isShownHex, bool isHex8Razd, int kolBytePerLineIndex, bool isHexRazd, int razdByteIndex,
                                    bool isShowText, int indexKodStranicaNomer)
            {
                ListViewItem lvi = new ListViewItem();              
                // заполняем данными
                for (int ii = 0; ii < (isShowTime ? 1 : 0) + (isShownHex ? 1 : 0) + (isShowText ? 1 : 0); ii++)
                    lvi.SubItems.Add("");
                int indexColumnUse = 0;
                if (isShowTime)
                {
                    if (isFirstLineOfData)
                        lvi.SubItems[indexColumnUse].Text = Assist.GetTimeFormat(dt,timeFormatIndex,isTimeMS,isTimePodpis);
                    indexColumnUse++;
                }
                if (isShownHex)
                {
                    if (isError)
                        lvi.SubItems[indexColumnUse].Text = message;
                    else
                        lvi.SubItems[indexColumnUse].Text = Assist.GetVerifyHexText(dataCommand,
                                                                 OUnit.GetRazdByteCharByIndex(razdByteIndex),
                                                                 isHexRazd,
                                                                 isHex8Razd);
                    indexColumnUse++;
                }
                if (isShowText)
                {
                    if (isError)
                        lvi.SubItems[indexColumnUse].Text = message;
                    else
                        lvi.SubItems[indexColumnUse].Text = Assist.StringFromHexTextBoxCommandToText(dataCommand, OUnit.GetCodePageByIndex(indexKodStranicaNomer));
                }
                lvi.ToolTipText = "Консоль отправленных данных в serial-порт";
                listViewCommand.Items.Add(lvi);
                // если слишком много строк, удаляем лишние
                if (this.listViewCommand.Items.Count > OUnit.getMaxPacketBytesCount)
                    this.listViewCommand.Items.RemoveAt(0);
                // фокус на последний элемент
                this.listViewCommand.EnsureVisible(listViewCommand.Items.Count - 1);
                this.listViewCommand.Items[listViewCommand.Items.Count - 1].Focused = true;
                this.listViewCommand.Items[listViewCommand.Items.Count - 1].Selected = true;
            }

            // вставляет в listViewAnswer строку с данными byte[] dataCommand
            // isFirstLineOfData - первая ли строка данных (разбивается)
            // остальные описания переменных скопированы из CheckConsolColumns()
            // 1 вариант: запись с настройками, которые сохранёны
            // 2 вариант: запись с индвивидуальными настройками
            private void AddRowToListViewAnswer(DateTime dt, bool isError, string message, byte[] dataCommand, bool isFirstLineOfData)
            {
                AddRowToListViewAnswer(dt, isError, message, dataCommand, isFirstLineOfData,
                    Properties.Settings.Default.isShowColumnTime, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isShowTimeMS, Properties.Settings.Default.isTimeFormatPodpis,
                    Properties.Settings.Default.isShowColumnHex, Properties.Settings.Default.isRazdHex8, Properties.Settings.Default.kolBytesPerLineIndex, Properties.Settings.Default.isRazdByte, Properties.Settings.Default.razdByteIndex,
                    Properties.Settings.Default.isShowColumnText, Properties.Settings.Default.indexKodStranicaNomer,
                    Properties.Settings.Default.isRazdPaketByPosledovSimbols, Properties.Settings.Default.simbolsRazdPaketByPosledovSimbols);
            }
            private void AddRowToListViewAnswer(DateTime dt, bool isError, string message, byte[] dataCommand, bool isFirstLineOfData,
                                    bool isShowTime, int timeFormatIndex, bool isTimeMS, bool isTimePodpis,
                                    bool isShownHex, bool isHex8Razd, int kolBytePerLineIndex, bool isHexRazd, int razdByteIndex,
                                    bool isShowText, int indexKodStranicaNomer,
                                    bool isRazdByHexSimbols, string razdByHexSimbols)
            {
                ListViewItem lvi = new ListViewItem();
                // заполняем данными
                for (int ii = 0; ii < (isShowTime ? 1 : 0) + (isShownHex ? 1 : 0) + (isShowText ? 1 : 0); ii++)
                    lvi.SubItems.Add("");
                int indexColumnUse = 0;
                if (isShowTime)
                {
                    if (isFirstLineOfData)
                        lvi.SubItems[indexColumnUse].Text = Assist.GetTimeFormat(dt, timeFormatIndex, isTimeMS, isTimePodpis);
                    indexColumnUse++;
                }
                if (isShownHex)
                {
                    if (isError)
                        lvi.SubItems[indexColumnUse].Text = message;
                    else
                        lvi.SubItems[indexColumnUse].Text = Assist.GetVerifyHexText(dataCommand,
                                                                 OUnit.GetRazdByteCharByIndex(razdByteIndex),
                                                                 isHexRazd,
                                                                 isHex8Razd);
                    indexColumnUse++;
                }
                if (isShowText)
                {
                    if (isError)
                        lvi.SubItems[indexColumnUse].Text = message;
                    else
                        lvi.SubItems[indexColumnUse].Text = Assist.StringFromHexTextBoxCommandToText(dataCommand, OUnit.GetCodePageByIndex(indexKodStranicaNomer));
                }
                lvi.ToolTipText = "Консоль отправленных данных в serial-порт";
                listViewAnswer.Items.Add(lvi);
                // если слишком много строк, удаляем лишние
                if ( this.listViewAnswer.Items.Count > OUnit.getMaxPacketBytesCount)
                    this.listViewAnswer.Items.RemoveAt(0);
                // фокус на последний элемент
                this.listViewAnswer.EnsureVisible(listViewAnswer.Items.Count - 1);
                this.listViewAnswer.Items[listViewAnswer.Items.Count - 1].Focused = true;
                this.listViewAnswer.Items[listViewAnswer.Items.Count - 1].Selected = true;
            }

            // парсит строку данных и размещает в ListViewCommand 
            // dataCommand - пакект данных // остальные описания переменных скопированы из CheckConsolColumns()
            // 1 вариант: запись с настройками, которые сохранёны
            // 2 вариант: запись с индвивидуальными настройками
            private void SplitAndAddSentPacketToListViewCommand(structPacketBytes dataCommand)
            {
                SplitAndAddSentPacketToListViewCommand(dataCommand,
                    Properties.Settings.Default.isShowColumnTime, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isShowTimeMS, Properties.Settings.Default.isTimeFormatPodpis,
                    Properties.Settings.Default.isShowColumnHex, Properties.Settings.Default.isRazdHex8, Properties.Settings.Default.kolBytesPerLineIndex, Properties.Settings.Default.isRazdByte, Properties.Settings.Default.razdByteIndex,
                    Properties.Settings.Default.isShowColumnText, Properties.Settings.Default.indexKodStranicaNomer);
            }
            private void SplitAndAddSentPacketToListViewCommand(structPacketBytes dataCommand,
                                                                bool isShowTime, int timeFormatIndex, bool isTimeMS, bool isTimePodpis,
                                                                bool isShownHex, bool isHex8Razd, int kolBytePerLineIndex, bool isHexRazd, int razdByteIndex,
                                                                bool isShowText, int indexKodStranicaNomer)
            {
                if (dataCommand.isResultError)
                {
                    AddRowToListViewCommand(dataCommand.timeDone, true, dataCommand.resultDiagnosticMessage, new byte[0], true,
                                                           isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                           isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                           isShowText, indexKodStranicaNomer);
                    return;
                }
                byte[][] dataRowsBytes = Assist.BytesSplitByKolBytes(dataCommand.dataBytes, OUnit.GetKolBytesPerLineByIndex(kolBytePerLineIndex));
                for (int iRazdKol = 0; iRazdKol < dataRowsBytes.Length; iRazdKol++)
                    AddRowToListViewCommand(dataCommand.timeDone, false, dataCommand.resultDiagnosticMessage, dataRowsBytes[iRazdKol], iRazdKol == 0 ? true : false,
                                                           isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                           isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                           isShowText, indexKodStranicaNomer);
            }

            // парсит строку данных и размещает в ListViewAnswer 
            // dataCommand - пакект данных // остальные описания переменных скопированы из CheckConsolColumns()
            // 1 вариант: запись с настройками, которые сохранёны
            // 2 вариант: запись с индвивидуальными настройками
            private void SplitAndAddSentPacketToListViewAnswer(structPacketBytes dataAnswer)
            {
                SplitAndAddSentPacketToListViewAnswer(dataAnswer,
                    Properties.Settings.Default.isShowColumnTime, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isShowTimeMS, Properties.Settings.Default.isTimeFormatPodpis,
                    Properties.Settings.Default.isShowColumnHex, Properties.Settings.Default.isRazdHex8, Properties.Settings.Default.kolBytesPerLineIndex, Properties.Settings.Default.isRazdByte, Properties.Settings.Default.razdByteIndex,
                    Properties.Settings.Default.isShowColumnText, Properties.Settings.Default.indexKodStranicaNomer,
                    Properties.Settings.Default.isRazdPaketByPosledovSimbols, Properties.Settings.Default.simbolsRazdPaketByPosledovSimbols);
            }
            private void SplitAndAddSentPacketToListViewAnswer(structPacketBytes dataAnswer,
                                                                bool isShowTime, int timeFormatIndex, bool isTimeMS, bool isTimePodpis,
                                                                bool isShownHex, bool isHex8Razd, int kolBytePerLineIndex, bool isHexRazd, int razdByteIndex,
                                                                bool isShowText, int indexKodStranicaNomer,
                                                                bool isRazdByHexSimbols, string razdByHexSimbols)
            {                
                if (dataAnswer.isResultError)
                {                       
                    AddRowToListViewAnswer(dataAnswer.timeDone, true, dataAnswer.resultDiagnosticMessage, new byte[0], true,
                                                           isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                           isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                           isShowText, indexKodStranicaNomer,
                                                           isRazdByHexSimbols, razdByHexSimbols);
                    return;
                }                
                byte[][] dataRowsKolBytes = Assist.BytesSplitByKolBytes(dataAnswer.dataBytes, OUnit.GetKolBytesPerLineByIndex(kolBytePerLineIndex));
                for (int iRazdKol = 0; iRazdKol < dataRowsKolBytes.Length; iRazdKol++)
                {
                    if (isRazdByHexSimbols)
                    {
                        byte[][] dataRowsHexSimbols = Assist.BytesSplitByRazdPaket(dataRowsKolBytes[iRazdKol],
                                                             Assist.BytesFromFilteredHex(Assist.GetFilteredHex(razdByHexSimbols)));
                        for (int iRazdHex = 0; iRazdHex < dataRowsHexSimbols.Length; iRazdHex++)
                            AddRowToListViewAnswer(dataAnswer.timeDone, false, dataAnswer.resultDiagnosticMessage, dataRowsHexSimbols[iRazdHex], ((iRazdKol == 0) & (iRazdHex == 0)) ? true : false,
                                                               isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                               isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                               isShowText, indexKodStranicaNomer,
                                                               isRazdByHexSimbols, razdByHexSimbols);
                    }
                    else
                        AddRowToListViewAnswer(dataAnswer.timeDone, false, dataAnswer.resultDiagnosticMessage, dataRowsKolBytes[iRazdKol], (iRazdKol == 0) ? true : false,
                                                               isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                               isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                               isShowText, indexKodStranicaNomer,
                                                               isRazdByHexSimbols, razdByHexSimbols);
                }
            }

            // функция делегата
            void EventAnswerAdd(structPacketBytes d)
            {           
                // передаём в другой делегат в потоке this
                InserInListBoxAnswer dlgt = new InserInListBoxAnswer(SplitAndAddSentPacketToListViewAnswer);
                this.Invoke(dlgt, d);
            }

            // проверка на корректность количества символов в hex-строке отправляемой команды
            bool VerifyTextBoxComandHex()
            {
                if (Assist.GetFilteredHex(textBoxComand.Text).Count() % 2 != 0)
                    {
                        radioButtonHex.CheckedChanged -= radioButtonHex_CheckedChanged; // чтоб не вызывать лишний раз событие radioButtonHex_CheckedChanged
                        radioButtonText.CheckedChanged -= radioButtonText_CheckedChanged; // чтоб не вызывать лишний раз событие  radioButtonUtf8_CheckedChanged
                        radioButtonHex.Checked = true;
                        radioButtonText.Checked = false;
                        radioButtonHex.CheckedChanged += radioButtonHex_CheckedChanged;
                        radioButtonText.CheckedChanged += radioButtonText_CheckedChanged;
                        // готовим сообщение
                        string r = "";
                        if (Properties.Settings.Default.isRazdByte)
                            r = OUnit.razdBytes[Properties.Settings.Default.razdByteIndex][0].ToString();
                        MessageBox.Show("В отправляемой команде число символов нечётное.\r\n Например, \"39" + r + "9\" некорректно, \"39" + r + "09\" или \"39\" корректно.",
                                        "Некорректное число символов",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                        return (false);
                    }                    
                return (true);
            }

            // приводит отображение в консолях согласно параметрам
            // настройки для колонки время
            // настройки для колонки hex
            // настройки для колонки текст
            // остальные настройки
            public void CheckConsolColumns()
            {
                CheckConsolColumns(Properties.Settings.Default.isShowColumnTime, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isShowTimeMS, Properties.Settings.Default.isTimeFormatPodpis,
                                   Properties.Settings.Default.isShowColumnHex, Properties.Settings.Default.isRazdHex8, Properties.Settings.Default.kolBytesPerLineIndex, Properties.Settings.Default.isRazdByte, Properties.Settings.Default.razdByteIndex,
                                   Properties.Settings.Default.isShowColumnText, Properties.Settings.Default.indexKodStranicaNomer,
                                   Properties.Settings.Default.isRazdPaketByPosledovSimbols, Properties.Settings.Default.simbolsRazdPaketByPosledovSimbols);
            }
            public void CheckConsolColumns(bool isShowTime, int timeFormatIndex, bool isTimeMS, bool isTimePodpis,
                                           bool isShownHex, bool isHex8Razd, int kolBytePerLineIndex, bool isHexRazd, int razdByteIndex,
                                           bool isShowText, int indexKodStranicaNomer,
                                           bool isRazdByHexSimbols, string razdByHexSimbols)
            {
                // чтоб не мерцало, обходимся без Clear()
                int kolColumns = (isShowTime ? 1 : 0) + (isShownHex ? 1 : 0) + (isShowText ? 1 : 0);
                // I. корректируем структуру столбцов
                while (listViewCommand.Columns.Count < kolColumns)
                    listViewCommand.Columns.Add("");
                while (listViewAnswer.Columns.Count < kolColumns)
                    listViewAnswer.Columns.Add("");
                while (listViewCommand.Columns.Count > kolColumns)
                    listViewCommand.Columns.RemoveAt(0);
                while (listViewAnswer.Columns.Count > kolColumns)
                    listViewAnswer.Columns.RemoveAt(0);
                int indexColumnUse = 0;
                if ((!isShowTime) & (!isShownHex) & (!isShowText))
                {
                    labelCommandAllColsOff.Visible = true;
                    labelAnswerAllColsOff.Visible = true;
                    return;
                }
                else
                {
                    labelCommandAllColsOff.Visible = false;
                    labelAnswerAllColsOff.Visible = false;
                }
                // II. шапки столбцов
                if (isShowTime)
                {
                    listViewCommand.Columns[indexColumnUse].Text = GetNameColulumnTime;
                    listViewAnswer.Columns[indexColumnUse].Text = GetNameColulumnTime;
                    indexColumnUse++;
                }
                if (isShownHex)
                {
                    listViewCommand.Columns[indexColumnUse].Text = GetNameColulumnHex;
                    listViewAnswer.Columns[indexColumnUse].Text = GetNameColulumnHex;
                    indexColumnUse++;
                }
                if (isShowText)
                {
                    listViewCommand.Columns[indexColumnUse].Text = GetNameColulumnText;
                    listViewAnswer.Columns[indexColumnUse].Text = GetNameColulumnText;
                }
                // III.данные столбцов
                // listViewCommand
                listViewCommand.Items.Clear();
                foreach (var item in OUnit.dataCommand)
                    SplitAndAddSentPacketToListViewCommand(item.Value,
                                                           isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                           isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                           isShowText, indexKodStranicaNomer);
                // listViewAnswer         
                listViewAnswer.Items.Clear();
                foreach (var item in OUnit.dataAnswer)
                    SplitAndAddSentPacketToListViewAnswer(item.Value,
                                                          isShowTime, timeFormatIndex, isTimeMS, isTimePodpis,
                                                          isShownHex, isHex8Razd, kolBytePerLineIndex, isHexRazd, razdByteIndex,
                                                          isShowText, indexKodStranicaNomer,
                                                          isRazdByHexSimbols, razdByHexSimbols);
                // IV.ширина столбцов
                this.ShirinaColCommandAuto(isShowTime, isShownHex, isShowText);
                this.ShirinaColAnswerAuto(isShowTime, isShownHex, isShowText);     
            }

            // выравнивает автоматически ширину колонок в listViewCommand
            private void ShirinaColCommandAuto() { 
                ShirinaColCommandAuto(Properties.Settings.Default.isShowColumnTime,
                                      Properties.Settings.Default.isShowColumnHex,
                                      Properties.Settings.Default.isShowColumnText);
            }
            private void ShirinaColCommandAuto(bool isShowTime, bool isShownHex, bool isShowText)
            {                
                int clientWidthConsolCommand = listViewCommand.ClientSize.Width;
                int indexColumnUse = 0;
                if (isShowTime)
                {
                    if ((!isShownHex) & (!isShowText))
                    {
                        listViewCommand.Columns[indexColumnUse].Width = clientWidthConsolCommand;
                        return;
                    }
                    int wdt; // ширина колонки время
                    if (listViewCommand.Items.Count == 0)
                        wdt = TextRenderer.MeasureText(DateTime.Now.ToString() + "Мc", listViewCommand.Font).Width;
                    else
                    {
                        wdt = TextRenderer.MeasureText(listViewCommand.Items[0].Text, listViewCommand.Font).Width;
                        for (int i_temp1 = 1; i_temp1 < (listViewCommand.Items.Count > 100 ? 100 : listViewCommand.Items.Count); i_temp1++)
                        {
                            int w = TextRenderer.MeasureText(listViewCommand.Items[i_temp1].Text, listViewCommand.Font).Width;
                            if (w > wdt)
                                wdt = w;
                        }
                    }
                    listViewCommand.Columns[indexColumnUse].Width = wdt + 1;
                    clientWidthConsolCommand -= listViewCommand.Columns[0].Width;
                    indexColumnUse++;
                }
                if (isShownHex)
                {
                    if (isShowText)
                    {
                        int w = TextRenderer.MeasureText(listViewCommand.Columns[indexColumnUse + 1].Text, listViewCommand.Font).Width + 10;
                        listViewCommand.Columns[indexColumnUse + 1].Width = ((int)(clientWidthConsolCommand * 0.25) < w ? w :
                                                                            (int)(clientWidthConsolCommand * 0.25)) + 1;
                        listViewCommand.Columns[indexColumnUse].Width = clientWidthConsolCommand - listViewCommand.Columns[indexColumnUse + 1].Width;
                        
                    }
                    else
                        listViewCommand.Columns[indexColumnUse].Width = clientWidthConsolCommand;
                }
                else
                    if (isShowText)
                        listViewCommand.Columns[indexColumnUse].Width = clientWidthConsolCommand;
            }

            // выравнивает автоматически ширину колонок в listViewAnswer
            private void ShirinaColAnswerAuto()
            {
                ShirinaColAnswerAuto(Properties.Settings.Default.isShowColumnTime,
                                     Properties.Settings.Default.isShowColumnHex,
                                     Properties.Settings.Default.isShowColumnText);
            }
            private void ShirinaColAnswerAuto(bool isShowTime, bool isShownHex, bool isShowText)
            {
                int clientWidthConsolCommand = listViewAnswer.ClientSize.Width;
                int indexColumnUse = 0;
                if (isShowTime)
                {
                    if ((!isShownHex) & (!isShowText))
                    {
                        listViewAnswer.Columns[indexColumnUse].Width = clientWidthConsolCommand;
                        return;
                    }
                    // ширина колонки время
					int wdt; 
                    if (listViewAnswer.Items.Count == 0)
                        wdt = TextRenderer.MeasureText(DateTime.Now.ToString() + "Мc", listViewAnswer.Font).Width;
                    else
                    {
                        wdt = TextRenderer.MeasureText(listViewAnswer.Items[0].Text, listViewAnswer.Font).Width;
                        for (int i_temp1 = 1; i_temp1 < listViewAnswer.Items.Count; i_temp1++)
                        {
                            int w = TextRenderer.MeasureText(listViewAnswer.Items[i_temp1].Text, listViewAnswer.Font).Width;
                            if (w > wdt)
                                wdt = w;
                        }
                    }
                    listViewAnswer.Columns[indexColumnUse].Width = wdt + 1;
                    clientWidthConsolCommand -= listViewAnswer.Columns[0].Width;
                    indexColumnUse++;
                }
                if (isShownHex)
                {
                    if (isShowText)
                    {
                        int w = TextRenderer.MeasureText(listViewAnswer.Columns[indexColumnUse + 1].Text, listViewAnswer.Font).Width + 10;
                        listViewAnswer.Columns[indexColumnUse + 1].Width = ((int)(clientWidthConsolCommand * 0.25) < w ? w :
                                                                            (int)(clientWidthConsolCommand * 0.25)) + 1;
                        listViewAnswer.Columns[indexColumnUse].Width = clientWidthConsolCommand - listViewAnswer.Columns[indexColumnUse + 1].Width;

                    }
                    else
                        listViewAnswer.Columns[indexColumnUse].Width = clientWidthConsolCommand;
                }
                else
                    if (isShowText)
                        listViewAnswer.Columns[indexColumnUse].Width = clientWidthConsolCommand;
            }

            // сохраняет лог заданной консоли в используемой кодировке
            private void SaveLogConsol(ListView lv, string fName)
            {
                StreamWriter st = new StreamWriter(fName, false, Encoding.GetEncoding(OUnit.getCodePage.kodStranicaNomer));
                string betweenColumns = "     ";
                // число знакомест под колонку время
                int wight1 = lv.Items[0].Text.Length;
                if (Properties.Settings.Default.isShowColumnTime)
                    for (int i_temp1 = 1; i_temp1 < (lv.Items.Count > 100 ? 100 : lv.Items.Count); i_temp1++)
                        if (wight1 < lv.Items[i_temp1].Text.Length)
                            wight1 = lv.Items[i_temp1].Text.Length;
                // string row строка в файл
				string s_row = ""; 
                // какую колонку читаем
				int indexColumnUse = 0; 
                // шапка
                if (Properties.Settings.Default.isShowColumnTime)
                {
                    s_row += Assist.StringByLength(lv.Columns[indexColumnUse].Text, wight1);
                    indexColumnUse++;
                }
                if (Properties.Settings.Default.isShowColumnHex)
                {
                    if (Properties.Settings.Default.isShowColumnTime)
                        s_row += betweenColumns;
                    s_row += Assist.StringByLength(lv.Columns[indexColumnUse].Text, OUnit.maxLengthConsolHexText);
                    indexColumnUse++;
                }
                if (Properties.Settings.Default.isShowColumnText)
                {
                    if (Properties.Settings.Default.isShowColumnTime | Properties.Settings.Default.isShowColumnHex)
                        s_row += betweenColumns;
                    s_row += Assist.StringByLength(lv.Columns[indexColumnUse].Text, OUnit.getKolBytesPerLine);
                }
                st.WriteLine(s_row);
                // данные
                for (int i_temp1 = 0; i_temp1 < lv.Items.Count; i_temp1++)
                {
                    indexColumnUse = 0;
                    s_row = "";
                    if (Properties.Settings.Default.isShowColumnTime)
                    {
                        s_row += Assist.StringByLength(lv.Items[i_temp1].Text, wight1);
                        indexColumnUse++;
                    }
                    if (Properties.Settings.Default.isShowColumnHex)
                    {
                        if (Properties.Settings.Default.isShowColumnTime)
                            s_row += betweenColumns;
                        s_row += Assist.StringByLength(lv.Items[i_temp1].SubItems[1].Text, OUnit.maxLengthConsolHexText);
                        indexColumnUse++;
                    }
                    if (Properties.Settings.Default.isShowColumnText)
                    {
                        if (Properties.Settings.Default.isShowColumnTime | Properties.Settings.Default.isShowColumnHex)
                            s_row += betweenColumns;
                        s_row += Assist.StringByLength(lv.Items[i_temp1].SubItems[2].Text, OUnit.getKolBytesPerLine);
                    }
                    st.WriteLine(s_row);
                }
                st.Close();
            }

            // в первый ли раз запускается эта программа на компьютере, и создаёт папку=метку в реестре, что запускалась
            private bool IsFirstStartOnPCandMarkStart()
            {
                string podrazdel = "Software\\" + progRegName;
                RegistryKey rgk = Registry.CurrentUser.OpenSubKey(podrazdel);
                try
                {
                    if (rgk == null)
                    {
                        Registry.CurrentUser.CreateSubKey(podrazdel);
                        return (true);
                    }
                    else
                        return (false);
                }
                catch
                {
                    return (true);
                }
                finally
                {
                    if (rgk != null)
                        rgk.Close();
                }
            }

            #endregion
                                         
        }
 
}

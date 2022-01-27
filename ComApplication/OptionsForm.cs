using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ComApplication
{
    
    public partial class OptionsForm : Form
    {
        #region переменные, константы и простые методы для их получения

        //индекс последней выбранной вкладки tabControlOptions
        int lastSelectedIndextabControlOptions;

        //делегат функции обновления таблиц в mainForm //для изменения CheckConsolColumns сразу для всех объектов, использующих этот метод
        private delegate void mainFormConsolUpdate();
        mainFormConsolUpdate CheckConsolColumns;

        #endregion

        #region автоматически создаваемые методы

        public OptionsForm()
        {
            InitializeComponent();
            lastSelectedIndextabControlOptions = tabControlOptions.SelectedIndex;
        }

        private void FormOptions_Shown(object sender, EventArgs e)
        {
            CheckConsolColumns = new mainFormConsolUpdate(CheckConsolColumnsNull); //чтобы не вызывать лишний раз CheckConsolColumnsWork, заменяем его на CheckConsolColumnsNull

            #region вкладка Общие
            checkBoxAutoOpenSerial.Checked = Properties.Settings.Default.isAutoOpenSerial;
            checkBoxAllowEnterText.Checked = Properties.Settings.Default.isAllowEnterText;
            checkBoxDoubleCommandTextForm.Checked = Properties.Settings.Default.isDoubleCommandTextForm;
            labelInfo.Text = "        Следует иметь в виду при преобразовании " +
                             "\r\nhex-кода символа в его текстовое представление," +
                             "\r\nчто если hex-код принадлежит невидимому символу" +
                             "\r\nв таблице кодировки, то его символ отображаться" +
                             "\r\nне будет, а hex-код символа может быть потерян!";
            #endregion

            #region вкладка Порты
            checkBoxWindowSetSerialPort.Checked = Properties.Settings.Default.isShowSetSerialPort;
            checkBoxCheckCondition.Enabled = checkBoxWindowSetSerialPort.Checked;
            //следить за исправностью порта
            checkBoxCheckCondition.Checked = Properties.Settings.Default.isCheckConditionSerialPort;//имя порта
            //comboBoxSerialName //анализируем имя порта, сохранённое в настройках
            comboBoxSerialName.Items.Clear();
            if (Properties.Settings.Default.nameComPort == ((MainForm)this.Owner).GetBasePortNameNo)
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
            timerPortList_Tick(this, null);
            //скорость порта
            comboBoxSerialSpeed.Items.Clear();
            foreach (int ii in OUnit.serialSpeed.Values)
               comboBoxSerialSpeed.Items.Add(ii.ToString());
            comboBoxSerialSpeed.SelectedIndex = Properties.Settings.Default.indexComPort_Speed;
            //DataBits //бит данных
            comboBoxSerialDataBits.Items.Clear();
            foreach (int ii in OUnit.serialDataBits.Values)
                comboBoxSerialDataBits.Items.Add(ii.ToString());
            comboBoxSerialDataBits.SelectedIndex = Properties.Settings.Default.indexComPort_Databits;
            //SerialParity //бит чётности
            comboBoxSerialParity.Items.Clear();
            //foreach (Parity p in OUnit.serialParity.Values) comboBoxSerialParity.Items.Add(p.ToString());
            comboBoxSerialParity.Items.Add("не осуществляется");
            comboBoxSerialParity.Items.Add("число установленных битов всегда нечётно");
            comboBoxSerialParity.Items.Add("число установленных битов всегда чётно");
            comboBoxSerialParity.Items.Add("бит четности = 1");
            comboBoxSerialParity.Items.Add("бит четности = 0");
            comboBoxSerialParity.SelectedIndex = Properties.Settings.Default.indexComPort_Parity;
            //StopBits //стоповые биты
            comboBoxSerialStopBits.Items.Clear();
            //foreach (StopBits s in OUnit.serialStopBits.Values) comboBoxSerialStopBits.Items.Add(s.ToString());
            comboBoxSerialStopBits.Items.Add("стоповые биты не используются");
            comboBoxSerialStopBits.Items.Add("один стоповый бит");
            comboBoxSerialStopBits.Items.Add("1,5 стоповых бита");
            comboBoxSerialStopBits.Items.Add("два стоповых бита");
            comboBoxSerialStopBits.SelectedIndex = Properties.Settings.Default.indexComPort_StopBits;
            //ожидание записи по serial-порту
            textBoxSerialTimeWriteWait.Text = Properties.Settings.Default.serialPort_WaitWrite.ToString();
            checkBoxSerialTimeWriteWait.Checked = Properties.Settings.Default.isSerialPort_WaitWrite;
            textBoxSerialTimeWriteWait.Enabled = checkBoxSerialTimeWriteWait.Checked;
            //ожидание ответа по serial-порту
            textBoxSerialTimeAnswerWait.Text = Properties.Settings.Default.serialPort_WaitAnswer.ToString();
            checkBoxSerialTimeAnswerWait.Checked = Properties.Settings.Default.isSerialPort_WaitAnswer;
            textBoxSerialTimeAnswerWait.Enabled = checkBoxSerialTimeAnswerWait.Checked;            
            #endregion

            #region вкладка Консоль
            //файлы логов по умолчанию
            textBoxDefaultFileLogPrinat.Text = Properties.Settings.Default.fileAnswerDefault;
            textBoxDefailtFileLogSend.Text = Properties.Settings.Default.fileCommandDefault;
            //формат времени
            comboBoxTimeFormat.Items.Clear();
            foreach (string s in OUnit.timeFormat.Values)
                comboBoxTimeFormat.Items.Add(s);
            comboBoxTimeFormat.SelectedIndex = Properties.Settings.Default.timeFormatIndex;
            //показывать миллисекунды
            checkBoxTimeMS.Checked = Properties.Settings.Default.isTimeFormatPodpis;
            //подписывать ли время
            checkBoxTimePodpis.Checked = Properties.Settings.Default.isTimeFormatPodpis;
            //разделять hex-символы разделителем '|' по 8 байт:
            checkBoxRazd8byte.Checked = Properties.Settings.Default.isRazdHex8;
            //макс. кол-во байт в каждой строке данных
            comboBoxNumByte.Items.Clear();
            foreach (int s in OUnit.kolBytesPerLine.Values)
                comboBoxNumByte.Items.Add(s.ToString());
            comboBoxNumByte.SelectedIndex = Properties.Settings.Default.kolBytesPerLineIndex;
            //разделять ли байты, разделитель            
            checkBoxRazdByte.CheckedChanged -= checkBoxRazdByte_CheckedChanged; //т.к. связан
            checkBoxRazdByte.Checked = Properties.Settings.Default.isRazdByte;
            checkBoxRazdByte.CheckedChanged += checkBoxRazdByte_CheckedChanged;
            comboBoxRazdByte.SelectedIndexChanged -= comboBoxRazd_SelectedIndexChanged; //т.к. связан с другими объектами
            comboBoxRazdByte.Items.Clear();
            foreach (string s in OUnit.razdBytes.Values)
                comboBoxRazdByte.Items.Add(s);
            comboBoxRazdByte.SelectedIndex = Properties.Settings.Default.razdByteIndex;
            comboBoxRazdByte.SelectedIndexChanged += comboBoxRazd_SelectedIndexChanged;
            comboBoxRazdByte.Enabled = checkBoxRazdByte.Checked;
            //кодировка текста
            comboBoxCodePage.Items.Clear();
            foreach (var i in OUnit.codePage)
                comboBoxCodePage.Items.Add(i.Value.kodStranicaName);
            comboBoxCodePage.SelectedIndex = Properties.Settings.Default.indexKodStranicaNomer;
            //разделять принятые пакеты по посл-ти символов (hex), сами символы
            checkBoxRazdPaket.Checked = Properties.Settings.Default.isRazdPaketByPosledovSimbols;
            textBoxSimbolsRazdPaketByPosledovSimbols.Text = Properties.Settings.Default.simbolsRazdPaketByPosledovSimbols;
            textBoxSimbolsRazdPaketByPosledovSimbols.Enabled = checkBoxRazdPaket.Checked;
            //отображение колонок
            checkBoxShowInConsolColumnTime.Checked = Properties.Settings.Default.isShowColumnTime;
            checkBoxShowInConsolColumnHex.Checked = Properties.Settings.Default.isShowColumnHex;
            checkBoxShowInConsolColumnText.Checked = Properties.Settings.Default.isShowColumnText;
            #endregion

            CheckConsolColumns = new mainFormConsolUpdate(CheckConsolColumnsWork);
        }

        private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (lastSelectedIndextabControlOptions == 2)
                    if (checkBoxRazdPaket.Checked)
                        if (Assist.GetFilteredHex(textBoxSimbolsRazdPaketByPosledovSimbols.Text).Count() % 2 != 0)
                        {
                            tabControlOptions_Selecting(this, null);
                            this.DialogResult = DialogResult.None;
                            return;
                        }

                #region вкладка Общие
                try
                {
                    Properties.Settings.Default.isAutoOpenSerial = checkBoxAutoOpenSerial.Checked;
                    Properties.Settings.Default.isAllowEnterText = checkBoxAllowEnterText.Checked;
                    Properties.Settings.Default.isDoubleCommandTextForm = checkBoxDoubleCommandTextForm.Checked;
                }
                catch
                {
                    MessageBox.Show("Некорректные параметры на вкладке " + tabPageGeneral.Text,
                                        "Предупреждение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                #endregion

                #region вкладка Порты
                try
                {
                    //отображать раскрывающийся список выбора serial-порта
                    Properties.Settings.Default.isShowSetSerialPort = checkBoxWindowSetSerialPort.Checked;
                    //основные настройки порта
                    Properties.Settings.Default.nameComPort = comboBoxSerialName.Items[comboBoxSerialName.SelectedIndex].ToString();
                    Properties.Settings.Default.indexComPort_Speed = comboBoxSerialSpeed.SelectedIndex;
                    Properties.Settings.Default.indexComPort_Databits = comboBoxSerialDataBits.SelectedIndex;
                    Properties.Settings.Default.indexComPort_Parity = comboBoxSerialParity.SelectedIndex;
                    Properties.Settings.Default.indexComPort_StopBits = comboBoxSerialStopBits.SelectedIndex;
                    Properties.Settings.Default.serialPort_WaitAnswer = Int32.Parse(textBoxSerialTimeAnswerWait.Text);
                    //ожидание записи по serial-порту
                    Properties.Settings.Default.serialPort_WaitWrite = Int32.Parse(textBoxSerialTimeWriteWait.Text);
                    Properties.Settings.Default.isSerialPort_WaitWrite = checkBoxSerialTimeWriteWait.Checked;
                    //ожидание ответа по serial-порту
                    Properties.Settings.Default.serialPort_WaitAnswer = Int32.Parse(textBoxSerialTimeAnswerWait.Text);
                    Properties.Settings.Default.isSerialPort_WaitAnswer = checkBoxSerialTimeAnswerWait.Checked;
                    //следить за исправностью порта
                    Properties.Settings.Default.isCheckConditionSerialPort = checkBoxCheckCondition.Checked;
                }
                catch
                {
                    MessageBox.Show("Некорректные параметры на вкладке " + tabPageSerial.Text,
                                        "Предупреждение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                #endregion

                #region вкладка Консоль
                try
                {
                    //файлы логов по умолчанию
                    Properties.Settings.Default.fileAnswer = textBoxDefaultFileLogPrinat.Text;
                    Properties.Settings.Default.fileCommand = textBoxDefailtFileLogSend.Text;

                    //время
                    Properties.Settings.Default.isShowColumnTime = checkBoxShowInConsolColumnTime.Checked;
                    //формат времени
                    Properties.Settings.Default.timeFormatIndex = comboBoxTimeFormat.SelectedIndex;
                    //показывать миллисекунды
                    Properties.Settings.Default.isTimeFormatPodpis = checkBoxTimeMS.Checked;
                    //подписывать ли время
                    Properties.Settings.Default.isTimeFormatPodpis = checkBoxTimePodpis.Checked;

                    //hex
                    Properties.Settings.Default.isShowColumnHex = checkBoxShowInConsolColumnHex.Checked;
                    //разделять hex-символы разделителем '|' по 8 байт:
                    Properties.Settings.Default.isRazdHex8 = checkBoxRazd8byte.Checked;
                    //макс. кол-во байт в каждой строке данных
                    Properties.Settings.Default.kolBytesPerLineIndex = comboBoxNumByte.SelectedIndex;
                    //разделять ли байты, разделитель
                    Properties.Settings.Default.isRazdByte = checkBoxRazdByte.Checked;
                    Properties.Settings.Default.razdByteIndex = comboBoxRazdByte.SelectedIndex;

                    //текст
                    Properties.Settings.Default.isShowColumnText = checkBoxShowInConsolColumnText.Checked;
                    //кодировка текста
                    Properties.Settings.Default.indexKodStranicaNomer = comboBoxCodePage.SelectedIndex;
                    //разделять принятые пакеты по посл-ти символов (hex), сами символы
                    Properties.Settings.Default.isRazdPaketByPosledovSimbols = checkBoxRazdPaket.Checked;
                    Properties.Settings.Default.simbolsRazdPaketByPosledovSimbols = textBoxSimbolsRazdPaketByPosledovSimbols.Text;
                }
                catch
                {
                    MessageBox.Show("Некорректные параметры на вкладке " + tabPageConsol.Text,
                                        "Предупреждение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                #endregion

                OUnit.SaveOptions();

                //предупрежджение
                if (OUnit.isOpenSerialPort)
                    MessageBox.Show("Для принятия новых настроек serial-порта, следует открыть serial-порт заного");
            } //if (DialogResult == DialogResult.OK)  
            else
            {
                //приводим вид главной формы обратно

                #region вкладка Общие
                try
                {
                    //отображать текстовую форму команды
                    ((MainForm)this.Owner).labelCommandTextForm.Visible = Properties.Settings.Default.isDoubleCommandTextForm;

                    //позволять вводить текстовую команду
                    ((MainForm)this.Owner).radioButtonHex.Visible = Properties.Settings.Default.isAllowEnterText;
                    ((MainForm)this.Owner).radioButtonText.Visible = Properties.Settings.Default.isAllowEnterText;
                    ((MainForm)this.Owner).TextBoxCommand_PositionAndSize();
                }
                catch
                {
                    //позволять вводить текстовую команду
                    ((MainForm)this.Owner).radioButtonHex.Visible = Properties.Settings.Default.isDoubleCommandTextFormDefault;
                    ((MainForm)this.Owner).radioButtonText.Visible = Properties.Settings.Default.isDoubleCommandTextFormDefault;
                    ((MainForm)this.Owner).TextBoxCommand_PositionAndSize();
                }
                #endregion

                #region вкладка Порты
                try
                {
                    //отображать раскрывающийся список выбора serial-порта
                    ((MainForm)this.Owner).ComboBoxSerialName_ChangeVisible(); 
                }
                catch
                {
                }
                #endregion

                #region вкладка Консоль
                try
                {
                    ((MainForm)this.Owner).CheckConsolColumns();
                }
                catch
                {
                }
                #endregion

            }
        }

        private void buttonSerialDefault_Click(object sender, EventArgs e)
        {
            //отображать раскрывающийся список выбора serial-порта
            checkBoxWindowSetSerialPort.Checked = Properties.Settings.Default.isShowSetSerialPortDefault;
            //следить за исправностью порта
            checkBoxCheckCondition.Checked = Properties.Settings.Default.isCheckConditionSerialPortDefault;
            //основные настройки порта
            comboBoxSerialSpeed.SelectedIndex = Properties.Settings.Default.indexComPort_SpeedDefault;
            comboBoxSerialDataBits.SelectedIndex = Properties.Settings.Default.indexComPort_DatabitsDefault;
            comboBoxSerialParity.SelectedIndex = Properties.Settings.Default.indexComPort_ParityDefault;
            comboBoxSerialStopBits.SelectedIndex = Properties.Settings.Default.indexComPort_StopBitsDefault;            
            //ожидание записи по serial-порту
            textBoxSerialTimeWriteWait.Text = Properties.Settings.Default.serialPort_WaitAnswerDefault.ToString();
            checkBoxSerialTimeWriteWait.Checked = Properties.Settings.Default.isSerialPort_WaitWriteDefault;
            //ожидание ответа по serial-порту
            textBoxSerialTimeAnswerWait.Text = Properties.Settings.Default.serialPort_WaitAnswerDefault.ToString();
            checkBoxSerialTimeAnswerWait.Checked = Properties.Settings.Default.isSerialPort_WaitAnswerDefault;
        }

        private void buttonConsolDefault_Click(object sender, EventArgs e)
        {
            //файлы логов по умолчанию
            textBoxDefaultFileLogPrinat.Text = Properties.Settings.Default.fileAnswerDefault;
            textBoxDefailtFileLogSend.Text = Properties.Settings.Default.fileCommandDefault;
            //формат времени
            comboBoxTimeFormat.SelectedIndex = Properties.Settings.Default.timeFormatIndexDefault;
            //показывать миллисекунды
            checkBoxTimeMS.Checked = Properties.Settings.Default.isTimeFormatPodpisDefault;
            //подписывать ли время
            checkBoxTimePodpis.Checked = Properties.Settings.Default.isTimeFormatPodpisDefault;
            //разделять hex-символы разделителем '|' по 8 байт:
            checkBoxRazd8byte.Checked = Properties.Settings.Default.isRazdHex8Default;
            //макс. кол-во байт в каждой строке данных
            comboBoxNumByte.SelectedIndex = Properties.Settings.Default.kolBytesPerLineIndexDefault;
            //разделять ли байты, разделитель
            checkBoxRazdByte.Checked = Properties.Settings.Default.isRazdByteDefault;
            comboBoxRazdByte.SelectedIndex = Properties.Settings.Default.razdByteIndexDefault;
            //кодировка текста
            for (int i = 0; i < comboBoxCodePage.Items.Count; i++)
                if (Int32.Parse(comboBoxCodePage.Items[i].ToString()) == Properties.Settings.Default.indexKodStranicaNomerDefault)
                {
                    comboBoxCodePage.SelectedIndex = i;
                    break;
                }
            comboBoxCodePage.SelectedIndex = Properties.Settings.Default.indexKodStranicaNomerDefault;
            //разделять принятые пакеты по посл-ти символов (hex), сами символы
            checkBoxRazdPaket.Checked = Properties.Settings.Default.isRazdPaketByPosledovSimbolsDefault;
            textBoxSimbolsRazdPaketByPosledovSimbols.Text = Properties.Settings.Default.simbolsRazdPaketByPosledovSimbolsDefault;
            //отображение колонок
            checkBoxShowInConsolColumnTime.Checked = Properties.Settings.Default.isShowColumnTimeDefault;
            checkBoxShowInConsolColumnHex.Checked = Properties.Settings.Default.isShowColumnHexDefault;
            checkBoxShowInConsolColumnText.Checked = Properties.Settings.Default.isShowColumnTextDefault;
        }

        private void buttonFileLogPrinat_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            textBoxDefaultFileLogPrinat.Text = saveFileDialog1.FileName;
        }

        private void buttonFileLogSend_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            textBoxDefailtFileLogSend.Text = saveFileDialog1.FileName;
        }

        private void textBoxFileLog_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxDefaultFileLogPrinat, textBoxDefaultFileLogPrinat.Text);
        }

        private void labelFileLog_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelDefaultFileLogPrinat, "Файл, используемый для выполнения операции на консоли принятых данных: ПКМ -> Сохранить");
        }

        private void buttonDefaultGeneral_Click(object sender, EventArgs e)
        {
            checkBoxAutoOpenSerial.Checked = Properties.Settings.Default.isAutoOpenSerialDefault;
            checkBoxAllowEnterText.Checked = Properties.Settings.Default.isAllowEnterTextDefault;
            checkBoxDoubleCommandTextForm.Checked = Properties.Settings.Default.isDoubleCommandTextFormDefault;
        }

        private void buttonDefaultGeneral_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonDefaultGeneral, "Сбросить все настройки на вкладке "+tabPageGeneral.Text);
        }

        private void buttonFileLog_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonFileLogPrinat, "Выбрать файл");
        }

        private void buttonDefaultSerial_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonDefaultSerial, "Сбросить все настройки на вкладке " + tabPageSerial.Text);
        }

        private void buttonOK_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonOK, "Закрыть окно '" + this.Text + "' с сохранением всех изменений");
        }

        private void buttonCancel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonCancel, "Закрыть окно '" + this.Text + "'без сохранения изменений");
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void linkLabelTextKod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelTextKod.LinkVisited = true;
            System.Diagnostics.Process.Start("https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx");
        }

        private void textBoxTimeAnswerWait_TextChanged(object sender, EventArgs e)
        {
            int cursPosFromEnd = this.textBoxSerialTimeAnswerWait.Text.Count() - this.textBoxSerialTimeAnswerWait.SelectionStart; //чтоб курсор не прыгал
            string s_AsIs = "";
            const string mask = "0123456789";
            foreach (char c in textBoxSerialTimeAnswerWait.Text.ToUpper().Replace(Properties.Settings.Default.razdByteIndex.ToString(), ""))
                if (mask.IndexOf(c) != -1)
                    if (!((s_AsIs.Count() == 0) & (c == '0')))
                        s_AsIs += c;
            this.textBoxSerialTimeAnswerWait.TextChanged -= textBoxTimeAnswerWait_TextChanged; //чтоб не вызывать лишний раз событие textBoxTimeAnswerWait_TextChanged
            this.textBoxSerialTimeAnswerWait.Text = s_AsIs;
            if (this.textBoxSerialTimeAnswerWait.Text.Count() - cursPosFromEnd >= 0)
                this.textBoxSerialTimeAnswerWait.SelectionStart = this.textBoxSerialTimeAnswerWait.Text.Count() - cursPosFromEnd;
            this.textBoxSerialTimeAnswerWait.TextChanged += textBoxTimeAnswerWait_TextChanged;
        }

        private void checkBoxRazdByte_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxRazdByte.Enabled = checkBoxRazdByte.Checked;
            //textBoxSimbolsRazdPaketByPosledovSimbols
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged -= textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
            if (comboBoxRazdByte.SelectedIndex != -1) 
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            else
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 'x',
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged += textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged;
            //((MainForm)this.Owner).textBoxComand
            if (((MainForm)this.Owner).radioButtonHex.Checked)
            {
                ((MainForm)this.Owner).textBoxComand.TextChanged -= ((MainForm)this.Owner).textBoxComand_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
                if (comboBoxRazdByte.SelectedIndex != -1) 
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                     comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                     checkBoxRazdByte.Checked,
                                     checkBoxRazd8byte.Checked);
                else
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                    'x',
                                    checkBoxRazdByte.Checked,
                                    checkBoxRazd8byte.Checked);
                ((MainForm)this.Owner).textBoxComand.TextChanged += ((MainForm)this.Owner).textBoxComand_TextChanged;
            }
            //консоль
            CheckConsolColumns();
        }

        private void comboBoxRazd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBoxSimbolsRazdPaketByPosledovSimbols
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged -= textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
            if (comboBoxRazdByte.SelectedIndex != -1) 
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            else
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 'x',
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged += textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged;
            //((MainForm)this.Owner).textBoxComand
            if (((MainForm)this.Owner).radioButtonHex.Checked)
            {
                ((MainForm)this.Owner).textBoxComand.TextChanged -= ((MainForm)this.Owner).textBoxComand_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
                if (comboBoxRazdByte.SelectedIndex != -1) 
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                     comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                     checkBoxRazdByte.Checked,
                                     checkBoxRazd8byte.Checked);
                else
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                     'x',
                                     checkBoxRazdByte.Checked,
                                     checkBoxRazd8byte.Checked);
                ((MainForm)this.Owner).textBoxComand.TextChanged += ((MainForm)this.Owner).textBoxComand_TextChanged;
            }
            //консоль
            CheckConsolColumns();
        }

        private void textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged(object sender, EventArgs e)
        {
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged -= textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
            if (comboBoxRazdByte.SelectedIndex != -1) 
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            else
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                'x',
                                checkBoxRazdByte.Checked,
                                checkBoxRazd8byte.Checked);
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged += textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged;
        }

        private void checkBoxRazdPaket_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSimbolsRazdPaketByPosledovSimbols.Enabled = checkBoxRazdPaket.Checked;
        }

        private void checkBoxRazd8byte_CheckedChanged(object sender, EventArgs e)
        {
            //textBoxSimbolsRazdPaketByPosledovSimbols
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged -= textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
            if (comboBoxRazdByte.SelectedIndex != -1)
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            else
                Assist.VerifyTextbox(textBoxSimbolsRazdPaketByPosledovSimbols,
                                 'x',
                                 checkBoxRazdByte.Checked,
                                 checkBoxRazd8byte.Checked);
            textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged += textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged;
            //((MainForm)this.Owner).
            if (((MainForm)this.Owner).radioButtonHex.Checked)
            {
                ((MainForm)this.Owner).textBoxComand.TextChanged -= ((MainForm)this.Owner).textBoxComand_TextChanged; //http://www.cyberforum.ru/windows-forms/thread1649458.html#post8680110
                if (comboBoxRazdByte.SelectedIndex != -1)
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                     comboBoxRazdByte.Items[comboBoxRazdByte.SelectedIndex].ToString()[0],
                                     checkBoxRazdByte.Checked,
                                     checkBoxRazd8byte.Checked);
                else
                    Assist.VerifyTextbox(((MainForm)this.Owner).textBoxComand,
                                     'x',
                                     checkBoxRazdByte.Checked,
                                     checkBoxRazd8byte.Checked);
                ((MainForm)this.Owner).textBoxComand.TextChanged += ((MainForm)this.Owner).textBoxComand_TextChanged;                
            }
            //консоль
            CheckConsolColumns();
        }

        private void textBoxDefailtFileLogPrinat_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxDefaultFileLogPrinat, "Файл по умолчанию для сохранения принятых данных");
        }

        private void textBoxDefailtFileLogSend_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxDefailtFileLogSend, "Файл по умолчанию для сохранения отправленных данных");
        }

        private void comboBoxTimeFormat_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxTimeFormat, "Формат времени в колонке Время таблицы консоли");
        }

        private void comboBoxNumByte_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxNumByte, "Максимальное количество байт в каждой строке данных,\r\nпри превышении которого байты пененосятся на другую строку\r\nв колонке Hex таблицы консоли");
        }

        private void checkBoxTimePodpis_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxTimePodpis, "Подписывать ли время (например 08.03М 10:23:07с)\r\nв колонке Время таблицы консоли");
        }

        private void checkBoxRazd8byte_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxRazd8byte, "Разделять ли hex-символы разделителем '|' по 8 байт\r\nв колонке Hex таблицы консоли");
        }

        private void checkBoxRazdByte_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxRazdByte, "Разделять ли байты в колонке Hex таблицы консоли");
        }

        private void comboBoxRazdByte_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxRazdByte, "Разделитель байтов в колонке Hex таблицы консоли");
        }

        private void comboBoxCodePage_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxCodePage, "Кодовая страница символов в колонке Текст таблицы консоли." +
                                                  "\r\nПри изменении кодовой страницы считается, что набранный текст" +
                                                  "\r\nв командной строке набран в выбранной кодовой странице." +
                                                  "\r\nВыбирайте кодировку, которую поддерживает ваша операционная система!");
        }

        private void checkBoxRazdPaket_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxRazdPaket, "Разделять ли принятые пакеты по последовательноти символов\r\nв колонках Hex и Текст (hex-код)");
        }

        private void textBoxSimbolsRazdPaketByPosledovSimbols_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxSimbolsRazdPaketByPosledovSimbols, "Последовательность символов - разделитель принятых данных,\r\nв колонках Hex и Текст (hex-код)");
        }

        private void buttonConsolDefault_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonConsolDefault, "Сбросить все настройки на вкладке " + tabPageConsol.Text);
        }

        private void linkLabelTextKod_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(linkLabelTextKod, "Ссылка на интернет-ресурс о кодовых страницах.\r\nВыбирайте кодировку, которую поддерживает ваша операционная система!");
        }

        private void labelNumByte_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelNumByte, "Максимальное количество байт в каждой строке данных,\r\nпри превышении которого байты пененосятся на другую строку\r\nв колонке Hex таблицы консоли");
        }

        private void labelTimeFormat_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelTimeFormat, "Формат времени в колонке Время таблицы консоли");
        }

        private void labelDefailtFileLogSend_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelDefailtFileLogSend, "Файл по умолчанию для сохранения отправленных данных");
        }

        private void labelDefaultFileLogPrinat_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelDefaultFileLogPrinat, "Файл по умолчанию для сохранения принятых данных");
        }

        private void buttonFileLogPrinat_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonFileLogPrinat, "Выбрать файл по умолчанию для сохранения принятых данных");
        }

        private void buttonFileLogSend_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(buttonFileLogSend, "Выбрать файл по умолчанию для сохранения отправленных данных");
        }

        private void comboBoxSerialName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxSerialName, "Список существующих в системе serial-портов");
        }

        private void labelSerialName_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelSerialName, "Имя serial-порта");
        }

        private void comboBoxSerialSpeed_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxSerialSpeed, "Скорость serial-порта");
        }

        private void labelSerialSpeed_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelSerialSpeed, "Скорость serial-порта");
        }

        private void comboBoxSerialDataBits_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxSerialDataBits, "Количество бит данных serial-порта");
        }

        private void labelSerialBits_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelSerialBits, "Количество бит данных serial-порта");
        }

        private void comboBoxSerialParity_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxSerialParity, "Чётность serial-порта");
        }

        private void labelParity_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelParity, "Чётность serial-порта");
        }

        private void comboBoxSerialStopBits_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxSerialStopBits, "Количество стоп-битов serial-порта");
        }

        private void labelStopBits_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(labelStopBits, "Количество стоп-битов serial-порта");
        }

        private void tabControlOptions_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (lastSelectedIndextabControlOptions == 1)
            {
                int minSpeed = (int)Math.Ceiling((double)1000 / Int32.Parse(comboBoxSerialSpeed.Text)); //вреям одного такта порта
                //проверка минимльного времени ожидания на запись
                if (checkBoxSerialTimeWriteWait.Checked)
                    try
                    {
                        if (minSpeed > Int32.Parse(textBoxSerialTimeWriteWait.Text))
                        {
                            MessageBox.Show("Время ожидания на запись нельзя сделать меньше времени одного такта скорости порта",
                                            "Некорректное время ожидания на запись",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                            textBoxSerialTimeWriteWait.Text = minSpeed.ToString();
                        }
                    }
                    catch
                    {
                        Console.Beep();
                        textBoxSerialTimeAnswerWait.Text = Math.Ceiling((double)1000 / OUnit.serialPort_Speed).ToString();
                    }
                //проверка минимльного времени ожидания на ответ
                if (checkBoxSerialTimeAnswerWait.Checked)
                    try
                    {
                        if (minSpeed > Int32.Parse(textBoxSerialTimeAnswerWait.Text))
                        {
                            MessageBox.Show("Время ожидания на ответ нельзя сделать меньше времени одного такта скорости порта",
                                            "Некорректное время ожидания на чтение",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                            textBoxSerialTimeAnswerWait.Text = minSpeed.ToString();
                        }
                    }
                    catch
                    {
                        Console.Beep();
                        textBoxSerialTimeAnswerWait.Text = Math.Ceiling((double)1000 / OUnit.serialPort_Speed).ToString();
                    }
            }

            if (lastSelectedIndextabControlOptions == 2)
            {
                if (checkBoxRazdPaket.Checked)
                    if (Assist.GetFilteredHex(textBoxSimbolsRazdPaketByPosledovSimbols.Text).Count() % 2 != 0)
                    {
                        lastSelectedIndextabControlOptions = tabControlOptions.SelectedIndex;
                        tabControlOptions.SelectedIndex = 2;
                        //готовим сообщение
                        string r = "";
                        if (checkBoxRazdByte.Checked)
                            if (comboBoxRazdByte.SelectedIndex != -1)
                                r = OUnit.razdBytes[comboBoxRazdByte.SelectedIndex][0].ToString();
                        MessageBox.Show("В поле \"Разделять принятые пакеты по посл-ти символов (hex)\" число символов нечётное.\r\n Например, \"39" + r + "9\" некорректно, \"39" + r + "09\" или \"39\" корректно.",
                                        "Некорректное число символов",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                    }
            }
            else
                lastSelectedIndextabControlOptions = tabControlOptions.SelectedIndex;
        }

        private void textBoxSerialTimeAnswerWait_TextChanged(object sender, EventArgs e)
        {
            int cursPosFromEnd = this.textBoxSerialTimeAnswerWait.Text.Count() - this.textBoxSerialTimeAnswerWait.SelectionStart; //чтоб курсор не прыгал
            string s_AsIs = "";
            const string mask = "0123456789";
            foreach (char c in textBoxSerialTimeAnswerWait.Text.ToUpper())
                if (mask.IndexOf(c) != -1)
                    if (!((s_AsIs.Count() == 0) & (c == '0')))
                        s_AsIs += c;
            this.textBoxSerialTimeAnswerWait.TextChanged -= textBoxSerialTimeAnswerWait_TextChanged; //чтоб не вызывать лишний раз событие textBoxPeriod_TextChanged
            this.textBoxSerialTimeAnswerWait.Text = s_AsIs;
            if (this.textBoxSerialTimeAnswerWait.Text.Count() - cursPosFromEnd >= 0)
                this.textBoxSerialTimeAnswerWait.SelectionStart = this.textBoxSerialTimeAnswerWait.Text.Count() - cursPosFromEnd;
            this.textBoxSerialTimeAnswerWait.TextChanged += textBoxSerialTimeAnswerWait_TextChanged;
        }

        private void textBoxSerialTimeWriteWait_TextChanged(object sender, EventArgs e)
        {
            int cursPosFromEnd = this.textBoxSerialTimeWriteWait.Text.Count() - this.textBoxSerialTimeWriteWait.SelectionStart; //чтоб курсор не прыгал
            string s_AsIs = "";
            const string mask = "0123456789";
            foreach (char c in textBoxSerialTimeWriteWait.Text.ToUpper())
                if (mask.IndexOf(c) != -1)
                    if (!((s_AsIs.Count() == 0) & (c == '0')))
                        s_AsIs += c;
            this.textBoxSerialTimeWriteWait.TextChanged -= textBoxSerialTimeWriteWait_TextChanged; //чтоб не вызывать лишний раз событие textBoxPeriod_TextChanged
            this.textBoxSerialTimeWriteWait.Text = s_AsIs;
            if (this.textBoxSerialTimeWriteWait.Text.Count() - cursPosFromEnd >= 0)
                this.textBoxSerialTimeWriteWait.SelectionStart = this.textBoxSerialTimeWriteWait.Text.Count() - cursPosFromEnd;
            this.textBoxSerialTimeWriteWait.TextChanged += textBoxSerialTimeWriteWait_TextChanged;
        }

        private void checkBoxSerialTimeWriteWait_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSerialTimeWriteWait.Enabled = checkBoxSerialTimeWriteWait.Checked;
        }

        private void checkBoxSerialTimeAnswerWait_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSerialTimeAnswerWait.Enabled = checkBoxSerialTimeAnswerWait.Checked;
        }

        private void textBoxSerialTimeWriteWait_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxSerialTimeWriteWait, "Время ожидания записи в порт, мс:");
        }

        private void checkBoxSerialTimeWriteWait_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxSerialTimeWriteWait, "Задать ли время ожидания записи в порт, мс:");
        }

        private void textBoxSerialTimeAnswerWait_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxSerialTimeAnswerWait, "Время ожидания данных из порта, мс:");
        }

        private void checkBoxSerialTimeAnswerWait_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxSerialTimeAnswerWait, "Задать ли время ожидания данных из порта, мс:");
        }

        private void comboBoxSerialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //чтоб не мигало, обходимся без Clear(); 
            if (((MainForm)this.Owner).comboBoxSerialName.Items.Count == 0)
                ((MainForm)this.Owner).comboBoxSerialName.Items.Add(this.comboBoxSerialName.Items[this.comboBoxSerialName.SelectedIndex].ToString());
            else
                ((MainForm)this.Owner).comboBoxSerialName.Items[0] = this.comboBoxSerialName.Items[this.comboBoxSerialName.SelectedIndex];
            ((MainForm)this.Owner).comboBoxSerialName.SelectedIndex = 0;
        }

        private void timerPortList_Tick(object sender, EventArgs e)
        {
            if (!checkBoxCheckCondition.Checked)
            {
                comboBoxSerialName.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.WindowText);
                return;
            }
            //проверка порта на пригодность
            if (!OUnit.TestSerialPort(comboBoxSerialName.Items[comboBoxSerialName.SelectedIndex].ToString()))
                comboBoxSerialName.ForeColor = Color.Red;
            else
                comboBoxSerialName.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.WindowText);
        }

        private void checkBoxCheckCondition_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxCheckCondition, "Следить ли исправностью порта в реальном времени.\r\n" +
                                                        "Выбранный serial-порт каждые несколько секунд\r\n" +
                                                        "проверяется на присутствие в системе.\r\n" +
                                                        "Если serial-порт оказался вне доступа, то в списке выбора\r\n" +
                                                        "serial-порта он отображается красным шрифтом.");
        }

        private void comboBoxSerialName_DropDown(object sender, EventArgs e)
        {
            //имя порта
            int lastSelectedIndex = comboBoxSerialName.SelectedIndex;
            string findPortName;
            if (comboBoxSerialName.Items.Count > 0)
                findPortName = comboBoxSerialName.Text.Trim().ToLower(); //перед очисткой запоминаем, какой порт ищем
            else
                findPortName = Properties.Settings.Default.nameComPort.Trim().ToLower();
            string[] portNames = SerialPort.GetPortNames();
            comboBoxSerialName.Items.Clear();
            //если портов нет
            if (portNames.Count() == 0)
            {
                comboBoxSerialName.Items.Add(((MainForm)this.Owner).GetBasePortNameNo);
                comboBoxSerialName.ForeColor = Color.Red;
                comboBoxSerialName.SelectedIndex = 0;
                comboBoxSerialName.Enabled = false;
            }
            //если есть порты
            comboBoxSerialName.Enabled = true;
            comboBoxSerialName.Items.AddRange(portNames);
            if (findPortName == ((MainForm)this.Owner).GetBasePortNameNo.Trim().ToLower())
            {
                comboBoxSerialName.SelectedIndex = 0;
                return;
            }
            //анализ того что забили                    
            for (int ii = 0; ii < comboBoxSerialName.Items.Count; ii++)
                if (comboBoxSerialName.Items[ii].ToString().Trim().ToLower() == findPortName)
                {
                    comboBoxSerialName.SelectedIndex = ii;
                    break;
                }
        }

        private void checkBoxShowInConsolColumnTime_CheckedChanged(object sender, EventArgs e)
        {
            panelTime.Enabled = checkBoxShowInConsolColumnTime.Checked;
            CheckConsolColumns();
        }

        private void checkBoxShowInConsolColumnHex_CheckedChanged(object sender, EventArgs e)
        {
            panelHex.Enabled = checkBoxShowInConsolColumnHex.Checked;
            CheckConsolColumns();
        }

        private void checkBoxShowInConsolColumnText_CheckedChanged(object sender, EventArgs e)
        {
            panelText.Enabled = checkBoxShowInConsolColumnText.Checked;
            CheckConsolColumns();
        }

        private void comboBoxTimeFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckConsolColumns();
        }

        private void checkBoxTimePodpis_CheckedChanged(object sender, EventArgs e)
        {
            CheckConsolColumns();
        }

        private void comboBoxNumByte_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckConsolColumns();
        }

        private void comboBoxCodePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckConsolColumns();
        }

        private void checkBoxTimeMS_CheckedChanged(object sender, EventArgs e)
        {
            CheckConsolColumns();
        }

        private void checkBoxAllowEnterText_CheckedChanged(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).radioButtonHex.Visible = checkBoxAllowEnterText.Checked;
            ((MainForm)this.Owner).radioButtonText.Visible = checkBoxAllowEnterText.Checked;
            ((MainForm)this.Owner).TextBoxCommand_PositionAndSize(checkBoxAllowEnterText.Checked);
            labelInfo.Visible = (checkBoxAllowEnterText.Checked | checkBoxDoubleCommandTextForm.Checked);
        }

        private void checkBoxDoubleCommandTextForm_CheckedChanged(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).labelCommandTextForm.Visible = checkBoxDoubleCommandTextForm.Checked;
            labelInfo.Visible = (checkBoxAllowEnterText.Checked | checkBoxDoubleCommandTextForm.Checked);
        }

        private void checkBoxAutoOpenSerial_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxAutoOpenSerial, "Открывать serial-порт при старте программы");
        }

        private void checkBoxDoubleCommandTextForm_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxDoubleCommandTextForm, "Отображать текстовый вид hex-команды при её наборе");
        }

        private void checkBoxAllowEnterText_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxAllowEnterText, "Позволять набирать команду в текстовой форме");
        }

        private void checkBoxWindowSetSerialPort_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxWindowSetSerialPort, "Отображать раскрывающийся список выбора serial-порта");
        }

        private void checkBoxWindowSetSerialPort_CheckedChanged(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).ComboBoxSerialName_ChangeVisible(checkBoxWindowSetSerialPort.Checked);
            checkBoxCheckCondition.Enabled = checkBoxWindowSetSerialPort.Checked;
        }

        #endregion

        #region процедуры и функции VLeshka

        //пустой метод делегата CheckConsolColumns
        private void CheckConsolColumnsNull()
        {
        }
        //рабочий метод делегата CheckConsolColumns
        private void CheckConsolColumnsWork()
        {
            ((MainForm)this.Owner).
            CheckConsolColumns(checkBoxShowInConsolColumnTime.Checked, comboBoxTimeFormat.SelectedIndex, checkBoxTimeMS.Checked, checkBoxTimePodpis.Checked,
                               checkBoxShowInConsolColumnHex.Checked, checkBoxRazd8byte.Checked, comboBoxNumByte.SelectedIndex, checkBoxRazdByte.Checked, comboBoxRazdByte.SelectedIndex,
                               checkBoxShowInConsolColumnText.Checked, comboBoxCodePage.SelectedIndex == -1 ? -1 : Int32.Parse(comboBoxCodePage.Items[comboBoxCodePage.SelectedIndex].ToString()),
                               checkBoxRazdPaket.Checked, textBoxSimbolsRazdPaketByPosledovSimbols.Text);
        }

        #endregion

    }
}

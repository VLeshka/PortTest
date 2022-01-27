namespace ComApplication
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.labelInfo = new System.Windows.Forms.Label();
            this.checkBoxDoubleCommandTextForm = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowEnterText = new System.Windows.Forms.CheckBox();
            this.buttonDefaultGeneral = new System.Windows.Forms.Button();
            this.checkBoxAutoOpenSerial = new System.Windows.Forms.CheckBox();
            this.tabPageSerial = new System.Windows.Forms.TabPage();
            this.checkBoxWindowSetSerialPort = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckCondition = new System.Windows.Forms.CheckBox();
            this.checkBoxSerialTimeWriteWait = new System.Windows.Forms.CheckBox();
            this.checkBoxSerialTimeAnswerWait = new System.Windows.Forms.CheckBox();
            this.textBoxSerialTimeWriteWait = new System.Windows.Forms.TextBox();
            this.textBoxSerialTimeAnswerWait = new System.Windows.Forms.TextBox();
            this.comboBoxSerialDataBits = new System.Windows.Forms.ComboBox();
            this.comboBoxSerialSpeed = new System.Windows.Forms.ComboBox();
            this.buttonDefaultSerial = new System.Windows.Forms.Button();
            this.labelStopBits = new System.Windows.Forms.Label();
            this.comboBoxSerialStopBits = new System.Windows.Forms.ComboBox();
            this.labelParity = new System.Windows.Forms.Label();
            this.comboBoxSerialParity = new System.Windows.Forms.ComboBox();
            this.labelSerialBits = new System.Windows.Forms.Label();
            this.labelSerialSpeed = new System.Windows.Forms.Label();
            this.labelSerialName = new System.Windows.Forms.Label();
            this.comboBoxSerialName = new System.Windows.Forms.ComboBox();
            this.tabPageConsol = new System.Windows.Forms.TabPage();
            this.textBoxSimbolsRazdPaketByPosledovSimbols = new System.Windows.Forms.TextBox();
            this.checkBoxRazdPaket = new System.Windows.Forms.CheckBox();
            this.buttonConsolDefault = new System.Windows.Forms.Button();
            this.groupBoxText = new System.Windows.Forms.GroupBox();
            this.checkBoxShowInConsolColumnText = new System.Windows.Forms.CheckBox();
            this.panelText = new System.Windows.Forms.Panel();
            this.linkLabelTextKod = new System.Windows.Forms.LinkLabel();
            this.comboBoxCodePage = new System.Windows.Forms.ComboBox();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.checkBoxShowInConsolColumnTime = new System.Windows.Forms.CheckBox();
            this.panelTime = new System.Windows.Forms.Panel();
            this.checkBoxTimeMS = new System.Windows.Forms.CheckBox();
            this.checkBoxTimePodpis = new System.Windows.Forms.CheckBox();
            this.comboBoxTimeFormat = new System.Windows.Forms.ComboBox();
            this.labelTimeFormat = new System.Windows.Forms.Label();
            this.groupBoxHex = new System.Windows.Forms.GroupBox();
            this.checkBoxShowInConsolColumnHex = new System.Windows.Forms.CheckBox();
            this.panelHex = new System.Windows.Forms.Panel();
            this.checkBoxRazdByte = new System.Windows.Forms.CheckBox();
            this.comboBoxRazdByte = new System.Windows.Forms.ComboBox();
            this.checkBoxRazd8byte = new System.Windows.Forms.CheckBox();
            this.labelNumByte = new System.Windows.Forms.Label();
            this.comboBoxNumByte = new System.Windows.Forms.ComboBox();
            this.groupBoxSave = new System.Windows.Forms.GroupBox();
            this.panelSave = new System.Windows.Forms.Panel();
            this.buttonFileLogSend = new System.Windows.Forms.Button();
            this.labelDefailtFileLogSend = new System.Windows.Forms.Label();
            this.textBoxDefailtFileLogSend = new System.Windows.Forms.TextBox();
            this.labelDefaultFileLogPrinat = new System.Windows.Forms.Label();
            this.textBoxDefaultFileLogPrinat = new System.Windows.Forms.TextBox();
            this.buttonFileLogPrinat = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerPortList = new System.Windows.Forms.Timer(this.components);
            this.tabControlOptions.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageSerial.SuspendLayout();
            this.tabPageConsol.SuspendLayout();
            this.groupBoxText.SuspendLayout();
            this.panelText.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.groupBoxHex.SuspendLayout();
            this.panelHex.SuspendLayout();
            this.groupBoxSave.SuspendLayout();
            this.panelSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabPageGeneral);
            this.tabControlOptions.Controls.Add(this.tabPageSerial);
            this.tabControlOptions.Controls.Add(this.tabPageConsol);
            this.tabControlOptions.Location = new System.Drawing.Point(1, 2);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(357, 520);
            this.tabControlOptions.TabIndex = 0;
            this.tabControlOptions.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlOptions_Selecting);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.labelInfo);
            this.tabPageGeneral.Controls.Add(this.checkBoxDoubleCommandTextForm);
            this.tabPageGeneral.Controls.Add(this.checkBoxAllowEnterText);
            this.tabPageGeneral.Controls.Add(this.buttonDefaultGeneral);
            this.tabPageGeneral.Controls.Add(this.checkBoxAutoOpenSerial);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(349, 494);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "Общие";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.ForeColor = System.Drawing.Color.Red;
            this.labelInfo.Location = new System.Drawing.Point(60, 114);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(24, 13);
            this.labelInfo.TabIndex = 7;
            this.labelInfo.Text = "info";
            this.labelInfo.Visible = false;
            // 
            // checkBoxDoubleCommandTextForm
            // 
            this.checkBoxDoubleCommandTextForm.AutoSize = true;
            this.checkBoxDoubleCommandTextForm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDoubleCommandTextForm.Location = new System.Drawing.Point(22, 71);
            this.checkBoxDoubleCommandTextForm.Name = "checkBoxDoubleCommandTextForm";
            this.checkBoxDoubleCommandTextForm.Size = new System.Drawing.Size(310, 17);
            this.checkBoxDoubleCommandTextForm.TabIndex = 6;
            this.checkBoxDoubleCommandTextForm.Text = "Отображать текстовый вид hex-команды при её наборе";
            this.checkBoxDoubleCommandTextForm.UseVisualStyleBackColor = true;
            this.checkBoxDoubleCommandTextForm.CheckedChanged += new System.EventHandler(this.checkBoxDoubleCommandTextForm_CheckedChanged);
            this.checkBoxDoubleCommandTextForm.MouseHover += new System.EventHandler(this.checkBoxDoubleCommandTextForm_MouseHover);
            // 
            // checkBoxAllowEnterText
            // 
            this.checkBoxAllowEnterText.AutoSize = true;
            this.checkBoxAllowEnterText.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAllowEnterText.Location = new System.Drawing.Point(54, 94);
            this.checkBoxAllowEnterText.Name = "checkBoxAllowEnterText";
            this.checkBoxAllowEnterText.Size = new System.Drawing.Size(278, 17);
            this.checkBoxAllowEnterText.TabIndex = 5;
            this.checkBoxAllowEnterText.Text = "Позволять набирать команду в текстовой форме";
            this.checkBoxAllowEnterText.UseVisualStyleBackColor = true;
            this.checkBoxAllowEnterText.CheckedChanged += new System.EventHandler(this.checkBoxAllowEnterText_CheckedChanged);
            this.checkBoxAllowEnterText.MouseHover += new System.EventHandler(this.checkBoxAllowEnterText_MouseHover);
            // 
            // buttonDefaultGeneral
            // 
            this.buttonDefaultGeneral.Location = new System.Drawing.Point(7, 465);
            this.buttonDefaultGeneral.Name = "buttonDefaultGeneral";
            this.buttonDefaultGeneral.Size = new System.Drawing.Size(96, 23);
            this.buttonDefaultGeneral.TabIndex = 4;
            this.buttonDefaultGeneral.Text = "По умолчанию";
            this.buttonDefaultGeneral.UseVisualStyleBackColor = true;
            this.buttonDefaultGeneral.Click += new System.EventHandler(this.buttonDefaultGeneral_Click);
            this.buttonDefaultGeneral.MouseHover += new System.EventHandler(this.buttonDefaultGeneral_MouseHover);
            // 
            // checkBoxAutoOpenSerial
            // 
            this.checkBoxAutoOpenSerial.AutoSize = true;
            this.checkBoxAutoOpenSerial.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAutoOpenSerial.Location = new System.Drawing.Point(77, 20);
            this.checkBoxAutoOpenSerial.Name = "checkBoxAutoOpenSerial";
            this.checkBoxAutoOpenSerial.Size = new System.Drawing.Size(255, 17);
            this.checkBoxAutoOpenSerial.TabIndex = 0;
            this.checkBoxAutoOpenSerial.Text = "Открывать serial-порт при старте программы";
            this.checkBoxAutoOpenSerial.UseVisualStyleBackColor = true;
            this.checkBoxAutoOpenSerial.MouseHover += new System.EventHandler(this.checkBoxAutoOpenSerial_MouseHover);
            // 
            // tabPageSerial
            // 
            this.tabPageSerial.Controls.Add(this.checkBoxWindowSetSerialPort);
            this.tabPageSerial.Controls.Add(this.checkBoxCheckCondition);
            this.tabPageSerial.Controls.Add(this.checkBoxSerialTimeWriteWait);
            this.tabPageSerial.Controls.Add(this.checkBoxSerialTimeAnswerWait);
            this.tabPageSerial.Controls.Add(this.textBoxSerialTimeWriteWait);
            this.tabPageSerial.Controls.Add(this.textBoxSerialTimeAnswerWait);
            this.tabPageSerial.Controls.Add(this.comboBoxSerialDataBits);
            this.tabPageSerial.Controls.Add(this.comboBoxSerialSpeed);
            this.tabPageSerial.Controls.Add(this.buttonDefaultSerial);
            this.tabPageSerial.Controls.Add(this.labelStopBits);
            this.tabPageSerial.Controls.Add(this.comboBoxSerialStopBits);
            this.tabPageSerial.Controls.Add(this.labelParity);
            this.tabPageSerial.Controls.Add(this.comboBoxSerialParity);
            this.tabPageSerial.Controls.Add(this.labelSerialBits);
            this.tabPageSerial.Controls.Add(this.labelSerialSpeed);
            this.tabPageSerial.Controls.Add(this.labelSerialName);
            this.tabPageSerial.Controls.Add(this.comboBoxSerialName);
            this.tabPageSerial.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerial.Name = "tabPageSerial";
            this.tabPageSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerial.Size = new System.Drawing.Size(349, 494);
            this.tabPageSerial.TabIndex = 1;
            this.tabPageSerial.Text = "Порты";
            this.tabPageSerial.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowSetSerialPort
            // 
            this.checkBoxWindowSetSerialPort.AutoSize = true;
            this.checkBoxWindowSetSerialPort.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWindowSetSerialPort.Location = new System.Drawing.Point(12, 25);
            this.checkBoxWindowSetSerialPort.Name = "checkBoxWindowSetSerialPort";
            this.checkBoxWindowSetSerialPort.Size = new System.Drawing.Size(321, 17);
            this.checkBoxWindowSetSerialPort.TabIndex = 25;
            this.checkBoxWindowSetSerialPort.Text = "Отображать раскрывающийся список выбора serial-порта";
            this.checkBoxWindowSetSerialPort.UseVisualStyleBackColor = true;
            this.checkBoxWindowSetSerialPort.CheckedChanged += new System.EventHandler(this.checkBoxWindowSetSerialPort_CheckedChanged);
            this.checkBoxWindowSetSerialPort.MouseHover += new System.EventHandler(this.checkBoxWindowSetSerialPort_MouseHover);
            // 
            // checkBoxCheckCondition
            // 
            this.checkBoxCheckCondition.AutoSize = true;
            this.checkBoxCheckCondition.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxCheckCondition.Location = new System.Drawing.Point(142, 48);
            this.checkBoxCheckCondition.Name = "checkBoxCheckCondition";
            this.checkBoxCheckCondition.Size = new System.Drawing.Size(191, 17);
            this.checkBoxCheckCondition.TabIndex = 24;
            this.checkBoxCheckCondition.Text = "Следить за исправностью порта";
            this.checkBoxCheckCondition.UseVisualStyleBackColor = true;
            this.checkBoxCheckCondition.MouseHover += new System.EventHandler(this.checkBoxCheckCondition_MouseHover);
            // 
            // checkBoxSerialTimeWriteWait
            // 
            this.checkBoxSerialTimeWriteWait.AutoSize = true;
            this.checkBoxSerialTimeWriteWait.Location = new System.Drawing.Point(105, 242);
            this.checkBoxSerialTimeWriteWait.Name = "checkBoxSerialTimeWriteWait";
            this.checkBoxSerialTimeWriteWait.Size = new System.Drawing.Size(174, 17);
            this.checkBoxSerialTimeWriteWait.TabIndex = 23;
            this.checkBoxSerialTimeWriteWait.Text = "Время ожидания записи, мс:";
            this.checkBoxSerialTimeWriteWait.UseVisualStyleBackColor = true;
            this.checkBoxSerialTimeWriteWait.CheckedChanged += new System.EventHandler(this.checkBoxSerialTimeWriteWait_CheckedChanged);
            this.checkBoxSerialTimeWriteWait.MouseHover += new System.EventHandler(this.checkBoxSerialTimeWriteWait_MouseHover);
            // 
            // checkBoxSerialTimeAnswerWait
            // 
            this.checkBoxSerialTimeAnswerWait.AutoSize = true;
            this.checkBoxSerialTimeAnswerWait.Location = new System.Drawing.Point(105, 268);
            this.checkBoxSerialTimeAnswerWait.Name = "checkBoxSerialTimeAnswerWait";
            this.checkBoxSerialTimeAnswerWait.Size = new System.Drawing.Size(172, 17);
            this.checkBoxSerialTimeAnswerWait.TabIndex = 22;
            this.checkBoxSerialTimeAnswerWait.Text = "Время ожидания ответа, мс:";
            this.checkBoxSerialTimeAnswerWait.UseVisualStyleBackColor = true;
            this.checkBoxSerialTimeAnswerWait.CheckedChanged += new System.EventHandler(this.checkBoxSerialTimeAnswerWait_CheckedChanged);
            this.checkBoxSerialTimeAnswerWait.MouseHover += new System.EventHandler(this.checkBoxSerialTimeAnswerWait_MouseHover);
            // 
            // textBoxSerialTimeWriteWait
            // 
            this.textBoxSerialTimeWriteWait.Location = new System.Drawing.Point(283, 240);
            this.textBoxSerialTimeWriteWait.Name = "textBoxSerialTimeWriteWait";
            this.textBoxSerialTimeWriteWait.Size = new System.Drawing.Size(50, 20);
            this.textBoxSerialTimeWriteWait.TabIndex = 21;
            this.textBoxSerialTimeWriteWait.TextChanged += new System.EventHandler(this.textBoxSerialTimeWriteWait_TextChanged);
            this.textBoxSerialTimeWriteWait.MouseHover += new System.EventHandler(this.textBoxSerialTimeWriteWait_MouseHover);
            // 
            // textBoxSerialTimeAnswerWait
            // 
            this.textBoxSerialTimeAnswerWait.Location = new System.Drawing.Point(283, 266);
            this.textBoxSerialTimeAnswerWait.Name = "textBoxSerialTimeAnswerWait";
            this.textBoxSerialTimeAnswerWait.Size = new System.Drawing.Size(50, 20);
            this.textBoxSerialTimeAnswerWait.TabIndex = 19;
            this.textBoxSerialTimeAnswerWait.TextChanged += new System.EventHandler(this.textBoxSerialTimeAnswerWait_TextChanged);
            this.textBoxSerialTimeAnswerWait.MouseHover += new System.EventHandler(this.textBoxSerialTimeAnswerWait_MouseHover);
            // 
            // comboBoxSerialDataBits
            // 
            this.comboBoxSerialDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialDataBits.FormattingEnabled = true;
            this.comboBoxSerialDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxSerialDataBits.Location = new System.Drawing.Point(83, 124);
            this.comboBoxSerialDataBits.Name = "comboBoxSerialDataBits";
            this.comboBoxSerialDataBits.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSerialDataBits.TabIndex = 11;
            this.comboBoxSerialDataBits.MouseHover += new System.EventHandler(this.comboBoxSerialDataBits_MouseHover);
            // 
            // comboBoxSerialSpeed
            // 
            this.comboBoxSerialSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialSpeed.FormattingEnabled = true;
            this.comboBoxSerialSpeed.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "31250",
            "38400",
            "57600",
            "115200"});
            this.comboBoxSerialSpeed.Location = new System.Drawing.Point(83, 98);
            this.comboBoxSerialSpeed.Name = "comboBoxSerialSpeed";
            this.comboBoxSerialSpeed.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSerialSpeed.TabIndex = 10;
            this.comboBoxSerialSpeed.MouseHover += new System.EventHandler(this.comboBoxSerialSpeed_MouseHover);
            // 
            // buttonDefaultSerial
            // 
            this.buttonDefaultSerial.Location = new System.Drawing.Point(7, 465);
            this.buttonDefaultSerial.Name = "buttonDefaultSerial";
            this.buttonDefaultSerial.Size = new System.Drawing.Size(96, 23);
            this.buttonDefaultSerial.TabIndex = 3;
            this.buttonDefaultSerial.Text = "По умолчанию";
            this.buttonDefaultSerial.UseVisualStyleBackColor = true;
            this.buttonDefaultSerial.Click += new System.EventHandler(this.buttonSerialDefault_Click);
            this.buttonDefaultSerial.MouseHover += new System.EventHandler(this.buttonDefaultSerial_MouseHover);
            // 
            // labelStopBits
            // 
            this.labelStopBits.AutoSize = true;
            this.labelStopBits.Location = new System.Drawing.Point(15, 165);
            this.labelStopBits.Name = "labelStopBits";
            this.labelStopBits.Size = new System.Drawing.Size(62, 13);
            this.labelStopBits.TabIndex = 9;
            this.labelStopBits.Text = "Стоп-биты:";
            this.labelStopBits.MouseHover += new System.EventHandler(this.labelStopBits_MouseHover);
            // 
            // comboBoxSerialStopBits
            // 
            this.comboBoxSerialStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialStopBits.FormattingEnabled = true;
            this.comboBoxSerialStopBits.Items.AddRange(new object[] {
            "0",
            "1",
            "1.5",
            "2"});
            this.comboBoxSerialStopBits.Location = new System.Drawing.Point(83, 177);
            this.comboBoxSerialStopBits.Name = "comboBoxSerialStopBits";
            this.comboBoxSerialStopBits.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSerialStopBits.TabIndex = 8;
            this.comboBoxSerialStopBits.MouseHover += new System.EventHandler(this.comboBoxSerialStopBits_MouseHover);
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Location = new System.Drawing.Point(19, 138);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(58, 13);
            this.labelParity.TabIndex = 7;
            this.labelParity.Text = "Чётность:";
            this.labelParity.MouseHover += new System.EventHandler(this.labelParity_MouseHover);
            // 
            // comboBoxSerialParity
            // 
            this.comboBoxSerialParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialParity.FormattingEnabled = true;
            this.comboBoxSerialParity.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None",
            "Mark",
            "Space"});
            this.comboBoxSerialParity.Location = new System.Drawing.Point(83, 150);
            this.comboBoxSerialParity.Name = "comboBoxSerialParity";
            this.comboBoxSerialParity.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSerialParity.TabIndex = 6;
            this.comboBoxSerialParity.MouseHover += new System.EventHandler(this.comboBoxSerialParity_MouseHover);
            // 
            // labelSerialBits
            // 
            this.labelSerialBits.AutoSize = true;
            this.labelSerialBits.Location = new System.Drawing.Point(9, 112);
            this.labelSerialBits.Name = "labelSerialBits";
            this.labelSerialBits.Size = new System.Drawing.Size(68, 13);
            this.labelSerialBits.TabIndex = 5;
            this.labelSerialBits.Text = "Бит данных:";
            this.labelSerialBits.MouseHover += new System.EventHandler(this.labelSerialBits_MouseHover);
            // 
            // labelSerialSpeed
            // 
            this.labelSerialSpeed.AutoSize = true;
            this.labelSerialSpeed.Location = new System.Drawing.Point(19, 86);
            this.labelSerialSpeed.Name = "labelSerialSpeed";
            this.labelSerialSpeed.Size = new System.Drawing.Size(58, 13);
            this.labelSerialSpeed.TabIndex = 3;
            this.labelSerialSpeed.Text = "Скорость:";
            this.labelSerialSpeed.MouseHover += new System.EventHandler(this.labelSerialSpeed_MouseHover);
            // 
            // labelSerialName
            // 
            this.labelSerialName.AutoSize = true;
            this.labelSerialName.Location = new System.Drawing.Point(15, 59);
            this.labelSerialName.Name = "labelSerialName";
            this.labelSerialName.Size = new System.Drawing.Size(62, 13);
            this.labelSerialName.TabIndex = 1;
            this.labelSerialName.Text = "Serial-порт:";
            this.labelSerialName.MouseHover += new System.EventHandler(this.labelSerialName_MouseHover);
            // 
            // comboBoxSerialName
            // 
            this.comboBoxSerialName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialName.FormattingEnabled = true;
            this.comboBoxSerialName.Location = new System.Drawing.Point(83, 71);
            this.comboBoxSerialName.Name = "comboBoxSerialName";
            this.comboBoxSerialName.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSerialName.TabIndex = 0;
            this.comboBoxSerialName.DropDown += new System.EventHandler(this.comboBoxSerialName_DropDown);
            this.comboBoxSerialName.SelectedIndexChanged += new System.EventHandler(this.comboBoxSerialName_SelectedIndexChanged);
            this.comboBoxSerialName.MouseHover += new System.EventHandler(this.comboBoxSerialName_MouseHover);
            // 
            // tabPageConsol
            // 
            this.tabPageConsol.Controls.Add(this.textBoxSimbolsRazdPaketByPosledovSimbols);
            this.tabPageConsol.Controls.Add(this.checkBoxRazdPaket);
            this.tabPageConsol.Controls.Add(this.buttonConsolDefault);
            this.tabPageConsol.Controls.Add(this.groupBoxText);
            this.tabPageConsol.Controls.Add(this.groupBoxTime);
            this.tabPageConsol.Controls.Add(this.groupBoxHex);
            this.tabPageConsol.Controls.Add(this.groupBoxSave);
            this.tabPageConsol.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsol.Name = "tabPageConsol";
            this.tabPageConsol.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsol.Size = new System.Drawing.Size(349, 494);
            this.tabPageConsol.TabIndex = 2;
            this.tabPageConsol.Text = "Консоль";
            this.tabPageConsol.UseVisualStyleBackColor = true;
            // 
            // textBoxSimbolsRazdPaketByPosledovSimbols
            // 
            this.textBoxSimbolsRazdPaketByPosledovSimbols.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSimbolsRazdPaketByPosledovSimbols.Location = new System.Drawing.Point(150, 433);
            this.textBoxSimbolsRazdPaketByPosledovSimbols.Name = "textBoxSimbolsRazdPaketByPosledovSimbols";
            this.textBoxSimbolsRazdPaketByPosledovSimbols.Size = new System.Drawing.Size(183, 20);
            this.textBoxSimbolsRazdPaketByPosledovSimbols.TabIndex = 27;
            this.textBoxSimbolsRazdPaketByPosledovSimbols.TextChanged += new System.EventHandler(this.textBoxSimbolsRazdPaketByPosledovSimbols_TextChanged);
            this.textBoxSimbolsRazdPaketByPosledovSimbols.MouseHover += new System.EventHandler(this.textBoxSimbolsRazdPaketByPosledovSimbols_MouseHover);
            // 
            // checkBoxRazdPaket
            // 
            this.checkBoxRazdPaket.AutoSize = true;
            this.checkBoxRazdPaket.Location = new System.Drawing.Point(13, 415);
            this.checkBoxRazdPaket.Name = "checkBoxRazdPaket";
            this.checkBoxRazdPaket.Size = new System.Drawing.Size(322, 17);
            this.checkBoxRazdPaket.TabIndex = 26;
            this.checkBoxRazdPaket.Text = "Разделять принятые пакеты по послед-ти символов (hex):";
            this.checkBoxRazdPaket.UseVisualStyleBackColor = true;
            this.checkBoxRazdPaket.CheckedChanged += new System.EventHandler(this.checkBoxRazdPaket_CheckedChanged);
            this.checkBoxRazdPaket.MouseHover += new System.EventHandler(this.checkBoxRazdPaket_MouseHover);
            // 
            // buttonConsolDefault
            // 
            this.buttonConsolDefault.Location = new System.Drawing.Point(7, 465);
            this.buttonConsolDefault.Name = "buttonConsolDefault";
            this.buttonConsolDefault.Size = new System.Drawing.Size(96, 23);
            this.buttonConsolDefault.TabIndex = 25;
            this.buttonConsolDefault.Text = "По умолчанию";
            this.buttonConsolDefault.UseVisualStyleBackColor = true;
            this.buttonConsolDefault.Click += new System.EventHandler(this.buttonConsolDefault_Click);
            this.buttonConsolDefault.MouseHover += new System.EventHandler(this.buttonConsolDefault_MouseHover);
            // 
            // groupBoxText
            // 
            this.groupBoxText.Controls.Add(this.checkBoxShowInConsolColumnText);
            this.groupBoxText.Controls.Add(this.panelText);
            this.groupBoxText.Location = new System.Drawing.Point(6, 344);
            this.groupBoxText.Name = "groupBoxText";
            this.groupBoxText.Size = new System.Drawing.Size(337, 65);
            this.groupBoxText.TabIndex = 24;
            this.groupBoxText.TabStop = false;
            this.groupBoxText.Text = "                            Колонка Текст";
            // 
            // checkBoxShowInConsolColumnText
            // 
            this.checkBoxShowInConsolColumnText.AutoSize = true;
            this.checkBoxShowInConsolColumnText.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxShowInConsolColumnText.Location = new System.Drawing.Point(6, -3);
            this.checkBoxShowInConsolColumnText.Name = "checkBoxShowInConsolColumnText";
            this.checkBoxShowInConsolColumnText.Size = new System.Drawing.Size(88, 19);
            this.checkBoxShowInConsolColumnText.TabIndex = 29;
            this.checkBoxShowInConsolColumnText.Text = "показывать";
            this.checkBoxShowInConsolColumnText.UseVisualStyleBackColor = true;
            this.checkBoxShowInConsolColumnText.CheckedChanged += new System.EventHandler(this.checkBoxShowInConsolColumnText_CheckedChanged);
            // 
            // panelText
            // 
            this.panelText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelText.Controls.Add(this.linkLabelTextKod);
            this.panelText.Controls.Add(this.comboBoxCodePage);
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.Location = new System.Drawing.Point(3, 16);
            this.panelText.Name = "panelText";
            this.panelText.Size = new System.Drawing.Size(331, 46);
            this.panelText.TabIndex = 0;
            // 
            // linkLabelTextKod
            // 
            this.linkLabelTextKod.AutoSize = true;
            this.linkLabelTextKod.Location = new System.Drawing.Point(9, 13);
            this.linkLabelTextKod.Name = "linkLabelTextKod";
            this.linkLabelTextKod.Size = new System.Drawing.Size(102, 13);
            this.linkLabelTextKod.TabIndex = 9;
            this.linkLabelTextKod.TabStop = true;
            this.linkLabelTextKod.Text = "Кодировка текста:";
            this.linkLabelTextKod.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTextKod_LinkClicked);
            this.linkLabelTextKod.MouseHover += new System.EventHandler(this.linkLabelTextKod_MouseHover);
            // 
            // comboBoxCodePage
            // 
            this.comboBoxCodePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodePage.FormattingEnabled = true;
            this.comboBoxCodePage.Items.AddRange(new object[] {
            "37",
            "437",
            "500",
            "708",
            "720",
            "737",
            "775",
            "850",
            "852",
            "855",
            "857",
            "858",
            "860",
            "861",
            "862",
            "863",
            "864",
            "865",
            "866",
            "869",
            "870",
            "874",
            "875",
            "932",
            "936",
            "949",
            "950",
            "1026",
            "1047",
            "1140",
            "1141",
            "1142",
            "1143",
            "1144",
            "1145",
            "1146",
            "1147",
            "1148",
            "1149",
            "1200",
            "1201",
            "1250",
            "1251",
            "1252",
            "1253",
            "1254",
            "1255",
            "1256",
            "1257",
            "1258",
            "1361",
            "10000",
            "10001",
            "10002",
            "10003",
            "10004",
            "10005",
            "10006",
            "10007",
            "10008",
            "10010",
            "10017",
            "10021",
            "10029",
            "10079",
            "10081",
            "10082",
            "12000",
            "12001",
            "20000",
            "20001",
            "20002",
            "20003",
            "20004",
            "20005",
            "20105",
            "20106",
            "20107",
            "20108",
            "20127",
            "20261",
            "20269",
            "20273",
            "20277",
            "20278",
            "20280",
            "20284",
            "20285",
            "20290",
            "20297",
            "20420",
            "20423",
            "20424",
            "20833",
            "20838",
            "20866",
            "20871",
            "20880",
            "20905",
            "20924",
            "20932",
            "20936",
            "20949",
            "21025",
            "21866",
            "28591",
            "28592",
            "28593",
            "28594",
            "28595",
            "28596",
            "28597",
            "28598",
            "28599",
            "28603",
            "28605",
            "29001",
            "38598",
            "50220",
            "50221",
            "50222",
            "50225",
            "50227",
            "51932",
            "51936",
            "51949",
            "52936",
            "54936",
            "57002",
            "57003",
            "57004",
            "57005",
            "57006",
            "57007",
            "57008",
            "57009",
            "57010",
            "57011",
            "65000",
            "65001"});
            this.comboBoxCodePage.Location = new System.Drawing.Point(111, 10);
            this.comboBoxCodePage.Name = "comboBoxCodePage";
            this.comboBoxCodePage.Size = new System.Drawing.Size(211, 21);
            this.comboBoxCodePage.TabIndex = 8;
            this.comboBoxCodePage.SelectedIndexChanged += new System.EventHandler(this.comboBoxCodePage_SelectedIndexChanged);
            this.comboBoxCodePage.MouseHover += new System.EventHandler(this.comboBoxCodePage_MouseHover);
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.checkBoxShowInConsolColumnTime);
            this.groupBoxTime.Controls.Add(this.panelTime);
            this.groupBoxTime.Location = new System.Drawing.Point(6, 129);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(337, 92);
            this.groupBoxTime.TabIndex = 23;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "                            Колонка Время";
            // 
            // checkBoxShowInConsolColumnTime
            // 
            this.checkBoxShowInConsolColumnTime.AutoSize = true;
            this.checkBoxShowInConsolColumnTime.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxShowInConsolColumnTime.Location = new System.Drawing.Point(7, -3);
            this.checkBoxShowInConsolColumnTime.Name = "checkBoxShowInConsolColumnTime";
            this.checkBoxShowInConsolColumnTime.Size = new System.Drawing.Size(88, 19);
            this.checkBoxShowInConsolColumnTime.TabIndex = 1;
            this.checkBoxShowInConsolColumnTime.Text = "показывать";
            this.checkBoxShowInConsolColumnTime.UseVisualStyleBackColor = true;
            this.checkBoxShowInConsolColumnTime.CheckedChanged += new System.EventHandler(this.checkBoxShowInConsolColumnTime_CheckedChanged);
            // 
            // panelTime
            // 
            this.panelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTime.Controls.Add(this.checkBoxTimeMS);
            this.panelTime.Controls.Add(this.checkBoxTimePodpis);
            this.panelTime.Controls.Add(this.comboBoxTimeFormat);
            this.panelTime.Controls.Add(this.labelTimeFormat);
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTime.Location = new System.Drawing.Point(3, 16);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(331, 73);
            this.panelTime.TabIndex = 0;
            // 
            // checkBoxTimeMS
            // 
            this.checkBoxTimeMS.AutoSize = true;
            this.checkBoxTimeMS.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxTimeMS.Checked = true;
            this.checkBoxTimeMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTimeMS.Location = new System.Drawing.Point(24, 30);
            this.checkBoxTimeMS.Name = "checkBoxTimeMS";
            this.checkBoxTimeMS.Size = new System.Drawing.Size(298, 17);
            this.checkBoxTimeMS.TabIndex = 20;
            this.checkBoxTimeMS.Text = "Показывать миллисекунды (например 10:23:07.357с)";
            this.checkBoxTimeMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxTimeMS.UseVisualStyleBackColor = true;
            this.checkBoxTimeMS.CheckedChanged += new System.EventHandler(this.checkBoxTimeMS_CheckedChanged);
            // 
            // checkBoxTimePodpis
            // 
            this.checkBoxTimePodpis.AutoSize = true;
            this.checkBoxTimePodpis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxTimePodpis.Checked = true;
            this.checkBoxTimePodpis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTimePodpis.Location = new System.Drawing.Point(43, 49);
            this.checkBoxTimePodpis.Name = "checkBoxTimePodpis";
            this.checkBoxTimePodpis.Size = new System.Drawing.Size(279, 17);
            this.checkBoxTimePodpis.TabIndex = 19;
            this.checkBoxTimePodpis.Text = "Подписывать время (например 08.03М 10:23:07с)";
            this.checkBoxTimePodpis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxTimePodpis.UseVisualStyleBackColor = true;
            this.checkBoxTimePodpis.CheckedChanged += new System.EventHandler(this.checkBoxTimePodpis_CheckedChanged);
            this.checkBoxTimePodpis.MouseHover += new System.EventHandler(this.checkBoxTimePodpis_MouseHover);
            // 
            // comboBoxTimeFormat
            // 
            this.comboBoxTimeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeFormat.FormattingEnabled = true;
            this.comboBoxTimeFormat.Items.AddRange(new object[] {
            "дд:ММ:гг чч:мм:сс",
            "дд:ММ чч:мм:сс",
            "дд чч:мм:сс",
            "чч:мм:сс",
            "мм:сс",
            "сс"});
            this.comboBoxTimeFormat.Location = new System.Drawing.Point(194, 3);
            this.comboBoxTimeFormat.Name = "comboBoxTimeFormat";
            this.comboBoxTimeFormat.Size = new System.Drawing.Size(128, 21);
            this.comboBoxTimeFormat.TabIndex = 18;
            this.comboBoxTimeFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeFormat_SelectedIndexChanged);
            this.comboBoxTimeFormat.MouseHover += new System.EventHandler(this.comboBoxTimeFormat_MouseHover);
            // 
            // labelTimeFormat
            // 
            this.labelTimeFormat.AutoSize = true;
            this.labelTimeFormat.Location = new System.Drawing.Point(92, 6);
            this.labelTimeFormat.Name = "labelTimeFormat";
            this.labelTimeFormat.Size = new System.Drawing.Size(99, 13);
            this.labelTimeFormat.TabIndex = 17;
            this.labelTimeFormat.Text = "Формат времени:";
            this.labelTimeFormat.MouseHover += new System.EventHandler(this.labelTimeFormat_MouseHover);
            // 
            // groupBoxHex
            // 
            this.groupBoxHex.Controls.Add(this.checkBoxShowInConsolColumnHex);
            this.groupBoxHex.Controls.Add(this.panelHex);
            this.groupBoxHex.Location = new System.Drawing.Point(6, 227);
            this.groupBoxHex.Name = "groupBoxHex";
            this.groupBoxHex.Size = new System.Drawing.Size(337, 108);
            this.groupBoxHex.TabIndex = 22;
            this.groupBoxHex.TabStop = false;
            this.groupBoxHex.Text = "                            Колонка Hex";
            // 
            // checkBoxShowInConsolColumnHex
            // 
            this.checkBoxShowInConsolColumnHex.AutoSize = true;
            this.checkBoxShowInConsolColumnHex.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxShowInConsolColumnHex.Location = new System.Drawing.Point(6, -3);
            this.checkBoxShowInConsolColumnHex.Name = "checkBoxShowInConsolColumnHex";
            this.checkBoxShowInConsolColumnHex.Size = new System.Drawing.Size(88, 19);
            this.checkBoxShowInConsolColumnHex.TabIndex = 2;
            this.checkBoxShowInConsolColumnHex.Text = "показывать";
            this.checkBoxShowInConsolColumnHex.UseVisualStyleBackColor = true;
            this.checkBoxShowInConsolColumnHex.CheckedChanged += new System.EventHandler(this.checkBoxShowInConsolColumnHex_CheckedChanged);
            // 
            // panelHex
            // 
            this.panelHex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelHex.Controls.Add(this.checkBoxRazdByte);
            this.panelHex.Controls.Add(this.comboBoxRazdByte);
            this.panelHex.Controls.Add(this.checkBoxRazd8byte);
            this.panelHex.Controls.Add(this.labelNumByte);
            this.panelHex.Controls.Add(this.comboBoxNumByte);
            this.panelHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHex.Location = new System.Drawing.Point(3, 16);
            this.panelHex.Name = "panelHex";
            this.panelHex.Size = new System.Drawing.Size(331, 89);
            this.panelHex.TabIndex = 0;
            // 
            // checkBoxRazdByte
            // 
            this.checkBoxRazdByte.AutoSize = true;
            this.checkBoxRazdByte.Location = new System.Drawing.Point(12, 63);
            this.checkBoxRazdByte.Name = "checkBoxRazdByte";
            this.checkBoxRazdByte.Size = new System.Drawing.Size(188, 17);
            this.checkBoxRazdByte.TabIndex = 27;
            this.checkBoxRazdByte.Text = "Разделять байты, разделитель:";
            this.checkBoxRazdByte.UseVisualStyleBackColor = true;
            this.checkBoxRazdByte.CheckedChanged += new System.EventHandler(this.checkBoxRazdByte_CheckedChanged);
            this.checkBoxRazdByte.MouseHover += new System.EventHandler(this.checkBoxRazdByte_MouseHover);
            // 
            // comboBoxRazdByte
            // 
            this.comboBoxRazdByte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRazdByte.FormattingEnabled = true;
            this.comboBoxRazdByte.Items.AddRange(new object[] {
            "[ ] [пробел]",
            "_ [подчёркивание]",
            ". [точка]",
            ", [запятая]",
            "- [дефис]",
            "* [звёздочка]",
            ": [двоеточие]",
            "; [точка с запятой]"});
            this.comboBoxRazdByte.Location = new System.Drawing.Point(200, 61);
            this.comboBoxRazdByte.Name = "comboBoxRazdByte";
            this.comboBoxRazdByte.Size = new System.Drawing.Size(122, 21);
            this.comboBoxRazdByte.TabIndex = 19;
            this.comboBoxRazdByte.SelectedIndexChanged += new System.EventHandler(this.comboBoxRazd_SelectedIndexChanged);
            this.comboBoxRazdByte.MouseHover += new System.EventHandler(this.comboBoxRazdByte_MouseHover);
            // 
            // checkBoxRazd8byte
            // 
            this.checkBoxRazd8byte.AutoSize = true;
            this.checkBoxRazd8byte.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxRazd8byte.Checked = true;
            this.checkBoxRazd8byte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRazd8byte.Location = new System.Drawing.Point(35, 10);
            this.checkBoxRazd8byte.Name = "checkBoxRazd8byte";
            this.checkBoxRazd8byte.Size = new System.Drawing.Size(287, 17);
            this.checkBoxRazd8byte.TabIndex = 9;
            this.checkBoxRazd8byte.Text = "Разделять hex-символы разделителем \'|\' по 8 байт:";
            this.checkBoxRazd8byte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRazd8byte.UseVisualStyleBackColor = true;
            this.checkBoxRazd8byte.CheckedChanged += new System.EventHandler(this.checkBoxRazd8byte_CheckedChanged);
            this.checkBoxRazd8byte.MouseHover += new System.EventHandler(this.checkBoxRazd8byte_MouseHover);
            // 
            // labelNumByte
            // 
            this.labelNumByte.AutoSize = true;
            this.labelNumByte.Location = new System.Drawing.Point(9, 36);
            this.labelNumByte.Name = "labelNumByte";
            this.labelNumByte.Size = new System.Drawing.Size(255, 13);
            this.labelNumByte.TabIndex = 13;
            this.labelNumByte.Text = "Макс. количество байт в каждой строке данных:";
            this.labelNumByte.MouseHover += new System.EventHandler(this.labelNumByte_MouseHover);
            // 
            // comboBoxNumByte
            // 
            this.comboBoxNumByte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNumByte.FormattingEnabled = true;
            this.comboBoxNumByte.Items.AddRange(new object[] {
            "[пробел]",
            "-",
            "*",
            "_",
            "[нет]"});
            this.comboBoxNumByte.Location = new System.Drawing.Point(266, 33);
            this.comboBoxNumByte.Name = "comboBoxNumByte";
            this.comboBoxNumByte.Size = new System.Drawing.Size(56, 21);
            this.comboBoxNumByte.TabIndex = 20;
            this.comboBoxNumByte.SelectedIndexChanged += new System.EventHandler(this.comboBoxNumByte_SelectedIndexChanged);
            this.comboBoxNumByte.MouseHover += new System.EventHandler(this.comboBoxNumByte_MouseHover);
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSave.Controls.Add(this.panelSave);
            this.groupBoxSave.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(337, 117);
            this.groupBoxSave.TabIndex = 21;
            this.groupBoxSave.TabStop = false;
            this.groupBoxSave.Text = "Сохранение по умолчанию";
            // 
            // panelSave
            // 
            this.panelSave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSave.Controls.Add(this.buttonFileLogSend);
            this.panelSave.Controls.Add(this.labelDefailtFileLogSend);
            this.panelSave.Controls.Add(this.textBoxDefailtFileLogSend);
            this.panelSave.Controls.Add(this.labelDefaultFileLogPrinat);
            this.panelSave.Controls.Add(this.textBoxDefaultFileLogPrinat);
            this.panelSave.Controls.Add(this.buttonFileLogPrinat);
            this.panelSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSave.Location = new System.Drawing.Point(3, 16);
            this.panelSave.Name = "panelSave";
            this.panelSave.Size = new System.Drawing.Size(331, 98);
            this.panelSave.TabIndex = 23;
            // 
            // buttonFileLogSend
            // 
            this.buttonFileLogSend.Location = new System.Drawing.Point(289, 25);
            this.buttonFileLogSend.Name = "buttonFileLogSend";
            this.buttonFileLogSend.Size = new System.Drawing.Size(33, 20);
            this.buttonFileLogSend.TabIndex = 19;
            this.buttonFileLogSend.Text = "...";
            this.buttonFileLogSend.UseVisualStyleBackColor = true;
            this.buttonFileLogSend.MouseHover += new System.EventHandler(this.buttonFileLogSend_MouseHover);
            // 
            // labelDefailtFileLogSend
            // 
            this.labelDefailtFileLogSend.AutoSize = true;
            this.labelDefailtFileLogSend.Location = new System.Drawing.Point(3, 9);
            this.labelDefailtFileLogSend.Name = "labelDefailtFileLogSend";
            this.labelDefailtFileLogSend.Size = new System.Drawing.Size(285, 13);
            this.labelDefailtFileLogSend.TabIndex = 18;
            this.labelDefailtFileLogSend.Text = "Файл по умолч. для сохранения отправленных данных";
            this.labelDefailtFileLogSend.MouseHover += new System.EventHandler(this.labelDefailtFileLogSend_MouseHover);
            // 
            // textBoxDefailtFileLogSend
            // 
            this.textBoxDefailtFileLogSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDefailtFileLogSend.Location = new System.Drawing.Point(6, 25);
            this.textBoxDefailtFileLogSend.Name = "textBoxDefailtFileLogSend";
            this.textBoxDefailtFileLogSend.Size = new System.Drawing.Size(277, 20);
            this.textBoxDefailtFileLogSend.TabIndex = 17;
            this.textBoxDefailtFileLogSend.MouseHover += new System.EventHandler(this.textBoxDefailtFileLogSend_MouseHover);
            // 
            // labelDefaultFileLogPrinat
            // 
            this.labelDefaultFileLogPrinat.AutoSize = true;
            this.labelDefaultFileLogPrinat.Location = new System.Drawing.Point(3, 53);
            this.labelDefaultFileLogPrinat.Name = "labelDefaultFileLogPrinat";
            this.labelDefaultFileLogPrinat.Size = new System.Drawing.Size(261, 13);
            this.labelDefaultFileLogPrinat.TabIndex = 5;
            this.labelDefaultFileLogPrinat.Text = "Файл по умолч. для сохранения принятых данных";
            this.labelDefaultFileLogPrinat.MouseHover += new System.EventHandler(this.labelDefaultFileLogPrinat_MouseHover);
            // 
            // textBoxDefaultFileLogPrinat
            // 
            this.textBoxDefaultFileLogPrinat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDefaultFileLogPrinat.Location = new System.Drawing.Point(6, 69);
            this.textBoxDefaultFileLogPrinat.Name = "textBoxDefaultFileLogPrinat";
            this.textBoxDefaultFileLogPrinat.Size = new System.Drawing.Size(277, 20);
            this.textBoxDefaultFileLogPrinat.TabIndex = 4;
            this.textBoxDefaultFileLogPrinat.MouseHover += new System.EventHandler(this.textBoxDefailtFileLogPrinat_MouseHover);
            // 
            // buttonFileLogPrinat
            // 
            this.buttonFileLogPrinat.Location = new System.Drawing.Point(289, 71);
            this.buttonFileLogPrinat.Name = "buttonFileLogPrinat";
            this.buttonFileLogPrinat.Size = new System.Drawing.Size(33, 20);
            this.buttonFileLogPrinat.TabIndex = 6;
            this.buttonFileLogPrinat.Text = "...";
            this.buttonFileLogPrinat.UseVisualStyleBackColor = true;
            this.buttonFileLogPrinat.Click += new System.EventHandler(this.buttonFileLogPrinat_Click);
            this.buttonFileLogPrinat.MouseHover += new System.EventHandler(this.buttonFileLogPrinat_MouseHover);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(202, 530);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.MouseHover += new System.EventHandler(this.buttonOK_MouseHover);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(283, 530);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.MouseHover += new System.EventHandler(this.buttonCancel_MouseHover);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.ShowAlways = true;
            // 
            // timerPortList
            // 
            this.timerPortList.Enabled = true;
            this.timerPortList.Interval = 3000;
            this.timerPortList.Tick += new System.EventHandler(this.timerPortList_Tick);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 561);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "Настройки";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsForm_FormClosed);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.Shown += new System.EventHandler(this.FormOptions_Shown);
            this.tabControlOptions.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageSerial.ResumeLayout(false);
            this.tabPageSerial.PerformLayout();
            this.tabPageConsol.ResumeLayout(false);
            this.tabPageConsol.PerformLayout();
            this.groupBoxText.ResumeLayout(false);
            this.groupBoxText.PerformLayout();
            this.panelText.ResumeLayout(false);
            this.panelText.PerformLayout();
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.panelTime.PerformLayout();
            this.groupBoxHex.ResumeLayout(false);
            this.groupBoxHex.PerformLayout();
            this.panelHex.ResumeLayout(false);
            this.panelHex.PerformLayout();
            this.groupBoxSave.ResumeLayout(false);
            this.panelSave.ResumeLayout(false);
            this.panelSave.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageSerial;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSerialName;
        public System.Windows.Forms.ComboBox comboBoxSerialName;
        private System.Windows.Forms.Label labelSerialSpeed;
        private System.Windows.Forms.Label labelSerialBits;
        private System.Windows.Forms.Label labelStopBits;
        public System.Windows.Forms.ComboBox comboBoxSerialStopBits;
        private System.Windows.Forms.Label labelParity;
        public System.Windows.Forms.ComboBox comboBoxSerialParity;
        private System.Windows.Forms.Button buttonDefaultSerial;
        public System.Windows.Forms.CheckBox checkBoxAutoOpenSerial;
        public System.Windows.Forms.ComboBox comboBoxSerialSpeed;
        public System.Windows.Forms.ComboBox comboBoxSerialDataBits;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonDefaultGeneral;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPageConsol;
        private System.Windows.Forms.ComboBox comboBoxCodePage;
        private System.Windows.Forms.Button buttonFileLogPrinat;
        private System.Windows.Forms.Label labelDefaultFileLogPrinat;
        private System.Windows.Forms.TextBox textBoxDefaultFileLogPrinat;
        private System.Windows.Forms.CheckBox checkBoxRazd8byte;
        private System.Windows.Forms.Label labelNumByte;
        private System.Windows.Forms.Label labelTimeFormat;
        private System.Windows.Forms.ComboBox comboBoxTimeFormat;
        private System.Windows.Forms.ComboBox comboBoxRazdByte;
        private System.Windows.Forms.ComboBox comboBoxNumByte;
        private System.Windows.Forms.GroupBox groupBoxHex;
        private System.Windows.Forms.GroupBox groupBoxSave;
        private System.Windows.Forms.Panel panelSave;
        private System.Windows.Forms.Panel panelHex;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.GroupBox groupBoxText;
        private System.Windows.Forms.Panel panelText;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.Button buttonConsolDefault;
        private System.Windows.Forms.LinkLabel linkLabelTextKod;
        private System.Windows.Forms.CheckBox checkBoxRazdPaket;
        private System.Windows.Forms.TextBox textBoxSimbolsRazdPaketByPosledovSimbols;
        private System.Windows.Forms.CheckBox checkBoxRazdByte;
        private System.Windows.Forms.CheckBox checkBoxTimePodpis;
        private System.Windows.Forms.Button buttonFileLogSend;
        private System.Windows.Forms.Label labelDefailtFileLogSend;
        private System.Windows.Forms.TextBox textBoxDefailtFileLogSend;
        private System.Windows.Forms.TextBox textBoxSerialTimeAnswerWait;
        private System.Windows.Forms.TextBox textBoxSerialTimeWriteWait;
        private System.Windows.Forms.CheckBox checkBoxSerialTimeWriteWait;
        private System.Windows.Forms.CheckBox checkBoxSerialTimeAnswerWait;
        private System.Windows.Forms.CheckBox checkBoxShowInConsolColumnTime;
        private System.Windows.Forms.CheckBox checkBoxShowInConsolColumnText;
        private System.Windows.Forms.CheckBox checkBoxShowInConsolColumnHex;
        private System.Windows.Forms.Timer timerPortList;
        private System.Windows.Forms.CheckBox checkBoxCheckCondition;
        private System.Windows.Forms.CheckBox checkBoxTimeMS;
        public System.Windows.Forms.CheckBox checkBoxAllowEnterText;
        public System.Windows.Forms.CheckBox checkBoxDoubleCommandTextForm;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.CheckBox checkBoxWindowSetSerialPort;
    }
}
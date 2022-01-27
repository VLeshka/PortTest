namespace ComApplication
{
    partial class StartInfoForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartInfoForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(213, 263);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "       ПортТест, именуемая в дальнейшем \"программа\" - программа для тестирования",
            "приборов, подключённых через serial-порт к компьютеру.",
            "",
            "       Программа создавалась для новичков в программировании микроконтроллеров ",
            "(например, осваивающих отладочные комплекты TI LaunchPad, Arduino).",
            "       Программа облегчает передачу и приём текстовой информации различной ",
            "текстовой кодировки.",
            "",
            "       ПортТест - полностью бесплатная программа (freeware).",
            "       Для нормальной работы программы не требуется никаких финансовых отчислений" +
                ".",
            "",
            "       Программа ПортТест распространяется по принципу \"как есть\". Никаких явных " +
                "или ",
            "подразумеваемых гарантий не предусмотрено. Вся ответственность за последствия ",
            "использования этого продукта лежит исключительно на вас. Ни автор, ни его ",
            "представители не несут никакой ответственности за потери данных, повреждения, ",
            "упущенную прибыль или другие утраты, к которым привело правильное или ",
            "неправильное использование этой программы."});
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(466, 238);
            this.listBox1.TabIndex = 1;
            // 
            // StartInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 298);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartInfoForm";
            this.Text = "ПортТест";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox listBox1;
    }
}
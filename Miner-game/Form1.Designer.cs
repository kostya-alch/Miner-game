namespace Miner_game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размерПоляToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.х10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.х5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.х5ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(364, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.размерПоляToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // размерПоляToolStripMenuItem
            // 
            this.размерПоляToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.х10ToolStripMenuItem,
            this.х5ToolStripMenuItem,
            this.х5ToolStripMenuItem1});
            this.размерПоляToolStripMenuItem.Name = "размерПоляToolStripMenuItem";
            this.размерПоляToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.размерПоляToolStripMenuItem.Text = "Размер поля:";
            // 
            // х10ToolStripMenuItem
            // 
            this.х10ToolStripMenuItem.Name = "х10ToolStripMenuItem";
            this.х10ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.х10ToolStripMenuItem.Text = "10 х 10";
            this.х10ToolStripMenuItem.Click += new System.EventHandler(this.х10ToolStripMenuItem_Click);
            // 
            // х5ToolStripMenuItem
            // 
            this.х5ToolStripMenuItem.Name = "х5ToolStripMenuItem";
            this.х5ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.х5ToolStripMenuItem.Text = "5 х 5";
            this.х5ToolStripMenuItem.Click += new System.EventHandler(this.х5ToolStripMenuItem_Click);
            // 
            // х5ToolStripMenuItem1
            // 
            this.х5ToolStripMenuItem1.Name = "х5ToolStripMenuItem1";
            this.х5ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.х5ToolStripMenuItem1.Text = "1 х 5";
            this.х5ToolStripMenuItem1.Click += new System.EventHandler(this.х5ToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 371);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Miner";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размерПоляToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem х10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem х5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem х5ToolStripMenuItem1;
    }
}


namespace AES
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            canvas = new Panel();
            checkSyntaxBtn = new Button();
            executeBtn = new Button();
            cmdTextBox = new TextBox();
            programTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.Location = new Point(12, 12);
            canvas.Name = "canvas";
            canvas.Size = new Size(670, 640);
            canvas.TabIndex = 0;
            // 
            // checkSyntaxBtn
            // 
            checkSyntaxBtn.Location = new Point(704, 618);
            checkSyntaxBtn.Name = "checkSyntaxBtn";
            checkSyntaxBtn.Size = new Size(165, 34);
            checkSyntaxBtn.TabIndex = 1;
            checkSyntaxBtn.Text = "Check Syntax";
            checkSyntaxBtn.UseVisualStyleBackColor = true;
            checkSyntaxBtn.Click += checkSyntaxBtn_Click;
            // 
            // executeBtn
            // 
            executeBtn.Location = new Point(875, 618);
            executeBtn.Name = "executeBtn";
            executeBtn.Size = new Size(165, 34);
            executeBtn.TabIndex = 2;
            executeBtn.Text = "Execute";
            executeBtn.UseVisualStyleBackColor = true;
            // 
            // cmdTextBox
            // 
            cmdTextBox.Location = new Point(704, 12);
            cmdTextBox.Name = "cmdTextBox";
            cmdTextBox.Size = new Size(336, 31);
            cmdTextBox.TabIndex = 3;
            // 
            // programTextBox
            // 
            programTextBox.Location = new Point(704, 49);
            programTextBox.Name = "programTextBox";
            programTextBox.Size = new Size(336, 563);
            programTextBox.TabIndex = 4;
            programTextBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 664);
            Controls.Add(programTextBox);
            Controls.Add(cmdTextBox);
            Controls.Add(executeBtn);
            Controls.Add(checkSyntaxBtn);
            Controls.Add(canvas);
            Name = "Form1";
            Text = "AES";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel canvas;
        private Button checkSyntaxBtn;
        private Button executeBtn;
        private TextBox cmdTextBox;
        private RichTextBox programTextBox;
    }
}

namespace ChessAIForms
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerTurn = new System.Windows.Forms.Label();
            this.pieceselect = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.selectX = new System.Windows.Forms.Label();
            this.selectY = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(558, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player Turn :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(559, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Piece Selected : ";
            // 
            // playerTurn
            // 
            this.playerTurn.AutoSize = true;
            this.playerTurn.Location = new System.Drawing.Point(636, 58);
            this.playerTurn.Name = "playerTurn";
            this.playerTurn.Size = new System.Drawing.Size(27, 15);
            this.playerTurn.TabIndex = 3;
            this.playerTurn.Text = "null";
            // 
            // pieceselect
            // 
            this.pieceselect.AutoSize = true;
            this.pieceselect.Location = new System.Drawing.Point(656, 89);
            this.pieceselect.Name = "pieceselect";
            this.pieceselect.Size = new System.Drawing.Size(27, 15);
            this.pieceselect.TabIndex = 4;
            this.pieceselect.Text = "null";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(558, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "X :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(559, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y : ";
            // 
            // selectX
            // 
            this.selectX.AutoSize = true;
            this.selectX.Location = new System.Drawing.Point(612, 117);
            this.selectX.Name = "selectX";
            this.selectX.Size = new System.Drawing.Size(27, 15);
            this.selectX.TabIndex = 7;
            this.selectX.Text = "null";
            this.selectX.Click += new System.EventHandler(this.selectX_Click);
            // 
            // selectY
            // 
            this.selectY.AutoSize = true;
            this.selectY.Location = new System.Drawing.Point(612, 145);
            this.selectY.Name = "selectY";
            this.selectY.Size = new System.Drawing.Size(27, 15);
            this.selectY.TabIndex = 8;
            this.selectY.Text = "null";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(24, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 441);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 557);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectY);
            this.Controls.Add(this.selectX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pieceselect);
            this.Controls.Add(this.playerTurn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Chess AI by Quang";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label playerTurn;
        private System.Windows.Forms.Label pieceselect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label selectX;
        private System.Windows.Forms.Label selectY;
        private System.Windows.Forms.Panel panel1;
    }
}


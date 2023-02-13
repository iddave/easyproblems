using System.Drawing;
using System.Windows.Forms;

namespace sweeper
{
    partial class Form1 : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //public Form1()
        //    {
                
        //    }
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            InitializeComponent();
            NewGame(10);
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        Field field;
        GroupBox f;

        

        void NewGame(int n)
        {
            field = new Field(n);
            AddButtons();
            field.Change += ChangeButton;
            field.Loose += Loose;
            field.Win += Win;
            ChangeForm();
        }

        void AddButtons()
        {
            f = new GroupBox();
            f.Location = new Point(100, 100);
            f.Size = new Size(40 * field.N, 40 * field.N);
            f.Parent = this;

            for (int i = 0; i < field.N; ++i)
            {
                for (int j = 0; j < field.N; ++j)
                {
                    ControlButton b = new ControlButton(field, i, j);
                    b.Width = 37; b.Height = 37;
                    b.Location = new Point(j * 40, i * 40);
                    b.FlatStyle = FlatStyle.Flat;
                    b.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.cell));
                    b.ForeColor = SystemColors.Control; ;
                    b.Parent = f;
                }
            }
        }

        void ChangeButton(object s, ChangeArgs e)
        {
            foreach (object b in f.Controls)
            {
                if ((b as ControlButton) != null && (b as ControlButton).i == e.I && (b as ControlButton).j == e.J) 
                {
                        if (e.MinesArround == "0")
                        {
                            (b as ControlButton).Text = "";
                            (b as ControlButton).BackgroundImage = Resources.cellclear;
                        }
                        else
                        {
                            (b as ControlButton).Text = e.MinesArround;
                            (b as ControlButton).BackgroundImage = (System.Drawing.Image)sweeper.Resources.cellopen;
                        }
                }
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}
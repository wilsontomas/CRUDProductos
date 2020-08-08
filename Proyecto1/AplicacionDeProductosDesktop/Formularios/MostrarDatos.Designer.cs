namespace AplicacionDeProductosDesktop.Formularios
{
    partial class MostrarDatos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuplidoresGrid = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.ProductoIdRecibido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelDeImagenes = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.SuplidoresGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Imagenes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Suplidores";
            // 
            // SuplidoresGrid
            // 
            this.SuplidoresGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SuplidoresGrid.Location = new System.Drawing.Point(111, 29);
            this.SuplidoresGrid.Name = "SuplidoresGrid";
            this.SuplidoresGrid.Size = new System.Drawing.Size(383, 143);
            this.SuplidoresGrid.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "Cerrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ProductoIdRecibido
            // 
            this.ProductoIdRecibido.Location = new System.Drawing.Point(31, 15);
            this.ProductoIdRecibido.Name = "ProductoIdRecibido";
            this.ProductoIdRecibido.ReadOnly = true;
            this.ProductoIdRecibido.Size = new System.Drawing.Size(42, 20);
            this.ProductoIdRecibido.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Id";
            // 
            // PanelDeImagenes
            // 
            this.PanelDeImagenes.AutoScroll = true;
            this.PanelDeImagenes.Location = new System.Drawing.Point(111, 195);
            this.PanelDeImagenes.Name = "PanelDeImagenes";
            this.PanelDeImagenes.Size = new System.Drawing.Size(383, 143);
            this.PanelDeImagenes.TabIndex = 7;
            // 
            // MostrarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 426);
            this.Controls.Add(this.PanelDeImagenes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProductoIdRecibido);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SuplidoresGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MostrarDatos";
            this.Text = "MostrarDatos";
            this.Load += new System.EventHandler(this.MostrarDatos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SuplidoresGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView SuplidoresGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ProductoIdRecibido;
        private System.Windows.Forms.Panel PanelDeImagenes;
    }
}
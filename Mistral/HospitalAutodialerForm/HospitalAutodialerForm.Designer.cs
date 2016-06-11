namespace HospitalAutodialerForm
{
    partial class HospitalAutodialerForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HospitalAutodialerForm));
            this.dataGridViewCampaña = new System.Windows.Forms.DataGridView();
            this.botonNuevaCampaña = new System.Windows.Forms.Button();
            this.botonCargarCampaña = new System.Windows.Forms.Button();
            this.labelBuscar = new System.Windows.Forms.Label();
            this.textBoxBuscarCampaña = new System.Windows.Forms.TextBox();
            this.toolTipBuscar = new System.Windows.Forms.ToolTip(this.components);
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.botonBuscarUltima = new System.Windows.Forms.Button();
            this.botonIniciar = new System.Windows.Forms.Button();
            this.botonCargarListin = new System.Windows.Forms.Button();
            this.botonCargarResultados = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCampaña)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCampaña
            // 
            this.dataGridViewCampaña.BackgroundColor = System.Drawing.Color.Beige;
            this.dataGridViewCampaña.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCampaña.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewCampaña.Name = "dataGridViewCampaña";
            this.dataGridViewCampaña.Size = new System.Drawing.Size(490, 150);
            this.dataGridViewCampaña.TabIndex = 0;
            // 
            // botonNuevaCampaña
            // 
            this.botonNuevaCampaña.BackColor = System.Drawing.Color.Honeydew;
            this.botonNuevaCampaña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonNuevaCampaña.Location = new System.Drawing.Point(112, 171);
            this.botonNuevaCampaña.Name = "botonNuevaCampaña";
            this.botonNuevaCampaña.Size = new System.Drawing.Size(132, 33);
            this.botonNuevaCampaña.TabIndex = 1;
            this.botonNuevaCampaña.Text = "Nueva campaña";
            this.botonNuevaCampaña.UseVisualStyleBackColor = false;
            this.botonNuevaCampaña.Click += new System.EventHandler(this.botonNuevaCampaña_Click);
            this.botonNuevaCampaña.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.botonNuevaCampaña.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // botonCargarCampaña
            // 
            this.botonCargarCampaña.BackColor = System.Drawing.Color.Honeydew;
            this.botonCargarCampaña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonCargarCampaña.Location = new System.Drawing.Point(250, 171);
            this.botonCargarCampaña.Name = "botonCargarCampaña";
            this.botonCargarCampaña.Size = new System.Drawing.Size(132, 33);
            this.botonCargarCampaña.TabIndex = 2;
            this.botonCargarCampaña.Text = "Cargar campañas";
            this.botonCargarCampaña.UseVisualStyleBackColor = false;
            this.botonCargarCampaña.Click += new System.EventHandler(this.botonCargarCampaña_Click);
            this.botonCargarCampaña.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.botonCargarCampaña.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // labelBuscar
            // 
            this.labelBuscar.AutoSize = true;
            this.labelBuscar.BackColor = System.Drawing.Color.Transparent;
            this.labelBuscar.Location = new System.Drawing.Point(118, 378);
            this.labelBuscar.Name = "labelBuscar";
            this.labelBuscar.Size = new System.Drawing.Size(102, 13);
            this.labelBuscar.TabIndex = 3;
            this.labelBuscar.Text = "Campaña finalizada:";
            // 
            // textBoxBuscarCampaña
            // 
            this.textBoxBuscarCampaña.Location = new System.Drawing.Point(226, 375);
            this.textBoxBuscarCampaña.Name = "textBoxBuscarCampaña";
            this.textBoxBuscarCampaña.Size = new System.Drawing.Size(91, 20);
            this.textBoxBuscarCampaña.TabIndex = 4;
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.BackColor = System.Drawing.Color.Honeydew;
            this.buttonBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBuscar.Location = new System.Drawing.Point(112, 406);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(132, 33);
            this.buttonBuscar.TabIndex = 5;
            this.buttonBuscar.Text = "Buscar por código";
            this.buttonBuscar.UseVisualStyleBackColor = false;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            this.buttonBuscar.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.buttonBuscar.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // botonBuscarUltima
            // 
            this.botonBuscarUltima.BackColor = System.Drawing.Color.Honeydew;
            this.botonBuscarUltima.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonBuscarUltima.Location = new System.Drawing.Point(250, 406);
            this.botonBuscarUltima.Name = "botonBuscarUltima";
            this.botonBuscarUltima.Size = new System.Drawing.Size(132, 33);
            this.botonBuscarUltima.TabIndex = 6;
            this.botonBuscarUltima.Text = "Buscar la última";
            this.botonBuscarUltima.UseVisualStyleBackColor = false;
            this.botonBuscarUltima.Click += new System.EventHandler(this.botonBuscarUltima_Click);
            this.botonBuscarUltima.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.botonBuscarUltima.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // botonIniciar
            // 
            this.botonIniciar.BackColor = System.Drawing.Color.Transparent;
            this.botonIniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonIniciar.FlatAppearance.BorderSize = 0;
            this.botonIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonIniciar.Image = ((System.Drawing.Image)(resources.GetObject("botonIniciar.Image")));
            this.botonIniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonIniciar.Location = new System.Drawing.Point(112, 269);
            this.botonIniciar.Name = "botonIniciar";
            this.botonIniciar.Size = new System.Drawing.Size(59, 56);
            this.botonIniciar.TabIndex = 7;
            this.botonIniciar.UseVisualStyleBackColor = false;
            this.botonIniciar.Click += new System.EventHandler(this.botonIniciar_Click);
            // 
            // botonCargarListin
            // 
            this.botonCargarListin.BackColor = System.Drawing.Color.Honeydew;
            this.botonCargarListin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonCargarListin.Location = new System.Drawing.Point(112, 212);
            this.botonCargarListin.Name = "botonCargarListin";
            this.botonCargarListin.Size = new System.Drawing.Size(132, 33);
            this.botonCargarListin.TabIndex = 8;
            this.botonCargarListin.Text = "Cargar listín";
            this.botonCargarListin.UseVisualStyleBackColor = false;
            this.botonCargarListin.Click += new System.EventHandler(this.botonCargarListin_Click);
            this.botonCargarListin.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.botonCargarListin.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // botonCargarResultados
            // 
            this.botonCargarResultados.BackColor = System.Drawing.Color.Honeydew;
            this.botonCargarResultados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonCargarResultados.Location = new System.Drawing.Point(250, 212);
            this.botonCargarResultados.Name = "botonCargarResultados";
            this.botonCargarResultados.Size = new System.Drawing.Size(132, 33);
            this.botonCargarResultados.TabIndex = 9;
            this.botonCargarResultados.Text = "Cargar resultados";
            this.botonCargarResultados.UseVisualStyleBackColor = false;
            this.botonCargarResultados.Click += new System.EventHandler(this.botonCargarResultados_Click);
            this.botonCargarResultados.MouseEnter += new System.EventHandler(this.boton_MouseEnter);
            this.botonCargarResultados.MouseLeave += new System.EventHandler(this.boton_MouseLeave);
            // 
            // HospitalAutodialerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(514, 452);
            this.Controls.Add(this.botonCargarResultados);
            this.Controls.Add(this.botonCargarListin);
            this.Controls.Add(this.botonIniciar);
            this.Controls.Add(this.botonBuscarUltima);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.textBoxBuscarCampaña);
            this.Controls.Add(this.labelBuscar);
            this.Controls.Add(this.botonCargarCampaña);
            this.Controls.Add(this.botonNuevaCampaña);
            this.Controls.Add(this.dataGridViewCampaña);
            this.Name = "HospitalAutodialerForm";
            this.Text = "Autodialer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HospitalAutodialerForm_FormClosing);
            this.Load += new System.EventHandler(this.HospitalAutodialerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCampaña)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCampaña;
        private System.Windows.Forms.Button botonNuevaCampaña;
        private System.Windows.Forms.Button botonCargarCampaña;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.TextBox textBoxBuscarCampaña;
        private System.Windows.Forms.ToolTip toolTipBuscar;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button botonBuscarUltima;
        private System.Windows.Forms.Button botonIniciar;
        private System.Windows.Forms.Button botonCargarListin;
        private System.Windows.Forms.Button botonCargarResultados;
    }
}


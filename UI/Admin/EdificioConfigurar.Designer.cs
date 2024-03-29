﻿
using System.ComponentModel;
using MetroFramework.Controls;

namespace UI
{
    partial class EdificioConfigurar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.cbTipoEdificio = new MetroFramework.Controls.MetroComboBox();
            this.cbEdificio = new MetroFramework.Controls.MetroComboBox();
            this.lblEdificioTipo = new MetroFramework.Controls.MetroLabel();
            this.lblEdificio = new MetroFramework.Controls.MetroLabel();
            this.btnConfigurar = new MetroFramework.Controls.MetroButton();
            this.btnCerrar = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // cbTipoEdificio
            // 
            this.cbTipoEdificio.FormattingEnabled = true;
            this.cbTipoEdificio.ItemHeight = 23;
            this.cbTipoEdificio.Location = new System.Drawing.Point(158, 90);
            this.cbTipoEdificio.Name = "cbTipoEdificio";
            this.cbTipoEdificio.Size = new System.Drawing.Size(219, 29);
            this.cbTipoEdificio.TabIndex = 0;
            this.cbTipoEdificio.UseSelectable = true;
            this.cbTipoEdificio.SelectedIndexChanged += new System.EventHandler(this.CbTipoEdificio_SelectedIndexChanged);
            // 
            // cbEdificio
            // 
            this.cbEdificio.FormattingEnabled = true;
            this.cbEdificio.ItemHeight = 23;
            this.cbEdificio.Location = new System.Drawing.Point(158, 139);
            this.cbEdificio.Name = "cbEdificio";
            this.cbEdificio.Size = new System.Drawing.Size(219, 29);
            this.cbEdificio.TabIndex = 1;
            this.cbEdificio.UseSelectable = true;
            // 
            // lblEdificioTipo
            // 
            this.lblEdificioTipo.AutoSize = true;
            this.lblEdificioTipo.Location = new System.Drawing.Point(71, 93);
            this.lblEdificioTipo.Name = "lblEdificioTipo";
            this.lblEdificioTipo.Size = new System.Drawing.Size(81, 19);
            this.lblEdificioTipo.TabIndex = 2;
            this.lblEdificioTipo.Tag = "type_building";
            this.lblEdificioTipo.Text = "Tipo Edificio";
            // 
            // lblEdificio
            // 
            this.lblEdificio.AutoSize = true;
            this.lblEdificio.Location = new System.Drawing.Point(101, 144);
            this.lblEdificio.Name = "lblEdificio";
            this.lblEdificio.Size = new System.Drawing.Size(51, 19);
            this.lblEdificio.TabIndex = 3;
            this.lblEdificio.Tag = "building";
            this.lblEdificio.Text = "Edificio";
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.Location = new System.Drawing.Point(301, 175);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.Size = new System.Drawing.Size(75, 23);
            this.btnConfigurar.TabIndex = 4;
            this.btnConfigurar.Tag = "set_up";
            this.btnConfigurar.Text = "Configurar";
            this.btnConfigurar.UseSelectable = true;
            this.btnConfigurar.Click += new System.EventHandler(this.BtnConfigurar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(220, 174);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.Tag = "close";
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseSelectable = true;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // EdificioConfigurar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 229);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnConfigurar);
            this.Controls.Add(this.lblEdificio);
            this.Controls.Add(this.lblEdificioTipo);
            this.Controls.Add(this.cbEdificio);
            this.Controls.Add(this.cbTipoEdificio);
            this.Name = "EdificioConfigurar";
            this.Tag = "configure_building";
            this.Text = "Configurar Edificio";
            this.Load += new System.EventHandler(this.EdificioConfigurar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroComboBox cbTipoEdificio;
        private MetroComboBox cbEdificio;
        private MetroLabel lblEdificioTipo;
        private MetroLabel lblEdificio;
        private MetroButton btnConfigurar;
        private MetroButton btnCerrar;
    }
}
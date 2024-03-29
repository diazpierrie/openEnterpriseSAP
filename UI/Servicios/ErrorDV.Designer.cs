﻿
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace UI
{
    partial class ErrorDv
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
            this.btnBitacora = new MetroFramework.Controls.MetroButton();
            this.btnRestore = new MetroFramework.Controls.MetroButton();
            this.lblResponse = new MetroFramework.Controls.MetroLabel();
            this.listErrors = new System.Windows.Forms.ListBox();
            this.btnRestDV = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btnBitacora
            // 
            this.btnBitacora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBitacora.Location = new System.Drawing.Point(307, 63);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(198, 23);
            this.btnBitacora.TabIndex = 1;
            this.btnBitacora.Text = "Ver bitacora";
            this.btnBitacora.UseSelectable = true;
            this.btnBitacora.Click += new System.EventHandler(this.BtnBitacora_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(307, 92);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(198, 23);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restaurar base de datos";
            this.btnRestore.UseSelectable = true;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(488, 94);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 0);
            this.lblResponse.TabIndex = 3;
            // 
            // listErrors
            // 
            this.listErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listErrors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.listErrors.FormattingEnabled = true;
            this.listErrors.Location = new System.Drawing.Point(23, 63);
            this.listErrors.Name = "listErrors";
            this.listErrors.Size = new System.Drawing.Size(243, 82);
            this.listErrors.TabIndex = 4;
            // 
            // btnRestDV
            // 
            this.btnRestDV.Location = new System.Drawing.Point(307, 120);
            this.btnRestDV.Margin = new System.Windows.Forms.Padding(2);
            this.btnRestDV.Name = "btnRestDV";
            this.btnRestDV.Size = new System.Drawing.Size(199, 23);
            this.btnRestDV.TabIndex = 5;
            this.btnRestDV.Tag = "rest_dv";
            this.btnRestDV.Text = "Restaurar DV";
            this.btnRestDV.UseSelectable = true;
            this.btnRestDV.Click += new System.EventHandler(this.BtnRestDV_Click);
            // 
            // ErrorDv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 177);
            this.Controls.Add(this.btnRestDV);
            this.Controls.Add(this.listErrors);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBitacora);
            this.Name = "ErrorDv";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Text = "Error DV";
            this.Load += new System.EventHandler(this.ErrorDV_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private MetroButton btnBitacora;
		private MetroButton btnRestore;
		private MetroLabel lblResponse;
		private ListBox listErrors;
		private MetroButton btnRestDV;
	}
}
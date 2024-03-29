﻿
using System.ComponentModel;
using MetroFramework.Controls;

namespace UI
{
    partial class ProductoHome
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flpProductoHome = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCrearProducto = new MetroFramework.Controls.MetroButton();
            this.btnModificarProducto = new MetroFramework.Controls.MetroButton();
            this.btnBorrarProducto = new MetroFramework.Controls.MetroButton();
            this.gridProducto = new MetroFramework.Controls.MetroGrid();
            this.flpProductoHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // flpProductoHome
            // 
            this.flpProductoHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpProductoHome.Controls.Add(this.btnCrearProducto);
            this.flpProductoHome.Controls.Add(this.btnModificarProducto);
            this.flpProductoHome.Controls.Add(this.btnBorrarProducto);
            this.flpProductoHome.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProductoHome.Location = new System.Drawing.Point(759, 63);
            this.flpProductoHome.Name = "flpProductoHome";
            this.flpProductoHome.Size = new System.Drawing.Size(145, 153);
            this.flpProductoHome.TabIndex = 6;
            // 
            // btnCrearProducto
            // 
            this.btnCrearProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrearProducto.Location = new System.Drawing.Point(3, 3);
            this.btnCrearProducto.Name = "btnCrearProducto";
            this.btnCrearProducto.Size = new System.Drawing.Size(136, 41);
            this.btnCrearProducto.TabIndex = 0;
            this.btnCrearProducto.Tag = "";
            this.btnCrearProducto.Text = "Crear Producto";
            this.btnCrearProducto.UseSelectable = true;
            this.btnCrearProducto.Click += new System.EventHandler(this.BtnCrearProducto_Click);
            // 
            // btnModificarProducto
            // 
            this.btnModificarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificarProducto.Location = new System.Drawing.Point(3, 50);
            this.btnModificarProducto.Name = "btnModificarProducto";
            this.btnModificarProducto.Size = new System.Drawing.Size(136, 41);
            this.btnModificarProducto.TabIndex = 1;
            this.btnModificarProducto.Tag = "";
            this.btnModificarProducto.Text = "Modificar Producto";
            this.btnModificarProducto.UseSelectable = true;
            this.btnModificarProducto.Click += new System.EventHandler(this.BtnModificarProducto_Click);
            // 
            // btnBorrarProducto
            // 
            this.btnBorrarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarProducto.Location = new System.Drawing.Point(3, 97);
            this.btnBorrarProducto.Name = "btnBorrarProducto";
            this.btnBorrarProducto.Size = new System.Drawing.Size(136, 41);
            this.btnBorrarProducto.TabIndex = 2;
            this.btnBorrarProducto.Tag = "";
            this.btnBorrarProducto.Text = "Borrar Producto";
            this.btnBorrarProducto.UseSelectable = true;
            this.btnBorrarProducto.Click += new System.EventHandler(this.BtnBorrarProducto_Click);
            // 
            // gridProducto
            // 
            this.gridProducto.AllowUserToAddRows = false;
            this.gridProducto.AllowUserToDeleteRows = false;
            this.gridProducto.AllowUserToResizeRows = false;
            this.gridProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridProducto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridProducto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridProducto.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProducto.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridProducto.EnableHeadersVisualStyles = false;
            this.gridProducto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridProducto.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridProducto.Location = new System.Drawing.Point(23, 63);
            this.gridProducto.MultiSelect = false;
            this.gridProducto.Name = "gridProducto";
            this.gridProducto.ReadOnly = true;
            this.gridProducto.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProducto.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridProducto.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProducto.Size = new System.Drawing.Size(730, 436);
            this.gridProducto.TabIndex = 3;
            this.gridProducto.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridProducto_CellDoubleClick);
            // 
            // ProductoHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 522);
            this.Controls.Add(this.flpProductoHome);
            this.Controls.Add(this.gridProducto);
            this.Name = "ProductoHome";
            this.Tag = "";
            this.Text = "ProductoHome";
            this.Load += new System.EventHandler(this.ProductoHome_Load);
            this.flpProductoHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProducto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroGrid gridProducto;
        private MetroButton btnCrearProducto;
        private MetroButton btnModificarProducto;
        private MetroButton btnBorrarProducto;
        private System.Windows.Forms.FlowLayoutPanel flpProductoHome;
    }
}
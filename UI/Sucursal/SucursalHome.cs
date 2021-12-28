﻿using System;
using System.Windows.Forms;
using BLL;

// ReSharper disable PossibleNullReferenceException

namespace UI.Sucursal
{
    public partial class SucursalHome : UpdatableForm
    {
        public SucursalHome()
        {
            InitializeComponent();
        }

        private void btnCrearSucursal_Click(object sender, EventArgs e)
        {
            var sucursalAltaModificacion = new SucursalAltaModificacion(this);
            sucursalAltaModificacion.Show();
        }

        private void btnModificarSucursal_Click(object sender, EventArgs e)
        {
            if (gridSucursal.SelectedRows.Count == 0)
            {
                return;
            }

            var selectedItem = int.Parse(gridSucursal.SelectedRows[0].Cells["id"].Value.ToString());
            var sucursal = SucursalBll.Obtener(selectedItem);
            var provAm = new SucursalAltaModificacion(this, sucursal);
            provAm.Show();
        }

        private void btnBorrarSucursal_Click(object sender, EventArgs e)
        {
            if (gridSucursal.SelectedRows.Count == 0)
            {
                return;
            }

            var selectedItem = int.Parse(gridSucursal.SelectedRows[0].Cells["id"].Value.ToString());
            var sucursal = SucursalBll.Obtener(selectedItem);
            SucursalBll.Eliminar(sucursal);
            ActualizarGrid();
        }

        private void SucursalHome_Load(object sender, EventArgs e)
        {
            ActualizarGrid();
        }

        public void ActualizarGrid()
        {
            gridSucursal.DataSource = SucursalBll.ObtenerActivos();
            gridSucursal.Columns["id"].Visible = false;

            gridSucursal.Columns["nombre"].DisplayIndex = 0;
            gridSucursal.Columns["direccion"].DisplayIndex = 1;
            gridSucursal.Columns["mail"].DisplayIndex = 2;
            gridSucursal.Columns["codigoPostal"].DisplayIndex = 3;
            gridSucursal.Columns["telefono"].DisplayIndex = 4;
        }

        private void gridSucursal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridSucursal.SelectedRows.Count == 0)
            {
                return;
            }

            var selectedItem = int.Parse(gridSucursal.SelectedRows[0].Cells["id"].Value.ToString());
            var sucursal = SucursalBll.Obtener(selectedItem);
            var provAm = new SucursalAltaModificacion(this, sucursal);
            provAm.Show();
        }

    }

}
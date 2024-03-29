﻿using BLL;
using EE;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

// ReSharper disable PossibleNullReferenceException

namespace UI
{
    public partial class PagoRecibir : UpdatableForm
    {
        public readonly Mdi Mdi;
        private List<VentaEe> _dataTable;

        public PagoRecibir(Mdi mdi)
        {
            Mdi = mdi;
            InitializeComponent();

            var estados = VentaEstadoBll.Obtener();
            estados.Add(new EstadoEe { Id = 4, Nombre = "Todos" });

            ActualizarGrid();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == null && txtCliente.Text == null) return;

            gridVentas.DataSource = _dataTable.FindAll(x => x.Empleado.NombreCompleto.IndexOf(txtUsuario.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                                              && x.Comprador.NombreCompleto.IndexOf(txtCliente.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            gridVentas.Refresh();
        }

        public void ActualizarGrid()
        {
            _dataTable = VentaBll.ObtenerPendienteDePago(Sesion.ObtenerSesion().Sucursal);
            gridVentas.DataSource = _dataTable;

            gridVentas.Columns["id"].Visible = false;
            gridVentas.Columns["sucursal"].Visible = false;
            gridVentas.Columns["fecha"].Visible = false;

            gridVentas.Columns["empleado"].DisplayIndex = 0;
            gridVentas.Columns["comprador"].DisplayIndex = 1;
            gridVentas.Columns["metodoPago"].DisplayIndex = 2;
            gridVentas.Columns["estado"].DisplayIndex = 3;
            gridVentas.Columns["total"].DisplayIndex = 4;

            var format = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            format.CurrencySymbol = "$";
            gridVentas.Columns["total"].DefaultCellStyle.FormatProvider = format;
            gridVentas.Columns["total"].DefaultCellStyle.Format = "c";

            gridVentas.Refresh();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GridClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var venta = (VentaEe)gridVentas.SelectedRows[0].DataBoundItem;
            var detalles = VentaBll.ObtenerDetalles(venta);

            if (detalles.Count == 0)
            {
                return;
            }

            Mdi.OpenWindowForm(new VentaVerDetalle(venta, detalles));
        }

        private void BtnElegirVenta_Click(object sender, EventArgs e)
        {
            var venta = (VentaEe)gridVentas.SelectedRows[0].DataBoundItem;
            var confirmarPago = new ConfirmarPago(this, venta);
            Mdi.OpenWindowForm(confirmarPago);
        }
    }
}
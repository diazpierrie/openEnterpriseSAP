﻿using BLL;
using EE;
using Security;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable PossibleNullReferenceException

namespace UI
{
    public partial class VentaProductoAgregar : UpdatableForm
    {
        private readonly VentaHome _ventaHome;

        public VentaProductoAgregar(VentaHome homeForm)
        {
            InitializeComponent();

            AllControls = Program.GetAllControls(this);
            AllControls.Add(lblProductos);
            AllControls.Add(lblProductosAAgregar);
            Sesion.ObtenerSesion().Idioma.Forms.Add(this);

            IdiomaManager.Cambiar(Sesion.ObtenerSesion().Idioma, Sesion.ObtenerSesion().Idioma.Id, this);

            _ventaHome = homeForm;
            cbDepositos.DataSource = DepositoBll.ObtenerActivos();
            rbSucursal.Checked = true;
        }

        private void ActualizarGrids()
        {
            if (rbSucursal.Checked)
            {
                gridProductos.DataSource = _ventaHome.ProductosSucursal;
            }
            else
            {
                var depositosSelectedItem = (DepositoEe)cbDepositos.SelectedItem;
                gridProductos.DataSource = _ventaHome.ProductosDeposito.Where(x => x.Edificio.Id == depositosSelectedItem.Id).ToList();
            }

            gridProductos.Columns["id"].Visible = false;
            gridProductos.Columns["codigo"].Visible = false;
            gridProductos.Columns["activo"].Visible = false;
            gridProductos.Columns["fechaCreacion"].Visible = false;
            gridProductos.Columns["costo"].Visible = false;
            gridProductos.Columns["TotalProducto"].Visible = false;

            gridProductos.Columns["nombre"].DisplayIndex = 0;
            gridProductos.Columns["Edificio"].DisplayIndex = 1;
            gridProductos.Columns["precio"].DisplayIndex = 2;
            gridProductos.Columns["cantidad"].DisplayIndex = 3;
            gridProductos.Columns["CantidadAComprar"].DisplayIndex = 4;

            gridProductos.Columns["nombre"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["name"];
            gridProductos.Columns["Edificio"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["building"];
            gridProductos.Columns["precio"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["price"];
            gridProductos.Columns["cantidad"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["stock"];
            gridProductos.Columns["CantidadAComprar"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["amount_buy"];

            gridProductos.Columns["nombre"].ReadOnly = true;
            gridProductos.Columns["precio"].ReadOnly = true;
            gridProductos.Columns["Edificio"].ReadOnly = true;
            gridProductos.Columns["cantidad"].ReadOnly = true;
            gridProductos.Columns["CantidadAComprar"].ReadOnly = false;

            gridProductosAAgregar.DataSource = _ventaHome.ProductosAAsignar;
            gridProductosAAgregar.Columns["id"].Visible = false;
            gridProductosAAgregar.Columns["activo"].Visible = false;
            gridProductosAAgregar.Columns["codigo"].Visible = false;
            gridProductosAAgregar.Columns["fechaCreacion"].Visible = false;
            gridProductosAAgregar.Columns["cantidad"].Visible = false;
            gridProductosAAgregar.Columns["costo"].Visible = false;
            gridProductosAAgregar.Columns["cantidadaretirar"].Visible = false;

            gridProductosAAgregar.Columns["nombre"].DisplayIndex = 0;
            gridProductosAAgregar.Columns["Edificio"].DisplayIndex = 1;
            gridProductosAAgregar.Columns["precio"].DisplayIndex = 2;
            gridProductosAAgregar.Columns["CantidadAComprar"].DisplayIndex = 3;
            gridProductosAAgregar.Columns["TotalProducto"].DisplayIndex = 4;

            gridProductosAAgregar.Columns["nombre"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["name"];
            gridProductosAAgregar.Columns["Edificio"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["building"];
            gridProductosAAgregar.Columns["precio"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["price"];
            gridProductosAAgregar.Columns["CantidadAComprar"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["amount_buy"];
            gridProductosAAgregar.Columns["TotalProducto"].HeaderText = Sesion.ObtenerSesion().Idioma.Textos["total_product"];

            var format = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            format.CurrencySymbol = "$";

            gridProductosAAgregar.Columns["TotalProducto"].DefaultCellStyle.FormatProvider = format;
            gridProductosAAgregar.Columns["TotalProducto"].DefaultCellStyle.Format = "c";
            gridProductosAAgregar.Columns["precio"].DefaultCellStyle.FormatProvider = format;
            gridProductosAAgregar.Columns["precio"].DefaultCellStyle.Format = "c";

            gridProductos.Columns["precio"].DefaultCellStyle.FormatProvider = format;
            gridProductos.Columns["precio"].DefaultCellStyle.Format = "c";
        }

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (gridProductos.SelectedRows.Count == 0) return;

            for (var index = 0; index < gridProductos.SelectedRows.Count; index++)
            {
                var selectedRow = gridProductos.SelectedRows[index];
                var producto = (ProductoEdificioEe)selectedRow.DataBoundItem;
                var productoNuevo = (ProductoEdificioEe)producto.Clone();

                if ((int)selectedRow.Cells["CantidadAComprar"].Value == 0 || selectedRow.Cells["CantidadAComprar"].Value == null)
                {
                    productoNuevo.CantidadAComprar = 1;
                }
                else
                {
                    productoNuevo.CantidadAComprar = (int)selectedRow.Cells["CantidadAComprar"].Value;
                }

                productoNuevo.TotalProducto = productoNuevo.CantidadAComprar * productoNuevo.Precio;

                var productoAgregado = _ventaHome.ProductosAAsignar.FirstOrDefault(x => x.Id.Equals(productoNuevo.Id) && Equals(x.Edificio, productoNuevo.Edificio));
                if (productoAgregado != null)
                {
                    productoAgregado.CantidadAComprar += productoNuevo.CantidadAComprar;
                    productoAgregado.TotalProducto = productoAgregado.CantidadAComprar * productoNuevo.Precio;
                }
                else
                {
                    _ventaHome.ProductosAAsignar.Add(productoNuevo);
                }

                producto.Cantidad -= productoNuevo.CantidadAComprar;
                if (producto.Cantidad == 0)
                {
                    if (rbDeposito.Checked)
                    {
                        _ventaHome.ProductosDeposito.Remove((ProductoEdificioEe)selectedRow.DataBoundItem);
                    }
                    else
                    {
                        _ventaHome.ProductosSucursal.Remove((ProductoEdificioEe)selectedRow.DataBoundItem);
                    }

                    ActualizarGrids();
                }
                gridProductos.Refresh();
                gridProductos.RefreshEdit();
                gridProductosAAgregar.Refresh();
                gridProductosAAgregar.RefreshEdit();
            }
        }

        private void BtnAsignarProductos_Click(object sender, EventArgs e)
        {
            _ventaHome.ActualizarGrid();
            Close();
        }

        private void BtnRemoverProducto_Click(object sender, EventArgs e)
        {
            if (gridProductosAAgregar.SelectedRows.Count == 0) return;

            for (var index = 0; index < gridProductosAAgregar.SelectedRows.Count; index++)
            {
                var selectedRow = gridProductosAAgregar.SelectedRows[index];
                var producto = (ProductoEdificioEe)selectedRow.DataBoundItem;
                var productoNuevo = (ProductoEdificioEe)producto.Clone();

                productoNuevo.Cantidad = producto.CantidadAComprar;
                productoNuevo.CantidadAComprar = 0;

                if (productoNuevo.Edificio.GetType().Name == "SucursalEe")
                {
                    if (_ventaHome.ProductosSucursal.Any(x => x.Id == productoNuevo.Id))
                    {
                        _ventaHome.ProductosSucursal.FirstOrDefault(x => x.Id == productoNuevo.Id).Cantidad += productoNuevo.Cantidad;
                    }
                    else
                    {
                        _ventaHome.ProductosSucursal.Add(productoNuevo);
                    }
                }
                else
                {
                    if (_ventaHome.ProductosDeposito.Any(x => x.Id == productoNuevo.Id))
                    {
                        _ventaHome.ProductosDeposito.FirstOrDefault(x => x.Id == productoNuevo.Id).Cantidad += productoNuevo.Cantidad;
                    }
                    else
                    {
                        _ventaHome.ProductosDeposito.Add(productoNuevo);
                    }
                }
                _ventaHome.ProductosAAsignar.Remove(producto);
                ActualizarGrids();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (_ventaHome.ProductosAAsignar.Count == 0) Close();

            foreach (var producto in _ventaHome.ProductosAAsignar)
            {
                var productoNuevo = (ProductoEdificioEe)producto.Clone();

                productoNuevo.Cantidad = producto.CantidadAComprar;
                productoNuevo.CantidadAComprar = 0;

                if (productoNuevo.Edificio.GetType().Name == "SucursalEe")
                {
                    if (_ventaHome.ProductosSucursal.Any(x => x.Id == productoNuevo.Id))
                    {
                        _ventaHome.ProductosSucursal.FirstOrDefault(x => x.Id == productoNuevo.Id).Cantidad += productoNuevo.Cantidad;
                    }
                    else
                    {
                        _ventaHome.ProductosSucursal.Add(productoNuevo);
                    }
                }
                else
                {
                    if (_ventaHome.ProductosDeposito.Any(x => x.Id == productoNuevo.Id))
                    {
                        _ventaHome.ProductosDeposito.FirstOrDefault(x => x.Id == productoNuevo.Id).Cantidad += productoNuevo.Cantidad;
                    }
                    else
                    {
                        _ventaHome.ProductosDeposito.Add(productoNuevo);
                    }
                }
            }
            _ventaHome.ProductosAAsignar.Clear();
            ActualizarGrids();
            Close();
        }

        private void CbDepositos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarGrids();
        }

        private void GridProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = (ProductoEe)gridProductos.Rows[e.RowIndex].DataBoundItem;

            if (gridProductos.CurrentCell.Value == null) return;

            if ((int)gridProductos.CurrentCell.Value > row.Cantidad ||
                (int)gridProductos.CurrentCell.Value <= 0)
            {
                gridProductos.CurrentCell.Value = 0;
            }
        }

        private void GridProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gridProductos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gridProductos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            gridProductos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ProductoAgregar_Load(object sender, EventArgs e)
        {
            AllControls = Program.GetAllControls(this);

            ActualizarGrids();

            IdiomaManager.Cambiar(Sesion.ObtenerSesion().Idioma, Sesion.ObtenerSesion().Idioma.Id, this);
        }

        private void RbSucursal_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarGrids();

            cbDepositos.Visible = !rbSucursal.Checked;
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtBuscar.Text != null)
            {
                gridProductos.DataSource = ProductoBll.ObtenerPorNombre();
            }
        }
    }
}
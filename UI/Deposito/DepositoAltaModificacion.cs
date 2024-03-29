﻿using BLL;
using EE;
using Security;
using System;

namespace UI
{
    public partial class DepositoAltaModificacion : UpdatableForm
    {
        private readonly DepositoEe _deposito = new DepositoEe();
        private readonly DepositoHome _homeForm;
        private readonly bool _crear;

        public DepositoAltaModificacion(DepositoHome homeForm)
        {
            _homeForm = homeForm;

            _crear = true;
            InitializeComponent();
            Sesion.ObtenerSesion().Idioma.Forms.Add(this);

            btnAccion.Text = Sesion.ObtenerSesion().Idioma.Textos["create"];
            CambiarTitulo(Sesion.ObtenerSesion().Idioma.Textos["create"] + " " + Sesion.ObtenerSesion().Idioma.Textos["deposit"]);
        }

        public DepositoAltaModificacion(DepositoHome homeForm, DepositoEe deposito)
        {
            _homeForm = homeForm;
            _deposito = deposito;

            _crear = false;
            InitializeComponent();
            Sesion.ObtenerSesion().Idioma.Forms.Add(this);

            btnAccion.Text = Sesion.ObtenerSesion().Idioma.Textos["modify"];
            CambiarTitulo(Sesion.ObtenerSesion().Idioma.Textos["modify"] + " " + Sesion.ObtenerSesion().Idioma.Textos["deposit"]);
        }

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            if (_crear)
            {
                var deposito = new DepositoEe
                {
                    Nombre = txtNombre.Text,
                    Mail = txtMail.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text,
                    CodigoPostal = int.Parse(txtCodigoPostal.Text),
                };
                DepositoBll.Crear(deposito);
            }
            else
            {
                _deposito.Nombre = txtNombre.Text;
                _deposito.Mail = txtMail.Text;
                _deposito.Telefono = txtTelefono.Text;
                _deposito.Direccion = txtDireccion.Text;
                _deposito.CodigoPostal = int.Parse(txtCodigoPostal.Text);

                DepositoBll.Actualizar(_deposito);
            }

            Close();
            _homeForm.ActualizarGrid();
        }

        private void EmpleadoAltaModificacion_Load(object sender, EventArgs e)
        {
            AllControls.Add(lblDireccion);
            AllControls.Add(lblMail);
            AllControls.Add(lblNombre);
            AllControls.Add(lblCodigoPostal);
            AllControls.Add(lblDireccion);

            IdiomaManager.Cambiar(Sesion.ObtenerSesion().Idioma, Sesion.ObtenerSesion().Idioma.Id, this);

            CargarDetalleDeposito();
        }

        private void CargarDetalleDeposito()
        {
            if (_deposito.Id == 0) { return; }
            txtNombre.Text = _deposito.Nombre;
            txtMail.Text = _deposito.Mail;
            txtTelefono.Text = _deposito.Telefono;
            txtDireccion.Text = _deposito.Direccion;
            txtCodigoPostal.Text = _deposito.CodigoPostal.ToString();
        }

        private void CambiarTitulo(string titulo)
        {
            Text = titulo;
        }
    }
}
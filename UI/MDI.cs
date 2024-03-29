﻿using EE;
using MetroFramework;
using MetroFramework.Forms;
using Security;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.Familia;
using UI.Properties;
using UI.Sucursal;

namespace UI
{
    public partial class Mdi : UpdatableForm
    {
        private Form _activeForm;
        private bool _langLoaded;

        public Mdi()
        {
            InitializeComponent();

            var home = new Logo();
            panelContainer.Controls.Add(home);
            home.Show();
            HideSubMenu();
            CargarRoles();
            Sesion.ObtenerSesion().Idioma.Forms.Add(this);

            AllControls = Program.GetAllControls(this);
            AllControls.Add(lblEdificio);

            CargarEdificioLabel();
        }

        public void CargarEdificioLabel()
        {
            var depositoEe = Sesion.ObtenerSesion().Deposito;
            var sucursalEe = Sesion.ObtenerSesion().Sucursal;

            lblEdificio.Text = (string)Settings.Default["TipoEdificio"] == "Sucursal" ? sucursalEe.ToString() : depositoEe.ToString();
        }

        public void CargarIdiomas()
        {
            var idiomas = IdiomaManager.Obtener().ToDictionary(id => id.Id, id => id.Nombre);
            cbIdiomas.DisplayMember = "Value";
            cbIdiomas.ValueMember = "Key";
            cbIdiomas.DataSource = new BindingSource(idiomas, null);
            cbIdiomas.SelectedValue = Sesion.ObtenerSesion().Idioma.Id;
        }

        public void CargarRoles()
        {
            var edificio = Settings.Default.TipoEdificio;

            var usuarioLoggeado = Sesion.ObtenerSesion().Usuario;

            if (RolManager.ObtenerFamilia() == null)
            {
                MetroMessageBox.Show(this, Sesion.ObtenerSesion().Idioma.Textos["error_contact_admin"], Sesion.ObtenerSesion().Idioma.Textos["notification"]);
                Close();
                return;
            }

            if (!RolManager.VerificarPatente(usuarioLoggeado, "Admin"))
            {
                btnAdminConfig.Visible = false;
                btnAdminGestion.Visible = false;
            }
            else
            {
                btnAdminConfig.Visible = true;
                btnAdminGestion.Visible = true;
            }

            btnSucursalPanel.Visible =
                RolManager.VerificarPatente(usuarioLoggeado, "Sucursal") && edificio == "Sucursal";

            btnCajaPanel.Visible = RolManager.VerificarPatente(usuarioLoggeado, "Caja") && edificio == "Sucursal";

            btnDepositoPanel.Visible =
                RolManager.VerificarPatente(usuarioLoggeado, "Deposito") && edificio == "Deposito";

            btnEnviosPanel.Visible = RolManager.VerificarPatente(usuarioLoggeado, "EnviosRecepciones");

            HideSubMenu();
        }

        public void OpenWindowForm(UpdatableForm childForm)
        {
            childForm.TopLevel = false;
            childForm.Movable = false;
            childForm.Resizable = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.MaximizeBox = true;
            childForm.MinimizeBox = false;
            childForm.MaximizeBox = false;
            childForm.Style = MetroColorStyle.White;
            childForm.ShadowType = MetroFormShadowType.None;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void BtnAdminConfig_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelAdminConfig);
        }

        private void BtnAdminGestion_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelAdminGestion);
        }

        private void BtnAyuda_Click(object sender, EventArgs e)
        {
            var home = new Ayuda();
            OpenChildForm(home);
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new RealizarBackup();
            OpenChildForm(home);
        }

        private void BtnBitacora_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new BitacoraVer();
            OpenChildForm(home);
        }

        private void BtnBuscarVenta_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new VentaBuscar(this);
            OpenChildForm(home);
        }

        private void BtnCajaPanel_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelCaja);
        }

        private void BtnCancelarVenta_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new VentaCancelar(this);
            OpenChildForm(home);
        }

        private void BtnConfigurarEdificio_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new EdificioConfigurar(this);
            OpenChildForm(home);
        }

        private void BtnCrearQueja_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new QuejaCrear(this);
            OpenChildForm(home);
        }

        private void BtnDeposito_Click(object sender, EventArgs e)
        {
            var depositoHome = new DepositoHome(this);
            OpenChildForm(depositoHome);
        }

        private void BtnDepositoPanel_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelDeposito);
        }

        private void BtnDespacharEnvio_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var home = new EnvioGestion(this);
            OpenChildForm(home);
        }

        private void BtnEmpleado_Click(object sender, EventArgs e)
        {
            var empleadoHome = new EmpleadoHome(this);
            OpenChildForm(empleadoHome);
        }

        private void BtnEnviosPanel_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelEnvios);
        }

        private void BtnExportReport_Click(object sender, EventArgs e)
        {
            var exportData = new SaveFileDialog
            {
                Filter = "PDF(*.pdf)|*.pdf",
                Title = "Save PDF file",
                InitialDirectory = Directory.GetCurrentDirectory() + "\\..\\..\\..\\",
            };

            if (exportData.ShowDialog() == DialogResult.OK)
            {
                Process.Start(UsuarioManager.ExportPdf(exportData.FileName));
            }
        }

        private void BtnFamilia_Click(object sender, EventArgs e)
        {
            var gestionarFamilia = new GestionarFamilia(this);
            OpenChildForm(gestionarFamilia);
        }

        private void BtnIdioma_Click(object sender, EventArgs e)
        {
            RemoveUserControl();
            var gestionarIdioma = new IdiomaGestionar(this);
            OpenChildForm(gestionarIdioma);
        }

        private void BtnPenalizarProveedor_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var proveedorHome = new ProveedorHome(this, Sesion.ObtenerSesion().Idioma.Textos["penalize_vendor"]);
            OpenChildForm(proveedorHome);
        }

        private void BtnProducto_Click(object sender, EventArgs e)
        {
            var productoHome = new ProductoHome(this);
            OpenChildForm(productoHome);
        }

        private void BtnProveedor_Click(object sender, EventArgs e)
        {
            var proveedorHome = new ProveedorHome(this);
            OpenChildForm(proveedorHome);
        }

        private void BtnRealizarPedidoDeposito_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var sucursalPedidoHome = new SucursalPedidoHome(this);
            OpenChildForm(sucursalPedidoHome);
        }

        private void BtnRealizarPedidoProveedor_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var realizarPedido = new DepositoPedidoHome(this);
            OpenChildForm(realizarPedido);
        }

        private void BtnRealizarVenta_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var ventaHome = new VentaHome(this);
            OpenChildForm(ventaHome);
        }

        private void BtnRecepcionarPedidoDeposito_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var sucursalEntradaRegistrar = new SucursalEntradaRegistrar(this);
            OpenChildForm(sucursalEntradaRegistrar);
        }

        private void BtnRecibirPago_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var recibirPago = new PagoRecibir(this);
            OpenChildForm(recibirPago);
        }

        private void BtnRegistrarEntrada_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var entradaRegistrar = new DepositoEntradaRegistrar(this);
            OpenChildForm(entradaRegistrar);
        }

        private void BtnRestaurarDv_Click(object sender, EventArgs e)
        {
            Dv.ActualizarDv();
            MetroMessageBox.Show(this, Sesion.ObtenerSesion().Idioma.Textos["dv_restored"], Sesion.ObtenerSesion().Idioma.Textos["notification"], MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void BtnSucursal_Click(object sender, EventArgs e)
        {
            var sucursalHome = new SucursalHome(this);
            OpenChildForm(sucursalHome);
        }

        private void BtnSucursalPanel_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSucursal);
        }

        private void BtnVerEnvios_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var buscarEnvio = new EnvioBuscar(this);
            OpenChildForm(buscarEnvio);
        }

        private void BtnVerInventario_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var verInventario = new DepositoVerInventario();
            OpenChildForm(verInventario);
        }

        private void BtnVerQuejas_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            var verInventario = new DepositoVerInventario();
            OpenChildForm(verInventario);
        }

        private void CbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_langLoaded) return;
            IdiomaManager.Cambiar(Sesion.ObtenerSesion().Idioma, int.Parse(cbIdiomas.SelectedValue.ToString()));
            CargarEdificioLabel();
        }

        private void HideSubMenu()
        {
            panelCaja.Visible = false;
            panelDeposito.Visible = false;
            panelEnvios.Visible = false;
            panelSucursal.Visible = false;
            panelAdminConfig.Visible = false;
            panelAdminGestion.Visible = false;
        }

        private void Mdi_Load(object sender, EventArgs e)
        {
            PrepararIdiomas();
        }

        private void OpenChildForm(UpdatableForm childForm)
        {
            _activeForm?.Close();
            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.Movable = false;
            childForm.Resizable = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.ControlBox = false;
            childForm.Style = MetroColorStyle.White;
            childForm.ShadowType = MetroFormShadowType.None;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void PbLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PicLogo_Click(object sender, EventArgs e)
        {
            RemoveUserControl();
            var home = new Logo();
            panelContainer.Controls.Add(home);
            home.Show();
        }

        private void PrepararIdiomas()
        {
            AllControls = Program.GetAllControls(this);
            AllControls.Add(lblIdioma);

            CargarIdiomas();

            IdiomaManager.Cambiar(Sesion.ObtenerSesion().Idioma, Sesion.ObtenerSesion().Idioma.Id, this);

            _langLoaded = true;
        }

        private void RemoveUserControl()
        {
            if (panelContainer.Controls.Count > 0) panelContainer.Controls.RemoveAt(panelContainer.Controls.Count - 1);
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
    }
}
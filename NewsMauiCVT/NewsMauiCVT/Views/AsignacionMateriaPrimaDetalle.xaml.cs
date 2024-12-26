using DevExpress.Maui.Core.Internal;
using Microsoft.Maui.Devices.Sensors;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrimaDetalle : ContentPage
{
    #region Variables Globales
    int folioRecibido;
    string ItemCode;
    string Lote;
    string RecepcionPallet;
    string RecepcionTransfer;
    string username;
    bool LoteValido;
    bool ItemCodeValido;
    bool PalletValido;
    bool UbicacionAgregada;

    DatosPallets datos;
    DataTable tabla;

    int PackageID;
    int LayoutID;
    int StaffID;

    DatosAsignacionMP dta;
    List<string> LoteAsignado;
    List<string> ItemCodeAsignado;
    DatosProduccion datosProduccion;
    DatosUsuario datosUsuario;
    DatosApp datosApp;
    #endregion

    public AsignacionMateriaPrimaDetalle(int folio)
	{
        InitializeComponent();
        #region Inicializaciones
        folioRecibido = folio;
        datos = new DatosPallets();
        tabla = new DataTable();
        dta = new DatosAsignacionMP();
        LoteAsignado = new List<string>();
        ItemCodeAsignado = new List<string>();
        datosProduccion = new DatosProduccion();
        datosUsuario = new DatosUsuario();
        datosApp = new DatosApp();
        PackageID = 0;
        LayoutID = 0;
        StaffID = 0;
        #endregion
        CreateTable();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText(txtPallet);
        LogUsabilidad("Ingreso a asignacion MP Detalle");
    }
    private void SetFocusText(DevExpress.Maui.Editors.TextEdit textEdit)
    {
        _ = Task.Delay(200).ContinueWith(t => {
            textEdit.Focus();
        });
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
        lblResultado.Text = string.Empty;
        lblResultado.IsVisible = false;
        btn_asignaciones.IsVisible = false;

        LoteAsignado.Clear();
        ItemCodeAsignado.Clear();
    }
    private void CreateTable()
    {
        tabla = new("TablaAsignacionesRealizadas");
        tabla.Columns.Add("Transfer_ID", typeof(string));
        tabla.Columns.Add("ItemCode", typeof(string));
        tabla.Columns.Add("Lote", typeof(string));
        tabla.Columns.Add("RecepcionPallet");
    }
    private void LoadData()
    {
        try
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                //Datos asociados a la transferencia (Lote / ItemCode / RecepcionPallet).
                List<TransferenciaAsignacion> transferAsignacion = dta.TransferenciasAsignadas(folioRecibido);
                //Datos asociados al Pallet (Lote / ItemCode / RecepcionPallet).
                List<string> DatosPallet = datos.ObtieneDatos(txtPallet.Text);

                Lote = DatosPallet[0];
                ItemCode = DatosPallet[1];
                RecepcionPallet = DatosPallet[3];

                //Se valida que campo de BD RecepcionPallet es distinto a Null.
                //Asignando 
                foreach (var transfer in transferAsignacion)
                {
                    if (transfer.ItemCode == ItemCode && transfer.Lote == Lote)
                    {
                        if (!string.IsNullOrEmpty(transfer.Recepcion))
                        {
                            RecepcionTransfer = transfer.Recepcion;
                            break;
                        }
                    }
                }
                //Limpieza de listado:
                LoteAsignado.Clear();
                ItemCodeAsignado.Clear();

                //Se recorre los datos de una lista obtenidos de la BD (ItemCode/Lote/Rececpion).
                for (int i = 0; i < transferAsignacion.Count; i++)
                {
                    LoteAsignado.Add(transferAsignacion[i].Lote);
                    ItemCodeAsignado.Add(transferAsignacion[i].ItemCode);
                }

                //Se asignan las variables de los Lotes y codigos de productos asignados en la transferencia.
                LoteValido = LoteAsignado.Contains(Lote);
                ItemCodeValido = ItemCodeAsignado.Contains(ItemCode);
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine("LoadData: " + ex.Message);
        }
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 0;

        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
    private void txtPallet_Completed(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPallet.Text)) 
        {
            PalletValido = datos.ValidaPallet(txtPallet.Text);

            if (PalletValido)
            {
                txtPalletDestino.IsVisible = true;
                lblResultado.IsVisible = false;
                SetFocusText(txtPalletDestino);
            }
            else
            {
                lblResultado.Text = "N° de Pallet ingresado no es valido. Intentalo nuevamente. ";
                lblResultado.TextColor = Colors.Red;
                lblResultado.IsVisible = true;
                txtPalletDestino.IsVisible = false;
                txtPallet.Text = string.Empty;
                SetFocusText(txtPallet);
            }
        }
        else
        {
            txtPalletDestino.IsVisible  = false ;
        }
    }
    private async void btn_asignaciones_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenAsignacionMP(tabla));
    }
    private void txtPalletDestino_Completed(object sender, EventArgs e)
    {
        try
        {   
            if (!string.IsNullOrEmpty(txtPalletDestino.Text))
            {
                if (PalletValido)
                {
                    var ACC = Connectivity.NetworkAccess;
                    if (ACC == NetworkAccess.Internet)
                    {
                        #region txtPallet_Completed
                        LoadData();

                        List<PalletClass> pallets = datos.ObtieneInfoPallet(txtPallet.Text);
                        username = App.UserSistema;
                        StaffID = datosUsuario.ObtenerUsuarioStaff(username);

                        pallets.ForEach(p => {
                            if (p.Package_Id != 0 /*&& p.Layout_Id != 0*/)
                            {
                                PackageID = p.Package_Id;
                                //LayoutID = p.Layout_Id;
                            }
                        });

                        LayoutID = int.Parse(txtPalletDestino.Text);

                        HttpClient ClientHttp = new()
                        {
                            BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                        };

                        //ObtieneInfoLayout
                        var rest = ClientHttp.GetAsync("api/Bodega?CodLayoutInfo=" + Convert.ToInt32(txtPalletDestino.Text)).Result;
                        var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                        List<LayoutClass> ly = JsonConvert.DeserializeObject<List<LayoutClass>>(resultadoStr) ??
                                    throw new InvalidOperationException();
                        foreach (var p in ly) 
                        {
                            Console.WriteLine("Layout: " + p.Layout_Id);
                        }

                        //Validacion de Pallet valido
                        
                        if (PalletValido)
                        {
                            //Valdiacion de Lote de Pallet pistoleado coincide con Lote asignado en transferencia
                            //Lo mismo para el ItemCode
                            if (LoteValido && ItemCodeValido)
                            {
                                //Validacion de RecepcionTransfer obtenido de tabla TransferAsignacion
                                //se compara con campo Recepcion Pallet obtenido del pistoleado del Pallet.
                                if (!string.IsNullOrEmpty(RecepcionTransfer))
                                {
                                    if (RecepcionTransfer == RecepcionPallet)
                                    {
                                        //Actualizar en BD la nueva ubicacionAsignada.
                                        //Validacion de resultao de AddLocation. Bool con respuesta de asignacion.  
                                        UbicacionAgregada = datosProduccion.AddLocation(PackageID, LayoutID, StaffID); // Que LayoutID es el que pasa el método AddLocation.
                                        if (UbicacionAgregada)
                                        {
                                            try
                                            {
                                                tabla.Rows.Add(folioRecibido.ToString(), ItemCode, Lote);

                                                DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                                btn_asignaciones.IsVisible = true;
                                                lblResultado.Text = "Asignacion correcta. ";
                                                lblResultado.TextColor = Colors.Green;
                                                lblResultado.IsVisible = true;
                                                LogUsabilidad("Se asigna Materia Prima. ");
                                                txtPallet.Text = string.Empty;
                                                SetFocusText(txtPallet);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Catch_txtPallet_Completed/UbicacionAgregada: " + ex.ToString());
                                            }
                                        }
                                        else
                                        {
                                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                            DisplayAlert("Alerta", "La asignacion ya se encuentra registrada. ", "Aceptar");
                                        }
                                    }
                                    else
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                        DisplayAlert("Alerta", "Codigo de Recepcion no coincide. ", "Aceptar");
                                    }
                                }
                                else
                                {
                                    //Actualizar en BD la nueva ubicacionAsignada.
                                    //Validacion de resultao de AddLocation. Bool con respuesta de asignacion.  
                                    UbicacionAgregada = datosProduccion.AddLocation(PackageID, LayoutID, StaffID); // Que LayoutID es el que pasa el método AddLocation.
                                    if (UbicacionAgregada)
                                    {
                                        try
                                        {
                                            tabla.Rows.Add(folioRecibido.ToString(), ItemCode, Lote);

                                            DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                            btn_asignaciones.IsVisible = true;
                                            lblResultado.Text = "Asignacion correcta. ";
                                            lblResultado.TextColor = Colors.Green;
                                            lblResultado.IsVisible = true;
                                            LogUsabilidad("Se asigna Materia Prima. ");
                                            txtPallet.Text = string.Empty;
                                            SetFocusText(txtPallet);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Catch_txtPallet_Completed/UbicacionAgregada: " + ex.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                        DisplayAlert("Alerta", "La asignacion ya se encuentra registrada. ", "Aceptar");
                                    }
                                }
                            }
                            else
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                lblResultado.Text = "Lote asignado no es válido. ";
                                lblResultado.TextColor = Colors.Red;
                                lblResultado.IsVisible = true;
                                SetFocusText(txtPallet);
                                txtPalletDestino.Text = string.Empty;
                                txtPallet.Text = string.Empty;
                                txtPalletDestino.IsVisible = false;
                            }
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            lblResultado.Text = "El N° de Pallet no es válido. ";
                            lblResultado.TextColor = Colors.Red;
                            lblResultado.IsVisible = true;
                            txtPallet.Text = string.Empty;
                        }
                        #endregion
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                    }
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblResultado.Text = "Ingrese un destino valido. ";
                lblResultado.TextColor = Colors.Red;
                lblResultado.IsVisible = true;
                txtPalletDestino.Text = string.Empty;
                SetFocusText(txtPalletDestino);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("txtPallet_Completed: " + ex.Message);
        }
    }
    private void txtPallet_ClearIconClicked(object sender, System.ComponentModel.HandledEventArgs e)
    {
        txtPalletDestino.IsVisible = false;
        txtPalletDestino.Text = string.Empty ;
        lblResultado.IsVisible = false;
        SetFocusText(txtPallet);
    }
}
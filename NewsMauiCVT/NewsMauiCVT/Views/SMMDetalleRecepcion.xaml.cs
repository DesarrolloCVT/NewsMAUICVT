using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMDetalleRecepcion : ContentPage
{
    int _folio = 0;
    string _CodProd = "";
    string _Proveedor = "";
    int _OC = 0;
    decimal _CantidadOC = 0;
    public SMMDetalleRecepcion(int folio)
    {
        InitializeComponent();
        _folio = folio;
        lblFolioRecepcion.Text = Convert.ToString(_folio);
        txt_CodProducto.Focus();

    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        txt_CodProducto.Focus();
        txt_CodProducto.Text = string.Empty;
        txtDiaElab.Text = string.Empty;
        txtMesElab.Text = string.Empty;
        txtAnoElab.Text = string.Empty;
        txtDiaVenc.Text = string.Empty;
        txtMesVenc.Text = string.Empty;
        txtAnoVenc.Text = string.Empty;
        txt_cantidad.Text = string.Empty;
        lblDatosProd.IsVisible = false;
        //txt_CodProducto.Text = string.Empty;
        //txt_CodProducto.Focus();

    }


    private void Txt_CodProducto_Completed(object sender, EventArgs e)
    {

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {


            DatosRecepcionSMM rc = new DatosRecepcionSMM();

            int oc = rc.TraeOcRecepcion(Convert.ToInt32(lblFolioRecepcion.Text));

            List<SMMProductosEnRecepcionOC> pr = new List<SMMProductosEnRecepcionOC>();
            pr = rc.DatosProductosRecepcionXam(txt_CodProducto.Text, oc);

            int cl = pr.Count();

            if (cl != 0)
            {
                foreach (var t in pr)
                {
                    lblDatosProd.IsVisible = true;
                    lblIngresos.IsVisible = true;
                    lblIngresos2.IsVisible = true;
                    lblCodProducto.Text = "Cod.Prod: " + t.CodProducto.ToString();
                    _CodProd = t.CodProducto.ToString();
                    _Proveedor = t.CodProveedor.ToString();
                    _OC = t.NOrden;
                    _CantidadOC = Convert.ToDecimal(t.CantOrden);
                    lblDescripion.Text = t.Descripcion.ToString();
                    lblCosto.Text = "Precio OC: " + t.Precio;
                    lblCantSolicitada.Text = "Cantidad :" + t.CantOrden;
                    lblSku.Text = "UnMed: " + t.NombreUm;
                    btn_Guardar.IsVisible = true;
                }
            }
            else
            {

                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Producto No se encuentra en Orden de Compra", "Aceptar");
                //txt_CodProducto.Text = string.Empty;
                txt_CodProducto.Focus();
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

    }

    private void Btn_Guardar_Clicked(object sender, EventArgs e)
    {

        if (txt_CodProducto.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese un producto", "Aceptar");
        }
        else if (txt_cantidad.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese una Cantidad", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                if (Convert.ToDecimal(txt_cantidad.Text) > _CantidadOC)
                {

                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Estas Registrando una Cantidad Mayor a La registrada en OC, Favor Verificar", "Aceptar");
                }
                else
                {



                    DatosRecepcionSMM DRecp = new DatosRecepcionSMM();
                    string fechaV = txtAnoVenc.Text + "-" + txtMesVenc.Text + "-" + txtDiaVenc.Text;
                    string fechaEl = txtAnoElab.Text + "-" + txtMesElab.Text + "-" + txtDiaElab.Text;
                    string fActual = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

                    string cant = txt_cantidad.Text.Replace(".", ",");
                    DateTime fv;
                    DateTime fAct;
                    int Tdias = 0;
                    int idPack = 0;

                    if (fechaEl.Equals("--") && fechaV != "--")
                    {
                        string di = txtDiaElab.Text;
                        string Me = txtMesElab.Text;
                        string ye = txtAnoElab.Text;

                        if (DateTime.Compare(Convert.ToDateTime(fechaV), Convert.ToDateTime(fActual)) < 0)
                        {
                            DisplayAlert("Alerta", "Fecha de Vencimiento no puede ser menor  a la fecha actual, Producido en " + di + "-" + Me + "-" + ye, "Aceptar");
                        }
                        else
                        {
                            string fv1 = txtMesVenc.Text + "-" + txtDiaVenc.Text + "-" + txtAnoVenc.Text;
                            fv = Convert.ToDateTime(fv1);
                            TimeSpan tsp = fv.Date.AddDays(1) - DateTime.Now;
                            Tdias = tsp.Days;

                            int DiasUtil = DRecp.TraeDiasUtilPorc(_CodProd);
                            int DiasTot = 0;


                            DiasTot = DRecp.TraeDiasUtilTotal(_CodProd);
                            DateTime fechaElab = Convert.ToDateTime(fechaV).Date.AddDays(-DiasTot);
                            DateTime fechaElabEfec = fechaElab.Date.AddDays(1);

                            string d = fechaElabEfec.Date.Day.ToString();
                            string M = fechaElabEfec.Date.Month.ToString();
                            string y = fechaElabEfec.Date.Year.ToString();



                            if (Tdias >= DiasUtil)
                            {
                                fechaEl = fechaElabEfec.Date.Year + "-" + fechaElabEfec.Date.Month + "-" + fechaElabEfec.Date.Day;
                                txtDiaElab.Text = fechaElabEfec.Date.Day.ToString();
                                txtMesElab.Text = fechaElabEfec.Date.Month.ToString();
                                txtAnoElab.Text = fechaElabEfec.Date.Year.ToString();

                                idPack = DRecp.insertaDetalleRecepcion(_folio, _OC, _CodProd, cant, _Proveedor, fechaV, fechaEl, txt_CodProducto.Text);


                                if (idPack != 0)
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txtDiaElab.Text = string.Empty;
                                    txtMesElab.Text = string.Empty;
                                    txtAnoElab.Text = string.Empty;
                                    txtDiaVenc.Text = string.Empty;
                                    txtMesVenc.Text = string.Empty;
                                    txtAnoVenc.Text = string.Empty;
                                    txt_cantidad.Text = string.Empty;
                                    txt_CodProducto.Text = string.Empty;
                                    txt_CodProducto.Focus();

                                    #region limpieza

                                    lblDatosProd.IsVisible = false;
                                    lblIngresos.IsVisible = false;
                                    lblCodProducto.Text = string.Empty;
                                    _CodProd = "";
                                    lblDescripion.Text = string.Empty;
                                    lblCosto.Text = string.Empty;
                                    lblCantSolicitada.Text = string.Empty;
                                    lblSku.Text = string.Empty;
                                    btn_Guardar.IsVisible = false;
                                    #endregion
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    DisplayAlert("Alerta", "Error al Registrar Favor Verificar ", "Aceptar");
                                }

                            }
                            else
                            {
                                DisplayAlert("Alerta", "Vida util del producto es menor al porcentaje de aceptación, dias util minimo " + DiasUtil + " ,dias  Maximo " + DiasTot + ", vida util actual " + Tdias + " días ," + "Fecha de Elaboracion calculado en base a dias Maximo  : " + d + "-" + M + "-" + y, "Aceptar");
                            }
                        }
                    }
                    else if (fechaEl != "--" && fechaV.Equals("--"))
                    {
                        string d = txtDiaElab.Text;
                        string M = txtMesElab.Text;
                        string y = txtAnoElab.Text;

                        if (DateTime.Compare(Convert.ToDateTime(fechaEl), Convert.ToDateTime(fActual)) > 0)
                        {
                            DisplayAlert("Alerta", "Fecha de produccion no puede ser superior a la fecha actual, Producido en " + d + "-" + M + "-" + y, "Aceptar");
                        }
                        else
                        {

                            int DiasTot = DRecp.TraeDiasUtilTotal(_CodProd);
                            DateTime fechaVenc = Convert.ToDateTime(fechaEl).Date.AddDays(DiasTot);

                            string fv1 = txtMesVenc.Text + "-" + txtDiaVenc.Text + "-" + txtAnoVenc.Text;
                            fv = Convert.ToDateTime(fv1);
                            TimeSpan tsp = fv.Date.AddDays(1) - DateTime.Now;
                            Tdias = tsp.Days;

                            int DiasUtil = DRecp.TraeDiasUtilPorc(_CodProd);

                            string di = fechaVenc.Day.ToString();
                            string Me = fechaVenc.Month.ToString();
                            string ye = fechaVenc.Year.ToString();

                            if (Tdias >= DiasUtil)
                            {
                                fechaV = fechaVenc.Date.Year + "-" + fechaVenc.Date.Month + "-" + fechaVenc.Date.Day;

                                idPack = DRecp.insertaDetalleRecepcion(_folio, _OC, _CodProd, cant, _Proveedor, fechaV, fechaEl, txt_CodProducto.Text);


                                if (idPack != 0)
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txtDiaElab.Text = string.Empty;
                                    txtMesElab.Text = string.Empty;
                                    txtAnoElab.Text = string.Empty;
                                    txtDiaVenc.Text = string.Empty;
                                    txtMesVenc.Text = string.Empty;
                                    txtAnoVenc.Text = string.Empty;
                                    txt_cantidad.Text = string.Empty;
                                    txt_CodProducto.Text = string.Empty;
                                    txt_CodProducto.Focus();

                                    #region limpieza

                                    lblDatosProd.IsVisible = false;
                                    lblIngresos.IsVisible = false;
                                    lblCodProducto.Text = string.Empty;
                                    _CodProd = "";
                                    lblDescripion.Text = string.Empty;
                                    lblCosto.Text = string.Empty;
                                    lblCantSolicitada.Text = string.Empty;
                                    lblSku.Text = string.Empty;
                                    btn_Guardar.IsVisible = false;
                                    #endregion
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    DisplayAlert("Alerta", "Error al Registrar Favor Verificar ", "Aceptar");
                                }
                            }
                            else
                            {
                                DisplayAlert("Alerta", "Vida util del producto es menor al porcentaje de aceptación, dias util minimo " + DiasUtil + " ,dias  Maximo " + DiasTot + ", vida util actual " + Tdias + " días ," + "Fecha de vencimiento calculado en base a dias Maximo  : " + di + "-" + Me + "-" + ye, "Aceptar");
                            }
                        }
                    }
                    else
                    {


                        if (DateTime.Compare(Convert.ToDateTime(fechaEl), Convert.ToDateTime(fActual)) > 0)
                        {
                            string d = txtDiaElab.Text;
                            string M = txtMesElab.Text;
                            string y = txtAnoElab.Text;

                            DisplayAlert("Alerta", "Fecha de produccion no puede ser superior a la fecha actual, Producido en " + d + "-" + M + "-" + y, "Aceptar");
                        }
                        else if (DateTime.Compare(Convert.ToDateTime(fechaV), Convert.ToDateTime(fActual)) < 0)
                        {
                            string d = txtDiaVenc.Text;
                            string M = txtMesVenc.Text;
                            string y = txtAnoVenc.Text;

                            DisplayAlert("Alerta", "Fecha de Vencimiento no puede ser menor  a la fecha actual, Producido en " + d + "-" + M + "-" + y, "Aceptar");
                        }
                        else
                        {
                            string fv1 = txtMesVenc.Text + "-" + txtDiaVenc.Text + "-" + txtAnoVenc.Text;
                            fv = Convert.ToDateTime(fv1);
                            TimeSpan tsp = fv.Date.AddDays(1) - DateTime.Now;
                            Tdias = tsp.Days;

                            int DiasUtil = DRecp.TraeDiasUtilPorc(_CodProd);

                            if (Tdias >= DiasUtil)
                            {
                                idPack = DRecp.insertaDetalleRecepcion(_folio, _OC, _CodProd, cant, _Proveedor, fechaV, fechaEl, txt_CodProducto.Text);


                                if (idPack != 0)
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txtDiaElab.Text = string.Empty;
                                    txtMesElab.Text = string.Empty;
                                    txtAnoElab.Text = string.Empty;
                                    txtDiaVenc.Text = string.Empty;
                                    txtMesVenc.Text = string.Empty;
                                    txtAnoVenc.Text = string.Empty;
                                    txt_cantidad.Text = string.Empty;
                                    txt_CodProducto.Text = string.Empty;
                                    txt_CodProducto.Focus();

                                    #region limpieza

                                    lblDatosProd.IsVisible = false;
                                    lblIngresos.IsVisible = false;
                                    lblCodProducto.Text = string.Empty;
                                    _CodProd = "";
                                    lblDescripion.Text = string.Empty;
                                    lblCosto.Text = string.Empty;
                                    lblCantSolicitada.Text = string.Empty;
                                    lblSku.Text = string.Empty;
                                    btn_Guardar.IsVisible = false;
                                    #endregion
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    DisplayAlert("Alerta", "Error al Registrar Favor Verificar ", "Aceptar");
                                }

                            }
                            else
                            {
                                DisplayAlert("Alerta", "Vida util del producto es menor al porcentaje de aceptación, dias util minimo " + DiasUtil + ", vida util actual " + Tdias + " días", "Aceptar");
                            }
                        }

                    }

                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    private async void txtDiaElab_Completed(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtDiaElab.Text) > 31 || (txtDiaElab.Text.Equals("") || Convert.ToInt32(txtDiaElab.Text) == 0))
        {
            using (UserDialogs.Instance.Alert("ingrese dia correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese dia", "Aceptar");
            }
            txtDiaElab.Focus();
        }
        else { txtMesElab.Focus(); }
    }

    private async void txtMesElab_Completed(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtMesElab.Text) > 12 || (txtMesElab.Text.Equals("") || Convert.ToInt32(txtMesElab.Text) == 0))
        {
            using (UserDialogs.Instance.Alert("ingrese mes correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese mes correcto", "Aceptar");
            }
            txtMesElab.Focus();
        }
        else { txtAnoElab.Focus(); }
    }

    private async void txtAnoElab_Completed(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtAnoElab.Text) > DateTime.Now.Year)
        {
            using (UserDialogs.Instance.Alert("ingrese año correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese año correcto", "Aceptar");
            }
            txtAnoElab.Focus();
        }
        else { txtDiaVenc.Focus(); }
    }

    private async void txtDiaVenc_Completed(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtDiaVenc.Text) > 31 || (txtDiaVenc.Text.Equals("") || Convert.ToInt32(txtDiaVenc.Text) == 0))
        {
            using (UserDialogs.Instance.Alert("ingrese dia correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese dia", "Aceptar");
            }
            txtDiaVenc.Focus();
        }
        else { txtMesVenc.Focus(); }
    }

    private async void txtMesVenc_Completed(object sender, EventArgs e)
    {

        if (Convert.ToInt32(txtMesVenc.Text) > 12 || (txtMesVenc.Text.Equals("") || Convert.ToInt32(txtMesVenc.Text) == 0))
        {
            using (UserDialogs.Instance.Alert("ingrese mes correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese mes correcto", "Aceptar");
            }
            txtMesVenc.Focus();
        }
        else { txtAnoVenc.Focus(); }
    }

    private async void txtAnoVenc_Completed(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtAnoVenc.Text) < DateTime.Now.Year || txtAnoVenc.Text.Equals(""))
        {
            using (UserDialogs.Instance.Alert("ingrese año correcto"))
            {
                await Task.Delay(5);


                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese año correcto", "Aceptar");
            }
            txtAnoVenc.Focus();
        }
        else { txt_cantidad.Focus(); }
    }
}
namespace NewsMauiCVT.Views;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

public partial class PageMain : FlyoutPage
{
	public PageMain()
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        myPageMain();
    }
    public void myPageMain()
    {
        Detail = new NavigationPage(new MenuPruebas() { Title = "Men�" });

        DatosApp dpp = new DatosApp();
        List<MenuClass> mn = dpp.TraeMenu(App.idPerfil);


        List<Option> options = new List<Option>
            {  // new Option { page = new MenuPruebaChanta() { Title = "Men�" }, title = "Inicio", detail = "Inicio" /*image = "ic_ac_home.png" */},
                new Option{ page=new ConsultaUbicacion(){Title="Men�"},title="Consulta Ubicacion", detail="Busqueda detalle ubicacion" },
                new Option{ page=new Repaletizado(){Title="Men�"},title="Repaletizado", detail="Repaletizado"},
                new Option{ page=new Posicionamiento(){Title="Men�"},title="Posicionamiento", detail="Posicionamiento Pallets"},
                new Option{ page=new ActualizaTipoPallet(){Title="Men�"},title="Cambia Tipo Pallet", detail="Cambia el Tipo de Pallet"},
                new Option{ page=new TomaInventario(){Title="Men�"},title="Inventario", detail="Toma Inventario"},
                new Option{ page=new TomaInventarioFilm(){Title="Men�"},title="Toma Inventario Film", detail="Toma Inventario Film"},
                new Option{ page=new TrazabilidadPallet(){Title="Men�"},title="Trazabilidad Pallet", detail="Trazabilidad pallet"},
                new Option{ page=new InformeStock(){Title="Men�"},title="Fefo", detail="Informe Stock"},
                new Option{ page=new SMM_TomaInventario(){Title="Men�"},title="Toma Inventario Mayorista", detail="Inventario Supermercado Mayorista"},
                new Option{ page=new SMMTransferencia() { Title = "Men�" }, title = "Transferencia Mayorista", detail = "Transferencia Mayorista" /*image = "ic_ac_home.png" */},
                new Option{ page=new SMM_Repaletizado(){Title="Men�"},title="Repaletizado Mayorista", detail="Repaletizado Supermercado Mayorista"},
                new Option{ page=new SMM_ConsultaProducto(){Title="Men�"},title="Consulta Producto", detail="Consulta Producto Supermercado Mayorista"},
                new Option{ page=new SMM_Recepcion(){Title="Men�"},title="Recepcion Mayorista", detail="Recepcion Supermerdado Mayorista"},
                new Option{ page=new SMM_TomaInvenario_Bodegas(){Title="Men�"},title="Toma Inventario Bodega 02", detail="Toma inventario Bodega 02"},
                new Option{ page=new SMM_Posicionamiento(){Title="Men�"},title="Posicionamiento Mayorista", detail="Posicionamiento Supermercado Mayorista"},
                new Option{ page=new SMM_ExhibicionSala(){Title="Men�"},title="Exhibicion Sala", detail="Exhibicion en Sala Supermercado Mayorista"},
                new Option{ page=new Revision_Extintor_DatosCab(){Title="Men�"},title="Check Extintores", detail="Check List Equipos de Extincion de fuego"},
                new Option{ page=new SMM_Picking_Consolidado_Cabecera(){Title="Men�"},title="Picking SMM", detail="Picking Consolidado"},
                new Option{ page=new ControlFosfina(){Title="Men�"},title="Control Fosfina", detail="Registro Control Fosfina"},
                new Option{ page=new TestCapturaCodeCam(){Title="Men�"},title="Test Captura Cam", detail="Test captura codigo cam"},
                new Option{ page=new GeoLoc(){Title="Men�"},title="Localizacion", detail="Test captura codigo cam"},
                new Option{ page=new SMMOrdenDeVenta(){Title="Men�"},title="SMM Orden de Ventas", detail="Creacion de Ordenes de Ventas"},
                new Option{ page=new SMMTrazabilidadPallet(){Title="Men�"},title="SMM Trazabilidad Pallet", detail="Trazabilidad Pallet SMM"},
                new Option{ page=new SMM_ConfirmaPalletTransfer(){Title="Men�"},title="SMM Confirma Pallet", detail="Confirma Pallet en Transferencia"},
                new Option{ page=new SMMCumplimientoRepoSala(){Title="Men�"},title="SMM Cumplimiento Reposicion Sala", detail="Ingreso medici�n de cumplimiento de reposici�n en la sala"},
                new Option{ page=new SMMArmadoPedido(){Title="Men�"},title="SMM Medici�n armado de pedidos", detail="Ingreso medici�n armado de pedidos"},
                new Option{ page=new SMMRegImpEtiquetas(){Title="Men�"},title="SMM Registro Imp.Etiquetas", detail="Ingreso de productos para impresion etiquetas de precios"},
                new Option{ page=new SMMRegEtiqSala(){Title="Men�"},title="SMM Registro Etiquetas pallet sala", detail="ingreso de productos para impresion de etiquetas para sala de ventas"},
                new Option{ page=new Calidad_ControlHigiene(){Title="Men�"},title="Control Higiene", detail="Registro Control Higiene Personal"}
        };  
        List<Option> options1 = new List<Option>();

        options1.Add(new Option { page = new MenuPruebas { Title = "Men�" }, title = "Inicio", detail = "Inicio" /*image = "ic_ac_home.png" */});

        foreach (var t in options)
        {
            foreach (var tr in mn)
            {
                if (tr.TituloMenu.Equals(t.title))
                {
                    options1.Add(t);
                }
            }
        }
        options1.Add(new Option { page = null, title = "Cerrar Sesi�n", detail = "Abandonar App" });
        listPageMain.ItemsSource = options1;/* options1;*/
        lblNombre.Text = "Bienvenido " + App.NombreUsuario.ToString();
    }
    private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var option = e.SelectedItem as Option;
        if (option.page != null)
        {
            IsPresented = false;
            Detail = new NavigationPage(option.page);
        }
        else if (option.page == null || option.title == "Cerrar Sesi�n")
        {
            var result = await DisplayAlert("Confirmar", "Estas seguro de cerrar sesi�n", "SI", "NO");
            if (result)
            {
                await Navigation.PopToRootAsync();
                Application.Current.Quit();
            }
            else
            {
                await Navigation.PushAsync(new PageMain());
            }
        }
    }
}
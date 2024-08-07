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
        Detail = new NavigationPage(new MenuPruebas() { Title = "Menú" });

        DatosApp dpp = new DatosApp();
        List<MenuClass> mn = dpp.TraeMenu(App.idPerfil);


        List<Option> options = new List<Option>
            {  // new Option { page = new MenuPruebaChanta() { Title = "Menú" }, title = "Inicio", detail = "Inicio" /*image = "ic_ac_home.png" */},
                new Option{ page=new ConsultaUbicacion(){Title="Menú"},title="Consulta Ubicacion", detail="Busqueda detalle ubicacion" },
                new Option{ page=new Repaletizado(){Title="Menú"},title="Repaletizado", detail="Repaletizado"},
                new Option{ page=new Posicionamiento(){Title="Menú"},title="Posicionamiento", detail="Posicionamiento Pallets"},
                new Option{ page=new ActualizaTipoPallet(){Title="Menú"},title="Cambia Tipo Pallet", detail="Cambia el Tipo de Pallet"},
                new Option{ page=new TomaInventario(){Title="Menú"},title="Inventario", detail="Toma Inventario"},
                new Option{ page=new TomaInventarioFilm(){Title="Menú"},title="Toma Inventario Film", detail="Toma Inventario Film"},
                new Option{ page=new TrazabilidadPallet(){Title="Menú"},title="Trazabilidad Pallet", detail="Trazabilidad pallet"},
                new Option{ page=new InformeStock(){Title="Menú"},title="Fefo", detail="Informe Stock"},
                new Option{ page=new SMM_TomaInventario(){Title="Menú"},title="Toma Inventario Mayorista", detail="Inventario Supermercado Mayorista"},
                new Option{ page=new SMMTransferencia() { Title = "Menú" }, title = "Transferencia Mayorista", detail = "Transferencia Mayorista" /*image = "ic_ac_home.png" */},
                new Option{ page=new SMM_Repaletizado(){Title="Menú"},title="Repaletizado Mayorista", detail="Repaletizado Supermercado Mayorista"},
                new Option{ page=new SMM_ConsultaProducto(){Title="Menú"},title="Consulta Producto", detail="Consulta Producto Supermercado Mayorista"},
                new Option{ page=new SMM_Recepcion(){Title="Menú"},title="Recepcion Mayorista", detail="Recepcion Supermerdado Mayorista"},
                new Option{ page=new SMM_TomaInvenario_Bodegas(){Title="Menú"},title="Toma Inventario Bodega 02", detail="Toma inventario Bodega 02"},
                new Option{ page=new SMM_Posicionamiento(){Title="Menú"},title="Posicionamiento Mayorista", detail="Posicionamiento Supermercado Mayorista"},
                new Option{ page=new SMM_ExhibicionSala(){Title="Menú"},title="Exhibicion Sala", detail="Exhibicion en Sala Supermercado Mayorista"},
                new Option{ page=new Revision_Extintor_DatosCab(){Title="Menú"},title="Check Extintores", detail="Check List Equipos de Extincion de fuego"},
                new Option{ page=new SMM_Picking_Consolidado_Cabecera(){Title="Menú"},title="Picking SMM", detail="Picking Consolidado"},
                new Option{ page=new ControlFosfina(){Title="Menú"},title="Control Fosfina", detail="Registro Control Fosfina"},
                new Option{ page=new TestCapturaCodeCam(){Title="Menú"},title="Test Captura Cam", detail="Test captura codigo cam"},
                new Option{ page=new GeoLoc(){Title="Menú"},title="Localizacion", detail="Test captura codigo cam"},
                new Option{ page=new SMMOrdenDeVenta(){Title="Menú"},title="SMM Orden de Ventas", detail="Creacion de Ordenes de Ventas"},
                new Option{ page=new SMMTrazabilidadPallet(){Title="Menú"},title="SMM Trazabilidad Pallet", detail="Trazabilidad Pallet SMM"},
                new Option{ page=new SMM_ConfirmaPalletTransfer(){Title="Menú"},title="SMM Confirma Pallet", detail="Confirma Pallet en Transferencia"},
                new Option{ page=new SMMCumplimientoRepoSala(){Title="Menú"},title="SMM Cumplimiento Reposicion Sala", detail="Ingreso medición de cumplimiento de reposición en la sala"},
                new Option{ page=new SMMArmadoPedido(){Title="Menú"},title="SMM Medición armado de pedidos", detail="Ingreso medición armado de pedidos"},
                new Option{ page=new SMMRegImpEtiquetas(){Title="Menú"},title="SMM Registro Imp.Etiquetas", detail="Ingreso de productos para impresion etiquetas de precios"},
                new Option{ page=new SMMRegEtiqSala(){Title="Menú"},title="SMM Registro Etiquetas pallet sala", detail="ingreso de productos para impresion de etiquetas para sala de ventas"},
                new Option{ page=new Calidad_ControlHigiene(){Title="Menú"},title="Control Higiene", detail="Registro Control Higiene Personal"}
        };  
        List<Option> options1 = new List<Option>();

        options1.Add(new Option { page = new MenuPruebas { Title = "Menú" }, title = "Inicio", detail = "Inicio" /*image = "ic_ac_home.png" */});

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
        options1.Add(new Option { page = null, title = "Cerrar Sesión", detail = "Abandonar App" });
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
        else if (option.page == null || option.title == "Cerrar Sesión")
        {
            var result = await DisplayAlert("Confirmar", "Estas seguro de cerrar sesión", "SI", "NO");
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
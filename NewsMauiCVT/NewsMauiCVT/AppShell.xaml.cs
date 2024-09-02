using NewsMauiCVT.Views;

namespace NewsMauiCVT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            /*Items.Add(new ShellContent
            {   
                ContentTemplate = new DataTemplate(typeof(MainPage)),
                Route = "MainPage"
            });*/

            InitializeComponent();
            
            /*Routing.RegisterRoute("DashBoard", typeof(MenuPruebas));
            Routing.RegisterRoute("//DashBoard/Consulta", typeof(ConsultaUbicacion));
            Routing.RegisterRoute("//DashBoard/Consulta/Detalle", typeof(DetalleConsultaUbicacion));
            Routing.RegisterRoute("//Detalle/Consulta", typeof(ConsultaUbicacion));*/
        }
    }
}

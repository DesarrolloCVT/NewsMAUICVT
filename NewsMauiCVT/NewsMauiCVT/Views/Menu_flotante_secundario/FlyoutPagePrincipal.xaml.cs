using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class FlyoutPagePrincipal : FlyoutPage
{
	public FlyoutPagePrincipal()
	{
		InitializeComponent();

        flyoutPage.collectionView.SelectionChanged += CollectionView_SelectionChanged;



        Detail = new NavigationPage(new MenuPruebas());

        var menuButton = new ToolbarItem
        {
            Text = "Menu",
            IconImageSource = "hamburger.png",
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };
        menuButton.Clicked += (s, e) =>
        {
            IsPresented = !IsPresented;
        };
        (Detail as NavigationPage).ToolbarItems.Add(menuButton);
    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            IsPresented = false;
        }
    }
}
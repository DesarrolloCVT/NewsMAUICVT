using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class FlyoutPagePrincipal : FlyoutPage
{
	public FlyoutPagePrincipal()
	{
		InitializeComponent();
        flyoutPage.collectionView.SelectionChanged += CollectionView_SelectionChanged;
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
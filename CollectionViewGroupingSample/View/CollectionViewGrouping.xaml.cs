using CollectionViewGroupingSample.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectionViewGroupingSample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewGrouping : ContentPage
    {
        CollectionViewGroupingModel viewModel;
        public CollectionViewGrouping()
        {
            InitializeComponent();
            viewModel = new CollectionViewGroupingModel();
            BindingContext = viewModel;
        }
    }
}
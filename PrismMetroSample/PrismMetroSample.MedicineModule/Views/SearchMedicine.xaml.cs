using MahApps.Metro.Controls;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Infrastructure.Constants;
using System.Windows.Controls;

namespace PrismMetroSample.MedicineModule.Views
{
    /// <summary>
    /// Interaction logic for SearchMedicine
    /// </summary>
    public partial class SearchMedicine : Flyout, IFlyoutView
    {
        public SearchMedicine()
        {
            InitializeComponent();
        }

        public string FlyoutName { get { return FlyoutNames.SearchMedicineFlyout; } }
    }
}

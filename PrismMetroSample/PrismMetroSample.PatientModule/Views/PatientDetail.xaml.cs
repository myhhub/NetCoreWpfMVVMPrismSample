using MahApps.Metro.Controls;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Infrastructure.Constants;
using System.Windows.Controls;

namespace PrismMetroSample.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientDetail
    /// </summary>
    public partial class PatientDetail : Flyout, IFlyoutView
    {
        public PatientDetail()
        {
            InitializeComponent();
        }

        public string FlyoutName { get { return FlyoutNames.PatientDetailFlyout; } }
    }
}

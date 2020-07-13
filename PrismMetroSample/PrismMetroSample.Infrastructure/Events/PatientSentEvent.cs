using Prism.Events;
using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Events
{
   public class PatientSentEvent: PubSubEvent<Patient>
    {
    }
}

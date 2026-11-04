using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using DataverseStorageCostReducer.UI;

namespace DataverseStorageCostReducer
{
    [Export(typeof(IXrmToolBoxPlugin)),
     ExportMetadata("Name", "Dataverse Storage & Cost Reducer"),
     ExportMetadata("Description", "Audit, visualize, and reduce expensive Dataverse storage costs across database, file, and audit layers. Identifies top 80% cost drivers and safely archives/purges bloat."),
     ExportMetadata("BigImageBase64", null),
     ExportMetadata("SmallImageBase64", null),
     ExportMetadata("BackgroundColor", "#0f172a"),
     ExportMetadata("PrimaryFontColor", "#38bdf8"),
     ExportMetadata("SecondaryFontColor", "#94a3b8")]
    public class StorageReducerPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new StorageReducerControl();
        }
    }
}

using System.Threading;
namespace GTA_SP_Enchancement.Features.FeatureControl
{
    public struct FeatureControlDataInformation
    {
        public FeatureControlThread featureControlThread;
        public struct FeatureControlThread
        {
            public fVehicle.VehicleControl _vehicle;
            public Cursor.CursorControl _cursor;
            public Needs.NeedsControl _needs;
        }
    }
    public interface IFeatureControllableThread
    {
        fVehicle.VehicleControl _vehicle { get; }
        Cursor.CursorControl _cursor { get; }
        Needs.NeedsControl _needs { get; }
    }
}

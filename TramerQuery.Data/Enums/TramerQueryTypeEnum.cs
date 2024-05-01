using System.ComponentModel;

namespace TramerQuery.Data.Enums
{
    public enum TramerQueryTypeEnum : int
    {
        [Description("Şaşi Numarası")]
        ChassisNumber = 1,

        [Description("Plaka Numarası")]
        PlateNumber = 2
    }
}

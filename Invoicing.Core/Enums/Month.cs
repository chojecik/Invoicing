using System.ComponentModel;

namespace Invoicing.Core.Enums
{
    public enum Month
    {
        [Description("Styczeń")]
        January = 1,

        [Description("Luty")]
        February = 2,

        [Description("Marzec")]
        March = 3,

        [Description("Kwiecień")]
        April = 4,

        [Description("Maj")]
        May = 5,

        [Description("Czerwiec")]
        June = 6,

        [Description("Lipiec")]
        July = 7,

        [Description("Sierpnień")]
        August = 8,

        [Description("Wrzesień")]
        September = 9,

        [Description("Październik")]
        October = 10,

        [Description("Listopad")]
        November = 11,

        [Description("Grudzień")]
        December = 12
    }
}

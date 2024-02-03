using System.ComponentModel;


namespace Shared.Enum
{
    public enum LevelsEducations
    {
        [Description("Primary Education")]
        PrimaryEducation,

        [Description("Lower secondary education")]
        LowerSecondaryEducation,

        [Description("Upper secondary education")]
        UpperSecondaryEducation,

        [Description("Bachelor")]
        Bachelor,

        [Description("Master")]
        Master,

        [Description("Doctorate")]
        Doctorate
    }
}

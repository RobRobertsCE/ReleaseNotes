namespace CenterEdge.ReleaseNotes.Models
{
    public enum ReleaseScope
    {
        Development = 1,
        Operations = 2,
        Public = 4,
        Internal = Development | Operations,
        All = Development | Operations | Public
    }
}

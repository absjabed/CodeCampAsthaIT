namespace CodeCampWebapiClass.Models;

public class CodeCamperModel
{
    public int CamperId { get; set; }
    public string? CamperName { get; set; }
    public CodeCampTrack Track { get; set; }
}

public enum CodeCampTrack{

    FrontEnd = 1,
    Backend,
    Mobile
}
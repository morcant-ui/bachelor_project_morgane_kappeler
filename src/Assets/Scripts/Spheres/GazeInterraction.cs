public class GazeInteraction
{
    public int UserId { get; set; }
    public int SphereId { get; set; }
    public float TimeSpent { get; set; }
    public float StartTime { get; set; }

    public GazeInteraction(int userId, int sphereId, float timeSpent, float startTime)
    {
        UserId = userId;
        SphereId = sphereId;
        TimeSpent = timeSpent;
        StartTime = startTime;
    }
}

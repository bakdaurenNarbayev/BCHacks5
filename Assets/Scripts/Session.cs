public class Session
{
    public string sessionID;
    public int participantID;
    public float height;
    public float speed;
    public bool environment;

    public Session(int participantID, float height, float speed, bool environment)
    {
        this.sessionID = participantID + "-" + height;
        this.participantID = participantID;
        this.height = height;
        this.speed = speed;
        this.environment = environment;
    }
}
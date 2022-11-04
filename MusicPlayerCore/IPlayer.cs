namespace MusicPlayerCore;

public interface IPlayer
{
    void Start(string path);
    void PlayPause();
    void SeekForward();
    void SeekBackward();
    void Status();
    System.TimeSpan CurrentTime();
    System.TimeSpan TotalTime();
    void ChangeVolume(double volume);
    
}

namespace MusicPlayerCore;

public interface IPlayer
{
    void Start(string path);
    void PlayPause();
    void SeekForward();
    void SeekBackward();
    string Status();
    System.TimeSpan CurrentTime();
    System.TimeSpan TotalTime();
    void ChangeVolume(double volume);
    
}

public enum PlaybackState
{
    Playing, Paused, Stopped
}

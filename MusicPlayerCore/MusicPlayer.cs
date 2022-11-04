namespace MusicPlayerCore;

public class MusicPlayer
{
    
}

public interface IPlayer
{
    void PlayPause();
    void SeekForward();
    void SeekBackward();
    void Next();
    void Previous();
    void Status();
    System.TimeSpan CurrentTime();
    System.TimeSpan TotalTime();
    void ChangeVolume(double volume);
    
}

namespace MusicPlayerCore.Player;

public interface IPlayer
{

    PlaybackState PlaybackState { get; }
    int Volume { get; }
    void Start(string path);
    void PlayPause();
    void SeekForward();
    void SeekBackward();
    string Status();
    System.TimeSpan CurrentTime();
    System.TimeSpan TotalTime();
    void VolumeUp();
    void VolumeDown();

}

public enum PlaybackState
{
    Playing, Paused, Stopped
}

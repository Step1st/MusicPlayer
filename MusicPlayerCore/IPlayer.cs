namespace MusicPlayerCore;

public interface IPlayer
{

    PlaybackState PlaybackState { get; }
    void Start(string path);
    void PlayPause();
    void SeekForward();
    void SeekBackward();
    string Status();
    System.TimeSpan CurrentTime();
    System.TimeSpan TotalTime();
    void ChangeVolume(double volume);
    void VolumeUp();
    void VolumeDown();

}

public enum PlaybackState
{
    Playing, Paused, Stopped
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace MusicPlayerCore;
public class WindowsPlayer : IPlayer
{

    private readonly WaveOutEvent outputDevice;
    private AudioFileReader audioFile;

    public WindowsPlayer()
    {
        outputDevice = new WaveOutEvent();
        outputDevice.PlaybackStopped += OnPlaybackStopped;
    }


    public void Start(string path)
    {
        // if the file exits
        if (File.Exists(path))
        {
            // if the file is not already playing
            if (audioFile == null)
            {
                // create a new audio file reader
                audioFile = new AudioFileReader(path);
                // set the output device to the audio file
                outputDevice.Init(audioFile);
                // play the audio file
                outputDevice.Play();
            }
        }
    }

    public void Status() => throw new NotImplementedException();

    public void ChangeVolume(double volume) => throw new NotImplementedException();

    public void PlayPause()
    {
        if (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            outputDevice.Pause();
        }
        else
        {
            outputDevice.Play();
        }

    }

    public void SeekBackward() => throw new NotImplementedException();
    public void SeekForward() => throw new NotImplementedException();

    public TimeSpan CurrentTime() => throw new NotImplementedException();
    public TimeSpan TotalTime() => throw new NotImplementedException();


    public void OnPlaybackStopped(object? sender, StoppedEventArgs args)
    {
        if (audioFile is not null)
        {
            audioFile.Dispose();
        }

        outputDevice.Dispose();
    }
}

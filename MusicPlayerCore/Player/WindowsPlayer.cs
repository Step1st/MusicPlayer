using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace MusicPlayerCore.Player;
public class WindowsPlayer : IPlayer
{
    private readonly WaveOutEvent outputDevice;
    private AudioFileReader audioFile;

    public PlaybackState PlaybackState { get; private set; } = PlaybackState.Stopped;

    private int volume = 100;

    public int Volume => volume;

    public WindowsPlayer()
    {
        outputDevice = new WaveOutEvent();
        outputDevice.PlaybackStopped += OnPlaybackStopped;
    }

    public void Start(string path)
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
            Thread.Sleep(120);
        }

        // if the file exits
        if (File.Exists(path))
        {
            // create a new audio file reader
            audioFile = new AudioFileReader(path);
            //set the volume
            audioFile.Volume = volume / 100f;
            // set the output device to the audio file
            outputDevice.Init(audioFile);
            // play the audio file
            outputDevice.Play();
            // set the playback state to playing
            PlaybackState = PlaybackState.Playing;
        }
        
    }

    public void PlayPause()
    {
        if (PlaybackState == PlaybackState.Playing)
        {
            // pause the audio file
            outputDevice.Pause();
            // set the playback state to paused
            PlaybackState = PlaybackState.Paused;
        }
        // if the playback state is paused
        else if (PlaybackState == PlaybackState.Paused)
        {
            // play the audio file
            outputDevice.Play();
            // set the playback state to playing
            PlaybackState = PlaybackState.Playing;
        }
    }

    public void SeekForward()
    {
        if (PlaybackState != PlaybackState.Stopped && audioFile != null && audioFile.CurrentTime <= audioFile.TotalTime)
        {
            audioFile.CurrentTime += TimeSpan.FromSeconds(5);
        }
    }
    public void SeekBackward() 
    {
        if  (PlaybackState != PlaybackState.Stopped && audioFile != null)
        {
            if (audioFile.CurrentTime <= TimeSpan.FromSeconds(5))
            {
                audioFile.CurrentTime = TimeSpan.Zero;
            }
            else
            {
                audioFile.CurrentTime -= TimeSpan.FromSeconds(5);
            }
        }
    }

    public TimeSpan CurrentTime()
    {
        if (audioFile is not null && PlaybackState != PlaybackState.Stopped)
        {
            return audioFile.CurrentTime;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }
    public TimeSpan TotalTime()
    {
        if (audioFile is not null && PlaybackState != PlaybackState.Stopped)
        {
            return audioFile.TotalTime;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }


    public void OnPlaybackStopped(object? sender, StoppedEventArgs args)
    {
        if (audioFile is not null)
        {
            audioFile.Dispose();
        }
        outputDevice.Dispose();
        
        PlaybackState = PlaybackState.Stopped;
    }

    public void VolumeUp()
    {
        if (volume < 100)
        {
            volume += 5;
            if (audioFile is not null)
            {
                audioFile.Volume = volume / 100f;
            }
        }
    }

    public void VolumeDown()
    {
        if (volume > 0)
        {
            volume -= 5;
            if (audioFile is not null)
            {
                audioFile.Volume = volume / 100f;
            }
        }

    }
}

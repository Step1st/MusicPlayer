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

    public string Status()
    {
        if (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            return "Playing";
        }
        else if (outputDevice.PlaybackState == PlaybackState.Paused)
        {
            return "Paused";
        }
        else
        {
            return "Stopped";
        }
    }

    public void ChangeVolume(double volume)
    {
        // if the audio file is not null
        if (audioFile != null)
        {
            // set the volume of the audio file
            audioFile.Volume = (float)volume;
        }
    }

    public void PlayPause()
    {
        
        if (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            outputDevice.Pause();
        }
        else if (outputDevice.PlaybackState == PlaybackState.Paused)
        {
            outputDevice.Play();
        }
    }

    public void SeekForward()
    {
        if (audioFile is not null && audioFile.CurrentTime <= audioFile.TotalTime)
        {
            audioFile.CurrentTime += TimeSpan.FromSeconds(5);
        }
    }
    public void SeekBackward() 
    {
        if (audioFile is not null && audioFile.CurrentTime >= TimeSpan.FromSeconds(5))
        {
            audioFile.CurrentTime -= TimeSpan.FromSeconds(5);
        }
    }

    public TimeSpan CurrentTime()
    {
        if (audioFile is not null)
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
        if (audioFile is not null)
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
    }
}

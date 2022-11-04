using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace MusicPlayerCore;
public class WindowsPlayer : IPlayer
{

    private WaveOutEvent outputDevice;
    private AudioFileReader audioFile;

    private PlaybackState playbackState = PlaybackState.Stopped;

    public WindowsPlayer()
    {
    }


    public void Start(string path)
    {
        if (outputDevice != null)
        {
            if (audioFile is not null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            outputDevice.Dispose();
            playbackState = PlaybackState.Stopped;
        }

        outputDevice = new WaveOutEvent();
        outputDevice.PlaybackStopped += OnPlaybackStopped;

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
                // set the playback state to playing
                playbackState = PlaybackState.Playing;
            }
        }
    }

    public string Status()
    {
        return playbackState.ToString();
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
        if (playbackState == PlaybackState.Playing)
        {
            // pause the audio file
            outputDevice.Pause();
            // set the playback state to paused
            playbackState = PlaybackState.Paused;
        }
        // if the playback state is paused
        else if (playbackState == PlaybackState.Paused)
        {
            // play the audio file
            outputDevice.Play();
            // set the playback state to playing
            playbackState = PlaybackState.Playing;
        }
    }

    public void SeekForward()
    {
        if (playbackState != PlaybackState.Stopped && audioFile != null && audioFile.CurrentTime <= audioFile.TotalTime)
        {
            audioFile.CurrentTime += TimeSpan.FromSeconds(5);
        }
    }
    public void SeekBackward() 
    {
        if  (playbackState != PlaybackState.Stopped && audioFile is not null)
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
        if (audioFile is not null && playbackState != PlaybackState.Stopped)
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
        if (audioFile is not null && playbackState != PlaybackState.Stopped)
        {
            return audioFile.TotalTime;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }


    public void OnPlaybackStopped(object? sender, StoppedEventArgs? args)
    {
        if (audioFile is not null)
        {
            audioFile.Dispose();
            audioFile = null;
            Console.WriteLine("Audio file disposed");
        }
        else
        {
            Console.WriteLine("Audio file is null");
        }
        Console.WriteLine("device disposed");
        outputDevice.Dispose();
        playbackState = PlaybackState.Stopped;
    }
}

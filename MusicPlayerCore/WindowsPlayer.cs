using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace MusicPlayerCore;
internal class WindowsPlayer : IPlayer
{

    public WindowsPlayer()
    {
        
    }

    public void Status() => throw new NotImplementedException();

    public void ChangeVolume(double volume) => throw new NotImplementedException();

    public void PlayPause() => throw new NotImplementedException();

    public void Next() => throw new NotImplementedException();
    public void Previous() => throw new NotImplementedException();

    public void SeekBackward() => throw new NotImplementedException();
    public void SeekForward() => throw new NotImplementedException();

    public TimeSpan CurrentTime() => throw new NotImplementedException();
    public TimeSpan TotalTime() => throw new NotImplementedException();
}

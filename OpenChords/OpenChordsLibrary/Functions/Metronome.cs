using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Functions
{
    public class Metronome : IDisposable
    {
        private System.Timers.Timer timer;
        private int beatNumber = 1;
        private int beatsPerMeasure = 4;
        private SoundPlayer SoundPlayerTickTone;
        private SoundPlayer SoundPlayerBarTone;
        private const string TICK_TONE = "./Resources/Tick2.wav";
        private const string BAR_TONE = "./Resources/Tick1.wav";


        public bool Enabled
        {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }

        public Metronome()
        {
            timer = new System.Timers.Timer();
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;

            SoundPlayerTickTone = new SoundPlayer(TICK_TONE);
            SoundPlayerBarTone = new SoundPlayer(BAR_TONE);
        }

        public void SetSong(Song song)
        {
            beatNumber = 1;
            if ((song.time_sig ?? "").Length > 0)
            {
                int.TryParse(song.time_sig.Substring(0, 1), out beatsPerMeasure);
            }
            else
            {
                beatsPerMeasure = 4;
            }
            timer.Interval = 60.0 / song.BeatsPerMinute * 1000;
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (beatNumber % beatsPerMeasure == 1)
                playBarTone();
            else
                playTickTone();
            beatNumber++;
        }

        private void playTickTone()
        {
            if (!IsLinux)
                playSoundInWindows(SoundPlayerTickTone);
            else
                playSoundInLinux(TICK_TONE);
        }

        private void playBarTone()
        {
            if (!IsLinux)
                playSoundInWindows(SoundPlayerBarTone);
            else
                playSoundInLinux(BAR_TONE);
        }

        private void playSoundInWindows(SoundPlayer player)
        {
            player.Play();
        }

        private void playSoundInLinux(string soundToPlay)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process() 
            { 
                EnableRaisingEvents = false,
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = "aplay",
                    Arguments = "-t wav " + soundToPlay
                }
            };
            proc.Start();
        }

        public void Dispose()
        {
            timer.Elapsed -= OnTimedEvent;
        }

        private static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}

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
        private SoundPlayer BAR_TONE;
        private SoundPlayer TICK_TONE;
      
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

            BAR_TONE = new SoundPlayer("./Resources/Tick1.wav");
            TICK_TONE = new SoundPlayer("./Resources/Tick2.wav");
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
                BAR_TONE.Play();
            else
                TICK_TONE.Play();
            beatNumber++;
        }

        public void Dispose()
        {
            timer.Elapsed -= OnTimedEvent;
        }
    }
}

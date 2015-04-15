using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Functions
{
    public class Metronome : IDisposable
    {
        private System.Timers.Timer timer;
        private int beatNumber = 1;
        private int beatsPerMeasure = 4;
        private const int BAR_TONE = 1000;
        private const int TICK_TONE = 500;
        private const int BEAT_DURATION = 100;

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
                Console.Beep(BAR_TONE, BEAT_DURATION);
            else
                Console.Beep(TICK_TONE, BEAT_DURATION);
            beatNumber++;
        }

        public void Dispose()
        {
            timer.Elapsed -= OnTimedEvent;
        }
    }
}

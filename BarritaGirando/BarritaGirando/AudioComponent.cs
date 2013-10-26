using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AudioComponent : Microsoft.Xna.Framework.GameComponent
    {
        private AudioEngine audioEngine;
        private WaveBank waveBank;
        private SoundBank soundBank;
        private Cue cancion;

        public SoundBank SoundBank
        {
            get { return soundBank; }
            set { soundBank = value; }
        }

        public AudioComponent(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            audioEngine = new AudioEngine("Content\\barrita.xgs");
            waveBank = new WaveBank(audioEngine, "Content\\Wave Bank.xwb");
            if (waveBank != null) {
                soundBank = new SoundBank(audioEngine, "Content\\Sound Bank.xsb");
            }
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            audioEngine.Update();
            base.Update(gameTime);
        }
        public void PlayCue(string cue) {
            cancion = soundBank.GetCue(cue);
            if (cancion != null) {
                cancion.Play();
            }
          //if (soundBank!=null)          
            //soundBank.PlayCue(cue);
        }

        public void StopCue(string cue)
        {
            cancion = soundBank.GetCue(cue);
            if (cancion != null)
            {
                cancion.Stop(AudioStopOptions.Immediate);
            }
        }

        public void Play(string cue)
        {
            cancion = soundBank.GetCue(cue);
            if (cancion != null)
            {
                cancion.Play();
            }
        }

        public Cue GetCue(string cue) {
            if (soundBank != null)
                return soundBank.GetCue(cue);
            else
                return null;
        }
    }
}
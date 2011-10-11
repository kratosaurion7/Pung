using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pung
{
    class Player
    {
        // Player's name and score
        String name;
        int score;

        #region Properties

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        // Player instance of a pallet
        Pallet pallet;

        public Pallet Pallet
        {
            get { return pallet; }
            set { pallet = value; }
        }

        /// <summary>
        /// Create a new instance of the Player class.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="playerNumber"></param>
        public Player(PungGame game, Pallet.PlayerNumber playerNumber)
        {
            pallet = new Pallet(game, playerNumber);
        }

        /// <summary>
        /// Increment the score counter by the default of 1
        /// </summary>
        internal void AddScore()
        {
            score++;
        }

        /// <summary>
        /// Increment the score counter by an amount.
        /// </summary>
        /// <param name="amount"></param>
        internal void AddScore(int amount)
        {
            score += amount;
        }
    }
}

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

        // Reference to the game
        Game gameRef;

        public Player(Game game, Pallet.PlayerNumber playerNumber)
        {
            pallet = new Pallet(game, playerNumber);
        }


        internal void AddScore()
        {
            score++;
        }
    }
}

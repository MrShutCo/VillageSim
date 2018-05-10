using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim {
    public class Tile {

        // To be used for later, to determine if the pathfinding should pick up on it
        //bool _isKnown;
        int _x;
        int _y;
        bool _isPassable;
        int _movementCost;
        Texture2D _tex;

        public Tile(int x, int y, Texture2D tex, int move, bool pass) {
            _x = x;
            _y = y;
            _tex = tex;
            _movementCost = move;
            _isPassable = pass;
        }

        public Tuple<int, int> GetPos() {
            return new Tuple<int, int>(_x, _y);
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(_tex, new Vector2(_x * 32, _y * 32), Color.White);
        }

        public override string ToString() {
            return "X: " +_x.ToString() + ", Y:" + _y.ToString();
        }

    }
}

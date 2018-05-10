using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim.GroundObjects {
    public abstract class TileObject {

        int _x, _y;
        protected Texture2D _tex;
        public string TileType { get; }

        public Vector2 Position { get { return new Vector2(_x  * 32, _y * 32); } }

        public TileObject(int x, int y, Texture2D tex, string type) {
            _x = x;
            _y = y;
            _tex = tex;
            TileType = type;
        }

        public abstract void Draw(SpriteBatch sb);

    }
}

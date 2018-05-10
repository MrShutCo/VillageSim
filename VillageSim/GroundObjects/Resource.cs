using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim.GroundObjects {
    public class Resource : TileObject {

        ResourceType _resourceType;
        int _count;

        public Resource(int x, int y, Texture2D tex, ResourceType resource)
            : base(x, y, tex, "resource"){
            _resourceType = resource;
            _count = 10;
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(_tex, Position, Color.White);
            sb.DrawString(Game1.font, _count.ToString(), Position + new Vector2(7, 7), Color.White);
        }

    }
}

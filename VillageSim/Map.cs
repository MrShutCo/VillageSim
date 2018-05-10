using EpPathFinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageSim.GroundObjects;

namespace VillageSim {
    public class Map {

        private Tile[,] _mapLayout;
        private List<TileObject> _tileObjects;
        private int _width, _height;
        private BaseGrid _searchGrid;
        JumpPointParam jpParam;

        public Map(int width, int height) {
            _mapLayout = new Tile[width, height];
            _searchGrid = new StaticGrid(width, height);
            _width = width;
            _height = height;
            jpParam = new JumpPointParam(null, true, false, false);
            _tileObjects = new List<TileObject>();
        }

        //This is just a temporary function... Maybe villagers will have a sight that updates what they can do???
        public TileObject FindClosestObject(int x, int y, string type) {
            return _tileObjects.OrderBy(o => (o.Position - new Vector2(x * 32, y * 32)).LengthSquared()).FirstOrDefault();
        }


        // Manual Creation code for now... pass in an Object that will handle MapCreation
        public void CreateMap(Texture2D ground, Texture2D wall, Texture2D resource){
            Random r = new Random();
            for (int y = 0; y < _height; y++) {
                for (int x = 0; x < _width; x++) {
                    if (r.Next(0, 100) > 90) {
                        _mapLayout[x, y] = new Tile(x, y, wall, 1, false);
                        _searchGrid.SetWalkableAt(new GridPos(x, y), false);
                    } else {
                        _mapLayout[x, y] = new Tile(x, y, ground, 1, true);
                        _searchGrid.SetWalkableAt(new GridPos(x, y), true);
                    }
                 }
            }
            for (int i = 0; i < 25; i++) {
                int x = r.Next(0, 24);
                int y = r.Next(0, 24);
                _tileObjects.Add(new Resource(x, y, resource, ResourceType.Rock));
                //_searchGrid.SetWalkableAt(new GridPos(x, y), false);
            }
        }

        public List<Tile> FindPath(int sX, int sY, int eX, int eY) {
         
            GridPos start = new GridPos(sX, sY);
            GridPos end = new GridPos(eX, eY);
            if (!_searchGrid.IsWalkableAt(end)) {

                return new List<Tile>();
            }
            jpParam.Reset(start, end, _searchGrid);
            List<GridPos> result = JumpPointFinder.FindPath(jpParam);
            List<Tile> result2 = new List<Tile>();
            foreach (GridPos gp in result) {
                result2.Add(_mapLayout[gp.x, gp.y]);
            }



            return result2;
        }

        public void Draw(SpriteBatch sp) {
            foreach(Tile t in _mapLayout) {
                t.Draw(sp);
            }
            foreach(TileObject to in _tileObjects) {
                if (to.TileType == "resource") 
                to.Draw(sp);
            }
        }

    }
}

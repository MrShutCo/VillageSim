using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim.Tasks {

    public class MoveEventArgs : EventArgs {
        public Map map;
        public Villager v;
        public int eX, eY;

        public MoveEventArgs(Map map, Villager v, int eX, int eY) {
            this.map = map;
            this.v = v;
            this.eX = eX;
            this.eY = eY;
        }
    }


    class MoveTask : Task {


        Queue<Tile> _moveQueue;
        Queue<Tile> _moveQueueUntouch;
        int endX, endY;

        public MoveTask(int eX, int eY) {
            endX = eX;
            endY = eY;
            _moveQueue = new Queue<Tile>();
            _moveQueueUntouch = new Queue<Tile>();
            HasStarted = false;
            IsFinished = false;
            IsPaused = false;
            TaskName = "MoveTask";
        }

        public void StartTask(Vector2 pos) {
            List<Tile> path = Village.Map.FindPath((int)Math.Floor(pos.X) / 32, (int)Math.Floor(pos.Y) / 32, endX, endY);
            foreach (Tile t in path) {
                _moveQueue.Enqueue(t);
                _moveQueueUntouch.Enqueue(t);
            }
        }

        public void DoTask(float speed, ref Vector2 posi) {
            if (_moveQueue.Count > 0) {
                Tile t = _moveQueue.Peek();
                float distance = Vector2.Distance(posi, new Vector2(t.GetPos().Item1 * 32, t.GetPos().Item2 * 32));
                if (distance <= 1f) {
                    _moveQueue.Dequeue();
                    if (_moveQueue.Count == 0) return;
                    t = _moveQueue.Peek();
                }

                var pos = t.GetPos();
                Vector2 dir = new Vector2(pos.Item1 * 32, pos.Item2 * 32) - posi;
                dir.Normalize();
                posi += dir * speed;
            } else {
                IsFinished = true;
            }
        }

        public override string UIInfo() {
            string retval = "";
            foreach(Tile t in _moveQueue) {
                retval += t.ToString() + "\n";
            }
            return retval;
            //return "X: " + endX.ToString() + ", Y: " + endY.ToString();
        }
    }
}

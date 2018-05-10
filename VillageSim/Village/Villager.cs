using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageSim.Tasks;

namespace VillageSim {
    public class Villager {

        public Vector2 Position { get; private set; }
        // Speed indicates how many things they do in a second
        public float Speed { get; private set; }
        string _name;
        Texture2D _tex;
        //Queue<Task> _tasks;
        SimplePriorityQueue<Task, int> _tasks;
        public int FoodCount;

        event EventHandler currDoing;

        public Villager(int x, int y, string name, Texture2D tex) {
            Position = new Vector2(x * 32, y * 32);
            _name = name;
            _tex = tex;
            Speed = 2.0f;
            _tasks = new SimplePriorityQueue<Task, int>();
            FoodCount = 0;
        }

        public void AddTask(Task task, int priority) {
            _tasks.Enqueue(task, priority);
        }


        public void Update(GameTime gt) {
            if (_tasks.Count > 0) {
                Task temp = _tasks.First;
                if (temp.IsFinished) {
                    _tasks.Dequeue();
                    if (_tasks.Count == 0) {
                        return;
                    }
                    temp = _tasks.First;
                }
                //Ewwwww.... But I dunno what else to do
                if (!temp.HasStarted) {
                    temp.HasStarted = true;
                    switch(temp.TaskName) {
                        case "MoveTask":
                            MoveTask t = (MoveTask)temp;
                            t.StartTask(Position);
                            break;
                        case "GatherResourceTask":
                            GatherResourceTask grt = (GatherResourceTask)temp;
                            grt.StartTask(this);
                            break;

                    }
                }
                switch (temp.TaskName) {
                    case "MoveTask":
                        MoveTask t = (MoveTask)temp;
                        Vector2 p = Position;
                        t.DoTask(Speed, ref p);
                        Position = p;
                        break;
                    case "GatherResourceTask":
                        GatherResourceTask grt = (GatherResourceTask)temp;
                        int food = FoodCount;
                        grt.DoTask(ref food, gt);
                        FoodCount = food;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(_tex, Position, Color.White);
        }

        public void DrawUI(SpriteBatch sb) {
            sb.DrawString(Game1.font, "X: " + ((int)Position.X / 32).ToString() + "\nY: " + ((int)Position.Y / 32).ToString(), new Vector2(30, 400), Color.Black);
            sb.DrawString(Game1.font, "Food count: " + FoodCount, new Vector2(30, 380), Color.Black);
            string tasks = "";
            foreach(Task t in _tasks) {
                tasks += t.TaskName + ": " + t.UIInfo() + "\n";
            }
            sb.DrawString(Game1.font, tasks, new Vector2(600, 35), Color.Black);

        }


    }
}

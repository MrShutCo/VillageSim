using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageSim.GroundObjects;

namespace VillageSim.Tasks {
    public class GatherResourceTask : Task {

        ResourceType _resourceType;
        Resource _currentResource;
        float _passedTime;

        public GatherResourceTask(ResourceType rt) {
            _resourceType = rt;
            _passedTime = 0.0f;
            TaskName = "GatherResourceTask";
        }

        public void StartTask(Villager v) {
            int x = (int)v.Position.X / 32;
            int y = (int)v.Position.Y / 32;
            Resource nearest = (Resource)Village.Map.FindClosestObject(x, y, "resource");
            _currentResource = nearest;
            v.AddTask(new MoveTask((int)nearest.Position.X / 32, (int)nearest.Position.Y / 32), 0);
            HasStarted = true;
        }

        /// <summary>
        /// If within a certain distance, begin to collect the resources every 2 seconds until source it drained
        /// </summary>
        /// <param name="v">The current villager doing the task</param>
        /// <param name="gt">gameTime passed in from Game</param>
        public void DoTask(ref int food, GameTime gt) {
            _passedTime += gt.ElapsedGameTime.Milliseconds;
            if (_passedTime > 2000f) {
                if (_currentResource.Gather()) {
                    food += 1;
                } else {
                    IsFinished = true;
                }
                _passedTime = 0;
            }

        }

        public override string UIInfo() {
            return "Resource: " + _resourceType.ToString();
        }
    }
}

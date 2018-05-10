using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim {
    public class Village {

        public List<Villager> v;
        public static Map Map; //Icky bicky

        public Village(Map map) {
            Map = map;
            v = new List<Villager>();
        }

        public void Update(GameTime gt) {

        }

        /* v.AddTask(new MoveTask(5, 10), 4)
         * v.AddTask(new EatTask(), 4) 
         * 
         * 
         * 
         * 
         * */

    }
}

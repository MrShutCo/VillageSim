using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageSim {



    public abstract class Task {

        public string TaskName { get; protected set; }
        public bool HasStarted { get; set; }
        public bool IsFinished { get; protected set; }
        public bool IsPaused { get; protected set; }
        public abstract string UIInfo();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lone_Mars {
    class Bug : Boid {
        public Bug(PVector2 p, PVector2 v, List<Boid> bs, Random r)
            : base(p, v, bs, new FInt(3), r) {
            AlignmentWeight = new FInt(0.1);
            CohesionWeight = new FInt(1.0);
            SeperationWeight = new FInt(2.0);
            Momentum = new FInt(5.0);
            RandomWeight = new FInt(0.1);
            
            MaxVelocity = new FInt(5);
            VisionRangeSquared = new FInt(25.0 * 25.0);
        }
    }
}

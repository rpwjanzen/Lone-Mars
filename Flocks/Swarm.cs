using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lone_Mars {
    class Swarm : Flock{

        public Swarm(Random r, int nb)
            : base(r, nb) {
        }

        protected override Boid CreateBoid() {
            var b = new Bug(PVector2.Zero, PVector2.Zero, Boids, Random);
            return b;
        }
    }
}

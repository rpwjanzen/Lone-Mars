using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lone_Mars {
    class Boid {
        public PVector2 Position;
        public PVector2 VelocityPerTick;
        public List<Boid> Boids;

        FInt visionRangeSquared = new FInt(10.0 * 10.0);
        FInt scratch = FInt.ZeroF;

        public Boid(PVector2 p, PVector2 v, List<Boid> bs) {
            Position = p;
            VelocityPerTick = v;
            Boids = bs;
        }

        public virtual void Update() {
            var visibleBoids = new List<Boid>();
            foreach (var b in Boids) {
                if (b != this) {
                    if (IsVisible(b)) {
                        visibleBoids.Add(b);
                    }
                }
            }


        }

        protected virtual void Cohesion() {

        }

        protected virtual bool IsVisible(Boid b) {
            this.Position.DistanceSquaredTo(ref b.Position, out scratch);
            return scratch < visionRangeSquared;
        }
    }
}

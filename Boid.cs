using System;
using System.Collections.Generic;

namespace Lone_Mars {
    class Boid {
        public PVector2 Position;
        public PVector2 VelocityPerTick;
        public List<Boid> Boids;

        FInt visionRangeSquared = new FInt(5.0 * 5.0);
        FInt scratch = FInt.ZeroF;
        PVector2 scratchV = PVector2.Zero;

        protected FInt AlignmentWeight = new FInt(0.5);
        protected FInt CohesionWeight = new FInt(0.5);
        protected FInt SeperationWeight = new FInt(0.3);
        protected FInt MaxVelocity;

        public Boid(PVector2 p, PVector2 v, List<Boid> bs, FInt maxVelocity) {
            Position = p;
            VelocityPerTick = v;
            Boids = bs;
            MaxVelocity = maxVelocity;
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

            if (visibleBoids.Count != 0) {
                var dc = Cohesion(visibleBoids) * CohesionWeight;
                var ds = Seperation(visibleBoids) * SeperationWeight;
                var da = Alignment(visibleBoids) * AlignmentWeight;

                VelocityPerTick += (dc + ds + da);
                PVector2.Limit(ref VelocityPerTick, MaxVelocity);
            }

            Position += VelocityPerTick;
        }


        protected virtual PVector2 Alignment(List<Boid> visibleBoids) {
            var avgAlignment = PVector2.Zero;
            foreach (var b in visibleBoids) {
                PVector2.Add(ref avgAlignment, ref b.VelocityPerTick, out avgAlignment);
            }
            avgAlignment /= (FInt)visibleBoids.Count;
            PVector2.Sub(ref VelocityPerTick, ref avgAlignment, out scratchV);

            return avgAlignment;
        }

        protected virtual PVector2 Seperation(List<Boid> visibleBoids) {
            var avgPosition = PVector2.Zero;
            foreach (var b in visibleBoids) {
                PVector2.Add(ref avgPosition, ref b.Position, out avgPosition);
            }
            avgPosition /= (FInt)visibleBoids.Count;
            PVector2.Sub(ref Position, ref avgPosition, out scratchV);

            return scratchV;
        }

        protected virtual PVector2 Cohesion(List<Boid> visibleBoids) {
            var avgPosition = PVector2.Zero;
            foreach (var b in visibleBoids) {
                PVector2.Add(ref avgPosition, ref b.Position, out avgPosition);
            }
            avgPosition /= (FInt)visibleBoids.Count;
            PVector2.Sub(ref avgPosition, ref Position, out scratchV);

            return scratchV;
        }

        protected virtual bool IsVisible(Boid b) {
            this.Position.DistanceSquaredTo(ref b.Position, out scratch);
            return scratch < visionRangeSquared;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Lone_Mars {
    class Boid {
        public PVector2 Position;
        public PVector2 VelocityPerTick;
        public List<Boid> Boids;

        
        FInt scratch = FInt.ZeroF;
        PVector2 scratchV = PVector2.Zero;

        protected FInt VisionRangeSquared = new FInt(25.0 * 25.0);
        protected FInt AlignmentWeight = new FInt(1.0);
        protected FInt CohesionWeight = new FInt(1.0);
        protected FInt SeperationWeight = new FInt(2.0);
        protected FInt Momentum = new FInt(5.0);
        protected FInt RandomWeight = new FInt(0.0);
        public FInt MaxVelocity;

        protected FInt MinX = new FInt(-500);
        protected FInt MaxX = new FInt(500);
        protected FInt MinY = new FInt(-500);
        protected FInt MaxY = new FInt(500);

        public bool Dead = false;
        Random random;

        public Boid(PVector2 p, PVector2 v, List<Boid> bs, FInt maxVelocity, Random random) {
            Position = p;
            VelocityPerTick = v;
            Boids = bs;
            MaxVelocity = maxVelocity;
            this.random = random;
        }

        public virtual PVector2 Update() {
            Position += VelocityPerTick;
            if (Position.X > MaxX) {
                //Position.X = MinX + (Position.X - MaxX);
                Dead = true;
            }
            if (Position.X < MinX) {
                //Position.X = MaxX + (MinX - Position.X);
                Dead = true;
            }

            if (Position.Y > MaxY) {
                //Position.Y = MinY + (Position.Y - MaxY);
                Dead = true;
            }
            if (Position.Y < MinY) {
                //Position.Y = MaxY + (MinY - Position.Y);
                Dead = true;
            }

            var newVelocity = PVector2.Zero;
            if (!Dead) {
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
                    var closeBoids = new List<Boid>(visibleBoids.Count);
                    foreach (var b in visibleBoids) {
                        if (IsClose(b)) {
                            closeBoids.Add(b);
                        }
                    }
                    var ds = PVector2.Zero;
                    if (closeBoids.Count > 0) {
                        ds = Seperation(closeBoids) * SeperationWeight;
                    }

                    var da = Alignment(visibleBoids) * AlignmentWeight;

                    var v = VelocityPerTick * Momentum;
                    var r = new PVector2(new FInt(random.NextDouble() - 0.5), new FInt(random.NextDouble() - 0.5)) * RandomWeight;

                    newVelocity = (dc + ds + da + v + r);
                    PVector2.Limit(ref newVelocity, MaxVelocity);
                } else {
                    var v = VelocityPerTick * Momentum;
                    var r = new PVector2(new FInt(random.NextDouble() - 0.5), new FInt(random.NextDouble() - 0.5)) * RandomWeight;

                    newVelocity = (v + r);
                    PVector2.Limit(ref newVelocity, MaxVelocity);
                }
            }

            return newVelocity;
        }


        protected virtual PVector2 Alignment(List<Boid> visibleBoids) {
            var avgAlignment = PVector2.Zero;
            foreach (var b in visibleBoids) {
                PVector2.Add(ref avgAlignment, ref b.VelocityPerTick, out avgAlignment);
            }
            avgAlignment /= (FInt)visibleBoids.Count;
            PVector2.Sub(ref avgAlignment, ref VelocityPerTick, out scratchV);

            return scratchV;
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
            return scratch < VisionRangeSquared;
        }

        protected virtual bool IsClose(Boid b) {
            this.Position.DistanceSquaredTo(ref b.Position, out scratch);
            return scratch < VisionRangeSquared * new FInt(0.3);
        }
    }
}

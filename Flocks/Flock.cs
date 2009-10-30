using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Lone_Mars {
    class Flock {
        public List<Boid> Boids;
        
        protected FInt MaxVelocity = (FInt)3;

        protected Random Random;

        public Flock(Random r, int numBoids) {
            Random = r;
            Boids = new List<Boid>(numBoids);
            for (int i = 0; i < numBoids; i++) {
                Boids.Add(CreateBoid());
            }
        }

        protected virtual Boid CreateBoid() {
            var b = new Boid(PVector2.Zero, PVector2.Zero, Boids, MaxVelocity, Random);
            ResetBoid(b);
            return b;
        }

        protected virtual void ResetBoid(Boid b) {
            var direction = MathHelper.Lerp(0, MathHelper.TwoPi, (float)Random.NextDouble());
            var speed = Random.NextDouble() * MaxVelocity.ToDouble();
            var v = PVector2.Zero;
            v.X = new FInt((Math.Cos(direction) * speed));
            v.Y = new FInt((Math.Sin(direction) * speed));

            var p = new PVector2((FInt)Random.Next(-400, 400), (FInt)Random.Next(-400, 400));
            b.Position = p;
            b.VelocityPerTick = v;
            b.Dead = false;
        }

        public virtual void Update() {
            var velocities = new List<PVector2>(Boids.Count);
            foreach (var b in Boids) {
                var vel = b.Update();
                if (b.Dead) {
                    ResetBoid(b);
                    vel = b.VelocityPerTick;
                }
                velocities.Add(vel);
            }

            for (int i = 0; i < velocities.Count; i++) {
                Boids[i].VelocityPerTick = velocities[i];
            }
        }
    }
}

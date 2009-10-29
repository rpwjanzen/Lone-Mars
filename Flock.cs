using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Lone_Mars {
    class Flock {
        public List<Boid> Boids;
        Random random;
        FInt MaxVelocity = (FInt)3;

        public Flock(Random r, int numBoids) {
            random = r;
            Boids = new List<Boid>(numBoids);
            for (int i = 0; i < numBoids; i++) {
                Boids.Add(CreateBoid());
            }
        }

        Boid CreateBoid() {
            var direction = MathHelper.Lerp(0, MathHelper.TwoPi, (float)random.NextDouble());
            var speed = random.NextDouble() * MaxVelocity.ToDouble();
            var v = PVector2.Zero;
            v.X = new FInt((Math.Cos(direction) * speed));
            v.Y = new FInt((Math.Sin(direction) * speed));

            var p = new PVector2((FInt)random.Next(-25, 25), (FInt)random.Next(-25, 25));
            var b = new Boid(p, v, Boids, MaxVelocity);
            return b;
        }

        public virtual void Update() {
            foreach (var b in Boids) {
                b.Update();
            }
        }
    }
}

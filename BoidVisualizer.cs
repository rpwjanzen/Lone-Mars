using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Lone_Mars {
    class BoidVisualizer {

        Flock flock;
        Texture2D texture;
        float scale = 0.001f;
        Vector2 offset = Vector2.Zero;
        Vector2 origin;

        public BoidVisualizer(Flock flock, Vector2 offset, float scale) {
            this.flock = flock;
            this.offset = offset;
            this.scale = scale;
        }

        public void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>("Boid");
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Draw(SpriteBatch sb) {
            foreach (var b in flock.Boids) {
                var rotation = FInt.Atan2(b.VelocityPerTick.Y, b.VelocityPerTick.X);
                rotation += (FInt.PI / new FInt(2));
                var pos = (b.Position.ToVector2() * scale + offset);
                sb.Draw(texture, pos, null, Color.White, (float)rotation.ToDouble(), origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}

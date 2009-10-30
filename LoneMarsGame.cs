using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Lone_Mars {
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Flock flock;
        FlockVisualizer bv;

        int width = 1440;
        int height = 900;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            //width = Screen.PrimaryScreen.Bounds.Width;
            //height = Screen.PrimaryScreen.Bounds.Height;

            Content.RootDirectory = "Content";

            var r = new Random(0);
            flock = new Flock(r, 200);
            bv = new FlockVisualizer(flock, new Vector2(width / 2, height / 2), 1f);
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            //enable fullscreen mode
            graphics.IsFullScreen = true;

            //finally, apply the changes
            graphics.ApplyChanges();

            //hide the mouse pointer
            this.IsMouseVisible = false;

            base.Initialize();
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bv.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime) {
            flock.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            bv.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

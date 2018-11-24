using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace SpaceXdemo
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture;
        DrawablePhysicsObject crate;
        DrawablePhysicsObject floor;
        KeyboardState prevKeyboardState;

        Random rand;

        World world;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rand = new Random();

            texture = Content.Load<Texture2D>("rocket_body2");

            world = new World(new Vector2(0, 9.8f));
            //Vector2 size = new Vector2(30,120);


            floor = new DrawablePhysicsObject(world, Content.Load<Texture2D>("HUD"), new Vector2(320.0f, 60.0f), 1000);
            floor.Position = new Vector2((float)GraphicsDevice.Viewport.Width / 2.0f + 160.0f, (float)GraphicsDevice.Viewport.Height);
            floor.body.BodyType = BodyType.Static;
            // floor.body.CollidesWith = crate.body.fix;


            crate = new DrawablePhysicsObject(world, Content.Load<Texture2D>("rocket_body2"), new Vector2(35.0f, 120.0f), 0.1f);
            crate.Position = new Vector2((float)GraphicsDevice.Viewport.Width / 2, (float)-GraphicsDevice.Viewport.Height + 258.0f);
            crate.body.BodyType = BodyType.Static;
            crate.body.Rotation = 0.0f;

            prevKeyboardState = Keyboard.GetState();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && (!prevKeyboardState.IsKeyDown(Keys.Enter)))
            {
                crate.body.BodyType = BodyType.Dynamic;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                crate.Position = new Vector2(crate.body.Position.X, crate.body.Position.Y - 2.3f);
                crate.body.Rotation += crate.NextFloat(rand) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

            prevKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //Vector2 scale = new Vector2(30/(float)texture.Width,120/(float)texture.Height);


            crate.Draw(spriteBatch);


            floor.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

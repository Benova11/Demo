using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace SpaceXdemo
{
    class DrawablePhysicsObject
    {


        public Body body;
        public Vector2 Position
        {
            get { return body.Position; }
            set { body.Position = value; }
        }

        public Texture2D texture;

        private Vector2 size;
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        ///The farseer simulation this object should be part of
        ///The image that will be drawn at the place of the body
        ///The size in pixels
        ///The mass in kilograms
        public DrawablePhysicsObject(World world, Texture2D texture, Vector2 size, float mass)
        {
            body = BodyFactory.CreateRectangle(world, size.X, size.Y, 1);
            body.BodyType = BodyType.Dynamic;


            this.Size = size;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(Size.X / (float)texture.Width, Size.Y / (float)texture.Height);
            spriteBatch.Draw(texture, Position, null, Color.White, body.Rotation, new Vector2(texture.Width, texture.Height), scale, SpriteEffects.None, 0);
        }

        public float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, random.Next(-2, 2));
            Console.WriteLine((float)(mantissa * exponent));
            return (float)(mantissa * exponent);
        }
    }
}


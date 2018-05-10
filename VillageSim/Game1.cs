using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using VillageSim.Tasks;

namespace VillageSim {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputHandler inputHandler;
        Map testMap;
        Village village;
        Villager v;
        Random r;

        public static SpriteFont font;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            Camera.Pos = new Vector2(25 * 16, 15 * 16);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testMap = new Map(25,25);
            inputHandler = new InputHandler();
            font = Content.Load<SpriteFont>("text");
            inputHandler.RegisterKey(Keys.Left, () => Camera.Pos -= new Vector2(32, 0));
            inputHandler.RegisterKey(Keys.Right, () => Camera.Pos -= new Vector2(-32, 0));
            inputHandler.RegisterKey(Keys.Up, () => Camera.Pos -= new Vector2(0, 32));
            inputHandler.RegisterKey(Keys.Down, () => Camera.Pos -= new Vector2(0, -32));
            testMap.CreateMap(Content.Load<Texture2D>("ground"), Content.Load<Texture2D>("Wall"),Content.Load<Texture2D>("Food"));
            v = new Villager(5, 10, "Tyler", Content.Load<Texture2D>("robot"));
            village = new Village(testMap);
            inputHandler.RegisterKey(Keys.Enter, () => v.AddTask(new MoveTask(r.Next(0, 24), r.Next(0, 24)), 1));
            inputHandler.RegisterKey(Keys.Space, () => v.AddTask(new GatherResourceTask(GroundObjects.ResourceType.Food), 1));
            r = new Random();
            //v.AddTask(new MoveTask(10, 15), 1);
            //v.AddTask(new EatTask())
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            inputHandler.Update(gameTime);
            // TODO: Add your update logic here
 
            v.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,Camera.viewMatrix);
            testMap.Draw(spriteBatch);
            v.Draw(spriteBatch);
            spriteBatch.End();
            //Do UI Stuff here
            spriteBatch.Begin();
            v.DrawUI(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

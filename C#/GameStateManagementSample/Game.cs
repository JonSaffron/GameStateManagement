//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using System;

namespace GameStateManagement
    {
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameStateManagementGame : Game
        {
        readonly GraphicsDeviceManager _graphics;
        readonly ScreenManager _screenManager;

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameStateManagementGame()
            {
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // you can choose whether you want a landscape or portait
            // game by using one of the two helper functions.
            InitializePortraitGraphics();
            // InitializeLandscapeGraphics();

            // Create the screen manager component.
            _screenManager = new ScreenManager(this);

            Components.Add(_screenManager);

            // attempt to deserialize the screen manager from disk. if that
            // fails, we add our default screens.
            if (!_screenManager.DeserializeState())
            {
                // Activate the first screens.
                _screenManager.AddScreen(new BackgroundScreen(), null);
                _screenManager.AddScreen(new MainMenuScreen(), null);
            }
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            // serialize the screen manager whenever the game exits
            _screenManager.SerializeState();

            base.OnExiting(sender, args);
        }

        /// <summary>
        /// Helper method to the initialize the game to be a portrait game.
        /// </summary>
        private void InitializePortraitGraphics()
        {
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.PreferredBackBufferHeight = 800;
        }

        /// <summary>
        /// Helper method to initialize the game to be a landscape game.
        /// </summary>
        private void InitializeLandscapeGraphics()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

    }


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (GameStateManagementGame game = new GameStateManagementGame())
            {
                game.Run();
            }
        }
    }

}

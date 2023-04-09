﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using System;
using System.ComponentModel.Design;

// MAKE SURE YOU RENAME ALL PROJECT FILES FROM DevcadeGame TO YOUR YOUR GAME NAME
namespace DevcadeSlots
{
	public class SlotImage
	{
		private int num;
		private Texture2D texture;
		public SlotImage() 
		{
			Random rand = new Random();
			int randInt = rand.Next(0,100);
			if (randInt < 50)
			{
				//texture = FILL THIS IN;
				num = 1;
			}else if (randInt < 70)
			{
                //texture = FILL THIS IN;
                num = 2;
			}else if (randInt < 85)
			{
                //texture = FILL THIS IN;
                num = 3;
            }
            else if (randInt < 95)
			{
                //texture = FILL THIS IN;
                num = 4;
            }
            else
			{
                //texture = FILL THIS IN;
                num = 5;
            }
		}

		public SlotImage(int num)
		{
			this.num = num;
			if (num == 1)
			{
				//texture = FILL THIS IN
			}else if (num == 2)
			{
                //texture = FILL THIS IN
            }
            else if (num == 3)
			{
                //texture = FILL THIS IN
            }
            else if (num == 4)
			{
                //texture = FILL THIS IN
            }
            else
			{
                //texture = FILL THIS IN
            }
        }

		public int getNum()
		{
			return num;
		}

		public Texture2D GetTexture()
		{
			return texture;
		}
	}

	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
        private bool _readyForNextInput;
		private bool _colOneMoving;
		private bool _colTwoMoving;
		private bool _colThreeMoving;
		private SlotImage[] _colOne;
		private SlotImage[] _colTwo;
		private SlotImage[] _colThree;
        Texture2D texture;

        /// <summary>
        /// Stores the window dimensions in a rectangle object for easy use
        /// </summary>
        private Rectangle windowSize;


        /// <summary>
        /// Game constructor
        /// </summary>
        public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;
		}

		/// <summary>
		/// Performs any setup that doesn't require loaded content before the first frame.
		/// </summary>
		protected override void Initialize()
		{
			// Sets up the input library
			Input.Initialize();

			// Set window size if running debug (in release it will be fullscreen)
			#region
#if DEBUG
			_graphics.PreferredBackBufferWidth = 420;
			_graphics.PreferredBackBufferHeight = 980;
			_graphics.ApplyChanges();
#else
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
			#endregion

			// TODO: Add your initialization logic here
			_readyForNextInput = true;

			_colOneMoving = false;
			_colTwoMoving = false;
			_colThreeMoving = false;

			_colOne = new SlotImage[2];
			_colTwo = new SlotImage[2];
			_colThree = new SlotImage[2];
			
			_colOne[0] = new SlotImage();
			_colOne[1] = new SlotImage(1);

            _colTwo[0] = new SlotImage();
            _colTwo[1] = new SlotImage(1);

            _colThree[0] = new SlotImage();
            _colThree[1] = new SlotImage(1);


            texture = new Texture2D(GraphicsDevice, 1, 1);


            windowSize = GraphicsDevice.Viewport.Bounds;
			
			base.Initialize();
		}

		/// <summary>
		/// Performs any setup that requires loaded content before the first frame.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			// ex:
			// texture = Content.Load<Texture2D>("fileNameWithoutExtension");
		}


		/// <summary>
		/// Your main update loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Update(GameTime gameTime)
		{
			Input.Update(); // Updates the state of the input library

			// Exit when both menu buttons are pressed (or escape for keyboard debugging)
			// You can change this but it is suggested to keep the keybind of both menu
			// buttons at once for a graceful exit.
			if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
				(Input.GetButton(1, Input.ArcadeButtons.Menu) &&
				Input.GetButton(2, Input.ArcadeButtons.Menu)))
			{
				Exit();
			}

			

			

			

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// Your main draw loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Gray);

			if (_readyForNextInput && Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				GraphicsDevice.Clear(Color.White);
			}

			

            // Batches all the draw calls for this frame, and then performs them all at once
            _spriteBatch.Begin();

            if (_readyForNextInput && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _spriteBatch.Draw(texture, new Rectangle(100, 100, 100, 100), Color.White);
                _readyForNextInput = false;
            }
            
			// TODO: Add your drawing code here
			
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
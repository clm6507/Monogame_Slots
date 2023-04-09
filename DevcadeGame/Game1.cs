using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;
using System;
using System.ComponentModel.Design;
using System.Net.Mime;

// MAKE SURE YOU RENAME ALL PROJECT FILES FROM DevcadeGame TO YOUR YOUR GAME NAME
namespace DevcadeSlots
{
	public class SlotImage
	{
        private int num;
		private Texture2D texture;
		private GraphicsDeviceManager graphics;


		public SlotImage(Texture2D[] texture2D)
		{
			Random rand = new Random();
			int randInt = rand.Next(0,100);
			if (randInt < 40)
			{
				texture = texture2D[0];
				num = 1;
			}else if (randInt < 65)
			{
                texture = texture2D[1];
                num = 2;
			}else if (randInt < 85)
			{
                texture = texture2D[2];
                num = 3;
            }
            else if (randInt < 95)
			{
                texture = texture2D[3];
                num = 4;
            }
            else
			{
                texture = texture2D[4];
                num = 5;
            }
		}

		public SlotImage(Texture2D[] texture2D, int num)
		{
			this.num = num;
			if (num == 1)
			{
                texture = texture2D[0];
            }
            else if (num == 2)
			{
                texture = texture2D[1];
            }
            else if (num == 3)
			{
                texture = texture2D[2];
            }
            else if (num == 4)
			{
                texture = texture2D[3];
            }
            else
			{
                texture = texture2D[4];
            }
        }

		public int getNum()
		{
			return num;
		}

		public Texture2D getTexture()
		{
			return texture;
		}
	}

	public class Game1 : Game
	{
        private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

        private int _topImageLine;
        private int _bottomImageLine;
        private int _imageWidth;

        private int _col1image0y;
        private int _col1image1y;

        private int _col2image0y;
        private int _col2image1y;

        private int _col3image0y;
        private int _col3image1y;

		private int _spinSpeed;

		private int _col1Counter;
		private int _col2Counter;
		private int _col3Counter;

        private bool _readyForNextInput;

		private SlotImage[] _colOne;
		private SlotImage[] _colTwo;
		private SlotImage[] _colThree;

		private Texture2D[] _images;
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

            windowSize = GraphicsDevice.Viewport.Bounds;

            // TODO: Add your initialization logic here
            _imageWidth = windowSize.Width / 3;
			_bottomImageLine = windowSize.Height/2 - _imageWidth / 2 ;
			_topImageLine = _bottomImageLine - _imageWidth;

			_col1image0y = _topImageLine;
			_col1image1y = _bottomImageLine;

			_col2image0y = _topImageLine;
			_col2image1y = _bottomImageLine;

			_col3image0y = _topImageLine;
			_col3image1y = _bottomImageLine;

			_spinSpeed = _imageWidth / 4;

			_col1Counter = 0;
			_col2Counter = 0;
			_col3Counter = 0;
			
			_readyForNextInput = true;

            _colOne = new SlotImage[2];
            _colTwo = new SlotImage[2];
            _colThree = new SlotImage[2];

			Color[] data = new Color[] { Color.White };
            texture = new Texture2D(GraphicsDevice, 1, 1);
			texture.SetData(data);

			
			base.Initialize();
		}

		/// <summary>
		/// Performs any setup that requires loaded content before the first frame.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_images = new Texture2D[5];
			_images[0] = this.Content.Load<Texture2D>("GrapeImage");
			_images[1] = this.Content.Load<Texture2D>("WatermelonImage");
			_images[2] = this.Content.Load<Texture2D>("DiceImage");
			_images[3] = this.Content.Load<Texture2D>("CrownImage");
			_images[4] = this.Content.Load<Texture2D>("7Image");

            _colOne[0] = new SlotImage(_images);
            _colOne[1] = new SlotImage(_images,5);

            _colTwo[0] = new SlotImage(_images);
            _colTwo[1] = new SlotImage(_images,5);

            _colThree[0] = new SlotImage(_images);
            _colThree[1] = new SlotImage(_images,5);

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

            if (_readyForNextInput && (Keyboard.GetState().IsKeyDown(Keys.Down) || Input.GetButtonDown(1,Input.ArcadeButtons.StickDown) || Input.GetButtonDown(2, Input.ArcadeButtons.StickDown)))
			{
				_readyForNextInput = false;
			}
			else if( !_readyForNextInput )
			{
                if (_col1Counter < 30)
                {
                    _col1image0y += _spinSpeed;
                    _col1image1y += _spinSpeed;
                }

                if (_col2Counter < 40)
                {
                    _col2image0y += _spinSpeed;
                    _col2image1y += _spinSpeed;
                }

                if (_col3Counter < 50)
                {
                    _col3image0y += _spinSpeed;
                    _col3image1y += _spinSpeed;
                }


                if (_col1image1y >= _bottomImageLine + _imageWidth)
                {
                    _col1image0y = _topImageLine;
                    _col1image1y = _bottomImageLine;

                    _colOne[1] = _colOne[0];
                    _colOne[0] = new SlotImage(_images);

                    _col1Counter++;
                    //PLAY TICK SOUND?
                }

                if (_col2image1y >= _bottomImageLine + _imageWidth)
                {
                    _col2image0y = _topImageLine;
                    _col2image1y = _bottomImageLine;

                    _colTwo[1] = _colTwo[0];
                    _colTwo[0] = new SlotImage(_images);

                    _col2Counter++;
                    //PLAY TICK SOUND?
                }

                if (_col3image1y >= _bottomImageLine + _imageWidth)
                {
                    _col3image0y = _topImageLine;
                    _col3image1y = _bottomImageLine;

                    _colThree[1] = _colThree[0];
                    _colThree[0] = new SlotImage(_images);

                    _col3Counter++;
                    //PLAY TICK SOUND?
                }

				if (_col3Counter >= 50)
				{
                    _readyForNextInput = true;
                }
            }

            if (_readyForNextInput && _col3Counter == 50)
            {
                _col1Counter = 0;
                _col2Counter = 0;
                _col3Counter = 0;
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

			

            // Batches all the draw calls for this frame, and then performs them all at once
            _spriteBatch.Begin();

            _spriteBatch.Draw(_colOne[0].getTexture(), new Rectangle(0, _col1image0y, _imageWidth, _imageWidth), Color.White);
            _spriteBatch.Draw(_colOne[1].getTexture(), new Rectangle(0, _col1image1y,_imageWidth,_imageWidth), Color.White);

            _spriteBatch.Draw(_colTwo[0].getTexture(), new Rectangle(_imageWidth, _col2image0y, _imageWidth, _imageWidth), Color.White);
            _spriteBatch.Draw(_colTwo[1].getTexture(), new Rectangle(_imageWidth, _col2image1y, _imageWidth, _imageWidth), Color.White);

            _spriteBatch.Draw(_colThree[0].getTexture(), new Rectangle(_imageWidth*2, _col3image0y, _imageWidth, _imageWidth), Color.White);
            _spriteBatch.Draw(_colThree[1].getTexture(), new Rectangle(_imageWidth*2, _col3image1y, _imageWidth, _imageWidth), Color.White);

			_spriteBatch.Draw(texture, new Rectangle(0, 0, windowSize.Width, _bottomImageLine), Color.Gray);
			_spriteBatch.Draw(texture, new Rectangle(0, _bottomImageLine + _imageWidth, windowSize.Width, _bottomImageLine), Color.Gray);

            
			// TODO: Add your drawing code here
			
			_spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
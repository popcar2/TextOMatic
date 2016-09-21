using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using System.IO;

namespace SeriousMonoGame2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState previousState = Mouse.GetState();

        private Texture2D TextArea;
        private static Texture2D TextButton1;
        private static Texture2D TextButton2;
        private static Texture2D TextButton3;
        private static Texture2D TextButton4;

        private static int TextButton1X = 35;
        private static int TextButton1Y = 550;
        private static int TextButton2X = 345;
        private static int TextButton2Y = 550;
        private static int TextButton3X = 655;
        private static int TextButton3Y = 550;
        private static int TextButton4X = 965;
        private static int TextButton4Y = 550;

        private static string TextButton1Text = "Start";
        private static string TextButton2Text = "Editor Mode";
        private static string TextButton3Text = "";
        private static string TextButton4Text = "";

        private SpriteFont font;
        private static int ChoiceNumber = 0;
        private static bool SkipMainMenuFalse = false;
        private static bool InMainMenu = true;
        private static bool InEditorMode = false;

        private static string FontText = "Welcome.";
        private static int FinishedTyping = 1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            Window.Position = new Point(0, 0);
            Window.Title = "Text-O-Matic 2000 - BETA";
            StreamReader sr = new StreamReader("script/title.txt");
            string line = sr.ReadLine();
            sr.Close();
            EnterText("Welcome to the Text-O-Matic 2000.\nCurrent adventure: " + line + ".", 10);
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("textfont");
            font.LineSpacing = 28;
            TextArea = Content.Load<Texture2D>("Images/TextArea2");
            TextButton1 = Content.Load<Texture2D>("Images/TextButton");
            TextButton2 = Content.Load<Texture2D>("Images/TextButton");
            TextButton3 = Content.Load<Texture2D>("Images/TextButton4");
            TextButton4 = Content.Load<Texture2D>("Images/TextButton4");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState state = Mouse.GetState();

            CheckButton1(state, Content, previousState);
            CheckButton2(state, Content, previousState);
            CheckButton3(state, Content, previousState);
            CheckButton4(state, Content, previousState);

            previousState = state;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(TextArea, new Vector2(30));

            spriteBatch.Draw(TextButton1, destinationRectangle: new Rectangle(TextButton1X, TextButton1Y, 280, 150));
            spriteBatch.Draw(TextButton2, destinationRectangle: new Rectangle(TextButton2X, TextButton2Y, 280, 150));
            spriteBatch.Draw(TextButton3, destinationRectangle: new Rectangle(TextButton3X, TextButton3Y, 280, 150));
            spriteBatch.Draw(TextButton4, destinationRectangle: new Rectangle(TextButton4X, TextButton4Y, 280, 150));

            spriteBatch.DrawString(font, FontText, new Vector2(50, 50), Color.Black);

            spriteBatch.DrawString(font, TextButton1Text, new Vector2(TextButton1X + 140 - font.MeasureString(TextButton1Text).X / 2, TextButton1Y + 75 - 8), Color.Black);
            spriteBatch.DrawString(font, TextButton2Text, new Vector2(TextButton2X + 140 - font.MeasureString(TextButton2Text).X / 2, TextButton2Y + 75 - 8), Color.Black);
            spriteBatch.DrawString(font, TextButton3Text, new Vector2(TextButton3X + 140 - font.MeasureString(TextButton3Text).X / 2, TextButton3Y + 75 - 8), Color.Black);
            spriteBatch.DrawString(font, TextButton4Text, new Vector2(TextButton4X + 140 - font.MeasureString(TextButton4Text).X / 2, TextButton4Y + 75 - 8), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }




        async static void EnterText(string text, int wait = 50, int delete = 1)
        {
            if (FinishedTyping == 1)
            {
                FinishedTyping = 0;
                if (delete == 1)
                {
                    FontText = "";
                }
                int i = 0;
                while (i != text.Length)
                {
                    if (i + 1 != text.Length)
                    {
                        if (text[i] + text[i + 1].ToString() == @"\n")
                        {
                            FontText = FontText + "\n";
                            i++;
                        }
                        else
                        {
                            FontText = FontText + text[i];
                        }
                    }
                    else
                    {
                        FontText = FontText + text[i];
                    }
                    i++;
                    await Task.Delay(wait);
                }
                FinishedTyping = 1;
            }
        }

        async static void CheckStory(int i, Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            if (!InEditorMode)
            {
                bool buttonhit = false;
                if (i == 1 && ChoiceNumber == 0)
                {
                    var lines = File.ReadLines("script/script.txt");
                    ChoiceNumber++;
                    foreach (var line in lines)
                    {
                        string[] line2 = line.Split('|');
                        if (line != "CLEAR" && line != "CLEARBUTTONS" && !line.StartsWith("BUTTONTEXT") && line != "PROMPT" && !line.StartsWith("IFBUTTON") && !line.StartsWith("//") && !line.StartsWith("CHOICE") && !line.StartsWith("MAINMENU") && line2.Length == 1)
                        {
                            if (FontText == "")
                            {
                                EnterText(line2[0], 50, 0);
                            }
                            else
                            {
                                if (line2[0].StartsWith("--"))
                                {
                                    EnterText(line2[0].Substring(2), 50, 0);
                                }
                                else
                                {
                                    EnterText("\n" + line2[0], 50, 0);
                                }
                            }
                            await Task.Delay(line2[0].Length * 70);
                        }
                        else if (line != "CLEAR" && line != "CLEARBUTTONS" && !line.StartsWith("BUTTONTEXT") && line != "PROMPT" && !line.StartsWith("IFBUTTON") && !line.StartsWith("//") && !line.StartsWith("CHOICE") && !line.StartsWith("MAINMENU") && line2.Length > 1)
                        {
                            if (FontText == "")
                            {
                                EnterText(line2[0], 50, 0);
                            }
                            else
                            {
                                if (line2[0].StartsWith("--"))
                                {
                                    EnterText(line2[0].Substring(2), 50, 0);
                                }
                                else
                                {
                                    EnterText("\n" + line2[0], 50, 0);
                                }
                            }
                            await Task.Delay(line2[0].Length * 70 + int.Parse(line2[1]));
                        }
                        else if (line == "CLEAR")
                        {
                            FontText = "";
                        }
                        else if (line == "CLEARBUTTONS")
                        {
                            ClearButtons(Content);
                        }
                        else if (line.StartsWith("BUTTONTEXT1"))
                        {
                            TextButton1 = Content.Load<Texture2D>("Images/TextButton");
                            TextButton1Text = line.Substring(12);
                        }
                        else if (line.StartsWith("BUTTONTEXT2"))
                        {
                            TextButton2 = Content.Load<Texture2D>("Images/TextButton");
                            TextButton2Text = line.Substring(12);
                        }
                        else if (line.StartsWith("BUTTONTEXT3"))
                        {
                            TextButton3 = Content.Load<Texture2D>("Images/TextButton");
                            TextButton3Text = line.Substring(12);
                        }
                        else if (line.StartsWith("BUTTONTEXT4"))
                        {
                            TextButton4 = Content.Load<Texture2D>("Images/TextButton");
                            TextButton4Text = line.Substring(12);
                        }
                        else if (line.StartsWith("IFBUTTON"))
                        {
                            ChoiceNumber = int.Parse(line.Substring(10));
                            break;
                        }
                        else if (line.StartsWith("CHOICE"))
                        {
                            ChoiceNumber = int.Parse(line.Substring(7));
                            break;
                        }
                    }
                }

                if (ChoiceNumber == 0 && i == 2 && InMainMenu)
                {
                    InMainMenu = false;
                    InEditorMode = true;
                }

                if (ChoiceNumber != 0 && i == 1 && !InMainMenu || ChoiceNumber != 0 && i == 2 && !InMainMenu || ChoiceNumber != 0 && i == 3 && !InMainMenu || ChoiceNumber != 0 && i == 4 && !InMainMenu)
                {
                    var lines = File.ReadLines("script/script.txt");
                    foreach (var line in lines)
                    {
                        if (buttonhit)
                        {
                            string[] line2 = line.Split('|');
                            if (line != "CLEAR" && line != "CLEARBUTTONS" && !line.StartsWith("BUTTONTEXT") && line != "PROMPT" && !line.StartsWith("IFBUTTON") && !line.StartsWith("//") && !line.StartsWith("CHOICE") && !line.StartsWith("MAINMENU") && line2.Length == 1)
                            {
                                if (FontText == "")
                                {
                                    EnterText(line2[0], 50, 0);
                                }
                                else
                                {
                                    if (line2[0].StartsWith("--"))
                                    {
                                        EnterText(line2[0].Substring(2), 50, 0);
                                    }
                                    else
                                    {
                                        EnterText("\n" + line2[0], 50, 0);
                                    }
                                }
                                await Task.Delay(line2[0].Length * 70);
                            }
                            else if (line != "CLEAR" && line != "CLEARBUTTONS" && !line.StartsWith("BUTTONTEXT") && line != "PROMPT" && !line.StartsWith("IFBUTTON") && !line.StartsWith("//") && !line.StartsWith("CHOICE") && !line.StartsWith("MAINMENU") && line2.Length > 1)
                            {
                                if (FontText == "")
                                {
                                    EnterText(line2[0], 50, 0);
                                }
                                else
                                {
                                    if (line2[0].StartsWith("--"))
                                    {
                                        EnterText(line2[0].Substring(2), 50, 0);
                                    }
                                    else
                                    {
                                        EnterText("\n" + line2[0], 50, 0);
                                    }
                                }
                                await Task.Delay(line2[0].Length * 70 + int.Parse(line2[1]));
                            }
                            else if (line == "CLEAR")
                            {
                                FontText = "";
                            }
                            else if (line == "CLEARBUTTONS")
                            {
                                ClearButtons(Content);
                            }
                            else if (line.StartsWith("BUTTONTEXT1"))
                            {
                                TextButton1 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton1Text = line.Substring(12);
                            }
                            else if (line.StartsWith("BUTTONTEXT2"))
                            {
                                TextButton2 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton2Text = line.Substring(12);
                            }
                            else if (line.StartsWith("BUTTONTEXT3"))
                            {
                                TextButton3 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton3Text = line.Substring(12);
                            }
                            else if (line.StartsWith("BUTTONTEXT4"))
                            {
                                TextButton4 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton4Text = line.Substring(12);
                            }
                            else if (line.StartsWith("IFBUTTON"))
                            {
                                ChoiceNumber = int.Parse(line.Substring(10));
                                break;
                            }
                            else if (line.StartsWith("CHOICE"))
                            {
                                ChoiceNumber = int.Parse(line.Substring(7));
                                break;
                            }
                            else if (line.StartsWith("MAINMENU"))
                            {
                                ChoiceNumber = 0;
                                InMainMenu = true;
                                TextButton1 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton2 = Content.Load<Texture2D>("Images/TextButton");
                                TextButton1Text = "Start";
                                TextButton2Text = "Editor Mode";
                                SkipMainMenuFalse = true;
                                FontText = "";
                                StreamReader sr = new StreamReader("script/title.txt");
                                string lineread = sr.ReadLine();
                                sr.Close();
                                EnterText("Welcome to the Text-O-Matic 2000.\nCurrent adventure: " + lineread + ".", 10);
                                break;
                            }
                        }
                        if (i == 1)
                        {
                            if (line.StartsWith("IFBUTTON1") && ChoiceNumber == int.Parse(line.Substring(10)))
                            {
                                buttonhit = true;
                            }
                        }
                        else if (i == 2)
                        {
                            if (line.StartsWith("IFBUTTON2") && ChoiceNumber == int.Parse(line.Substring(10)))
                            {
                                buttonhit = true;
                            }
                        }
                        else if (i == 3)
                        {
                            if (line.StartsWith("IFBUTTON3") && ChoiceNumber == int.Parse(line.Substring(10)))
                            {
                                buttonhit = true;
                            }
                        }
                        else if (i == 4)
                        {
                            if (line.StartsWith("IFBUTTON4") && ChoiceNumber == int.Parse(line.Substring(10)))
                            {
                                buttonhit = true;
                            }
                        }
                    }
                }
                if (!SkipMainMenuFalse)
                {
                    InMainMenu = false;
                }
                SkipMainMenuFalse = false;
            }
        }

        //TextButton1 is normal text button state. TextButton2 is mouse hovering over state. TextButton3 is mouseclick state. TextButton4 is greyed out button state. TextButton 5 is grey button clicked state.

        static void CheckButton1(MouseState state, Microsoft.Xna.Framework.Content.ContentManager Content, MouseState previousState)
        {
            if (state.X >= TextButton1X && state.X <= TextButton1X + 280 && state.Y > TextButton1Y && state.Y <= TextButton1Y + 150)
            {
                if (TextButton1 != Content.Load<Texture2D>("Images/TextButton4") && TextButton1 != Content.Load<Texture2D>("Images/TextButton5"))
                {
                    if (state.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                    {
                        CheckStory(1, Content);
                    }
                    else if (state.LeftButton == ButtonState.Pressed)
                    {
                        TextButton1 = Content.Load<Texture2D>("Images/TextButton3");
                    }
                    else
                    {
                        TextButton1 = Content.Load<Texture2D>("Images/TextButton2");
                    }
                }
                else if (TextButton1 == Content.Load<Texture2D>("Images/TextButton5") && state.LeftButton == ButtonState.Released)
                {
                    TextButton1 = Content.Load<Texture2D>("Images/TextButton4");
                }
                else if (TextButton1 == Content.Load<Texture2D>("Images/TextButton4") && state.LeftButton == ButtonState.Pressed)
                {
                    TextButton1 = Content.Load<Texture2D>("Images/TextButton5");
                }
            }
            else if (TextButton1 != Content.Load<Texture2D>("Images/TextButton4") && TextButton1 != Content.Load<Texture2D>("Images/TextButton5"))
            {
                TextButton1 = Content.Load<Texture2D>("Images/TextButton");
            }
        }

        static void CheckButton2(MouseState state, Microsoft.Xna.Framework.Content.ContentManager Content, MouseState previousState)
        {
            if (state.X >= TextButton2X && state.X <= TextButton2X + 280 && state.Y > TextButton2Y && state.Y <= TextButton2Y + 150)
            {
                if (TextButton2 != Content.Load<Texture2D>("Images/TextButton4") && TextButton2 != Content.Load<Texture2D>("Images/TextButton5"))
                {
                    if (state.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                    {
                        CheckStory(2, Content);
                    }
                    else if (state.LeftButton == ButtonState.Pressed)
                    {
                        TextButton2 = Content.Load<Texture2D>("Images/TextButton3");
                    }
                    else
                    {
                        TextButton2 = Content.Load<Texture2D>("Images/TextButton2");
                    }
                }
                else if (TextButton2 == Content.Load<Texture2D>("Images/TextButton5") && state.LeftButton == ButtonState.Released)
                {
                    TextButton2 = Content.Load<Texture2D>("Images/TextButton4");
                }
                else if (TextButton2 == Content.Load<Texture2D>("Images/TextButton4") && state.LeftButton == ButtonState.Pressed)
                {
                    TextButton2 = Content.Load<Texture2D>("Images/TextButton5");
                }
            }
            else if (TextButton2 != Content.Load<Texture2D>("Images/TextButton4") && TextButton2 != Content.Load<Texture2D>("Images/TextButton5"))
            {
                TextButton2 = Content.Load<Texture2D>("Images/TextButton");
            }
        }

        static void CheckButton3(MouseState state, Microsoft.Xna.Framework.Content.ContentManager Content, MouseState previousState)
        {
            if (state.X >= TextButton3X && state.X <= TextButton3X + 280 && state.Y > TextButton3Y && state.Y <= TextButton3Y + 150)
            {
                if (TextButton3 != Content.Load<Texture2D>("Images/TextButton4") && TextButton3 != Content.Load<Texture2D>("Images/TextButton5"))
                {
                    if (state.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                    {
                        CheckStory(3, Content);
                    }
                    else if (state.LeftButton == ButtonState.Pressed)
                    {
                        TextButton3 = Content.Load<Texture2D>("Images/TextButton3");
                    }
                    else
                    {
                        TextButton3 = Content.Load<Texture2D>("Images/TextButton2");
                    }
                }
                else if (TextButton3 == Content.Load<Texture2D>("Images/TextButton5") && state.LeftButton == ButtonState.Released)
                {
                    TextButton3 = Content.Load<Texture2D>("Images/TextButton4");
                }
                else if (TextButton3 == Content.Load<Texture2D>("Images/TextButton4") && state.LeftButton == ButtonState.Pressed)
                {
                    TextButton3 = Content.Load<Texture2D>("Images/TextButton5");
                }
            }
            else if (TextButton3 != Content.Load<Texture2D>("Images/TextButton4") && TextButton3 != Content.Load<Texture2D>("Images/TextButton5"))
            {
                TextButton3 = Content.Load<Texture2D>("Images/TextButton");
            }
        }

        static void CheckButton4(MouseState state, Microsoft.Xna.Framework.Content.ContentManager Content, MouseState previousState)
        {
            if (state.X >= TextButton4X && state.X <= TextButton4X + 280 && state.Y > TextButton4Y && state.Y <= TextButton4Y + 150)
            {
                if (TextButton4 != Content.Load<Texture2D>("Images/TextButton4") && TextButton4 != Content.Load<Texture2D>("Images/TextButton5"))
                {
                    if (state.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                    {
                        CheckStory(4, Content);
                    }
                    else if (state.LeftButton == ButtonState.Pressed)
                    {
                        TextButton4 = Content.Load<Texture2D>("Images/TextButton3");
                    }
                    else
                    {
                        TextButton4 = Content.Load<Texture2D>("Images/TextButton2");
                    }
                }
                else if (TextButton4 == Content.Load<Texture2D>("Images/TextButton5") && state.LeftButton == ButtonState.Released)
                {
                    TextButton4 = Content.Load<Texture2D>("Images/TextButton4");
                }
                else if (TextButton4 == Content.Load<Texture2D>("Images/TextButton4") && state.LeftButton == ButtonState.Pressed)
                {
                    TextButton4 = Content.Load<Texture2D>("Images/TextButton5");
                }
            }
            else if (TextButton4 != Content.Load<Texture2D>("Images/TextButton4") && TextButton4 != Content.Load<Texture2D>("Images/TextButton5"))
            {
                TextButton4 = Content.Load<Texture2D>("Images/TextButton");
            }
        }

        static void ClearButtons(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            TextButton1Text = "";
            TextButton2Text = "";
            TextButton3Text = "";
            TextButton4Text = "";
            TextButton1 = Content.Load<Texture2D>("Images/TextButton4");
            TextButton2 = Content.Load<Texture2D>("Images/TextButton4");
            TextButton3 = Content.Load<Texture2D>("Images/TextButton4");
            TextButton4 = Content.Load<Texture2D>("Images/TextButton4");
        }
    }
}

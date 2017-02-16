using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2
{
    //TODO comment like a boss
    public partial class Form1 : Form
    {
        SoundPlayer meow;
        SoundPlayer hit;
        SoundPlayer mouse;
        SoundPlayer select;

        private BoopController myBoopController;
        private TimerController myTimerController;
        private ScoreController myScoreController;

        string catPawPointer = "\\tehcatspaw64.ico";
        string highScoreFileLocation = "\\HighScores.txt";

        string EASY = "EASY";
        string NOTEASY = "NOT EASY";
        string IMPOSSIBLE = "IMPOSSIBLE";
        string[] scores = { "First", "Second", "Third" };
        int _SizeX;
        int _SizeY;

        private Button[,] _MoleButtons = new Button[4, 4];
        

        public Form1()
        {
            InitializeComponent();

            #if (DEBUG)
                label1.Visible = true;
            #endif

            _SizeX = this.Width;
            _SizeY = this.Height;

            this.Cursor = new Cursor(Application.StartupPath + catPawPointer);

            meow = new SoundPlayer(Properties.Resources.meow);
            hit = new SoundPlayer(Properties.Resources.hit);
            mouse = new SoundPlayer(Properties.Resources.mouse);
            select = new SoundPlayer(Properties.Resources.select);

            myBoopController = new BoopController();
            myTimerController = new TimerController();
            myScoreController = new ScoreController();

            _MoleButtons = new Button[,]

            {
                { moleButton0, moleButton1, moleButton2, moleButton3 },
                { moleButton4, moleButton5, moleButton6, moleButton7 },
                { moleButton8, moleButton9, moleButton10, moleButton11 },
                { moleButton12, moleButton13, moleButton14, moleButton15 }
            };

            int i =  0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    BoopData bd = new BoopData();
                    bd.X = x;
                    bd.Y = y;
                    bd.I = i;
                    _MoleButtons[x, y].Tag = bd;
                    i++;
                }
            
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            easy();
            gameTimeControl.Interval = myTimerController.TimerInterval;
            gameTimeLabel.Text = myTimerController.GameTimeCounter.ToString();  
            meow.Play();
            loadScores();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            mouse.Play();
            myTimerController.ResetGameTime();
            myScoreController.Reset();//resets the score to 0
            scoreLabel.Text = myScoreController.Score.ToString();//applies the score to the score label
            gameTimeControl.Start();//starts the games game-length timer
            startButton.Enabled = false;//disables the start button for the duration of the game
            easyButton.Enabled = false;
            notEasyButton.Enabled = false;
            impossibleButton.Enabled = false;
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            select.Play();
            easy();
        }

        private void notEasyButton_Click(object sender, EventArgs e)
        {
            select.Play();
            notEasy();
        }

        private void impossibleButton_Click(object sender, EventArgs e)
        {
            select.Play();
            impossible();            
        }

        private void boopScore()
        {
            hit.Play();
            myScoreController.Score++;//increments the score one point
            scoreLabel.Text = myScoreController.Score.ToString();//posts the score in the label
        }

        private void easy()
        {
            myScoreController.EasySelected = true;
            myScoreController.NotEasySelected = false;
            myScoreController.ImpossibleSelected = false;
            myTimerController.SetDifficultyEasy();
            difficultyLabel.Text = EASY;
            myScoreController.Reset();
            scoreLabel.Text = myScoreController.Score.ToString();
            highScoreLabel.Text = myScoreController.HighScore().ToString();
        }

        private void notEasy()
        {
            myScoreController.EasySelected = false;
            myScoreController.NotEasySelected = true;
            myScoreController.ImpossibleSelected = false;
            myTimerController.SetDifficultyNotEasy();
            difficultyLabel.Text = NOTEASY;
            myScoreController.Reset();
            scoreLabel.Text = myScoreController.Score.ToString();
            highScoreLabel.Text = myScoreController.HighScore().ToString();   
        }

        private void impossible()
        {
            myScoreController.EasySelected = false;
            myScoreController.NotEasySelected = false;
            myScoreController.ImpossibleSelected = true;
            myTimerController.SetDifficultyImpossible();
            difficultyLabel.Text = IMPOSSIBLE;
            myScoreController.Reset();
            scoreLabel.Text = myScoreController.Score.ToString();
            highScoreLabel.Text = myScoreController.HighScore().ToString();
        }

        private void loadScores()
        {
            try
            {
                scores = System.IO.File.ReadAllLines(Application.StartupPath + highScoreFileLocation);
                myScoreController.EASY_HIGH_SCORE = Int32.Parse(scores[0]);
                myScoreController.NOT_EASY_HIGH_SCORE = Int32.Parse(scores[1]);
                myScoreController.IMPOSSIBLE_HIGH_SCORE = Int32.Parse(scores[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading high scores; Erroneous scores will be reset. See log.txt for details.");
                System.IO.File.WriteAllText(Application.StartupPath + "\\log.txt", ex.Message);
                for (int i = 0; i <= scores.Length; i++)
                {
                    scores[i] = "0";
                }
            }

            highScoreLabel.Text = myScoreController.HighScore().ToString();
        }

        #region Timer Controls

        private void gameTimeControl_Tick(object sender, EventArgs e)
        {
            gameTimeLabel.Text = myTimerController.GameTimeCounter.ToString();

            myTimerController.GameTimeCounter--;
            myTimerController.MoleSpawnControl++;

            if (myTimerController.GameTimeCounter > myTimerController.StopSpawn)
            {
                if (myTimerController.MoleSpawnControl == myTimerController.MoleSpawnFrequency)
                {
                    #region Mouse Selector

                    switch (myBoopController.MoleSelect)
                    {
                        case 0:
                            if (!myBoopController.Mole0)
                            {
                                myBoopController.Mole0 = true;                                
                            }
                            break;
                        case 1:
                            if (!myBoopController.Mole1)
                            {
                                myBoopController.Mole1 = true;
                            }
                            break;
                        case 2:
                            if (!myBoopController.Mole2)
                            {
                                myBoopController.Mole2 = true;
                            }
                            break;
                        case 3:
                            if (!myBoopController.Mole3)
                            {
                                myBoopController.Mole3 = true;
                            }
                            break;
                        case 4:
                            if (!myBoopController.Mole4)
                            {
                                myBoopController.Mole4 = true;
                            }
                            break;
                        case 5:
                            if (!myBoopController.Mole5)
                            {
                                myBoopController.Mole5 = true;
                            }
                            break;
                        case 6:
                            if (!myBoopController.Mole6)
                            {
                                myBoopController.Mole6 = true;
                            }
                            break;
                        case 7:
                            if (!myBoopController.Mole7)
                            {
                                myBoopController.Mole7 = true;
                            }
                            break;
                        case 8:
                            if (!myBoopController.Mole8)
                            {
                                myBoopController.Mole8 = true;
                            }
                            break;
                        case 9:
                            if (!myBoopController.Mole9)
                            {
                                myBoopController.Mole9 = true;
                            }
                            break;
                        case 10:
                            if (!myBoopController.Mole10)
                            {
                                myBoopController.Mole10 = true;
                            }
                            break;
                        case 11:
                            if (!myBoopController.Mole11)
                            {
                                myBoopController.Mole11 = true;
                            }
                            break;
                        case 12:
                            if (!myBoopController.Mole12)
                            {
                                myBoopController.Mole12 = true;
                            }
                            break;
                        case 13:
                            if (!myBoopController.Mole13)
                            {
                                myBoopController.Mole13 = true;
                            }
                            break;
                        case 14:
                            if (!myBoopController.Mole14)
                            {
                                myBoopController.Mole14 = true;
                            }
                            break;
                        case 15:
                            if (!myBoopController.Mole15)
                            {
                                myBoopController.Mole15 = true;
                            }
                            break;
                        default:
                            break;
                    }

                    #endregion

                    myTimerController.MoleSpawnControl = myTimerController.Reset;
                }
            }
            else if (myTimerController.GameTimeCounter < myTimerController.Reset)
            {
                startButton.Enabled = true;
                easyButton.Enabled = true;
                notEasyButton.Enabled = true;
                impossibleButton.Enabled = true;
                myTimerController.MoleSpawnControl = myTimerController.Reset;
                highScoreLabel.Text = myScoreController.HighScore().ToString();

                try
                {
                    scores[0] = myScoreController.EASY_HIGH_SCORE.ToString();
                    scores[1] = myScoreController.NOT_EASY_HIGH_SCORE.ToString();
                    scores[2] = myScoreController.IMPOSSIBLE_HIGH_SCORE.ToString();
                    System.IO.File.WriteAllLines(Application.StartupPath + highScoreFileLocation, scores);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while saving high scores; Old Scores will be kept. See log.txt for details.");
                    System.IO.File.WriteAllText(Application.StartupPath + "\\log.txt", ex.Message);
                }

                gameTimeControl.Stop();
                meow.Play();
            }

            #region Mice Counters

            if (myBoopController.Mole0)
            {
                myTimerController.MoleCounter0++;
                moleButton0.Enabled = true;
                moleButton0.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter0 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter0 = myTimerController.Reset;
                    moleButton0.Enabled = false;
                    moleButton0.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole0 = false;
                }
            }

            if (myBoopController.Mole1)
            {
                myTimerController.MoleCounter1++;
                moleButton1.Enabled = true;
                moleButton1.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter1 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter1 = myTimerController.Reset;
                    moleButton1.Enabled = false;
                    moleButton1.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole1 = false;
                }
            }

            if (myBoopController.Mole2)
            {
                myTimerController.MoleCounter2++;
                moleButton2.Enabled = true;
                moleButton2.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter2 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter2 = myTimerController.Reset;
                    moleButton2.Enabled = false;
                    moleButton2.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole2 = false;
                }
            }

            if (myBoopController.Mole3)
            {
                myTimerController.MoleCounter3++;
                moleButton3.Enabled = true;
                moleButton3.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter3 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter3 = myTimerController.Reset;
                    moleButton3.Enabled = false;
                    moleButton3.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole3 = false;
                }
            }

            if (myBoopController.Mole4)
            {
                myTimerController.MoleCounter4++;
                moleButton4.Enabled = true;
                moleButton4.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter4 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter4 = myTimerController.Reset;
                    moleButton4.Enabled = false;
                    moleButton4.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole4 = false;
                }
            }

            if (myBoopController.Mole5)
            {
                myTimerController.MoleCounter5++;
                moleButton5.Enabled = true;
                moleButton5.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter5 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter5 = myTimerController.Reset;
                    moleButton5.Enabled = false;
                    moleButton5.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole5 = false;
                }
            }

            if (myBoopController.Mole6)
            {
                myTimerController.MoleCounter6++;
                moleButton6.Enabled = true;
                moleButton6.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter6 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter6 = myTimerController.Reset;
                    moleButton6.Enabled = false;
                    moleButton6.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole6 = false;
                }
            }

            if (myBoopController.Mole7)
            {
                myTimerController.MoleCounter7++;
                moleButton7.Enabled = true;
                moleButton7.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter7 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter7 = myTimerController.Reset;
                    moleButton7.Enabled = false;
                    moleButton7.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole7 = false;
                }
            }

            if (myBoopController.Mole8)
            {
                myTimerController.MoleCounter8++;
                moleButton8.Enabled = true;
                moleButton8.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter8 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter8 = myTimerController.Reset;
                    moleButton8.Enabled = false;
                    moleButton8.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole8 = false;
                }
            }

            if (myBoopController.Mole9)
            {
                myTimerController.MoleCounter9++;
                moleButton9.Enabled = true;
                moleButton9.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter9 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter9 = myTimerController.Reset;
                    moleButton9.Enabled = false;
                    moleButton9.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole9 = false;
                }
            }

            if (myBoopController.Mole10)
            {
                myTimerController.MoleCounter10++;
                moleButton10.Enabled = true;
                moleButton10.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter10 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter10 = myTimerController.Reset;
                    moleButton10.Enabled = false;
                    moleButton10.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole10 = false;
                }
            }

            if (myBoopController.Mole11)
            {
                myTimerController.MoleCounter11++;
                moleButton11.Enabled = true;
                moleButton11.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter11 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter11 = myTimerController.Reset;
                    moleButton11.Enabled = false;
                    moleButton11.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole11 = false;
                }
            }

            if (myBoopController.Mole12)
            {
                myTimerController.MoleCounter12++;
                moleButton12.Enabled = true;
                moleButton12.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter12 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter12 = myTimerController.Reset;
                    moleButton12.Enabled = false;
                    moleButton12.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole12 = false;
                }
            }

            if (myBoopController.Mole13)
            {
                myTimerController.MoleCounter13++;
                moleButton13.Enabled = true;
                moleButton13.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter13 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter13 = myTimerController.Reset;
                    moleButton13.Enabled = false;
                    moleButton13.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole13 = false;
                }
            }

            if (myBoopController.Mole14)
            {
                myTimerController.MoleCounter14++;
                moleButton14.Enabled = true;
                moleButton14.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter14 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter14 = myTimerController.Reset;
                    moleButton14.Enabled = false;
                    moleButton14.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole14 = false;
                }
            }

            if (myBoopController.Mole15)
            {
                myTimerController.MoleCounter15++;
                moleButton15.Enabled = true;
                moleButton15.BackgroundImage = Properties.Resources.mouse_out_the_hole;

                if (myTimerController.MoleCounter15 > myTimerController.MoleActiveTime)
                {
                    myTimerController.MoleCounter15 = myTimerController.Reset;
                    moleButton15.Enabled = false;
                    moleButton15.BackgroundImage = Properties.Resources.mouse_in_the_hole;
                    myBoopController.Mole15 = false;
                }
            }

            #endregion

        }

        #endregion

        #region Mouse Click Handlers

        private void mouseButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                // Optionally complain loudly, log, etc
                return;
            }
            // Otherwise DoStuff™
            b.Enabled = false;
            b.BackgroundImage = Properties.Resources.mouse_in_the_hole;
            BoopData bd = b.Tag as BoopData;
            if (bd != null)
            {//EVIIIIIIIL * Insert Manic laugh here * -- Refactor to use Arrays to List<>
                System.Reflection.PropertyInfo prop = typeof(TimerController).GetProperty("MoleCounter" + bd.I.ToString());
                System.Reflection.MethodInfo moleAccess = prop.GetSetMethod();
                moleAccess.Invoke(myTimerController, new object[] { myTimerController.Reset });


                prop = typeof(BoopController).GetProperty("Mole" + bd.I.ToString());
                moleAccess = prop.GetSetMethod();
                moleAccess.Invoke(myBoopController, new object[] { false });

                label1.Text = string.Format("{0} - {1}", bd.X, bd.Y);
            }
            boopScore();
        } 

        #endregion

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Width = _SizeX;
            this.Height = _SizeY;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class TimerController
    {
        int TIMER_INTERVAL = 10;
        int RESET = 0;
        int GAME_TIME = 1000;
        int GAME_TIME_COUNTER = 1000;
        int MOLE_SPAWN_CONTROL = 0;

        int STOP_SPAWN = 0;
        int MOLE_ACTIVE_TIME = 0;
        int MOLE_SPAWN_FREQUENCY = 0;

        int STOP_SPAWN_EASY = 100;
        int STOP_SPAWN_NOT_EASY = 40;
        int STOP_SPAWN_IMPOSSIBLE = 30;
        int MOLE_ACTIVE_TIME_EASY = 100;
        int MOLE_ACTIVE_TIME_NOT_EASY = 40;
        int MOLE_ACTIVE_TIME_IMPOSSIBLE = 30;
        int MOLE_SPAWN_FREQUENCY_EASY = 10;
        int MOLE_SPAWN_FREQUENCY_NOT_EASY = 25;
        int MOLE_SPAWN_FREQUENCY_IMPOSSIBLE = 5;

        public void SetDifficultyEasy()
        {
            STOP_SPAWN = STOP_SPAWN_EASY;
            MOLE_ACTIVE_TIME = MOLE_ACTIVE_TIME_EASY;
            MOLE_SPAWN_FREQUENCY = MOLE_SPAWN_FREQUENCY_EASY;
        }

        public void SetDifficultyNotEasy()
        {
            STOP_SPAWN = STOP_SPAWN_NOT_EASY;
            MOLE_ACTIVE_TIME = MOLE_ACTIVE_TIME_NOT_EASY;
            MOLE_SPAWN_FREQUENCY = MOLE_SPAWN_FREQUENCY_NOT_EASY;
        }

        public void SetDifficultyImpossible()
        {
            STOP_SPAWN = STOP_SPAWN_IMPOSSIBLE;
            MOLE_ACTIVE_TIME = MOLE_ACTIVE_TIME_IMPOSSIBLE;
            MOLE_SPAWN_FREQUENCY = MOLE_SPAWN_FREQUENCY_IMPOSSIBLE;
        }

        public void ResetGameTime()
        {
            GAME_TIME_COUNTER = GAME_TIME;
        }

        public int StopSpawn
        {
            set { STOP_SPAWN = value; }
            get { return STOP_SPAWN; }
        }

        public int TimerInterval
        {
            set { TIMER_INTERVAL = value; }
            get { return TIMER_INTERVAL; }
        }

        public int Reset
        {
            set { RESET = value; }
            get { return RESET; }
        }

        public int GameTimeComparator
        {
            set { GAME_TIME = value; }
            get { return GAME_TIME; }
        }

        public int MoleSpawnFrequency
        {
            set { MOLE_SPAWN_FREQUENCY = value; }
            get { return MOLE_SPAWN_FREQUENCY; }
        }

        public int MoleActiveTime
        {
            set { MOLE_ACTIVE_TIME = value; }
            get { return MOLE_ACTIVE_TIME; }
        }

        public int GameTimeCounter
        {
            set { GAME_TIME_COUNTER = value; }
            get { return GAME_TIME_COUNTER; }
        }

        public int MoleSpawnControl
        {
            set { MOLE_SPAWN_CONTROL = value; }
            get { return MOLE_SPAWN_CONTROL; }
        }

        public int MoleCounter0 { set; get;}

        public int MoleCounter1 { set; get;}

        public int MoleCounter2 { set; get;}

        public int MoleCounter3 { set; get;}

        public int MoleCounter4 { set; get;}

        public int MoleCounter5 { set; get;}

        public int MoleCounter6 { set; get;}

        public int MoleCounter7 { set; get;}

        public int MoleCounter8 { set; get;}

        public int MoleCounter9 { set; get;}

        public int MoleCounter10 { set; get;}

        public int MoleCounter11 { set; get;}

        public int MoleCounter12 { set; get; }

        public int MoleCounter13 { set; get; }

        public int MoleCounter14 { set; get; }

        public int MoleCounter15 { set; get; }
    }
}

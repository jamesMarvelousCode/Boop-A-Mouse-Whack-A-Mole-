using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class ScoreController
    {
        int SCORE = 0;
        int RESET = 0;

        public bool EasySelected { set; get; }
        public bool NotEasySelected { set; get; }
        public bool ImpossibleSelected { set; get; }

        public int EASY_HIGH_SCORE { set; get; }
        public int NOT_EASY_HIGH_SCORE { set; get; }
        public int IMPOSSIBLE_HIGH_SCORE { set; get; }

        public void Reset()
        {
            SCORE = RESET;
        }

        public int Score
        {
            set { SCORE = value; }
            get { return SCORE; }
        }

        public int HighScore()
        {
            if (EasySelected)
            {
                if (SCORE > EASY_HIGH_SCORE)
                {
                    EASY_HIGH_SCORE = SCORE;
                }
                return EASY_HIGH_SCORE;
            }
            else if (NotEasySelected)
            {
                if (SCORE > NOT_EASY_HIGH_SCORE)
                {
                    NOT_EASY_HIGH_SCORE = SCORE;
                }
                return NOT_EASY_HIGH_SCORE;
            }
            else if (ImpossibleSelected)
            {
                if (SCORE > IMPOSSIBLE_HIGH_SCORE)
                {
                    IMPOSSIBLE_HIGH_SCORE = SCORE;
                }
                return IMPOSSIBLE_HIGH_SCORE;
            }
            return RESET;
        }     
    }
}

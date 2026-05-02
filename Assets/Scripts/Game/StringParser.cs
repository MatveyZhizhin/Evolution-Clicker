using System;

namespace Game
{
    public static class StringParser
    {

        public static string ParseFloatToShortString(float value, int digits)
        {
            char suffix = ' ';

            if (value >= 1000 && value < 1000000)
            {
                value /= 1000;
                suffix = 'K';
            }
            else if (value >= 1000000 && value < 1000000000)
            {
                value /= 1000000;
                suffix = 'M';
            }
            else if (value >= 1000000000 && value < 1000000000000)
            {
                value /= 1000000000;
                suffix = 'B';
            }
            else if (value >= 1000000000000 && value < 1000000000000000)
            {
                value /= 1000000000000;
                suffix = 'T';
            }
            else if (value >= 1000000000000000)
            {
                value /= 1000000000000000;
                suffix = 'q';
            }

<<<<<<< Updated upstream
=======
            if (suffix == ' ')
                return value.ToString();

>>>>>>> Stashed changes
            return Math.Round(value, digits).ToString() + suffix;
        }
    }
}

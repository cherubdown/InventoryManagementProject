using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementProject
{
    public static class CommonUtilities
    {
        /// <summary>
        /// Asks the user for a common Y/N and converts to boolean (Yes is true, No is false)
        /// If the user does not press Y or N (or y or n) this function will loop until they do
        /// Exiting out of this is not an option
        /// </summary>
        /// <returns>A boolean that represents Yes or No</returns>
        public static bool ReadUserKeyAndDetermineYesOrNo()
        {
            char keyPressed = Console.ReadKey().KeyChar;
            if (keyPressed == 'y')
            {
                return true;
            }
            else if (keyPressed == 'n')
            {
                return false;
            }
            else
            {
                Console.WriteLine("That is not a valid response. Please enter Y or N (or y or n) for Yes or No respectively.");
                return ReadUserKeyAndDetermineYesOrNo();
            }
        }
    }
}

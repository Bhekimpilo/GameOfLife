using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IValidation
    {
        string ValidateResponse(string response);

        string ValidateOption(string option);

        int ValidateGuess(string guess);

        bool ValidateEnterKey(ConsoleKeyInfo key);
    }
}

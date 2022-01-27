using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComApplication
{
    class MainUnit
    {
        // на старте программы
        public static bool OnProgramStart()
        {
            OUnit.OptionsInit();
            return (true);
        }

        // на закрытии программы
        public static bool OnProgramClose()
        {
            OUnit.SaveOptions();
            return (true);
        }

    }
}

using System;

namespace ConsoleApplication1
{
    [Flags]
    public enum TypeSynchro
    {
        Up = 1,
        Down = 2,
        Bidirectionnal = 4,
        Deletion = 8
    }
}

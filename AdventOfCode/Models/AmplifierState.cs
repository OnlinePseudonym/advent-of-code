using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Models
{
    class AmplifierState
    {
        public int Phase { get; set; }
        public bool PhaseIntialized { get; set; }
        public IList<int> Memory { get; set; }
        public int PointerPosition { get; set; }

        public AmplifierState(int phase, bool isIntialized, int pointerPosition, IList<int> memory)
        {
            Phase = phase;
            PhaseIntialized = isIntialized;
            PointerPosition = pointerPosition;
            Memory = memory;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Models
{
    public class IntcodeComputer
    {
        private IList<int> Memory { get; set; }
        public IList<int> Input { get; set; }
        public int Output { get; set; }

        public IntcodeComputer(List<int> input)
        {
            Input = input;
        }

        public IList<int> CalculateOutput()
        {
            InitilizeMemory();

            var i = 0;
            while (i < Memory.Count)
            {
                var instruction = new Instruction(Memory, i);
                instruction.ProcessInstruction();

                if (instruction.KillProgram) break;
                i = instruction.GetPointerPosition();
            };

            return Memory;
        }

        public IList<int> CalculateOutput(int phase, int input)
        {
            InitilizeMemory();
            var i = 0;
            var isPhaseInitilized = false;

            while (i < Memory.Count)
            {
                var instructionInput = isPhaseInitilized ? input : phase;

                var instruction = new Instruction(Memory, i);
                instruction.ProcessInstruction(instructionInput);

                if (instruction.KillProgram) break;
                if (instruction.Output != 0) Output = instruction.Output;
                if (instruction.PhaseInitialized) isPhaseInitilized = true;

                i = instruction.GetPointerPosition();
            };

            return Memory;
        }

        public void InitilizeMemory()
        {
            Memory = Input.Select(x => x).ToList();
        }

        private void RestoreProgramState(int noun, int verb)
        {
            Memory[1] = noun;
            Memory[2] = verb;
        }

        public int FindInputsForOutput(int targetOutput)
        {
            for (var noun = 0; noun < 99; noun++)
            {
                for (var verb = 0; verb < 99; verb++)
                {
                    InitilizeMemory();
                    RestoreProgramState(noun, verb);
                    var output = CalculateOutput().First();

                    if (targetOutput == output) return 100 * noun + verb;
                }
            }

            return -1;
        }
    }
}

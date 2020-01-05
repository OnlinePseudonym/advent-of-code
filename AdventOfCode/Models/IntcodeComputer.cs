using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Models
{
    class IntcodeComputer
    {
        private IList<int> Memory;
        private IList<int> Input;
        private int MaxOutput;
        private Queue<AmplifierState> _queue = new Queue<AmplifierState>();
        public int Output { get; private set; }

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

        public int CalculateOutput(int phase, int input)
        {
            InitilizeMemory();
            var i = 0;
            bool isPhaseInitialized = false;

            while (i < Memory.Count)
            {
                var instructionInput = isPhaseInitialized ? input : phase;
                var instruction = new Instruction(Memory, i, instructionInput);

                var isOutput = instruction.ProcessInstruction();

                if (isOutput) Output = instruction.Output;
                if (instruction.PhaseInitialized) isPhaseInitialized = true;
                if (instruction.KillProgram) break;
                i = instruction.GetPointerPosition();
            };

            return Output;
        }

        public int GetMaximumAmplifierOutput()
        {
            var permutations = Permutation.HamiltonianPermutations(5);

            foreach(var permutation in permutations)
            {
                RunAmplifiers(permutation);
                if (Output > MaxOutput) MaxOutput = Output;
            }

            return MaxOutput;
        }

        public int GetLoopingAmplifierOutput()
        {
            var permutations = Permutation.HamilitonianPermutations(new List<int>() { 5, 6, 7, 8, 9 });

            foreach (var permutation in permutations)
            {
                Console.WriteLine(permutation.ToString());
                RunAmplifiers(permutation);
                if (Output > MaxOutput) MaxOutput = Output;
            }

            return MaxOutput;
        }

        public void RunAmplifiers(Permutation permutation)
        {
            _queue = new Queue<AmplifierState>();
            Output = 0;
            InitilizeMemory();
            
            foreach(var phase in permutation)
            {
                var memoryState = Memory.Select(x => x).ToList();
                _queue.Enqueue(new AmplifierState(phase, false, 0, memoryState));
            }

            while(_queue.Count > 0)
            {
                var amp = _queue.Dequeue();
                var input = Output;

                CalculateOutputOfAmplifier(amp.Phase, amp.PhaseIntialized, input, amp.PointerPosition, amp.Memory);
            }
        }

        private void CalculateOutputOfAmplifier(int phase, bool phaseInitialized, int input, int pointerPosition, IList<int> memoryState)
        {
            var i = pointerPosition;
            bool isPhaseInitialized = phaseInitialized;
            bool isInputUsed = false;

            while (i < memoryState.Count)
            {
                var instructionInput = isPhaseInitialized ? input : phase;
                var instruction = new Instruction(memoryState, i, phase, input, isPhaseInitialized, isInputUsed);

                var isOutput = instruction.ProcessInstruction();
                i = instruction.GetPointerPosition();

                if (isOutput) Output = instruction.Output;
                if (instruction.KillProgram) break;
                if (instruction.PhaseInitialized) isPhaseInitialized = true;
                if (instruction.InputUsed) isInputUsed = true;
                if (instruction.Paused)
                {
                    _queue.Enqueue(new AmplifierState(phase, isPhaseInitialized, i, memoryState.Select(x => x).ToList()));
                    break;
                }
            }
        }
        // for quick testing of provided inputs
        public int RunAmplifiers(IList<int> permutation)
        {
            _queue = new Queue<AmplifierState>();
            Output = 0;
            InitilizeMemory();

            foreach (var phase in permutation)
            {
                var memoryState = Memory.Select(x => x).ToList();
                _queue.Enqueue(new AmplifierState(phase, false, 0, memoryState));
            }

            while (_queue.Count > 0)
            {
                var amp = _queue.Dequeue();
                var input = Output;
                Console.WriteLine(input);

                CalculateOutputOfAmplifier(amp.Phase, amp.PhaseIntialized, input, amp.PointerPosition, amp.Memory);
                //Console.WriteLine(Output);
            }

            return Output;
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

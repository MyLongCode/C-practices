using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            actions = new Dictionary<char, Action<IVirtualMachine>>(program.Length);
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            if (!actions.ContainsKey(symbol)) actions.Add(symbol, execute);
        }

        private Dictionary<char, Action<IVirtualMachine>> actions;

        public void Run()
        {
            while(InstructionPointer < Instructions.Length)
            {
                if (actions.ContainsKey(Instructions[InstructionPointer]))
                    actions[Instructions[InstructionPointer]](this);
                InstructionPointer++;
            }   
        }
    }
}

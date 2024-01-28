using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
        private static Stack<int> stack = new Stack<int>();
        private static Dictionary<int, int> startEnd = new Dictionary<int, int>();
        private static Dictionary<int, int> endStart = new Dictionary<int, int>();
        public static void RegisterTo(IVirtualMachine vm)
		{
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[') stack.Push(i);
				if (vm.Instructions[i] == ']')
				{
					var index = stack.Pop();
					endStart[i] = index;
					startEnd[index] = i;
				}
			}
			vm.RegisterCommand('[', b => 
			{
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = startEnd[b.InstructionPointer];
            });
			vm.RegisterCommand(']', b => 
			{
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = endStart[b.InstructionPointer];
            });
		}
	}
}
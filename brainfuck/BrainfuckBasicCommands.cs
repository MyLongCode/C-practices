using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
            vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer]++; });
            vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer]--; });
            vm.RegisterCommand('<', b => 
            {
                if (b.MemoryPointer == 0) b.MemoryPointer = b.Memory.Length - 1;
                else b.MemoryPointer--;
            });
            vm.RegisterCommand('>', b => 
            {
                if (b.MemoryPointer == b.Memory.Length - 1) b.MemoryPointer = 0;
                else b.MemoryPointer++;
            });
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
            SaveCode(vm);
        }

        public static string GenerationSymbols()
        {
            var str = new StringBuilder();
            for (char c = 'A'; c <= 'Z'; c++)
                str.Append(c);
            for (char c = 'a'; c <= 'z'; c++)
                str.Append(c);
            for (char c = '0'; c <= '9'; c++)
                str.Append(c);
            return str.ToString();
        }

        public static void SaveCode(IVirtualMachine vm)
        {
            string symbols = GenerationSymbols();
            foreach (var e in symbols)
                vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(e); });
        }
    }
}
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

        public static void SaveCode(IVirtualMachine vm)
        {
            var symbols =
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
            foreach (var e in symbols)
                vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(e); });
        }
    }
}
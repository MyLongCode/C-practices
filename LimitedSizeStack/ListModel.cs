using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class Task<TItem>
{
	public TItem Value;
	public bool TaskFlag;
	public int Position { get; set; } // Когда удаляем элемент, запоминаем его позицию в списке
}

public class ListModel<TItem>
{
	public List<TItem> Items { get; }
	public int UndoLimit;
	private LimitedSizeStack<Task<TItem>> stack;
        
	public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
    }

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = new List<TItem>();
		UndoLimit = undoLimit;
        stack = new LimitedSizeStack<Task<TItem>>(UndoLimit);
    }

	public void AddItem(TItem item)
	{
		var task = new Task<TItem> { Value = item, TaskFlag = true };
		stack.Push(task);
        Items.Add(item);
    }

	public void RemoveItem(int index)
	{
		var task = new Task<TItem> { Value = Items[index], TaskFlag = false, Position = index };
        stack.Push(task);
        Items.RemoveAt(index);
		
	}

	public bool CanUndo()
	{
        return stack.Count != 0;
    }

	public void Undo()
	{
		var task = stack.Pop();
		if (task.TaskFlag) Items.Remove(task.Value);
		else Items.Insert(task.Position, task.Value);
	}
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with priorities 1, 5, and 3.
    // Expected Result: Dequeue should return the item with priority 5.
    // Defect(s) Found: The highest-priority item was not always selected.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Add two items with the same priority and remove them in sequence.
    // Expected Result: The first item added should be removed first, then the second.
    // Defect(s) Found: Priority ties were not preserving FIFO order.
    public void TestPriorityQueue_TieUsesFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("A", first);
        Assert.AreEqual("B", second);
    }

    [TestMethod]
    // Scenario: Add two items, dequeue once, then verify the remaining item is removed next.
    // Expected Result: The first dequeue removes the highest-priority item and the second dequeue returns the remaining item.
    // Defect(s) Found: Dequeue returned an item without removing it from the queue.
    public void TestPriorityQueue_RemovesDequeuedItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 10);
        priorityQueue.Enqueue("B", 5);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("A", first);
        Assert.AreEqual("B", second);
    }

    [TestMethod]
    // Scenario: Add an item with the highest priority at the end of the queue.
    // Expected Result: Dequeue should still return the last item because it has the highest priority.
    // Defect(s) Found: The last item in the queue was not being checked.
    public void TestPriorityQueue_ExaminesLastItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("C", result);
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException with message "The queue is empty." should be thrown.
    // Defect(s) Found: The empty-queue exception was not handled correctly.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    // Add more test cases as needed below.
}
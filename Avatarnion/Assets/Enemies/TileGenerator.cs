using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DoublyNode<T>
{
    public DoublyNode(T data)
    {
        Data = data;
    }
    public T Data { get; set; }
    public DoublyNode<T> Previous { get; set; }
    public DoublyNode<T> Next { get; set; }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    DoublyNode<T> head;
    DoublyNode<T> tail;
    int count;

    public void Add(T data)
    {
        DoublyNode<T> node = new DoublyNode<T>(data);

        if (head == null)
            head = node;
        else
        {
            tail.Next = node;
            node.Previous = tail;
        }
        tail = node;
        count++;
    }
    public void AddFirst(T data)
    {
        DoublyNode<T> node = new DoublyNode<T>(data);
        DoublyNode<T> temp = head;
        node.Next = temp;
        head = node;
        if (count == 0)
            tail = head;
        else
            temp.Previous = node;
        count++;
    }
    // удаление
    public bool Remove(T data)
    {
        DoublyNode<T> current = head;

        // поиск удаляемого узла
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                break;
            }
            current = current.Next;
        }
        if (current != null)
        {
            // если узел не последний
            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                // если последний, переустанавливаем tail
                tail = current.Previous;
            }

            // если узел не первый
            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                // если первый, переустанавливаем head
                head = current.Next;
            }
            count--;
            return true;
        }
        return false;
    }

    public int Count { get { return count; } }
    public bool IsEmpty { get { return count == 0; } }

    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public bool Contains(T data)
    {
        DoublyNode<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
                return true;
            current = current.Next;
        }
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)this).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        DoublyNode<T> current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    public IEnumerable<T> BackEnumerator()
    {
        DoublyNode<T> current = tail;
        while (current != null)
        {
            yield return current.Data;
            current = current.Previous;
        }
    }
}
public class TileGenerator : MonoBehaviour
{

    GameObject tile;

    void Awake()
    {
        DoublyLinkedList<Dictionary<bool, Vector3>> tilesPos = new DoublyLinkedList<Dictionary<bool, Vector3>>();
        for (int i = 0; i < 5; i++)
        {
            tilesPos.Add(new Dictionary<bool, Vector3>() { { false, new Vector3(i * 20f, 0, 0) } });
        }
        System.Random random = new System.Random();

        // foreach
        // List<bool> keys = new List<string>(tilesPos.Keys);
        // string randomKey = keys[rnd.Next(keys.Count)];
        // Console.WriteLine($"The capital of {randomKey} is {capitals[randomKey]}");

        // GameObject firstTile = Instantiate(tile, )
    }

    void GenerateTile()
    {
        System.Random random = new System.Random();



    }
}

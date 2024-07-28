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

    [SerializeField]
    GameObject saveTile, killingTile;
    GameObject[] tiles;

    void Awake()
    {

        tiles = new GameObject[4];
        StartCoroutine("GenerateTile");
    }

    IEnumerator GenerateTile()
    {
        System.Random random = new System.Random();
        int randomPos = random.Next(0, 4);
        // for (int i = 0; i < tiles.Length; i++)
        // {
        //     Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + i * 5);
        //     if (i == randomPos)
        //     {
        //         GameObject newTile = Instantiate(saveTile, pos, Quaternion.identity);
        //     }
        //     else
        //     {
        //         GameObject newTile = Instantiate(killingTile, pos, Quaternion.identity);

        //     }
        // }
        Debug.Log(randomPos);
        for (int i = 0; i < 20; i++)
        {
            if (randomPos == 0)
            {
                randomPos++;
            }
            else if (randomPos == 3)
            {
                randomPos--;
            }
            else
            {
                randomPos += random.Next(-1, 2);
            }
            Debug.Log($"New {randomPos}");
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(randomPos);

                Vector3 pos = new Vector3(gameObject.transform.position.x - i * 5, gameObject.transform.position.y, gameObject.transform.position.z + j * 5);
                if (j == randomPos)
                {
                    GameObject newTile = Instantiate(saveTile, pos, Quaternion.identity);
                }
                else
                {
                    GameObject newTile = Instantiate(killingTile, pos, Quaternion.identity);

                }
                yield return new WaitForSeconds(.5f);
            }
            yield return new WaitForSeconds(1f);
        }

    }
}

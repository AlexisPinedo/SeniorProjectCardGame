using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Queue<GameObject> myQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int x = 7;
        int y = 2;

        Debug.Log(isPalin("asdasdasdasd"));
    }

    bool isPalin(string wrd)
    {
        if (wrd == "")
            return false;
        char[] ary = wrd.ToUpper().ToCharArray();
        Array.Sort(ary);
        
        int buffer = ary[0];
        int count = 1;
        foreach(char let in ary)
        {
            if (buffer != (int)let)
                count++;
            buffer = (int)let;
        }
        float wordLen = wrd.Length;
        int maxCount = (int)Math.Round(wordLen / 2f);
        if (count <= maxCount)
            return true;
        return false;
    }

    int countChars(char[] arr, int size, char sym)
    {
        int count = 0; 
        for(int i = 0; i < size; i++)
        {
            if (arr[i] == sym)
                count++;
        }
        return count;
    }

    string URLify(string msg, int trueSize)
    {
        char[] arr = msg.ToCharArray();
        int spaceCount = countChars(arr, trueSize, ' ');
        int newIndex = trueSize - 1 + spaceCount * 2;
        if (newIndex + 1 < arr.Length)
            arr[newIndex + 1] = '\n';
        for(int oldIndex = trueSize -1; oldIndex >= 0; oldIndex -= 1)
        {
            if(arr[oldIndex] == ' ')
            {
                arr[newIndex] = '0';
                arr[newIndex - 1] = '2';
                arr[newIndex - 2] = '%';
                newIndex -= 3;
            }
            else
            {
                arr[newIndex] = arr[oldIndex];
                newIndex--;
            }
        }
        return new string(arr);
    }

    //public string URLify(string msg, int size)
    //{
    //    char[] arr = msg.ToCharArray();
    //    for(int i = 0; i < arr.Length; i++)
    //    {
    //        if(arr[i] == ' ')
    //        {
    //            arr[i] = '%';
    //            char temp = arr[i + 1];
    //            arr[i + 1] = '2';
    //            char temp2 = arr[i + 2];
    //            arr[i + 2] = '0';
    //            for(int j = i + 3; j < arr.Length; j += 2)
    //            {
    //                char temp3 = arr[j];
    //                arr[j] = temp;
    //                temp = temp3;
    //                if(j + 1 < arr.Length)
    //                {
    //                    temp3 = arr[j + 1];
    //                    arr[j + 1] = temp2;
    //                    temp2 = temp3;
    //                }
    //            }
    //        }
            
    //    }
    //    return new string(arr);
    //}

    public bool isPerm(string msg1, string msg2)
    {
        if (msg1.Length != msg2.Length)
            return false;
        char[] arr1 = msg1.ToUpper().ToCharArray();
        char[] arr2 = msg2.ToUpper().ToCharArray();
        Array.Sort(arr1);
        Array.Sort(arr2);

        for(int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i].Equals(arr2[i]) == false)
                return false;
        }
        return true;
    }

    public bool isUnique(string msg)
    {
        if (msg.Length > 256)
            return false;
        bool[] uniqArr = new bool[256];
        for (int i = 0; i < 256; i++)
            uniqArr[i] = false;
        foreach(var chars in msg)
        {
            if (uniqArr[(int)chars] == true)
                return false;
            uniqArr[(int)chars] = true;
        }

        return true;

    }
}

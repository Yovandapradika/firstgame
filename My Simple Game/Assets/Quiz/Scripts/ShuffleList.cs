using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ShuffleList
{
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);
        List<E> randomList = new List<E>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (originalList.Count > 0)
        {
            randomIndex --;
            randomIndex = r.Next(0, originalList.Count - 1 ); //Choose a random object in the list
            randomList.Add(originalList[randomIndex]); //add it to the new, random list
            originalList.RemoveAt(randomIndex); //remove to avoid duplicates
            Debug.Log (randomList);
        }

        return randomList; //return the new random list
    }
}

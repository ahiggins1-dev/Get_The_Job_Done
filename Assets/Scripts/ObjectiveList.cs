/*****************************************************************************
// File Name : ObjectiveList.cs
// Author : Drew Higgins
// Creation Date : April 22nd, 2025
//
// Brief Description : This script shows the list of objectives throughout the
                       entire game
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{
    //Creates a dictionary and an index to store them in
    private static Dictionary<int, string> objectives = new Dictionary<int, string>();
    private static int objectiveIndex = 1;

    public static Dictionary<int, string> Objectives { get => objectives; set => objectives = value; }
    public static int ObjectiveIndex { get => objectiveIndex; set => objectiveIndex = value; }

    /// <summary>
    /// Adds everything to the list in order
    /// </summary>
    void Start()
    {
        if(objectives.Count != 0)
        {
            return;
        }
        objectives.Add(1, "Make Breakfast!");
        objectives.Add(2, "Find your House Key!");
        objectives.Add(3, "Go to Work!");
        objectives.Add(4, "Get Work Done at Your Computer!");
        objectives.Add(5, "Eat Lunch in the Lunch Room (For Lunching!)!");
        objectives.Add(6, "Get Your Keys and Go Home!");
        objectives.Add(7, "Eat Dinner at the Table!!");
        objectives.Add(8, "Find the Key to your Lemonade Vault!");
        objectives.Add(9, "Drink Your Lemonade!");
    }
}

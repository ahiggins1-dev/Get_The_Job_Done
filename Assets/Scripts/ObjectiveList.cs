using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{
    private static Dictionary<int, string> objectives = new Dictionary<int, string>();
    private static int objectiveIndex = 1;

    public static Dictionary<int, string> Objectives { get => objectives; set => objectives = value; }
    public static int ObjectiveIndex { get => objectiveIndex; set => objectiveIndex = value; }

    // Start is called before the first frame update
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
        objectives.Add(5, "Eat Lunch in the Break Room!");
        objectives.Add(6, "Get Your Keys and Go Home!");
        objectives.Add(7, "Make Yourself Dinner!");
        objectives.Add(8, "Unlock your Lemonade Vault!");
        objectives.Add(9, "Drink Your Lemonade!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*****************************************************************************
// File Name : FollowPlayer.cs
// Author : Drew Higgins
// Creation Date : March 25th, 2025
//
// Brief Description : This script forces the camera to stay on the player.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    /// <summary>
    /// Puts the camera on the player
    /// </summary>
    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}

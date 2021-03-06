﻿using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	//transforms the arrow will be pointing at
	public List<Transform> waypoints;
	//current index of transform the arrow should be pointing at
	public int waypointIndex = 0;
	public int playerIndex = 0;
	//object that will be pointed at
	public Transform arrow;
	//toggles whether or not arrow is visible
	public bool isEnabled;

    public List<Transform> tradingPosts;
    public List<Transform> gates;
    public List<Transform> turrets;
	public List<Transform> prefabFighters;
    

	// Use this for initialization
	void Start () {
        SceneState.OnStateChange += OnStateChange;
        CameraState.OnStateChange += OnStateChange;
        EventNotifier.OnTriggerStateChange += OnTriggerStateChange;

        Transform tradingPostsParent = GameObject.Find("Trading Posts").transform;
        foreach (Transform tradingPost in tradingPostsParent)
        {
            if (tradingPost.parent == tradingPostsParent)
            {
                tradingPosts.Add(tradingPost);
            }    
        }
        Transform gatesParent = GameObject.Find("Gates").transform;
        foreach (Transform gate in gatesParent)
        {
            if (gate.parent == gatesParent)
            {
                gates.Add(gate);
            }
        }
        Transform turretsParent = GameObject.Find("turrets").transform;
        foreach (Transform turret in turretsParent)
        {
            if (turret.parent == turretsParent)
            {
                turrets.Add(turret);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnabled && PhotonNetwork.playerList.Length > 1)
		{
			foreach (var prefabFightersParent in FindObjectsOfType(typeof(GameObject)) as GameObject[])
			{
				if (prefabFightersParent.name == "prefabFighter(Clone)")
				{
					prefabFighters.Add(prefabFightersParent.transform);
				}
			}
			//if (prefabFighters[playerIndex].position == fighter.position)
			//{
			//	playerIndex += 1;
			//}
			//else
			//{
			arrow.LookAt(prefabFighters[playerIndex]);
			//}
		}
		else if (isEnabled && PhotonNetwork.playerList.Length == 1)
        {
			playerIndex += 1;
            arrow.LookAt(waypoints[waypointIndex]);
        }
	}

    void OnTriggerStateChange(Collider other)
    {
		waypointIndex += 1;
        if (waypointIndex >= waypoints.Count)
        {
			waypointIndex = 0;
		};

    }

    void OnStateChange()
    {
        if (SceneState.sceneIndex == 1)
        {
            waypoints = gates;
        }
        else if (SceneState.sceneIndex == 2)
        {
            waypoints = gates;
        }
        else if (SceneState.sceneIndex == 3)
        {
            waypoints = turrets;
        }
        else
        {
            waypointIndex = 0;
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class EventNotifier : MonoBehaviour {
    public delegate void MenuStateChange(string scene);
    public static event MenuStateChange OnMenuStateChange;
    public delegate void TriggerStateChange(Collider other);
    public static event TriggerStateChange OnTriggerStateChange;
    public delegate void DestroyStateChange();
    public static event DestroyStateChange OnDestroyStateChange;

    public bool menu = false;
    public bool trigger = false;
    public bool destroy = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseUp()
    {
        if (OnMenuStateChange != null && menu)
        {
            OnMenuStateChange(gameObject.tag);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnTriggerStateChange != null && trigger)
        {
            OnTriggerStateChange(other);
        }
    }

    void OnDestroy()
    {
        if (OnDestroyStateChange != null && destroy)
        {
            OnDestroyStateChange();
        }
    }
}
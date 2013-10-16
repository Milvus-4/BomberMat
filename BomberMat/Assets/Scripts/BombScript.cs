using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    public float time_before_explosion = 5f;

    private float time;

	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        
        if (Time.time == time + time_before_explosion)
        {
            Debug.Log("explosion");
        }
	}

    void OnTriggerExit()
    {
        transform.collider.isTrigger = false;
    }
}

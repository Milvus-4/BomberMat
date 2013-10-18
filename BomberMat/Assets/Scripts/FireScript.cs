using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

    public float timeBeforeDie = 0.5f;

    private float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - time >= timeBeforeDie)
        {
            Destroy(gameObject);

        }
	}
}

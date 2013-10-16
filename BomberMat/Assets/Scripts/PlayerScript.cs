using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float player_speed=1;
    public float time_before_explosion=5f;

    private float time;
    private bool bomb_putted=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * Time.deltaTime * player_speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * Time.deltaTime * player_speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * player_speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * player_speed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            GameObject bomb = (GameObject)Instantiate( Resources.Load("Prefab/Bomb"));
            Debug.Log(bomb);
            time = Time.time;
            bomb_putted = true;
            //bomb.transform.position = transform.position;
        }

        /*if (bomb_putted && Time.time == time + time_before_explosion)
        {
            bomb
        }*/
	}
}

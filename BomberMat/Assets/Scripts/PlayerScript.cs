using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float player_speed=1;
    
    private bool bomb_putted=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rigidbody.AddForce( Vector3.forward * Time.deltaTime * player_speed*100);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rigidbody.AddForce( Vector3.back * Time.deltaTime * player_speed*100);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rigidbody.AddForce( Vector3.right * Time.deltaTime * player_speed*100);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rigidbody.AddForce(Vector3.left * Time.deltaTime * player_speed*100);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            GameObject bomb = (GameObject)Instantiate( Resources.Load("Prefab/Bomb"));
            /*Debug.Log(bomb);
            
            bomb_putted = true;*/
            bomb.transform.position= new Vector3((float)(((int)(transform.position.x + 0.5))),
                                        0,
                                        (float)(((int)(transform.position.z + 0.5))));
        }


        
	}

    void OnTrigger(Collider other)
    {
        if (other.gameObject.tag == "bomb")
        {
            bomb_putted = true;
        }
    }
}

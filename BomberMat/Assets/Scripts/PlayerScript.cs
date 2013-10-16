using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float player_speed=3; //vitesse du joueur
    
    private bool bomb_drop=true; 
    private Vector3 last_bomb_position; //position de la dernière bombe posée
    private Vector3 rounded_player_position; //position arroudie du joueur


	void Start () {
	
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rigidbody.AddForce(Vector3.forward * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rigidbody.AddForce(Vector3.back * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rigidbody.AddForce(Vector3.right * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rigidbody.AddForce(Vector3.left * Time.deltaTime * player_speed * 100);
            transform.localEulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        }
        
        //poser une bombe
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rounded_player_position = new Vector3((float)(((int)(transform.position.x + 0.5))),
                                        0,
                                        (float)(((int)(transform.position.z + 0.5))));

            if (rounded_player_position != last_bomb_position)//ne pose pas une bombe sur une autre
            {
                //charge le prefab Bomb
                GameObject bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb"));
                bomb.transform.position = rounded_player_position;
                last_bomb_position = rounded_player_position;

            }
        }


    }
	
	void Update () {

        
        
	}

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "bomb")
        {
            bomb_drop = false;
        }
        Debug.Log(bomb_drop);
    }

    void OnCollisionExit()
    {
        bomb_drop = true;
    }
}

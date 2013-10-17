using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float player_speed=3; //vitesse du joueur
    
    private Vector3 lastBombPosition; //position de la dernière bombe posée
    private Vector3 roundedPlayerPosition; //position arroudie du joueur


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
            DropBomb();
        }


    }
    void DropBomb()
    {
        roundedPlayerPosition = new Vector3((float)(((int)(transform.position.x + 0.5))),
                                        0,
                                        (float)(((int)(transform.position.z + 0.5))));

       if (StaticBoard.bomb[(int)roundedPlayerPosition.x][(int)roundedPlayerPosition.z] == null)//ne pose pas une bombe sur une autre
       {
            //charge le prefab Bomb
            GameObject bomb = (GameObject)Instantiate(Resources.Load("Prefab/Bomb"));
            bomb.transform.position = roundedPlayerPosition;
            lastBombPosition = roundedPlayerPosition;

        }
    }
	
	void Update () {        
	}
}

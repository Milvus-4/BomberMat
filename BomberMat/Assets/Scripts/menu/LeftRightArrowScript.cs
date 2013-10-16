/**
 *by Jules Maurer
 *
 * Desc :
 * Script for gest fleet's arrows
 * 
 * 
**/
using UnityEngine;
using System.Collections;

public class LeftRightArrowScript : MonoBehaviour {

    public bool _right = false;

    public float _speed = 1;

    private MoveInFleetScript superScript;
    private Vector3 startPos;
    private Vector3 lastPos;
    private bool onMove = false;
    private float timer;
    private GameObject[] fleet;

	// Use this for initialization
	void Start () {
        this.superScript = GameObject.Find("GestFleet").GetComponent<MoveInFleetScript>();
	}
	
	// Update is called once per frame
	void Update () {
        var ray = new Ray();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0) && !onMove)
            if (collider.Raycast(ray, out hit, 100.0f))
            {
                this.fleet = this.superScript.getFleetOnY();
                this.lastPos = this.fleet[1].transform.position;
                if (_right)
                    this.startPos = this.superScript.getFleetNext().transform.position;
                else
                    this.startPos = this.superScript.getFleetPrev().transform.position;
                
                this.timer = 0;
                this.onMove = true;

            }
        if (collider.Raycast(ray, out hit, 100.0f))
        {
             Debug.DrawLine(ray.origin, hit.point, Color.green, 1);
            //  texte.material.color = new Color(01 / 255, 47 / 255, 98 / 2);
        }
        if (this.onMove)
        {
            this.timer += Time.deltaTime * this._speed;
            this.moveOn();
        }
        if (this.timer == 1)
        {
            this.onMove = false;
            if (_right)
                this.superScript.updateXbyRight();
            else
                this.superScript.updateXbyLeft();
            this.timer = 0;
        }
	}

    void moveOn()
    {
        if (this.timer >= 1) 
            this.timer = 1;
        Vector3 A = new Vector3(this.lastPos.x, this.lastPos.y, this.startPos.z);
        float initPos;
        if(_right)
            initPos = 14 ;
        else
            initPos = 0;
        for (int i = 0 ; i < 3 ; i ++)
            this.fleet[i].transform.position = Vector3.Lerp(this.startPos+new Vector3(i*7-initPos,0,0), A + new Vector3(i*7-initPos,0,0), timer);

    }
}

/**
 *by Jules Maurer
 *
 * Desc :
 * Script for gest fleet's tabs
 * 
 * 
**/

using UnityEngine;
using System.Collections;

public class TabFleetScript : MonoBehaviour
{
    /**
     * Position where go
     */
    public float _position;

    /**
     * Lerp speed
     */
    public float _speed = 1;

    //private MeshRenderer texte;

    private GameObject parent;

    private MoveInFleetScript superScript;

    private Vector3 startPos;

    private float timer;

    private bool onMove;

    void Start()
    {
       // this.texte = GetComponentInChildren<MeshRenderer>();
        this.parent = GameObject.Find("GestFleet");
        this.superScript = this.parent.GetComponent<MoveInFleetScript>();
    }



    // Update is called once per frame
    void Update()
    {
        var ray = new Ray();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
            if (collider.Raycast(ray, out hit, 100.0f))
            {
                this.startPos = this.parent.transform.position;
        
                this.timer = 0;
                this.onMove = true;
                this.superScript.updateY((int)Mathf.Abs(_position/7));

            }

        if (this.onMove)
        {
            this.moveOn();
            this.timer += Time.deltaTime * this._speed;
        }
        if (this.timer >= 1) this.onMove = false;

        if (collider.Raycast(ray, out hit, 100.0f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 1);
          //  texte.material.color = new Color(01 / 255, 47 / 255, 98 / 2);
        }
        else
        {
           // texte.material.color = Color.white;
        }
    }

    void moveOn()
    {
        Vector3 A = new Vector3(this.startPos.x, _position, this.startPos.z);
        this.parent.transform.position = Vector3.Lerp(this.startPos, A, timer);
       
    }
}

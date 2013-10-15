/**
 *by Jules Maurer
 *
 * Desc :
 * Script for buttons, enter a var as an action.
 * 
 * creation : 12/12/12
 * last modification 12/02/13
 * 
**/

using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    /**
     * Name of the action to do
     */
    public string action;

    //private MeshRenderer texte;

    void Start()
    {
      //  texte = GetComponentInChildren<MeshRenderer>();
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
                if (action.Equals("nothing"))
                {

                }
                else if (action.Equals("pos"))
                {

                }
                else if (action.Equals("split"))
                {

                }
                else if (action.Equals("garage"))
                {

                }
                else if (action.Equals("ok"))
                {

                }
                else if (action.Equals("back"))
                {

                }
                else if (action.Equals("research"))
                {

                }
                else if (action.Equals("study"))
                {

                }
                else if (action.Equals("built"))
                {

                }
                else if (action.Equals("make"))
                {

                }
                else if (action.Equals("other"))
                {

                }
            }

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
}

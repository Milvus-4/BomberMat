/**
 *by Jules Maurer
 *
 * Desc :
 * Script for an example for gest fleet
 * 
 * 
**/

using UnityEngine;
using System.Collections;

public class CreatFleetScript : MonoBehaviour {

    public int[] _nbShip;
    public GameObject[][] myTab;

	// Use this for initialization
	void Awake () {

	    this.myTab = new GameObject[5][];
        for (var i = 0; i < 5; i++)
        {
            this.myTab[i] = new GameObject[this._nbShip[i]];
        }
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < this.myTab[i].Length; j++)
            {
            }
        } 
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}

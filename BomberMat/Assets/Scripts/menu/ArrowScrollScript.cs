using UnityEngine;
using System.Collections;

public class ArrowScrollScript : MonoBehaviour {

    public GameObject _cursor;

    public bool _down = false;

    public float _height = 0.1f;

    private GameObject[] contents;
    private float proportion;
	// Use this for initialization
	void Start () {
        this.contents = GameObject.FindGameObjectsWithTag("ContentFleet");
        //this.proportion = GameObject.Find("ScrollBox").GetComponent<ScrollBoxScript>().getScale();
	}
	
	// Update is called once per frame
	void Update () {
	    var ray = new Ray();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
            if (collider.Raycast(ray, out hit, 100.0f))
            {
                if (_down)
                {
                    foreach (GameObject content in this.contents)
                    {
                        content.transform.localPosition += new Vector3(0, this.proportion * this._height, 0);
                        Debug.Log(this.proportion * this._height);
                    }
                    _cursor.transform.localPosition -= new Vector3(0, this._height, 0);
                }
                else
                {
                    foreach (GameObject content in this.contents)
                    {
                        content.transform.localPosition -= new Vector3(0, this.proportion * this._height, 0);
                    }
                    _cursor.transform.localPosition += new Vector3(0, this._height, 0);
                }

            }
	}
}

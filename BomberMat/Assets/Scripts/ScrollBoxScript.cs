using UnityEngine;
using System.Collections;

public class ScrollBoxScript : MonoBehaviour {

    public GameObject _box;
    public GameObject _scroll;
    public GameObject _cursor;
    public GameObject _arrow;
    public GameObject _content;

    private int nbContent = 8; //A trouver procéduralement normalement
    private float scale = 0;
	// Use this for initialization
	void Start ()
    {
	    //Taille = 10*scale
        float boxHeight = 10 * _box.transform.localScale.z;
        float scrollHeight = 10 * _scroll.transform.localScale.z;
        float cursorHeight;
        float arrowHeight = 10 * _arrow.transform.localScale.z;
        float contentHeight = 10 * _content.transform.localScale.z;


        this.scale = (contentHeight * this.nbContent) / (scrollHeight - 2 * arrowHeight);
        cursorHeight = (scrollHeight / (contentHeight * this.nbContent)) * (scrollHeight - 2 * arrowHeight);
        Debug.Log(boxHeight);
        Debug.Log(scrollHeight);
        Debug.Log(arrowHeight);
        Debug.Log(contentHeight);
        Debug.Log(scale);
        Debug.Log(cursorHeight);
        this._cursor.transform.localScale=new Vector3( this._cursor.transform.localScale.x, this._cursor.transform.localScale.y, cursorHeight/10);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public float getScale()
    {
        float scrollHeight = 10 * _scroll.transform.localScale.z;
        float arrowHeight = 10 * _arrow.transform.localScale.z;
        float contentHeight = 10 * _content.transform.localScale.z;

        this.scale = (contentHeight * this.nbContent) / (scrollHeight - 2 * arrowHeight);
        return this.scale;
    }
}

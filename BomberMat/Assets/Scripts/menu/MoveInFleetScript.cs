/**
 *by Jules Maurer
 *
 * Desc :
 * Script for move in gest fleet
 * 
 * 
**/
using UnityEngine;
using System.Collections;

public class MoveInFleetScript : MonoBehaviour {

    public GameObject _prefab;

    public GameObject _arrowLeft;
    public GameObject _arrowRight;
    public GameObject[] _tabs;

    private GameObject[][] fleet;

    private int actualY;
    private int[] actualX;

    private GameObject substitu;



	// Use this for initialization
    void Start()
    {
        this.actualX = new int[5];
        fleet = GetComponent<CreatFleetScript>().myTab;
        Vector3 pos = new Vector3(0, 0, 0);
        Quaternion angle = Quaternion.Euler(new Vector3(270, 0, 0));
        for (var i = 0; i < 5; i++)
        {
            if (this.fleet[i].Length != 0)
            {
                pos = new Vector3(0, -i * 7, 0);
                this.fleet[i][0] = myInstantiate(pos, angle, "Ship" + i + " - 0" );
            }
            else
            {
                //On grise l'onglet et on en parle plus
                this._tabs[i].SetActive(false);
            }
            this.actualX[i] = 0;
        }
        for (var i = 0; i < 5; i++)
        {
            if (this.fleet[i].Length != 0)
            {
                this.transform.position.Set(0, i * -7, 0);
                this.actualY = i;
                this.updateY(this.actualY);
                break;
            }
        }

        
        setCard();
    }
	
	// Update is called once per frame
    void Update()
    {

    }

    GameObject myInstantiate(Vector3 f_pos, Quaternion f_angle, string f_text)
    {
        GameObject inter = Instantiate(_prefab, f_pos, f_angle) as GameObject;
        inter.GetComponentInChildren<TextMesh>().text = f_text;
        return inter;
    }

    public void updateXbyRight()
    {
        //this.destroyCard();
        if (this.fleet[actualY].Length == 2)
        {
            Destroy(this.substitu);
        }
        else
        {
            Destroy(this.fleet[actualY][this.actualX[this.actualY] > 0 ? this.actualX[this.actualY] - 1 : this.fleet[this.actualY].Length - 1]);
        }
        Destroy(this.fleet[actualY][actualX[this.actualY]]);
        this.actualX[this.actualY] = ++this.actualX[this.actualY] % this.fleet[actualY].Length;
        this.setCard();
    }

    public void updateXbyLeft()
    {
        if (this.fleet[actualY].Length == 2)
        {
            GameObject inter = this.substitu;
            this.substitu = this.fleet[actualY][actualX[this.actualY] == 0 ? 1 : 0];
            this.fleet[actualY][actualX[this.actualY] == 0 ? 1 : 0] = inter;
            Destroy(this.substitu);
        }
        else
        {
            Destroy(this.fleet[actualY][this.actualX[this.actualY] == this.fleet[this.actualY].Length - 1 ? 0 : this.actualX[this.actualY] + 1]);
        }
        Destroy(this.fleet[actualY][actualX[this.actualY]]);
        this.actualX[this.actualY] = this.actualX[this.actualY] == 0 ? this.fleet[this.actualY].Length - 1 : this.actualX[this.actualY] -= 1;
         
        this.setCard();
    }

    public void updateY(int f_y)
    {
        
        this.destroyCard();
        this.actualY = f_y;
        this.setCard();
        this.showArrow();
    }

    public void showArrow()
    {
        if (this.fleet[this.actualY].Length == 1)
        {
            this._arrowLeft.SetActive(false);
            this._arrowRight.SetActive(false);
        }
        else
        {
            this._arrowLeft.SetActive(true);
            this._arrowRight.SetActive(true);
        }
    }

    private void setCard()
    {
        Quaternion angle = Quaternion.Euler(new Vector3(270,0,0));
        if (this.fleet[actualY].Length == 2)
        {
            if (actualX[this.actualY] == 0)
            {
                this.fleet[actualY][1] = myInstantiate(new Vector3(7, actualY * -7, 0), angle, "Ship" + actualY + " - " + 1) as GameObject;
                this.substitu = myInstantiate(new Vector3(-7, actualY * -7, 0), angle, "Ship" + actualY + " - sub") as GameObject;
            }
            else
            {
                this.fleet[actualY][0] = myInstantiate(new Vector3(7, actualY * -7, 0), angle, "Ship" + actualY + " - " + 0) as GameObject;
                this.substitu = myInstantiate(new Vector3(-7, actualY * -7, 0), angle, "Ship" + actualY + " - sub") as GameObject;
            }
        }
        if (this.fleet[actualY].Length > 2)
        {
            if (actualX[this.actualY] != 0)
                this.fleet[actualY][actualX[this.actualY] - 1] = myInstantiate(new Vector3(-7, actualY * -7, 0), angle, "Ship" + actualY + " - " + (actualX[this.actualY]-1)) as GameObject;
            else
                this.fleet[actualY][this.fleet[actualY].Length - 1] = myInstantiate(new Vector3(-7, actualY * -7, 0), angle, "Ship" + actualY + " - " + (this.fleet[actualY].Length - 1)) as GameObject;

            if (actualX[this.actualY] != this.fleet[actualY].Length - 1)
                this.fleet[actualY][actualX[this.actualY] + 1] = myInstantiate(new Vector3(7, actualY * -7, 0), angle, "Ship" + actualY + " - " + (actualX[this.actualY]+1)) as GameObject;
            else
                this.fleet[actualY][0] = myInstantiate(new Vector3(7, actualY * -7, 0), angle, "Ship" + actualY + " - " + 0) as GameObject;
        }
    }

    private void destroyCard()
    {

        if (this.fleet[actualY].Length == 2)
        {
            if (actualX[this.actualY] == 0)
            {
                Destroy(this.fleet[actualY][1]);
            }
            else
                Destroy(this.fleet[actualY][0]);
            Destroy(this.substitu);
        }
        if (this.fleet[actualY].Length > 2)
        {
            if (actualX[this.actualY] != 0)
                Destroy(this.fleet[actualY][actualX[this.actualY] - 1]);
            else
                Destroy(this.fleet[actualY][this.fleet[actualY].Length - 1]); 

            if (actualX[this.actualY] != this.fleet[actualY].Length - 1)
                Destroy(this.fleet[actualY][actualX[this.actualY] + 1]);
            else
                Destroy(this.fleet[actualY][0]);
        }
    }


    public GameObject getFleet()
    {
        return this.fleet[this.actualY][this.actualX[this.actualY]];
    }
    public GameObject getFleetNext()
    {
        if (this.actualX[this.actualY] + 1 >= this.fleet[this.actualY].Length)
        {
            return this.fleet[this.actualY][0];
        }
        return this.fleet[this.actualY][this.actualX[this.actualY]+1];
    }
    public GameObject getFleetPrev()
    {
        if (this.fleet[this.actualY].Length == 2)
        {
            Vector3 inter = this.substitu.transform.position;
            this.substitu.transform.position = this.fleet[actualY][actualX[this.actualY] == 0 ? 1 : 0].transform.position;
            this.fleet[actualY][actualX[this.actualY] == 0 ? 1 : 0].transform.position = inter;
        }
        if (this.actualX[this.actualY] != 0)
            return this.fleet[this.actualY][this.actualX[this.actualY] - 1];
        return this.fleet[this.actualY][this.fleet[this.actualY].Length-1];
    }

    public GameObject[] getFleetOnY()
    {
        GameObject[] inter = new GameObject[3];
        if (this.fleet[actualY].Length == 2)
        {
            if (actualX[this.actualY] == 0)
            {
                inter[2] = this.fleet[this.actualY][1];
                inter[1] = this.fleet[this.actualY][0];
                inter[0] = substitu;
            }
            else
            {
                inter[2] = this.fleet[this.actualY][0];
                inter[1] = this.fleet[this.actualY][1];
                inter[0] = substitu;
            }

            

        }
        else
        {
            inter[0] = this.fleet[this.actualY][this.actualX[this.actualY]>0?this.actualX[this.actualY] - 1 : this.fleet[this.actualY].Length-1];
            inter[1] = this.fleet[this.actualY][this.actualX[this.actualY]];
            inter[2] = this.fleet[this.actualY][this.actualX[this.actualY] == this.fleet[this.actualY].Length - 1 ? 0 :this.actualX[this.actualY] + 1];
        }
        return inter;
    }

}

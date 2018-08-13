using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

    [SerializeField] GameObject currentHex;
    //public GameObject currentPlayer;
    MeshRenderer mr;
    //GameObject cPlayer;
    Color defaultColor = new Color(1, 1, 1, 1);
    public HexMapMaker HMM;
    [SerializeField] GameObject selectedUnit;
    string selectedUnitName;
    public Text selectedUnitNameText;
    [SerializeField] GameObject sUnit;

    // Use this for initialization
    void Start () {
        currentHex = null;
        //cPlayer = (GameObject)Instantiate(currentPlayer, new Vector3(0, 0, -1), Quaternion.identity);
        selectedUnitName = "none";
        selectedUnitNameText.text = selectedUnitName;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Mouse position: " + Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        GameObject hitUnit;
        GameObject hitHex;
        LayerMask unitMask = (1 << 16);
        LayerMask mapMask = (1 << 8);
        bool unitHitBool = false;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, unitMask.value))
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitUnit = hitInfo.collider.transform.gameObject;
                Debug.Log("hitUnit changed to: " + hitUnit.name);

                if (selectedUnitName == "none")
                {
                    //sUnit = GameObject.Find("bob");
                    selectedUnitName = hitUnit.name;
                    selectedUnitNameText.text = hitUnit.name;
                    Debug.Log("selectedUnitName was blank. selectedUnitName: " + selectedUnitName);
                }
                else
                {
                    selectedUnit = GameObject.Find(selectedUnitName);
                    int thisAttack = selectedUnit.GetComponent<Unit>().attack;
                    int thisHealth = hitUnit.GetComponent<Unit>().health;
                    hitUnit.GetComponent<Unit>().health -= thisAttack;

                    if (hitUnit.GetComponent<Unit>().health < 1)
                        Destroy(hitUnit);
                }
            }
            unitHitBool = true;
        }

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mapMask.value))
        {
            hitHex = hitInfo.collider.transform.gameObject;

            if (currentHex == null)
            {
                mr = hitHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = Color.red;
                currentHex = hitHex;
            }
            else if (currentHex.transform.parent.name != hitHex.transform.parent.name)
            {
                mr = currentHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = defaultColor;

                mr = hitHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = Color.red;

                currentHex = hitHex;
            }

            if(Input.GetMouseButtonDown(0) && !unitHitBool)
            {
                Debug.Log("selectedUnitName: " + selectedUnitName);
                if(selectedUnitName != "" && selectedUnitName != "none")
                {
                    hitUnit = GameObject.Find(selectedUnitName);
                    hitUnit.transform.position = new Vector3(currentHex.transform.position.x, currentHex.transform.position.y, -1);
                    selectedUnitName = "none";
                    selectedUnitNameText.text = selectedUnitName;
                }
            }
        }
        else
        {
            if (currentHex != null)
            {
                mr = currentHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = defaultColor;
                currentHex = null;
            }
        }

        //Camera movement
        float cameraMoveSpeed = 10f;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.position += Vector3.right * Time.deltaTime * cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.position += Vector3.left * Time.deltaTime * cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position += Vector3.up * Time.deltaTime * cameraMoveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.position += Vector3.down * Time.deltaTime * cameraMoveSpeed;
        }

        //Cleanup
        unitHitBool = false;




        /*if (Physics.Raycast(ray, out hitInfo))
        {
            hitHex = hitInfo.collider.transform.gameObject;
            //if (currentHex == null) Debug.Log("currentHex is null");
            //else Debug.Log("Raycast hit: " + hitInfo.collider.transform.parent.name + " / currentHex: " + currentHex.transform.parent.name);

            if (currentHex == null)
            {
                mr = hitHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = Color.red;
                currentHex = hitHex;
            }
            else if (currentHex.transform.parent.name != hitHex.transform.parent.name)
            {
                mr = currentHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = defaultColor;

                mr = hitHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = Color.red;

                currentHex = hitHex;
            }
            
            //On left mouseclick
            if (Input.GetMouseButtonDown(0))
            {
                hitUnit = hitInfo.collider.transform.gameObject;
                //Debug.Log("Raycast collider tag: " + hitInfo.collider.tag.ToString());
                if (hitUnit.GetComponent<Collider>().CompareTag("Unit"))
                {
                    Debug.Log("Name of unit hit: " + hitUnit.name);
                    if (selectedUnitName == "")
                    {
                        //sUnit = GameObject.Find("bob");
                        selectedUnitName = hitUnit.name;
                        //Debug.Log("selectedUnitName was blank. selectedUnitName: " + selectedUnitName);
                    }
                    else
                    {
                        //sUnit = GameObject.Find(selectedUnitName);
                        sUnit = GameObject.Find(hitUnit.name);
                        sUnit.transform.position = new Vector3(currentHex.transform.position.x, currentHex.transform.position.y, -1);
                        //Debug.Log("selectedUnitName is not blank. sUnit.position: " + sUnit.transform.position);
                    }
                }

                if(selectedUnitName != "" && GameObject.Find(selectedUnitName).GetComponent<Collider>().CompareTag("Unit"))
                {
                    //sUnit = GameObject.Find(selectedUnitName);
                    sUnit = GameObject.Find(hitUnit.name);
                    sUnit.transform.position = new Vector3(currentHex.transform.position.x, currentHex.transform.position.y, -1);
                    //Debug.Log("selectedUnitName is not blank. sUnit.position: " + sUnit.transform.position);
                }

                //How to move game objects    //cPlayer.transform.position = new Vector3(currentHex.transform.position.x, currentHex.transform.position.y, -1);
            }
        }
        else
        {
            if(currentHex != null)
            {
                mr = currentHex.GetComponentInChildren<MeshRenderer>();
                mr.material.color = defaultColor;
                currentHex = null;
            }
        }*/
    }
}

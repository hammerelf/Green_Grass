using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexMapMaker : MonoBehaviour
{
    public GameObject hexTile;
    public List<HexPrefab> unitTracker = new List<HexPrefab>();
    public GameObject unitObject;
    public Text EnterUnitNameText;

    int width = 10;
    int height = 10;

    float xOffset = 1.732f;
    float yOffset = 1.5f;

    // Use this for initialization
    void Start() {
        createField();
    }

    // Update is called once per frame
    void Update() {

    }

    public void createField()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;

                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }

                GameObject hexGO = (GameObject)Instantiate(hexTile, new Vector3(xPos, y * yOffset, 0), Quaternion.identity);

                hexGO.name = "Hex_" + x + "_" + y;
                hexGO.GetComponent<HexPrefab>().x = x;
                hexGO.GetComponent<HexPrefab>().y = y;
                //Not sure why this is here. Was in tutorial so might be important for stuff later. //hexGO.transform.SetParent(this.transform);
                
                unitTracker.Add(hexGO.GetComponent<HexPrefab>());
            }
        }
    }

    public void createUnit()
    {
        GameObject unitThing = (GameObject)Instantiate(unitObject, new Vector3(0, 0, -1), Quaternion.identity);
        unitThing.name = EnterUnitNameText.text;
        
        //Add new unit to unitTracker
        unitTracker[0].currentUnit = unitThing.name;
        unitTracker[0].isOccupied = true;
    }

    public int getUnitIndex(int x, int y)
    {
        for (int i = 0; i < unitTracker.Count; i++)
        {
            if (unitTracker[i].name == "Hex_" + x + "_" + y)
            {
                return i;
            }
        }
        return 0;
    }
}

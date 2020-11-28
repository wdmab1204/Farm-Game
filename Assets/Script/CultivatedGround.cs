using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CultivatedGround : MonoBehaviour
{

    [Header("Tile")]
    public GameObject midTileObejct;
    public GameObject upperLeftTileObject;
    public GameObject upperRightTileObject;
    public GameObject lowerLeftTileObject;
    public GameObject lowerRightTileObject;

    public Item.CropType cropType;
    private CompositeCollider2D col;
    private Vector3 gridPos;

    private void Awake()
    {
        col = GetComponent<CompositeCollider2D>();
        gridPos = GameObject.Find("Grid").transform.position;
    }

    private void RegenerateCollider()
    {
        col.GenerateGeometry();
    }

    public void GenerateTile(int level)
    {
        if (level < 1 || level > 3) return;
        
        //3 = 1*3-(1-1)
        //5 = 2*3-(2-1)
        //7 = 3*3-(3-1)
        int length = level * 3 - (level - 1);

        int minrow = -length / 2, maxrow = length / 2;
        int mincol = minrow, maxcol = maxrow;

        GameObject obj;

        for (int row = minrow; row <= maxrow; row++)
        {
            for (int col = mincol; col <= maxcol; col++)
            {
                Vector3 targetPos;
                int x = col, y = row;

                targetPos = new Vector2(x, y);

                if (col == mincol && row == maxrow)
                {
                    obj = Instantiate(upperLeftTileObject, transform);
                }
                else if (col == maxcol && row == maxrow)
                {
                    obj = Instantiate(upperRightTileObject, transform);
                }
                else if (col == mincol && row == minrow)
                {
                    obj = Instantiate(lowerLeftTileObject, transform);
                }
                else if(col==maxcol && row == minrow)
                {
                    obj = Instantiate(lowerRightTileObject, transform);
                }
                else
                {
                    obj = Instantiate(midTileObejct, transform);
                }

                obj.transform.localPosition = Vector3.zero;
                obj.transform.localPosition += targetPos - gridPos;
            }
        }
        
        Invoke("RegenerateCollider", 0.1f);
    }

    public void SetCrop()
    {

    }


    public void RemoveCrop()
    {

    }

    public bool CheckCropTile(Vector3 v)
    {
        return true;
    }
}

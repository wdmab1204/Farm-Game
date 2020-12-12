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
    private List<GameObject> cropObjList;
    private Transform signObj;

    private void Awake()
    {
        col = GetComponent<CompositeCollider2D>();
        gridPos = GameObject.Find("Grid").transform.position;
        cropObjList = new List<GameObject>();
        signObj = transform.GetChild(0);
    }

    private void RegenerateCollider()
    {
        col.GenerateGeometry();
    }

    public void GenerateTile(int level)
    {
        if (level < 1 || level > 3) return;

        Destroy(signObj.gameObject);

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gs">플레이어가 심을 농작물</param>
    /// <param name="tilePos">농작물 위치</param>
    public void SetCrop(Item item, Vector3 tilePos)
    {
        //땅에 작물심기
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/" + item.id), transform);
        obj.transform.position = tilePos;

        GrowSystem gs = obj.GetComponentInChildren<GrowSystem>();
        gs.item = item;

        if (gs.item.cropType == this.cropType)
        {
            gs.maxGrowTime -= gs.maxGrowTime * 0.3f; //0.3배 성장속도 증가!
        }

        cropObjList.Add(obj);
    }


    public void RemoveCrop(Vector3 tilePos)
    {
        foreach(GameObject cropObj in cropObjList)
        {
            if((cropObj.transform.localPosition + gridPos) == tilePos)
            {
                GrowSystem gs = cropObj.transform.GetChild(0).GetComponent<GrowSystem>();
                gs.Harvest();
                cropObjList.Remove(cropObj);
                Destroy(cropObj);
                return;
            }
        }
    }

    public bool CheckCrop(Vector3 tilePos)
    {
        foreach(GameObject cropObj in cropObjList)
        {
            if((cropObj.transform.localPosition + gridPos) == tilePos)
            {
                return true;
            }
        }
        return false;
    }
}

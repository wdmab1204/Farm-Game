using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CultivatedGround : MonoBehaviour
{
    private int tileSize = 1;

    [Header("Tile")]
    private List<GameObject> tileList;
    public GameObject midTileObejct;
    public GameObject upperLeftTileObject;
    public GameObject upperRightTileObject;
    public GameObject lowerLeftTileObject;
    public GameObject lowerRightTileObject;

    public Item.CropType cropType;
    private CompositeCollider2D col;
    private int[] lengthArray;
    private int index;
    private GameObject[] vertexTile; //upperLeft,upperRight,lowerLeft,lowerRight
    private int vertexIndex;
    private bool[] numberArray;

    private void Awake()
    {
        tileList = new List<GameObject>();
        col = GetComponent<CompositeCollider2D>();
        lengthArray = new int[3] { 5, 5, 7 };
        index = -1;
        vertexTile = new GameObject[4];
        vertexIndex = 0;
    }
    private void Start()
    {
        GenerateTile();
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
    }

    private void RegenerateCollider()
    {
        col.GenerateGeometry();
    }

    [ContextMenu("UpdateTile")]
    public void GenerateTile()
    {
        if (index >= lengthArray.Length) return;

        index++;
        int rows = lengthArray[index], cols = lengthArray[index];
        int minRow = -rows / 2, maxRow = rows / 2;
        int minCol = -cols / 2, maxCol = cols / 2;

        numberArray = new bool[lengthArray[index] * lengthArray[index]];

        for (int i = 0; i < vertexIndex; i++)
        {
            vertexTile[i].GetComponent<SpriteRenderer>().sprite = midTileObejct.GetComponent<SpriteRenderer>().sprite;
            vertexTile[i] = null;
        }
        vertexIndex = 0;

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                GameObject tile;
                if (col == minCol && row == minRow)
                {
                    tile = (GameObject)Instantiate(upperLeftTileObject, transform);
                    vertexTile[vertexIndex++] = tile;
                }
                else if (col == maxCol && row == minRow)
                {
                    tile = (GameObject)Instantiate(upperRightTileObject, transform);
                    vertexTile[vertexIndex++] = tile;
                }
                else if (col == minCol && row == maxRow)
                {
                    tile = (GameObject)Instantiate(lowerLeftTileObject, transform);
                    vertexTile[vertexIndex++] = tile;
                }
                else if (col == maxCol && row == maxRow)
                {
                    tile = (GameObject)Instantiate(lowerRightTileObject, transform);
                    vertexTile[vertexIndex++] = tile;
                }
                else
                    tile = (GameObject)Instantiate(midTileObejct, transform);

                tileList.Add(tile);




                float posX = col * tileSize + transform.localPosition.x;
                float posY = row * -tileSize + transform.localPosition.y;
                Vector2 tilePos = new Vector2(posX, posY);
                tile.transform.position = tilePos;
                int number = GetNumberOfGround(new Vector2(tile.transform.localPosition.x - 0.5f, tile.transform.localPosition.y - 0.5f));
                numberArray[number] = true;
            }
        }
        Invoke("RegenerateCollider", 0.1f);
    }

      
    [ContextMenu("Clear")]
    public void ClearTiles()
    {
        foreach (GameObject obj in tileList)
        {
            Destroy(obj);
        }
    }

    private int GetNumberOfGround(Vector2 v)
    {
        int number = -1;
        number = (int)((v.x + v.y) + ((lengthArray[index] / 2 - v.y) * (lengthArray[index] + 1)));

        if (number >= 0 && number <= lengthArray[index] * lengthArray[index])
            return number;
        else 
            return -1;
    }

    public bool CheckCropTile(Vector2 v)
    {
        int number = GetNumberOfGround(v);
        return numberArray[number];
    }

    public void SetCrop(GrowSystem gs, Vector2 tilePos)
    {
        if (gs.item.cropType == this.cropType)
        {
            gs.maxGrowTime -= gs.maxGrowTime * 0.3f; //0.3배 성장속도 증가
            //성장속도 증가
        }

        int number = GetNumberOfGround(tilePos);
        numberArray[number] = false;
    }

}

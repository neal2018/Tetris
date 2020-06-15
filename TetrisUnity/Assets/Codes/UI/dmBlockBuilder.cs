using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dmBlockBuilder : MonoBehaviour
{
    public List<dmBlockBase> blockBaseList;
    public GameObject squarePrefab;
    public Vector2 squareSize;
    public Vector2 genePos;
    [HideInInspector]
    public dmBlock nowBlock;

    void Start()
    {
    }
    public void BuildRandomBlock()
    {
        if (nowBlock != null)
        {
            DestroySquares(nowBlock);
        }

        dmBlockBase inBuildingBlock = blockBaseList[Random.Range(0, blockBaseList.Count)];
        nowBlock = new dmBlock();
        nowBlock.InitBlock(inBuildingBlock);
        foreach (Vector2 vec in nowBlock.bindBase.squareCoordList)
        {
            GameObject newSquare = Instantiate(squarePrefab);
            newSquare.transform.SetParent(transform);
            newSquare.GetComponent<RectTransform>().sizeDelta = squareSize;
            newSquare.transform.localPosition = genePos +  new Vector2(squareSize.x * vec.x * 2.5f, -squareSize.x * vec.y * 2.5f);
            Debug.Log(vec);
            nowBlock.squareList.Add(newSquare);
        }
    }

    public void DestroySquares(dmBlock block)
    {
        foreach (GameObject squareInstance in block.squareList)
        {
            Destroy(squareInstance);
        }
    }
}
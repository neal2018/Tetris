using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class BlockBuilder : MonoBehaviour {
    public List<BlockBase> blockBaseList;
    public GameObject squarePrefab;
    public Vector2 squareSize;
    [HideInInspector]
    public Block nowBlock;
    public void BuildRandomBlock()
    {
        if(nowBlock!=null)DestroySquares(nowBlock);
        BlockBase inBuildingBlock = blockBaseList[Random.Range(0, blockBaseList.Count)];
        nowBlock = new Block();
        nowBlock.InitBlock(inBuildingBlock);
        foreach (Vector2 vec in inBuildingBlock.squareCoordList)
        {
            GameObject newSquare = Instantiate(squarePrefab);
            newSquare.transform.SetParent(transform);
            newSquare.transform.localPosition = new Vector3(squareSize.x * vec.x, -squareSize.x * vec.y);
            newSquare.GetComponent<RectTransform>().sizeDelta = squareSize;
            nowBlock.squareList.Add(newSquare);
        }
    }
    public void DestroySquares(Block block)
    {
        foreach(GameObject squareInstance in block.squareList)
        {
            Destroy(squareInstance);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmGameArea : MonoBehaviour
{
    public dmBlockBuilder blockBuilder;
    public Vector2 areaSize;
    public Vector2 initPos;
    RectTransform myRectTransform { get { return GetComponent<RectTransform>(); } }
    dmBlock nowControlBlock;

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform.sizeDelta = new Vector2(areaSize.x * blockBuilder.squareSize.x,
                                                areaSize.y * blockBuilder.squareSize.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        if (nowControlBlock != null)
        {
            blockBuilder.DestroySquares(nowControlBlock);
        }
        blockBuilder.BuildRandomBlock();
        NextBlock();
    }

    public void NextBlock()
    {
        PutInBlock(blockBuilder.nowBlock);
        blockBuilder.BuildRandomBlock();
    }

    public void PutInBlock(dmBlock block)
    {
        nowControlBlock = block;
        blockBuilder.nowBlock = null;
        foreach (GameObject square in block.squareList)
        {
            square.transform.SetParent(transform);
        }
        MoveBlockTo(block, initPos);
    }

    public void MoveBlockTo(dmBlock onMoveBlock, Vector2 targetPos)
    {
        for (int i = 0; i < onMoveBlock.bindBase.squareCoordList.Count; i++)
        {
            Vector2 squareCoord = targetPos + onMoveBlock.bindBase.squareCoordList[i];
            onMoveBlock.squareList[i].transform.localPosition = AreaPos2Local(squareCoord);
            onMoveBlock.squareList[i].SetActive(squareCoord.y >= 0);
        }
    }

    public Vector2 AreaPos2Local(Vector2 areaPos)
    {
        return new Vector2((areaPos.x + 0.5f) * blockBuilder.squareSize.x,
                        -(areaPos.y + 0.5f) * blockBuilder.squareSize.y);
    }
}

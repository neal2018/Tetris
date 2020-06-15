using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dmGameArea : MonoBehaviour
{
    public dmBlockBuilder blockBuilder;
    public Vector2 areaSize;
    public Vector2 initPos;
    [Range(1, 10)]
    public float fallSpeed;
    RectTransform myRectTransform { get { return GetComponent<RectTransform>(); } }
    dmBlock nowControlBlock;
    bool runFlag;
    float fallInteval { get { return 1 / fallSpeed; } }
    float timeCount;
    Vector2 nowBlockPos;
    Dictionary<Vector2, GameObject> squareMap = new Dictionary<Vector2, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform.sizeDelta = new Vector2(areaSize.x * blockBuilder.squareSize.x,
                                                areaSize.y * blockBuilder.squareSize.y);
        runFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (runFlag)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= fallInteval)
            {
                timeCount -= fallInteval;
                DoFall();
            }
        }
    }

    public void DoFall()
    {
        Vector2 targetPos = nowBlockPos + new Vector2(0, 1);
        if (!CheckCollision(nowControlBlock, targetPos))
        {
            MoveBlockTo(nowControlBlock, targetPos);
        }
        else
        {
            FallGround(nowControlBlock);
            NextBlock();
        }
    }

    public bool CheckCollision(dmBlock block, Vector2 targetPos)
    {
        foreach (Vector2 squareCoord in block.bindBase.squareCoordList)
        {
            Vector2 squarePos = squareCoord + targetPos;
            if (squarePos.y >= areaSize.y || squarePos.x < 0 || squarePos.x >= areaSize.x)
            {
                return true;
            }

            if (squareMap.ContainsKey(squarePos))
            {
                return true;
            }
        }
        return false;
    }

    public void FallGround(dmBlock block)
    {
        for (int i = 0; i < block.bindBase.squareCoordList.Count; i++)
        {
            squareMap.Add(nowBlockPos + block.bindBase.squareCoordList[i], block.squareList[i]);
            block.squareList[i].GetComponentInChildren<Image>().color = Color.gray;
        }
    }

    public void StartGame()
    {
        if (nowControlBlock != null)
        {
            blockBuilder.DestroySquares(nowControlBlock);
        }
        if (squareMap != null)
        {
            foreach (Vector2 squareCoord in squareMap.Keys)
            {
                Destroy(squareMap[squareCoord]);
            }
        }
        squareMap.Clear();
        blockBuilder.BuildRandomBlock();
        NextBlock();
        timeCount = 0;
        runFlag = true;
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
        if (CheckCollision(onMoveBlock, targetPos))
        {
            runFlag = false;
            return;
        }

        for (int i = 0; i < onMoveBlock.bindBase.squareCoordList.Count; i++)
        {
            Vector2 squareCoord = targetPos + onMoveBlock.bindBase.squareCoordList[i];
            onMoveBlock.squareList[i].transform.localPosition = AreaPos2Local(squareCoord);
            onMoveBlock.squareList[i].SetActive(squareCoord.y >= 0);
        }

        nowBlockPos = targetPos;
    }

    public Vector2 AreaPos2Local(Vector2 areaPos)
    {
        return new Vector2((areaPos.x) * blockBuilder.squareSize.x * 2.5f,
                        -(areaPos.y) * blockBuilder.squareSize.y * 2.5f);
    }
}

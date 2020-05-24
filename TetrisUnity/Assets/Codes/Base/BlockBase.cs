﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewBlock", menuName = "Tetris/BlockBase")]
public class BlockBase : ScriptableObject
{
    public Vector2 blockSize;
    public List<Vector2> squareCoordList;
    public BlockBase()
    {
        squareCoordList = new List<Vector2>();
    }
}

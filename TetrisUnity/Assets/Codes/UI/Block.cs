using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Block  {
    public List<GameObject> squareList = new List<GameObject>();
    public BlockBase bindBase;
    public void InitBlock(BlockBase blockBase)
    {
        bindBase = blockBase;
    }
}
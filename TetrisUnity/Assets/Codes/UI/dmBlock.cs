using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class dmBlock  {
    public List<GameObject> squareList = new List<GameObject>();
    public dmBlockBase bindBase;
    public void InitBlock(dmBlockBase blockBase)
    {
        bindBase = blockBase;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : InventoryObject
{

    // Start is called before the first frame update
    void Start()
    {
        canHaveMultiple = true;
        base.Init();
    }

   
}

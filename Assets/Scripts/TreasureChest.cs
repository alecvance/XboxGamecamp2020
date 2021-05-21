using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : OpenAndCloseableObject
{

    public InventoryObject treasure;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        if (treasure != null) treasure.Conceal();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Open()
    {
        Debug.Log(gameObject.name + " open chest: " + Time.time);

        animator.enabled = true;
        base.Open();

        if (treasure != null) treasure.Reveal();

    }
    
    override public void StartInteraction(PlayerMovement player)
    {
        Debug.Log(name + ": " + player.name + " interact : " + Time.time);


        if (state == OpenAndCloseableObject.OCObjectState.closed)
        {

            //animator.SetBool("Opening", true);
            //state = OpenAndCloseableObject.OCObjectState.opening;
            

          //  treasure.Reveal();
            base.TryOpenBy(player.gameObject.GetComponent<Collider2D>());
            

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses a list of strings and integers as names and count of object types, styled as a Dictionary.

public class PlayerInventory : MonoBehaviour
{

    Dictionary<string, int> inventory = new Dictionary<string, int>(20); // init with a specific capacity of different object types.

    // add one item of type 'name' to the list
    public void addItem(string name, bool canHaveMultiple){

        // need to check to see if it is already in the list, and if we can have multiple
        if(inventory.ContainsKey(name)){

            int count = inventory[name];

            if(canHaveMultiple){
                // add one to objects with this name
                count++;
                inventory[name] = count ;
            }

        }else{
            inventory.Add(name, 1);
        }
    }

    // add multiple items of the same type 'name' to the list
    public void addItems(string name, int count){
        if (inventory.ContainsKey(name))
        {
            inventory[name] += count;
        }else{
            inventory.Add(name, count);
        }
    }


    // true if has one or more item of type 'name'
    public bool hasItem(string name){
        if (inventory.ContainsKey(name))
        {
            int count = inventory[name];
            if (count > 0)
            {
                return true;
            }
        } 
        return false;
    }

    // remove one item of type 'name'
    public void removeItem(string name){
        if (inventory.ContainsKey(name)){
            int count = inventory[name];
            if(count > 0 ){
                count--;
                inventory[name]=count;
            }
        }
    }



}

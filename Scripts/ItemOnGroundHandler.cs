using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGroundHandler : MonoBehaviour
{
    public Item item;
    public Sprite sprite;
    public GameObject arrow;

    private bool isBeingLookedAt = false;

    private void Start()
    {
        Refreash();
    }

    public void ShowArrow(bool show)
    {
        arrow.SetActive(show);
    }

    public void Refreash()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        GetComponent<SpriteRenderer>().color = item.color;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && !isBeingLookedAt)
        {
            player_script.instance.AddItemOnGroundHandler(this);
            isBeingLookedAt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player" && isBeingLookedAt)
        {
            player_script.instance.RemoveItemOnGroundHandler(this);
            isBeingLookedAt = false;
        }
    }
}

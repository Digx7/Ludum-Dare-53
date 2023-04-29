using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public bool canPlayerSee = false;
    public bool wasSeenWithGPS = false;

    private bool isTurningOn = false;
    private bool isTurningOff = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isTurningOn)
        {
            Color color = spriteRenderer.color;
            float value = color.a;
            value -= Time.deltaTime;
            if(value < 0) {
                value = 0;
                isTurningOn = false;
            }
            color.a = value;
            spriteRenderer.color = color;
        }
        else if(isTurningOff)
        {
            Color color = spriteRenderer.color;
            float value = color.a;
            value += Time.deltaTime;
            if(value > 1) {
                value = 1;
                isTurningOff = false;
            }
            color.a = value;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && !canPlayerSee)
        {
            TurnLightOn();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player" && !wasSeenWithGPS)
        {
            TurnLightOff();
        }
    }

    private void TurnLightOn()
    {
        canPlayerSee = true;
        isTurningOn = true;

        if(player_script.instance.playerHasAGPS) wasSeenWithGPS = true;

        //Refreash();
    }

    private void TurnLightOff()
    {
        canPlayerSee = false;
        isTurningOff = true;

        //Refreash();
    }

    private void Refreash()
    {
        Color color = spriteRenderer.color;
        
        if(canPlayerSee)
        {
            color.a = 0;
        }
        else
        {
            color.a = 1;
        }
        spriteRenderer.color = color;

        
    }
}

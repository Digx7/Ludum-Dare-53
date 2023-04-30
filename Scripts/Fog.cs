using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public bool canPlayerSee = false;
    public bool wasSeenWithGPS = false;
    public float turnOnSpeed = 1f;
    public float turnOffSpeed = 1;

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
            value -= Time.deltaTime*turnOnSpeed;
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
            value += Time.deltaTime * turnOffSpeed;
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
        if(col.tag == "Vision" && !canPlayerSee)
        {
            TurnLightOn();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Vision" && !wasSeenWithGPS)
        {
            TurnLightOff();
        }
    }

    private void TurnLightOn()
    {
        canPlayerSee = true;
        isTurningOn = true;
        isTurningOff = false;


        if (player_script.instance.playerHasAGPS) wasSeenWithGPS = true;

        //Refreash();
    }

    private void TurnLightOff()
    {
        canPlayerSee = false;
        isTurningOff = true;
        isTurningOn = false;


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

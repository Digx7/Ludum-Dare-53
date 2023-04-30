using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public bool canPlayerSee = false;
    public bool wasSeenWithGPS = false;

    [Range(0.0f, 1.0f)]
    public float LightsOn = 0;
    public float TurnOnSpeed = 1;

    [Range(0.0f, 1.0f)]
    public float LightsOff = 1;
    public float TurnOffSpeed = 1;

    private bool isTurningOn = false;
    private bool isTurningOff = true;

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
            value -= Time.deltaTime * TurnOnSpeed;
            if(value < LightsOn) {
                value = LightsOn;
                isTurningOn = false;
            }
            color.a = value;
            spriteRenderer.color = color;
        }
        else if(isTurningOff)
        {
            Color color = spriteRenderer.color;
            float value = color.a;
            value += Time.deltaTime * TurnOffSpeed;
            if(value > LightsOff) {
                value = LightsOff;
                isTurningOff = false;
            }
            color.a = value;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
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
        isTurningOff = false;

        if(player_script.instance.playerHasAGPS) wasSeenWithGPS = true;

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

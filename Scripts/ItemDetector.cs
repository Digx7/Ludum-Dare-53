using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDetector : MonoBehaviour
{
    public Item itemToLookFor;
    public float maxDistance;

    public UnityEvent OnItemDetected;

    private void Start()
    {
        player_script.instance.OnDropAllItems.AddListener(TryToDetectItem);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TryToDetectItem();
    }

    public void TryToDetectItem()
    {
        ItemOnGroundHandler[] itemsOnGround = FindObjectsOfType<ItemOnGroundHandler>();
        
        foreach (ItemOnGroundHandler itemOnGround in itemsOnGround)
        {
            float distance = Vector3.Distance(this.transform.position, itemOnGround.gameObject.transform.position);
            if(distance > maxDistance) continue;

            if(itemOnGround.item == itemToLookFor)
            {
                Debug.Log("Found item to look for");
                Destroy(itemOnGround.gameObject);
                OnItemDetected.Invoke();
            }
        }
    }
}

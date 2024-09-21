using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
       //make the coin disappear when it is triggered by the player
        gameObject.SetActive(false);
    }
}



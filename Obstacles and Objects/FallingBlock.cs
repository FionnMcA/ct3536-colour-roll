using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FallingBlock : MonoBehaviour{
    private Rigidbody rb;
    private Vector3 initialPosition;
    public float heightBeforeFall = 10f;
    public float fallSpeed = 10f;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        float randomDelay = Random.Range(0f, 3f);
        StartCoroutine(FallAndRise(randomDelay));
}

    IEnumerator FallAndRise(float randomDelay){
        //wait a random time before starting the falling and rising sequence
        //so the blocks aren't in sync
        yield return new WaitForSeconds(randomDelay);
        while (true){
            //block falls down
            rb.isKinematic = false;  
            rb.useGravity = true;   

            //wait on the floor for 3 seconds
            yield return new WaitForSeconds(3);

            rb.isKinematic = true;
            rb.useGravity = false;  

            //rise the block back to its initial position
            while (Vector3.Distance(transform.position, initialPosition) >= 0.1f){
                //move the blck towards the initiial position
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, fallSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = initialPosition;
            //wait 1 sec before falling back down
            yield return new WaitForSeconds(1);
        }
    }
}

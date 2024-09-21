using UnityEngine;
using System.Collections;

public class SidewaysBlock: MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private float speed = 5f; 

    void Start(){
        //the left edge and right edge of section
        startPosition = new Vector3(8f, transform.position.y, transform.position.z);
        targetPosition = new Vector3(27f, transform.position.y, transform.position.z);

        float randomDelay = Random.Range(0f, 2f);
        StartCoroutine(MoveToTarget(randomDelay));
    }

    IEnumerator MoveToTarget(float randomDelay){
        //wait a random time before starting the left and right sequence
        //so the blocks aren't in sync
        yield return new WaitForSeconds(randomDelay);
        while (true){
            //move towards the target edge
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f){
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            // Swap the start and target positions
            (startPosition, targetPosition) = (targetPosition, startPosition);
            //wait a second before moving back
            yield return new WaitForSeconds(1f);
        }
    }
}

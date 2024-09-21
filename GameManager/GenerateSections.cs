using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GenerateSections : MonoBehaviour{
    public GameObject[] easySections, mediumSections, hardSections, veryHardSections, impossibleSections;
    public GameObject noObstacleSection;
    public PlayerController playerController;
    private List<GameObject> activeSections = new List<GameObject>();
    private float zPos = 0;
    private int sectionsPassed = 0;
    private float initialDelay = 0.5f;
    private float sectionGenerationDelay = 1.25f;

    private void Start(){
        GenerateInitialSection();
        //the first section with spawns after 0.5 seconds where as the others spawn
        //after 1.25 seconds
        StartCoroutine(GenerateSection(initialDelay));
    }

    //generate a blank section so the player can roam freely during the countdown
    private void GenerateInitialSection(){
        //the sections spawning got a little messy so i had to mess around in the scene
        //view to find coordinates that had seamless generation of new sections
        //hence the strange values for coordinates
        Vector3 spawnPosition = new Vector3(17.4f, -3.73f, 24f);
        GameObject section = PoolManager.GetGameObject(noObstacleSection.name, spawnPosition, Quaternion.identity);
        if (section != null){
            activeSections.Add(section);
            zPos = spawnPosition.z + 20f;
        }
    }

    private void Update(){
        ReturnSections();
    }

    private IEnumerator GenerateSection(float delay){
        //wait before generating a new section
        yield return new WaitForSeconds(delay);

        GameObject[] currentSections = SelectCurrentSections();
        //pick a random variant from the sections array
        int secNum = Random.Range(0, currentSections.Length);
        //spawn position 20 units forward from the previous section
        Vector3 spawnPosition = new Vector3(8.697972f, -1.865086f, zPos);
        //borrow section from pool
        GameObject section = PoolManager.GetGameObject(currentSections[secNum].name, spawnPosition, Quaternion.identity);
        if (section != null){
            activeSections.Add(section);
        }       
        zPos += 20f;
        sectionsPassed++;
        StartCoroutine(GenerateSection(sectionGenerationDelay));
    }


    private GameObject[] SelectCurrentSections(){
        //the higher the number of sections passed the harder the sections
        //the players speed also increases
        //and we change the section in the score controller
        //first 10 sections are easy, next 15 are medium, next 20 are hard
        //next 30 are very hard and then an indefinite amount of impossible levels
        if (sectionsPassed < 10){
            playerController.IncreaseSpeed(0.2f);
            playerController.IncreaseSidewaysMoveSpeed(0.25f);
            ScoreController.ChangeSection(1); 
            return easySections;
        }
        else if (sectionsPassed < 25){
            playerController.IncreaseSpeed(0.25f);
            playerController.IncreaseSidewaysMoveSpeed(0.25f);
            ScoreController.ChangeSection(2); 
            return mediumSections;
        }
        else if (sectionsPassed < 45){
            playerController.IncreaseSpeed(0.3f);
            playerController.IncreaseSidewaysMoveSpeed(0.3f);
            ScoreController.ChangeSection(3); 
            return hardSections;
        }
        else if (sectionsPassed < 75){
            playerController.IncreaseSpeed(0.35f);
            playerController.IncreaseSidewaysMoveSpeed(0.35f);
            ScoreController.ChangeSection(4); 
            return veryHardSections;
        }
        else{
            playerController.IncreaseSpeed(0.35f);
            playerController.IncreaseSidewaysMoveSpeed(0.35f);
            ScoreController.ChangeSection(5); 
            return impossibleSections;
        }
    }

    //loop through all active sections, if the sections position is 2.5 units behind
    //the player return it to the pool and remove it from active sections
    private void ReturnSections(){
        float thresholdZ = playerController.transform.position.z - 50f;
        for (int i = activeSections.Count - 1; i >= 0; i--){
            if (activeSections[i].transform.position.z < thresholdZ){
                PoolManager.ReturnGameObject(activeSections[i]);
                activeSections.RemoveAt(i);
            }
        }
    }
}








using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolManager {
    private static Dictionary<string, LinkedList<GameObject>> Pools = 
        new Dictionary<string, LinkedList<GameObject>>();
        public static GameObject GetGameObject(string prefabName, Vector3 spawnPosition, Quaternion rotation){
    if (!Pools.ContainsKey(prefabName)){
        Pools.Add(prefabName, new LinkedList<GameObject>());
    }

    LinkedList<GameObject> pool = Pools[prefabName];

    while (pool.Count > 0){
        GameObject go = pool.First.Value;
        pool.RemoveFirst();

        // Check if GameObject is valid and not active
        if (go != null && go.activeSelf == false) {
            go.SetActive(true);
            go.transform.position = spawnPosition;
            go.transform.rotation = rotation;
            return go;
        }
    }

    //if no valid object is found in the pool, instantiate a new one
    GameObject newGo = (GameObject)GameObject.Instantiate(Resources.Load(prefabName));
    newGo.transform.position = spawnPosition;
    newGo.transform.rotation = rotation;
    newGo.name = prefabName;
    return newGo;
}
    public static void ReturnGameObject(GameObject go) {
        if (!Pools.ContainsKey(go.name)){
            Pools.Add(go.name, new LinkedList<GameObject>());
        }

        LinkedList<GameObject> pool = Pools[go.name];

        pool.AddFirst(go);

        go.transform.position = Vector3.zero;
        
        if (go.transform.rotation.z != 0f) {
            go.transform.rotation = Quaternion.identity;
        }

        go.SetActive(false);
    }

}

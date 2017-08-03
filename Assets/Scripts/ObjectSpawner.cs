using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public static bool useObjectPool = true;

    public PooledGameObject pooledPrefab;

    private void Update () {
        if (useObjectPool) {
            PooledGameObject go = pooledPrefab.GetPooledInstance (this.transform);
            go.transform.position = this.transform.position;
        } else {
            PooledGameObject go = GameObject.Instantiate<PooledGameObject> (pooledPrefab);
        }
    }

    private void OnDisable () {
        pooledPrefab.Clear ();
    }
}

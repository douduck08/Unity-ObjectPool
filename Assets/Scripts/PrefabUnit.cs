using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabUnit : MonoBehaviour {

    public static int createCount = 0;
    public static int destoryCount = 0;

    private void Awake () {
        createCount += 1;
        Debug.Log ("PrefabUnit.createCount = " + createCount);
    }

    private void OnDestroy () {
        destoryCount += 1;
        Debug.Log ("PrefabUnit.destoryCount = " + destoryCount);
    }

    private float m_lifeTime;
    private PooledGameObject m_pooledGameObject;

    private void OnEnable () {
        m_lifeTime = 1f;
        m_pooledGameObject = this.GetComponent<PooledGameObject> ();
    }

    private void Update () {
        if (m_lifeTime < 0f) {
            if (ObjectSpawner.useObjectPool) {
                m_pooledGameObject.BackToPool ();
            } else {
                Destroy (this.gameObject);
            }
        }
        m_lifeTime -= Time.deltaTime;
    }
}
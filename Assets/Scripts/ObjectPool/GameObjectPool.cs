using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool {

    private PooledGameObject m_prefab;
    private List<PooledGameObject> m_availableObjects = new List<PooledGameObject> ();
    private List<PooledGameObject> m_usingObjects = new List<PooledGameObject> ();

    public GameObjectPool (PooledGameObject prefab, int initailSize) {
        m_prefab = prefab;
        for (int i = 0; i < initailSize; i++) {
            PooledGameObject go = GameObject.Instantiate<PooledGameObject> (m_prefab);
            go.pool = this;
            m_availableObjects.Add (go);
            go.gameObject.SetActive (false);
        }
    }

    public GameObjectPool (PooledGameObject prefab, Transform anchor, int initailSize) {
        m_prefab = prefab;
        for (int i = 0; i < initailSize; i++) {
            PooledGameObject go = GameObject.Instantiate<PooledGameObject> (m_prefab, anchor);
            go.pool = this;
            m_availableObjects.Add (go);
            go.gameObject.SetActive (false);
        }
    }

    public PooledGameObject GetPooledInstance (Transform parent) {
        lock (m_availableObjects) {
            int lastIndex = m_availableObjects.Count - 1;
            if (lastIndex >= 0) {
                PooledGameObject go = m_availableObjects[lastIndex];
                m_availableObjects.RemoveAt (lastIndex);
                m_usingObjects.Add (go);
                go.gameObject.SetActive (true);
                if (go.transform.parent != parent) {
                    go.transform.SetParent (parent);
                }
                return go;
            } else {
                PooledGameObject go = GameObject.Instantiate<PooledGameObject> (m_prefab, parent);
                go.pool = this;
                m_usingObjects.Add (go);
                return go;
            }
        }
    }

    public void BackToPool (PooledGameObject go) {
        lock (m_availableObjects) {
            m_availableObjects.Add (go);
            go.gameObject.SetActive (false);
        }
    }

    public void Clear (bool includeUsingObject = true) {
        lock (m_availableObjects) {
            for (int i = m_availableObjects.Count - 1; i >= 0; i--) {
                PooledGameObject go = m_availableObjects[i];
                m_availableObjects.RemoveAt (i);
                GameObject.Destroy (go.gameObject);
            }
        }
        if (includeUsingObject) {
            lock (m_usingObjects) {
                for (int i = m_usingObjects.Count - 1; i >= 0; i--) {
                    PooledGameObject go = m_usingObjects[i];
                    m_usingObjects.RemoveAt (i);
                    GameObject.Destroy (go.gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObject : MonoBehaviour {

    [SerializeField]
    private int m_initailSize = 5;

    private GameObjectPool m_pool;
    public GameObjectPool pool {
        set {
            m_pool = value;
        }
    }

    public void InitializePool (Transform anchor) {
        if (m_pool == null) {
            m_pool = new GameObjectPool (this, anchor, m_initailSize);
        }
    }

    public PooledGameObject GetPooledInstance (Transform parent) {
        if (m_pool == null) {
            m_pool = new GameObjectPool (this, parent, m_initailSize);
        }
        return m_pool.GetPooledInstance (parent);
    }

    public void BackToPool () {
        m_pool.BackToPool (this);
    }

    public void Clear (bool includeUsingObject = true) {
        m_pool.Clear (includeUsingObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObject : MonoBehaviour {

    [SerializeField]
    private int m_initailSize = 5;

    private GameObjectPool m_pool;
    public GameObjectPool pool {
        get {
            if (m_pool == null) {
                m_pool = new GameObjectPool (this, m_initailSize);
            }
            return m_pool;
        }
        set {
            m_pool = value;
        }
    }

    public void InitailizePool (Transform anchor) {
        if (m_pool == null) {
            m_pool = new GameObjectPool (this, anchor, m_initailSize);
        }
    }

    public PooledGameObject GetPooledInstance (Transform parent) {
        return this.pool.GetPooledInstance (parent);
    }

    public void BackToPool () {
        this.pool.BackToPool (this);
    }

    public void Clear () {
        this.pool.Clear ();
    }
}

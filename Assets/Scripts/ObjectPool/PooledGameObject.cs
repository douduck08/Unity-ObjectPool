using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObject : MonoBehaviour {

    [SerializeField]
    private int m_initailSize = 5;

    private GameObjectPool m_pool;
    private bool Initialized = false;
    public GameObjectPool pool {
        //get {
        //    if (m_pool == null) {
        //        m_pool = new GameObjectPool (this, m_initailSize);
        //    }
        //    return m_pool;
        //}
        set {
            m_pool = value;
        }
    }

    public void InitailizePool (Transform anchor) {
        if (m_pool == null) {
            m_pool = new GameObjectPool (this, anchor, m_initailSize);
            Initialized = true;
        }
    }

    public PooledGameObject GetPooledInstance (Transform parent) {
        if (Initialized == false) { 
            InitailizePool(parent);
        }
        return this.m_pool.GetPooledInstance (parent);
    }

    public void BackToPool () {
        this.m_pool.BackToPool (this);
    }

    public void Clear (bool includeUsingObject = true) {
        this.m_pool.Clear (includeUsingObject);
    }
}

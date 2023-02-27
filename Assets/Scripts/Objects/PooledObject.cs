using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : CustomBehaviour, IPooledObject
{
    private Transform m_DeactiveParent;
    protected string m_PooledTag;
    public void SetPooledTag(string _pooledTag)
    {
        m_PooledTag = _pooledTag;
    }
    public override void Initialize()
    {
        m_DeactiveParent = this.transform.parent;
    }
    public virtual void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
    }
    public virtual void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);
        this.transform.SetParent(m_DeactiveParent);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }
}

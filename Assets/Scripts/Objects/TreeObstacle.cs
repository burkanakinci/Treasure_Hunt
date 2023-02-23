using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObstacle : PooledObject
{
    public TreeType TreeCurrentType = TreeType.Green;
    [SerializeField] private SpriteRenderer m_TreeRenderer;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
    }

    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }

    public void SetTree(TreeType _treeType)
    {
        TreeCurrentType = _treeType;
        m_TreeRenderer.sprite = GameManager.Instance.Entities.GetTreeSprite(_treeType);
    }
}

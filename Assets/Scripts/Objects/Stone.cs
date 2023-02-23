using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : PooledObject
{
    public StoneType StoneCurrentType = StoneType.SingleStone;
    [SerializeField] private SpriteRenderer m_StoneRenderer;
    [SerializeField] private BoxCollider2D m_StoneCollider;
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

    public void SetStone(StoneType _stoneType)
    {
        StoneCurrentType = _stoneType;

        m_StoneCollider.offset = GameManager.Instance.Entities.GetStoneValue(_stoneType).StoneColliderOffset;
        m_StoneCollider.size = GameManager.Instance.Entities.GetStoneValue(_stoneType).StoneColliderSize;

        m_StoneRenderer.sprite = GameManager.Instance.Entities.GetStoneValue(_stoneType).StoneSprite;
    }
}

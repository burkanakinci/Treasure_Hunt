using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entities : CustomBehaviour
{
    [System.Serializable]
    public class StoneValue
    {
        public Sprite StoneSprite;
        public Vector2 StoneColliderOffset;
        public Vector2 StoneColliderSize;
    }
    #region Fields
    [SerializeField] private Transform[] m_ActiveParents;
    [SerializeField] private Sprite[] m_TreeSprites;
    [SerializeField] private StoneValue[] m_StoneValues;
    #endregion

    #region ExternalAccess
    #endregion

    public override void Initialize()
    {
    }

    public Transform GetActiveParent(ActiveParents _activeParent)
    {
        return m_ActiveParents[(int)_activeParent];
    }
    public Sprite GetTreeSprite(TreeType _treeType)
    {
        return m_TreeSprites[(int)_treeType];
    }

    public StoneValue GetStoneValue(StoneType _stoneType)
    {
        return m_StoneValues[(int)_stoneType];
    }


}
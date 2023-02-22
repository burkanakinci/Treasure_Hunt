using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : CustomBehaviour
{
    [SerializeField] private SpriteRenderer m_SensorRenderer;
    [SerializeField] private List<Sprite> m_RadarSprites;

    public override void Initialize()
    {
        m_SensorRenderer.sprite = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : CustomBehaviour<PlayerMiner>
{
    [SerializeField] private Transform m_MainCamera;
    [SerializeField] private CameraData m_CameraData;
    public override void Initialize(PlayerMiner _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }

    private void LateUpdate()
    {
        m_MainCamera.transform.position = CachedComponent.transform.position + (Vector3.forward * m_CameraData.CameraZDistance);
    }
}

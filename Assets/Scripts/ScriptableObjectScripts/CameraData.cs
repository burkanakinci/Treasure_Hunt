using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CameraData", menuName = "Camera Data")]
public class CameraData : ScriptableObject
{

    #region Datas
    [SerializeField] private float m_CameraZDistance;
    #endregion

    #region ExternalAccess
    public float CameraZDistance => m_CameraZDistance;
    #endregion
}

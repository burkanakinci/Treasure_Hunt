using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomBehaviour : MonoBehaviour
{
    public abstract void Initialize();
}

public abstract class CustomBehaviour<T> : MonoBehaviour
{
    [HideInInspector] public T CachedComponent;
    public virtual void Initialize(T _cachedComponent)
    {
        CachedComponent = _cachedComponent;
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PoolBase : MonoBehaviour
{
    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable()
    {
        Recycle();
    }

    public virtual void Spawn()
    {
        
    }

    public virtual void Recycle()
    {

    }
}

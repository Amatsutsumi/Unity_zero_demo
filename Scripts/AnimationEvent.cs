using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private void PlaySound(string name)
    {
        GamePoolManager.Instance().TryGetPoolItem(name);
    }
}

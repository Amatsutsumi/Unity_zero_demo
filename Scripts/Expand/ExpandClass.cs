using UnityEngine;

public static class ExpandClass
{
    public static bool AnimationAtTag(this Animator animator, string tagName, int indexLayer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(indexLayer).IsTag(tagName);
    }

    public static float GetTransformDistance(Transform player,Transform enemy)
    {
        return (enemy.transform.position - player.transform.position).magnitude;
    }

    public static Vector3 GetTransformVector3(Transform enemy,Transform player)
    {
        return(enemy.transform.position - player.transform.position);
    }

    public static void RotateTowards(Transform currentTrans,Transform targetTrans)
    {
        Vector3 targetVector = new Vector3(targetTrans.position.x - currentTrans.position.x, 0, targetTrans.position.z - currentTrans.position.z);
        Quaternion targetQua = Quaternion.LookRotation(targetVector);
        currentTrans.rotation = Quaternion.Slerp(currentTrans.rotation, targetQua, 0.9f);
    }
}

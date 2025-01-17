using UnityEngine;

//敌人管理器，发放指令用
public class EnemyManager : MonoSingle<EnemyManager>
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public Transform GetPlayer()
    {
        return player;
    }
}

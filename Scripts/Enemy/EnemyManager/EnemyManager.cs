using UnityEngine;

//���˹�����������ָ����
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

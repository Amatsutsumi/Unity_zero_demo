using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthData", menuName = "Scriptable Objects/CharacterHealthData")]
public class CharacterHealthData : ScriptableObject
{
    [SerializeField]private float _maxHP;
    [SerializeField]private float _maxPower;
    //Ӳֱʱ��
    [SerializeField]private float hitTime;
    public float MaxHP => _maxHP;
    public float MaxPower => _maxPower;
    public float HitTime => hitTime;
}

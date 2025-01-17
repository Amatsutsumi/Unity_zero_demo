using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData", menuName = "Scriptable Objects/ComboData")]
public class ComboData : ScriptableObject
{
    [SerializeField]private string _comboName;
    [SerializeField]private List<ComboDamage> _comboDamage;
    [SerializeField] private float _coldTime;
    [SerializeField] private ComboData _nextCombo;
    [SerializeField] private ComboData _branchCombo;
    [SerializeField] private DamageType _damageType;

    public string ComboName => _comboName;
    public List<ComboDamage> ComboDamage => _comboDamage;
    public float ColdTime => _coldTime;
    public ComboData NextCombo => _nextCombo;
    public ComboData BranchCombo => _branchCombo;
    public DamageType DamageType => _damageType;
}


public enum DamageType
{ 
    weapon,
    punch,
}

[System.Serializable]
public class ComboDamage
{
    public string hitName;
    public string parryName;
    public float damage;
}

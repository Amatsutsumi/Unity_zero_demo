using UnityEngine;

public enum SoundType
{ 
    Attack,
    Hit,
    Parry,
    Foot
}

[System.Serializable]
public class SoundData
{
    public SoundType Type;
    public AudioClip[] audioClips;
}

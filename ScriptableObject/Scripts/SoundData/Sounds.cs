using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "Scriptable Objects/Sounds")]
public class Sounds : ScriptableObject
{
    [SerializeField]private List<SoundData> sounds = new List<SoundData>();

    public AudioClip GetSound(SoundType type)
    {
        switch (type)
        {
            case (SoundType.Attack):
                return (sounds[0].audioClips[Random.Range(0, sounds[0].audioClips.Length - 1)]);
            case (SoundType.Hit):
                return (sounds[1].audioClips[Random.Range(0, sounds[1].audioClips.Length - 1)]);
            case (SoundType.Parry):
                return (sounds[2].audioClips[Random.Range(0, sounds[2].audioClips.Length - 1)]);
            case (SoundType.Foot):
                return (sounds[3].audioClips[Random.Range(0, sounds[3].audioClips.Length - 1)]);
        }
        return null;
    }
}
 
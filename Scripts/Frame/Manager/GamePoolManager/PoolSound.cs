using UnityEngine;

public class PoolSound : PoolBase
{
    [SerializeField]private SoundType soundType;
    [SerializeField]private Sounds sounds;
    private AudioClip soundClip;
    private AudioSource soundSource;
    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }
    public override void Spawn()
    {
        PlaySound();
    }

    private void PlaySound()
    {
        soundClip =  sounds.GetSound(soundType);
        soundSource.clip = soundClip;
        soundSource.Play();
        TimerManager.Instance().TryGetOneTimer(0.3f,DisableAudio);
    }

    private void DisableAudio()
    {
        soundSource.Stop();
        this.gameObject.SetActive(false);
    }
}

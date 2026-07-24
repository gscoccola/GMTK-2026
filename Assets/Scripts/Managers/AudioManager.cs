using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfx;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            sfx = GetComponent<AudioSource>();
            instance = this;
        } else
        {
            DontDestroyOnLoad(this);
        }

    }

    public void PlayAudio(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
}

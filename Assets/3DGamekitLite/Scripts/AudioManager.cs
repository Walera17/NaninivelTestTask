using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sounds sounds;
    private ObjectPool<AudioSource> audioPool;
    private static AudioManager _instance;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null) 
                _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return _instance;
        }
    }

    void Start()
    {
        audioPool = new(CreateAudioSource, null, null, DestroyAudioSource, false, 3, 3);
    }

    private void DestroyAudioSource(AudioSource obj)
    {
        Destroy(obj.gameObject);
    }

    private AudioSource CreateAudioSource()
    {
        AudioSource audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        audioSource.transform.SetParent(transform);

        audioSource.volume = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1f;

        return audioSource;
    }

    public void PlayDeathEnemy(Vector3 position, float volume = 1f)
    {
        PlayAudioAtLocation(sounds.GetClip(), position, volume);
    }

    private void PlayAudioAtLocation(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource audioSource = audioPool.Get();
        audioSource.volume = volume;
        audioSource.transform.position = position;
        audioSource.PlayOneShot(clip);
        StartCoroutine(ReleaseAudioSource(audioSource, clip.length));
    }

    private IEnumerator ReleaseAudioSource(AudioSource audioSource, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        audioPool.Release(audioSource);
    }
}
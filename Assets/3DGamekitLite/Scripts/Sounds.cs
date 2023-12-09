using UnityEngine;

[CreateAssetMenu(menuName = "Sounds")]
public class Sounds : ScriptableObject
{
    public AudioClip[] clips;

    public AudioClip GetClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
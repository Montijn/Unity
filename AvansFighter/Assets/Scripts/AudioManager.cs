using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource kickSoundEffect;
    [SerializeField] private AudioSource punchSoundEffect;
    [SerializeField] private AudioSource jumpKickSoundEffect;
    [SerializeField] private AudioSource jumpSoundEffect;

    public void PlayKickSound()
    {
        kickSoundEffect.Play();
    }

    public void PlayPunchSound()
    {
        punchSoundEffect.Play();
    }

    public void PlayJumpKickSound()
    {
        jumpKickSoundEffect.Play();
    }

    public void PlayJumpSound()
    {
        jumpSoundEffect.Play();
    }
}

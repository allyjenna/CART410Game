using UnityEngine;

public class AreaAudioTrigger : MonoBehaviour
{
    // Assign the audio clips for each area in the Inspector
    public AudioClip baseSong;
    public AudioClip song1_Reverb;
    public AudioClip song2_Slow;
    public AudioClip song3_Backwards;

    private AudioSource audioSource;

    // Enum to identify areas
    private enum Area
    {
        None,
        Area1,
        Area2,
        Area3
    }

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found. Attach an AudioSource to this GameObject.");
        }
        PlaySong(baseSong);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has a tag corresponding to an area
        Area area = IdentifyArea(other.gameObject.tag);

        // Play the corresponding song based on the area
        switch (area)
        {
            case Area.Area1:
                PlaySong(song1_Reverb);
                break;
            case Area.Area2:
                PlaySong(song2_Slow);
                break;
            case Area.Area3:
                PlaySong(song3_Backwards);
                break;
        }
    }

    private Area IdentifyArea(string tag)
    {
        // Map tags to areas
        switch (tag)
        {
            case "Area1":
                return Area.Area1;
            case "Area2":
                return Area.Area2;
            case "Area3":
                return Area.Area3;
            default:
                return Area.None;
        }
    }

    private void PlaySong(AudioClip clip)
    {
        // Play only if the clip is not already playing
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }


    private void ChangeAudioVolume()
    {
        audioSource.volume = 0.05f;
    }

    private void ChangeAudioSpeed()
    {
        audioSource.pitch = 2f;
    }

    private void ChangeAudioSpatialization()
    {
        audioSource.panStereo = 1f;
    }

    public string GetCurrentPlayingSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            if (audioSource.clip == song1_Reverb) return "Reverbed Music";
            if (audioSource.clip == song2_Slow) return "Slowed Music";
            if (audioSource.clip == song3_Backwards) return "Backwards Music";
        }

        return null; // No sound is currently playing
    }

}

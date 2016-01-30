using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] virginSounds;
    public AudioClip[] angryDemon;
    public AudioClip[] demonFeed;
    public AudioClip[] conjurerSounds;
    public AudioClip[] grapplerSounds;
    public AudioClip[] illusionistSounds;
    public AudioClip sprinterSound;

    public AudioSource virginSource;
    public AudioSource virginSource2;
    public AudioSource demonSource;
    public AudioSource classesSource;

    public void Awake()
    {

    }

    private void PlayVirginSound(int zeroOrSecond)
    {
        if (zeroOrSecond == 0)
        {
            virginSource.PlayOneShot(virginSounds[Random.Range(0, virginSounds.Length)], Random.Range(0.3f, 0.8f));
        }
        else
        {
            virginSource2.PlayOneShot(virginSounds[Random.Range(0, virginSounds.Length)], Random.Range(0.3f, 0.8f));
        }
    }

    private void PlayAngryDemonSound()
    {
        demonSource.PlayOneShot(angryDemon[Random.Range(0, angryDemon.Length)], 1);
    }

    private void PlayFedDemonSound()
    {
        demonSource.PlayOneShot(demonFeed[Random.Range(0, demonFeed.Length)], 1);
    }

    private void PlayConjurerActivate()
    {
        classesSource.PlayOneShot(conjurerSounds[0],1);
    }

    private void PlayConjurerDeactivate()
    {
        classesSource.PlayOneShot(conjurerSounds[1], 1);
    }

    private void PlaySprinterRun()
    {
        classesSource.PlayOneShot(sprinterSound, 1);
    }

    private void PlayIllusionistActivate()
    {
        classesSource.PlayOneShot(illusionistSounds[0], 1);
    }

    private void PlayIllusioinstDeactivate()
    {
        classesSource.PlayOneShot(illusionistSounds[1], 1);
    }

    private void PlayGrapplerActivate()
    {
        classesSource.PlayOneShot(grapplerSounds[0], 1);
    }

    private void PlayGrapplerDeactivate()
    {
        classesSource.PlayOneShot(grapplerSounds[1], 1);
    }
}


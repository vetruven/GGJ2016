using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] virginSounds;
    public AudioClip[] angryDemon;
    public AudioClip[] demonFeed;
    public AudioClip demonHandDown;
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
        EventBus.BeaconActivated.AddListener(PlayConjurerActivate);
        EventBus.BeaconDeactivated.AddListener(PlayConjurerDeactivate);
        // Demon angry = he hit the thing.
        EventBus.GrapplerActivated.AddListener(PlayGrapplerActivate);
        EventBus.GrapplerDeactivated.AddListener(PlayGrapplerDeactivate);
        EventBus.IllusionActivated.AddListener(PlayIllusionistActivate);
        EventBus.IllusionDeactivated.AddListener(PlayIllusioinstDeactivate);
        EventBus.SprintActivated.AddListener(PlaySprinterRun);

        EventBus.TotalVirginsDied.AddListener(CheckDemonSounds);

        EventBus.StartGame.AddListener(StartSounds);
        EventBus.EndGame.AddListener(StopSounds);
    }

    

    private void StartSounds()
    {
        StartCoroutine(VirginSoundsLoop());
    }

    private void StopSounds()
    {
        StopCoroutine(VirginSoundsLoop());
    }

    private IEnumerator VirginSoundsLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 3f));
            PlayVirginSound(Random.Range(0, 3));
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            PlayVirginSound(Random.Range(0, 3));
        }
    }

    private int lastVirginSound;

    private void PlayVirginSound(int zeroOrSecond)
    {
        int newSound = Random.Range(0, virginSounds.Length);

        if (newSound != lastVirginSound)
        {
            lastVirginSound = newSound;

            if (zeroOrSecond == 0)
            {
                virginSource.PlayOneShot(virginSounds[newSound], Random.Range(0.1f, 0.4f));
            }
            else if  (zeroOrSecond == 1)
            {
                virginSource2.PlayOneShot(virginSounds[newSound], Random.Range(0.1f, 0.4f));
            }
        }
    }

    private void CheckDemonSounds(int virginsNumber)
    {
        if (virginsNumber == 0)
        {
            PlayAngryDemonSound();
        }
        else
        {
            PlayFedDemonSound();
        }
    }

    private void PlayAngryDemonSound()
    {
        demonSource.PlayOneShot(angryDemon[Random.Range(0, angryDemon.Length)], 1);
    }

    private void PlayDemonHandDown()
    {
        demonSource.PlayOneShot(demonHandDown, 1);
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


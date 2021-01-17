using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLibrary : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource sfx;

    private static List<AudioClip> realClips = new List<AudioClip>();
    private static AudioSource realSFX;

    private void Awake()
    {
        realClips.AddRange(audioClips);
        realSFX = sfx;

        Debug.Log(realClips.Count);
    }

    public static void PlayDefaultMissile(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[0];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[0];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlaySpreadMissile(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[1];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[1];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayClusterMissile(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[2];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[2];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayProtectMissile(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[3];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[3];
                    realSFX.Play();
                }
            }
        }
    }


    public static void PlayHomingMissile(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[4];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[4];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlaySmallExplosion(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[5];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[5];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayMediumExplosion(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[5];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[5];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayBigExplosion(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[6];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[6];
                    realSFX.Play();
                }
            }
        }
    }


    public static void PlayEnemyFire(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[7];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[7];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayPlayerHit(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[8];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[9];
                    realSFX.Play();
                }
            }
        }
    }

    public static void PlayEnemyHit(bool wait = false)
    {
        if (realSFX != null)
        {
            if (realClips.Count > 0)
            {
                if (wait == true)
                {
                    if (!realSFX.isPlaying)
                    {
                        realSFX.clip = realClips[9];
                        realSFX.Play();
                    }
                }
                else
                {
                    realSFX.clip = realClips[9];
                    realSFX.Play();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFadeOut
{
    public static IEnumerator FadeOut(Sound sound, float fadeTime)
    {
        float startVolume = sound.source.volume;

        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        sound.source.Stop();
        sound.source.volume = startVolume;
    }
}
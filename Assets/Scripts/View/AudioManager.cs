using UnityEngine;
using System.Collections;

public class AudioManager : MonoSingleton<AudioManager> {

    public AudioSourcePool SFXPool;

    [SerializeField]
    private UnityEngine.Audio.AudioMixerGroup _musicAudioMixer; 
    private AudioSource _mainMusicSource;
    private AudioClip _currentMainClip;
    private AudioSource _secondaryMusicSource;
    private AudioClip _currentSecondaryClip;

    private Coroutine _coroutine = null;

    protected override void Init() {
        _mainMusicSource = gameObject.AddComponent<AudioSource>();
        _mainMusicSource.outputAudioMixerGroup = _musicAudioMixer;
        _secondaryMusicSource = gameObject.AddComponent<AudioSource>();
        _secondaryMusicSource.outputAudioMixerGroup = _musicAudioMixer;
    }

    public void PlayMusic(AudioClip clip, bool main = false, bool loop = false) {
        AudioSource source = (main ? _mainMusicSource : _secondaryMusicSource);
        if ((main ? _currentMainClip : _currentSecondaryClip) != clip) {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Transition(source, clip, 1, 1, 0, loop));
            if (main)
                _currentMainClip = clip;
            else
                _currentSecondaryClip = clip;
        }
    }

    IEnumerator Transition(AudioSource source, AudioClip clip, float volume, float time, float delay, bool loop) {
        float originVolume = source.volume;
        float originTime = time;
        yield return new WaitForSeconds(delay);
        while (time >= 0) {
            source.volume = Mathf.Lerp(0, originVolume, time / originTime);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        source.Stop();
        source.volume = volume;
        source.loop = loop;
        source.clip = clip;
        source.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1) {
        SFXPool.Play(clip, volume);
    }
}

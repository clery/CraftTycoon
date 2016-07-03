using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioSourcePool : MonoBehaviour {

	public int PoolSize = 5;
	public AudioMixerGroup Group;
	List<AudioSource> Sources = new List<AudioSource>();

	void Awake() {
		for (int i = 0; i < PoolSize; ++i) {
			Sources.Add(gameObject.AddComponent<AudioSource>());
			Sources[i].outputAudioMixerGroup = Group;
		}
	}

	public void Play(AudioClip clip, float volume = 1) {
		foreach (var source in Sources) {
			if (source.isPlaying == false) {
				source.volume = volume;
				source.PlayOneShot(clip);
                break;
			}
		}
	}
}

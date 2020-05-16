using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    [Tooltip("Nombre del audio")]
	public string name;

    [Tooltip("Audioclip")]
	public AudioClip clip;

    [Tooltip("Volumen de un valor de 0 a 1")]
	[Range(0f, 1f)]
	public float volume = .75f;

    [Tooltip("Pitch del audio")]
	[Range(.1f, 3f)]
	public float pitch = 1f;

    [Tooltip("¿Será loop?")]
	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
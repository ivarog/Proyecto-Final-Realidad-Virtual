using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

    //Arreglo que contiene todos los sonidos que utilizaremos, utilizará instancias de la clase Sound
	public Sound[] sounds;


	void Awake()
	{
        //Patrón singleton

		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}


		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = false;
			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    ///////////////////////////////////////////////////////////////////////////////////////////
    // Método que reproduce el audio dado con los parámetros establecidos en el inspector    //
    //////////////////////////////////////////////////////////////////////////////////////////
    
	public void Play(string sound, bool forcePlay = false)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		if(forcePlay || !s.source.isPlaying)
		{
			s.source.Play();
		}
	}


    //////////////////////////////////////////////////////////////////////////////////////////
    // Sobrecarga del método anterior que permite modificar el vlolumen                     //
    //////////////////////////////////////////////////////////////////////////////////////////

	public void Play(string sound, float volume)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = volume;
		s.source.pitch = s.pitch;

		if(!s.source.isPlaying)
		{
			s.source.Play();
		}
	}

    //////////////////////////////////////////////////////////////////////////////////////////
    // Detiene el sonido dado como parámetro                                                //
    //////////////////////////////////////////////////////////////////////////////////////////

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		if(s.source.isPlaying)
		{
			s.source.Stop();
		}
	}

    //////////////////////////////////////////////////////////////////////////////////////////
    // Pausa el sonido dado como parámetro                                                  //
    //////////////////////////////////////////////////////////////////////////////////////////

	public void Pause(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		if(s.source.isPlaying)
		{
			s.source.Pause();
		}
	}

    //////////////////////////////////////////////////////////////////////////////////////////
    // Detiene todos los sonidos en la escena                                               //
    //////////////////////////////////////////////////////////////////////////////////////////

	public void StopAllSounds()
	{
		var allAudioSources = FindObjectsOfType<AudioSource>();
		foreach(AudioSource audioS in allAudioSources) 
		{
			audioS.Stop();
		}
	}

}
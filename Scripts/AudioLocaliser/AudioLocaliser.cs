using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using mrstruijk.Events;


namespace _mrstruijk.Localisation
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioLocaliser : MonoBehaviour
	{
		public List<AudioClip> dutch;
		public List<AudioClip> english; // Add extra languages here

		private List<AudioClip> used;

		private AudioSource source;

		private Coroutine playMultipleClips;
		private Coroutine playClip;


		private void Awake()
		{
			source = GetComponent<AudioSource>();
		}


		private void OnEnable()
		{
			EventSystem.LanguageHasBeenChanged += ChangeLanguage;
		}


		private void Start()
		{
			ChangeLanguage();
		}


		private void ChangeLanguage()
		{
			StartCoroutine(ChangeLanguageCR());
		}


		private IEnumerator ChangeLanguageCR()
		{
			while (playMultipleClips != null)
			{
				yield return null;
			}

			switch (Language.language)
			{
				case Language.Lang.NL:
					used = dutch;
					break;

				case Language.Lang.EN:
					used = english;
					break; // Add extra languages here

				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		public void PlayMultipleClips()
		{
			playMultipleClips ??= StartCoroutine(PlayMultipleClipsCR());
		}


		private IEnumerator PlayMultipleClipsCR()
		{
			foreach (var clip in used)
			{
				playClip = StartCoroutine(PlayClipCR(0.5f, 0.5f, clip, source));

				yield return playClip;
			}

			playMultipleClips = null;
		}


		private static IEnumerator PlayClipCR(float preDelay, float postDelay, AudioClip audio, AudioSource source)
		{
			yield return new WaitForSeconds(preDelay);
			source.clip = audio;
			source.Play();
			yield return new WaitForSeconds(audio.length + postDelay);
		}


		private void OnDisable()
		{
			EventSystem.LanguageHasBeenChanged -= ChangeLanguage;
			StopAllCoroutines();
		}
	}
}

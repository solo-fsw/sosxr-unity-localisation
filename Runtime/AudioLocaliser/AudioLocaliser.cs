using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOSXR.Localiser
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioLocaliser : MonoBehaviour
    {
        public List<AudioClip> Dutch;
        public List<AudioClip> English; // Add extra languages here

        private List<AudioClip> _used;

        private AudioSource _source;

        private Coroutine _playMultipleClips;
        private Coroutine _playClip;


        private void OnValidate()
        {
            if (_source == null)
            {
                _source = GetComponent<AudioSource>();
            }
        }


        private void OnEnable()
        {
            Language.LanguageChanged += ChangeLanguage;
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
            while (_playMultipleClips != null)
            {
                yield return null;
            }

            switch (Language.ChosenLanguage)
            {
                case Language.Lang.NL:
                    _used = Dutch;

                    break;

                case Language.Lang.EN:
                    _used = English;

                    break; // Add extra languages here

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void PlayMultipleClips()
        {
            _playMultipleClips ??= StartCoroutine(PlayMultipleClipsCR());
        }


        private IEnumerator PlayMultipleClipsCR()
        {
            foreach (var clip in _used)
            {
                _playClip = StartCoroutine(PlayClipCR(0.5f, 0.5f, clip, _source));

                yield return _playClip;
            }

            _playMultipleClips = null;
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
            Language.LanguageChanged -= ChangeLanguage;
            StopAllCoroutines();
        }
    }
}
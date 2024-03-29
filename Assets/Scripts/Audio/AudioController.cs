using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {

        public class AudioController : MonoBehaviour
        {
            public static AudioController instance;

            public bool debugMode;
            public AudioTrack[] tracks;

            private Dictionary<AudioType, AudioTrack> m_AudioDict; // relation [AudioType ; AudioClip]
            private Dictionary<AudioType, IEnumerator> m_CoroutineDict; // [type d'audio ; Ienumerator]

            private enum AudioState {
                START,
                STOP,
                RESTART
            }

            [System.Serializable]
            public class AudioObject {
                public AudioType type;
                public AudioClip clip;
            }
            
            [System.Serializable]
            public class AudioTrack {
                public AudioSource source;
                public AudioObject[] audioArray;
            }


            private class AudioRoutine {
                public AudioState state;
                public AudioType type;
                public bool fade;
                public WaitForSeconds delay;

                public AudioRoutine(AudioState _state, AudioType _type, bool _fade, float _delay) {
                    state = _state;
                    type = _type;
                    fade = _fade;
                    delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
                }
            }


#region  Fonctions de Unity
            private void Awake() {
                if(!instance) {
                    Configure();
                }
            }
            private void OnDisable() {
                Dispose();
            }
#endregion

#region  Public
            // Pas encore de possibilité de jouer un son localisé. Mais ça marche bien pour les sons généraux
            public void PlayAudio(AudioType type, bool fade=false, float delay=0.0F){
                AddRoutine(
                    new AudioRoutine(AudioState.START, type, fade, delay)
                );
            }
            public void StopAudio(AudioType type, bool fade=false, float delay=0.0F) {
                AddRoutine(
                    new AudioRoutine(AudioState.STOP, type, fade, delay)
                );
            }

            public void RestartAudio(AudioType type, bool fade=false, float delay=0.0F) {
                AddRoutine(
                    new AudioRoutine(AudioState.RESTART, type, fade, delay)
                );
            }

#endregion

#region  Private
            private void Configure(){
                instance = this;
                m_AudioDict = new Dictionary<AudioType, AudioTrack>();
                m_CoroutineDict = new Dictionary<AudioType, IEnumerator>();
                GenerateAudioTable();
            }
            private void Dispose() {
                // cancel all coroutines in progress
                foreach(KeyValuePair<AudioType, IEnumerator> kvp in m_CoroutineDict) {
                    IEnumerator cor = kvp.Value;
                    StopCoroutine(cor);
                }
            }
            private void AddRoutine(AudioRoutine routine) {
                // cancel any coroutine that could be using this coroutine's audio source
                RemoveConflictingRoutines(routine.type);

                IEnumerator routineRunner = RunAudioRoutine(routine);
                StartCoroutine(routineRunner);
                m_CoroutineDict.Add(routine.type, routineRunner);
                Log("Starting Coroutine on ["+routine.type+"] with operation: "+routine.state);
            }

            private void RemoveRoutine(AudioType type) {
                if (!m_CoroutineDict.ContainsKey(type)) {
                    Log("Trying to stop a Coroutine ["+type+"] that is not running.");
                    return;
                }
                IEnumerator runningRoutine = m_CoroutineDict[type];
                StopCoroutine(runningRoutine);
                m_CoroutineDict.Remove(type);
            }

            private void RemoveConflictingRoutines(AudioType type) {
                // cancel the coroutine if one exists with the same type
                if (m_CoroutineDict.ContainsKey(type)) {
                    RemoveRoutine(type);
                }

                // cancel coroutines that share the same audio track
                AudioType conflictAudio = AudioType.None;
                AudioTrack audioTrackNeeded = GetAudioTrack(type, "Get Audio Track Needed");

                foreach (KeyValuePair<AudioType, IEnumerator> entry in m_CoroutineDict) {
                    AudioType audioType = (AudioType)entry.Key;
                    AudioTrack audioTrackInUse = GetAudioTrack(audioType, "Get Audio Track In Use");
                    if (audioTrackInUse.source == audioTrackNeeded.source) {
                        conflictAudio = audioType;
                        break;
                    }
                }
                if (conflictAudio != AudioType.None) {
                    RemoveRoutine(conflictAudio);
                }
            }

            private IEnumerator RunAudioRoutine(AudioRoutine routine) {
                if (routine.delay != null) yield return routine.delay;

                AudioTrack track = GetAudioTrack(routine.type); // track existence should be verified by now
                track.source.clip = GetAudioClipFromAudioTrack(routine.type, track);

                float initial = 0f;
                float target = 1f;
                switch (routine.state) {
                    case AudioState.START:
                        track.source.Play();
                    break;
                    case AudioState.STOP when !routine.fade:
                        track.source.Stop();
                    break;
                    case AudioState.STOP:
                        initial = 1f;
                        target = 0f;
                    break;
                    case AudioState.RESTART:
                        track.source.Stop();
                        track.source.Play();
                    break;
                }

                // fade volume
                if (routine.fade) {
                    float duration = 1.0f;
                    float timer = 0.0f;

                    while (timer <= duration) {
                        track.source.volume = Mathf.Lerp(initial, target, timer / duration);
                        timer += Time.deltaTime;
                        yield return null;
                    }

                    // if _timer was 0.9999 and Time.deltaTime was 0.01 we would not have reached the target
                    // make sure the volume is set to the value we want
                    track.source.volume = target;

                    if (routine.state == AudioState.STOP) {
                        track.source.Stop();
                    }
                }

                m_CoroutineDict.Remove(routine.type);
                Log("Coroutine count: "+m_CoroutineDict.Count);
            }
            private void GenerateAudioTable(){                
                foreach(AudioTrack track in tracks){
                    foreach(AudioObject obj in track.audioArray){
                        // Check dupes
                        if(m_AudioDict.ContainsKey(obj.type)){
                            LogWarning("Can't register audio ["+obj.type+"]. It has already been registered.");
                        } else {
                            m_AudioDict.Add(obj.type, track);
                            Log("Succesfully registered audio of type ["+obj.type+"].");
                        }
                    }
                }
            }

            private AudioTrack GetAudioTrack(AudioType type, string routineInfo="") {
                if (!m_AudioDict.ContainsKey(type)) {
                    LogWarning("You are trying to <color=#fff>"+routineInfo+"</color> for ["+type+"] but no track was found supporting this audio type.");
                    return null;
                }
                return m_AudioDict[type];
            }

            private AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track) {
                foreach (AudioObject obj in track.audioArray) {
                    if (obj.type == type) {
                        return obj.clip;
                    }
                }
                return null;
            }
            private void Log(string msg){
                if(!debugMode) return;
                Debug.Log("[AudioController]: "+ msg);
            }

            private void LogWarning(string msg){
                if(!debugMode) return;
                Debug.LogWarning("[AudioController]: "+ msg);
            }
#endregion
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrannyCore{
    namespace Audio {

        public class AudioTester : MonoBehaviour
        {
            public AudioController audioController;

            public SoundType testType1 = SoundType.None;
            public SoundType testType2 = SoundType.None;

#region  Fonctions Unity
#if UNITY_EDITOR
            private void Update() {
                if(Input.GetKeyUp(KeyCode.T)){
                    audioController.PlayAudio(testType1);
                }
                if(Input.GetKeyUp(KeyCode.G)){
                    audioController.StopAudio(testType1);
                }
                if(Input.GetKeyUp(KeyCode.B)){
                    audioController.RestartAudio(testType1);
                }

                if(Input.GetKeyUp(KeyCode.Z)){
                    audioController.PlayAudio(testType2);
                }
                if(Input.GetKeyUp(KeyCode.H)){
                    audioController.StopAudio(testType2);
                }
                if(Input.GetKeyUp(KeyCode.N)){
                    audioController.RestartAudio(testType2);
                }
            }            
#endif
#endregion
        }
    }
}

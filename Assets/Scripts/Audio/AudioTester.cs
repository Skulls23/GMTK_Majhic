using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore{
    namespace Audio {

        public class AudioTester : MonoBehaviour
        {
            public AudioController audioController;

#region  Fonctions Unity
#if UNITY_EDITOR
            private void Update() {
                if(Input.GetKeyUp(KeyCode.T)){
                    audioController.PlayAudio(AudioType.M_01);
                }
                if(Input.GetKeyUp(KeyCode.G)){
                    audioController.StopAudio(AudioType.M_01);
                }
                if(Input.GetKeyUp(KeyCode.B)){
                    audioController.RestartAudio(AudioType.M_01);
                }

                if(Input.GetKeyUp(KeyCode.Z)){
                    audioController.PlayAudio(AudioType.SFX_01);
                }
                if(Input.GetKeyUp(KeyCode.H)){
                    audioController.StopAudio(AudioType.SFX_01);
                }
                if(Input.GetKeyUp(KeyCode.N)){
                    audioController.RestartAudio(AudioType.SFX_01);
                }
            }            
#endif
#endregion
        }
    }
}

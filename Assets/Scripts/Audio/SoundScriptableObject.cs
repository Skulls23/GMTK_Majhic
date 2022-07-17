using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrannyCore.Audio;

[CreateAssetMenu(fileName = "Son de jeu", menuName = "ScriptableObjects/Son de jeu", order = 1)]
public class SoundScriptableObject : ScriptableObject
{
    public SoundType type = SoundType.None;

    public AudioClip clip;
}

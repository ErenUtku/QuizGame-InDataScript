using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAnswer;
    public QUESTIONTYPE questiontype;
    public Sprite questionImage;
    public AudioClip questionAudio;
    public UnityEngine.Video.VideoClip questionVideo;
}

[System.Serializable]
public enum QUESTIONTYPE
{
    TEXT, IMAGE, VIDEO, AUDIO
}

[System.Serializable]
public enum GAMESTATUS
{
    Next,
    Playing
}
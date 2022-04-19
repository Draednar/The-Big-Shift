using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioMgr : MonoBehaviour
{
    // Start is called before the first frame update
    

    [System.Serializable]
    public class AudioClip
    {
        public string Name;
        public EventReference event_;
    }

    public List<AudioClip> audioClips = new List<AudioClip>();


    public void PlayClip(int index)
    {
        RuntimeManager.PlayOneShot(audioClips[index].event_);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Singleton instance
    public static MusicPlayer instance;

    private void Awake()
    {
        // If there is no Singleton instance, make this gameobject the Singleton instance
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
            return;
        }
        
        // Destroy every Singleton instance that is not this gameobject.
        foreach (MusicPlayer inst in GameObject.FindObjectsOfType<MusicPlayer>())
        {
            if (inst.GetComponent<AudioSource>().clip != instance.GetComponent<AudioSource>().clip)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
            else if (inst != instance)
            {
                Destroy(inst.gameObject);
            }
        }

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
            return;
        }
    }
}

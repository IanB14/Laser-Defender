using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
	}
}


// If you want to change music on a per-scene basis, you could do the following:

/*
 * 
 * 
 * public AudioClip startClip;
 * public AudioClip gameClip;
 * public AudioClip endClip;
 * 
 * public class MusicPlayer : MonoBehaviour {
   static MusicPlayer instance = null;

    void Start () {
        if (instance != null && instance != this) {
                Destroy (gameObject);
                print ("Duplicate music player self-destructing!");
            } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();k
        }

    }

    void OnLevelWasLoaded(int level){
        Debug.Log("MusicPlayer: loaded level: " + level);
        music.Stop();
        
        if(level == 0){
            music.clip = startClip
        }
        if(level == 1){
            music.clip = gameClip
        }
        if(level == 2){
            music.clip = endClip
        }

        music.loop = true;
        music.Play();
    }

   }
 *
 */

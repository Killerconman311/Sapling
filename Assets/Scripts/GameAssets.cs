using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameAssets : MonoBehaviour
{
   // Start is called before the first frame update
   private static GameAssets _i;


   public static GameAssets Instance
   {
       get
       {
           if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
           return _i;
       }
   }
  
   public SoundAudioClip[] soundAudioClipArray;


   [System.Serializable]
   public class SoundAudioClip
   {
       public SoundManager.Sound sound;
       public AudioClip audioClip;
   }
}

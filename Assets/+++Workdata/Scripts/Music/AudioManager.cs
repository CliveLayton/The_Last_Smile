using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
   private List<EventInstance> eventInstances;
   private List<StudioEventEmitter> eventEmitters;

   private EventInstance ambienceEventInstance;
   private EventInstance musicEventInstance;
   private EventInstance dialogueSFXEventInstance;
   public static AudioManager instance { get; private set; }

   public void Awake()
   {
      if (instance != null)
      {
         Debug.LogError("Found more than one Audio Manager in the scene.");
      }

      instance = this;

      eventInstances = new List<EventInstance>();
      eventEmitters = new List<StudioEventEmitter>();
   }

   public void InitializeAmbience(EventReference ambienceEventReference)
   {
      ambienceEventInstance = CreateEventInstance(ambienceEventReference);
      ambienceEventInstance.start();
   }

   public void InitializeMusic(EventReference musicEventReference)
   {
      musicEventInstance = CreateEventInstance(musicEventReference);
      musicEventInstance.start();
   }

   public void InitializeDialogueSFX(EventReference dialogueSFXEventReference)
   { 
      dialogueSFXEventInstance = CreateEventInstance(dialogueSFXEventReference);
   }

   public void StartDialogueSFX()
   {
      dialogueSFXEventInstance.start();
   }

   public void StopDialogueSFX()
   {
      dialogueSFXEventInstance.stop(STOP_MODE.IMMEDIATE);
   }

   public void SetAmbienceParameter(string parameterName, float parameterValue)
   {
      ambienceEventInstance.setParameterByName(parameterName, parameterValue);
   }

   public void SetMusicArea(MusicArea area)
   {
      musicEventInstance.setParameterByName("area", (float)area);
   }

   public void PlayOneShot(EventReference sound, Vector3 worldPos)
   {
      RuntimeManager.PlayOneShot(sound, worldPos);
   }

   public EventInstance CreateEventInstance(EventReference eventReference)
   {
      EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
      eventInstances.Add(eventInstance);
      return eventInstance;
   }

   public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
   {
      StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
      emitter.EventReference = eventReference;
      eventEmitters.Add(emitter);
      return emitter;
   }

   public void CleanUp()
   {
      //stop and release any created instances
      foreach (EventInstance eventInstance in eventInstances)
      {
         eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
         eventInstance.release();
      }

      foreach (StudioEventEmitter emitter in eventEmitters)
      {
         emitter.Stop();
      }
   }

   private void OnDestroy()
   {
      CleanUp();
   }
}

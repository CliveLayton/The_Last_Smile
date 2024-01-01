using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

/// <summary>
/// In the GameData, all the data that we want to permanently save and load is collected.
/// Anything that is linked here has to be serializable and a data type that is supported by
/// the chosen json API.
/// </summary>
[System.Serializable]
public class GameData
{
    /// <summary>
    /// The gameplay scene that is currently loaded. Without it, we cannot know which scene to start in when
    /// loading a save game.
    /// </summary>
    public string loadedSceneName = GameStateManager.level1SceneName;
    
    /// <summary>
    /// All data regarding the current state of the player that has to persist from scene to scene and
    /// play session to play session.
    /// </summary>
    [FormerlySerializedAs("playerData")] public PlayerController.Data data;

    /// <summary>
    /// All interactable stats saved in a Dictionary, sorted by their unique Guid.
    /// We need them to be able to be referenced by Guid so that we can load the specific interactable instance we need.
    /// </summary>
    public Dictionary<string, Interactable.Data> interactableStatsByGuid = new Dictionary<string, Interactable.Data>();

    /// <summary>
    /// we call this via the interactable, whenever a new interactable should be added to the save game
    /// </summary>
    /// <param name="guid">the guid that is unique for every single interactable</param>
    /// <param name="data">the actual interactable data</param>
    public void AddInteractable(string guid, Interactable.Data data)
    {
        if (!interactableStatsByGuid.ContainsKey(guid))
            interactableStatsByGuid.Add(guid, data); //add new interactable none was yet saved
        else
            interactableStatsByGuid[guid] = data; //update data for existing interactable
    }

    /// <summary>
    /// this is called by the interactables when they initialize 
    /// </summary>
    /// <param name="guid">the guid that is unique for every single interactable</param>
    /// <returns>Returns the data if it already exists. If return value is null, no data was saved yet</returns>
    public Interactable.Data GetInteractableData(string guid)
    {
        if (interactableStatsByGuid.TryGetValue(guid, out var data))
            return data;
        return null; //return null if no value was yet saved
    }
    
    /// <summary>
    /// All interactable stats saved in a Dictionary, sorted by their unique Guid.
    /// We need them to be able to be referenced by Guid so that we can load the specific interactable instance we need.
    /// </summary>
    public Dictionary<string, TriggerBehavior.Data> triggerStatsByGuid = new Dictionary<string, TriggerBehavior.Data>();

    /// <summary>
    /// we call this via the interactable, whenever a new interactable should be added to the save game
    /// </summary>
    /// <param name="guid">the guid that is unique for every single interactable</param>
    /// <param name="data">the actual interactable data</param>
    public void AddTriggerBehavior(string guid, TriggerBehavior.Data data)
    {
        if (!triggerStatsByGuid.ContainsKey(guid))
            triggerStatsByGuid.Add(guid, data); //add new interactable none was yet saved
        else
            triggerStatsByGuid[guid] = data; //update data for existing interactable
    }

    /// <summary>
    /// this is called by the interactables when they initialize 
    /// </summary>
    /// <param name="guid">the guid that is unique for every single interactable</param>
    /// <returns>Returns the data if it already exists. If return value is null, no data was saved yet</returns>
    public TriggerBehavior.Data GetTrigggerBehaviorData(string guid)
    {
        if (triggerStatsByGuid.TryGetValue(guid, out var data))
            return data;
        return null; //return null if no value was yet saved
    }
    
    /// <summary>
    /// A list of all unique identifiers for all collected collectibles 
    /// </summary>
    public List<string> collectedCollectiblesIdentifiers = new List<string>();

    /// <summary>
    /// Called whenever a collectible is collected
    /// </summary>
    /// <param name="identifier">The identifier that is unique for every collectible</param>
    public void AddCollectible(string identifier)
    {
        if (collectedCollectiblesIdentifiers.Contains(identifier))
            return;
        collectedCollectiblesIdentifiers.Add(identifier);
    }

    /// <summary>
    /// Called when we try to find out if a collectible was already collected
    /// </summary>
    /// <param name="identifier">The identifier that is unique for every collectible</param>
    /// <returns>Returns true if collectible is collected, otherwise returns false</returns>
    public bool HasCollectible(string identifier)
    {
        return collectedCollectiblesIdentifiers.Contains(identifier);
    }
}
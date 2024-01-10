using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a scriptable object of the collectible.
/// A scriptable object is saved as an asset and can be edited and linked
/// in a mono behaviour as reference.
/// </summary>
[CreateAssetMenu(fileName = "new Collectible Data", menuName = "TheLastSmile/Create new Collectible Data", order = 0)]
public class CollectableData : ScriptableObject
{
    public Sprite icon;
    public string identifier;
}

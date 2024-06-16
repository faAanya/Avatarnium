using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Moving moving;
    public List<ObjectMaterial> material;

    public ObjectState objectState = ObjectState.General;



}

public enum Moving
{
    Static,
    Dynamic
}
public enum ObjectMaterial
{
    Water,
    FireFrigile,
    Meltable,
    Teleportable,
    Brokable,
    General
}
public enum ObjectState
{
    Solid,
    Broken,
    Burning,
    Wet,
    General
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location
{
    // World Coords
    private Vector3 worldCoord3D;
    private Vector2 worldCoord2D;

    // Zone
    private ZoneTypes zone;
    public enum ZoneTypes
    {
        A,
        B,
        C,
        None
    }

    public Vector3 WorldCoord3D
    {
        get
        {
            return worldCoord3D;
        }
    }

    public Vector2 WorldCoord2D
    {
        get
        {
            return worldCoord2D;
        }
    }

    public ZoneTypes Zone
    {
        get
        {
            return zone;
        }
    }

    public Location(Vector3 worldCoord3D)
    {
        this.worldCoord3D = worldCoord3D;
        worldCoord2D = Vector2.zero;
        zone = ZoneTypes.None;
    }
    public Location(Vector2 worldCoord2D)
    {
        this.worldCoord3D = Vector3.zero;
        this.worldCoord2D = worldCoord2D;
        zone = ZoneTypes.None;
    }
    public Location(ZoneTypes zone)
    {
        this.worldCoord3D = Vector3.zero;
        this.worldCoord2D = Vector2.zero;
        this.zone = ZoneTypes.None;
    }
}

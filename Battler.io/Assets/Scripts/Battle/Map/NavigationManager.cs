using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    public NavMeshSurface LandUnitSurface;
    public NavMeshSurface FlyingUnitsSurface;

    public void BakeAll()
    {
        LandUnitSurface.BuildNavMesh();
        FlyingUnitsSurface.BuildNavMesh();
    }
}

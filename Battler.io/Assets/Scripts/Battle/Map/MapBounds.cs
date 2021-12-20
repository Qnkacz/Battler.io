using System;
using Battle.Unit;
using UnityEngine;

namespace Battle.Map
{
    [Serializable]
    public class MapBounds
    {
        public string Name;
        public Bounds Bounds;
        public MapSide Side;
        public CombatAffiliation Owner;
        public CombatAffiliation Controller;
    }
}
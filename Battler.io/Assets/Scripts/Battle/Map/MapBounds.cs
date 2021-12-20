using Battle.Unit;
using UnityEngine;

namespace Battle.Map
{
    public class MapBounds : MonoBehaviour
    {
        public Bounds bounds;
        public MapSide side;
        public CombatAffiliation Owner;
        public CombatAffiliation Controller;
    }
}
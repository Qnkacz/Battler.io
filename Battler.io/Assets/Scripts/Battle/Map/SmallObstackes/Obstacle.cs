using System;
using Battle.Map.SmallObstackes;
using Extensions;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform Transform;

    public string Name;

    public SmallObstacleSizeCategory obstacleSizeCategory;
    public bool RandomizeSizeCategory;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if(Transform==null) Transform=gameObject.GetComponent<Transform>();
        if(Name==String.Empty) Name=transform.name;
        if(RandomizeSizeCategory) obstacleSizeCategory = (SmallObstacleSizeCategory)typeof(SmallObstacleSizeCategory).GetRandomEnumValue();
        SetSizeFromSizeCategory(obstacleSizeCategory);
    }

    private void SetSizeFromSizeCategory(SmallObstacleSizeCategory category)
    {
        Transform.localScale = category switch
        {
            SmallObstacleSizeCategory.Small => SmallObstacleSizes.Sizes[0],
            SmallObstacleSizeCategory.Medium => SmallObstacleSizes.Sizes[1],
            SmallObstacleSizeCategory.Large => SmallObstacleSizes.Sizes[2],
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }
}

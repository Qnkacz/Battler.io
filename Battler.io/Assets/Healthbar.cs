using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// If you want to use it on your object you need to:
///     - Make sure prefab is a children of any Canvas
///         - That canvas needs a 'Billboard' script attached
///             - To that script you need to attach the Main Camera
///     - Add 'public Healthbar Healthbar;' field to your object
///     - Slide Healthbar prefab onto the Healthbar field in the inspector tool of the desired object
///     - SetMaxHealth() on object initiation
///     - SetHealth() to current object health each time it takes damage
///     
///     --- (optionally) Change color of the fill in the inspector tool
/// </summary>

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Electricity : InteractableObject
{
    [SerializeField] private Light2D light;

    public override void Interact() {
        canInteract = false;
        GameStateManager.Instance.ElectricityOn();
        DOVirtual.Float(0.05f, 0.5f, 1.5f, value => {
            light.intensity = value;
        });
    }
}

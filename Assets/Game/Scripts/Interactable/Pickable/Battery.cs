using UnityEngine;

public class Battery : Item
{
    public override void PickUp(PlayerCharacter character)
    {
        // Memanggil function Pickup di class Parent/Base (Item)
        // untuk mengambil item
        base.PickUp(character);
        // Melakukan refill batre flashlight
        character.Flashlight.RefillBatteryLevel();
    }
}
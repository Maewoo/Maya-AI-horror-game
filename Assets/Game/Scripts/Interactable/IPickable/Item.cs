using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _itemData;
    public UnityEvent OnItemPicked;

    //Property Name diisikan nilai variable _itemData
    public string Name => _itemData.Name;
    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter character)
    {
        // Implement item interaction logic here
        Debug.Log($"You have interacted with the item: {Name}");
        PickUp(character);
    }

    public virtual void PickUp(PlayerCharacter character)
    {
        // Membuat variable salinan data dari variable _itemData
        ItemData newData = new ItemData(_itemData.ID, _itemData.Name);
        // Menambahkan salinan data ke list di inventory
        // menggunakan reference PlayerCharacter
        character.Inventory.AddItems(newData);
        // Memanggil event ketika item diambil
        OnItemPicked?.Invoke();
        // Menghapus item ketika item diambil
        Destroy(gameObject);
    }
}

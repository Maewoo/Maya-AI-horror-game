using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    [SerializeField] protected Transform _doorTransform;
    [SerializeField] protected float _duration = 1f;
    [SerializeField] protected bool _isLocked;
    [SerializeField] protected string _keyID;
    protected bool _isOpen;
    protected bool _isAnimating;
    public bool IsAnimating => _isAnimating;
    public string Name => _name;

    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;

    protected Coroutine _animatingDoorCoroutine;
    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter character)
    {
        // Mengecek apakah pintu dikunci
        if (_isLocked == true)
        {
            // Jika pintu dikunci
            // Mengecek apakah player memiliki kuncinya di inventory 
            // dengan menggunakan ID nya
            bool hasKey = character.Inventory.CheckItem(_keyID);
            if (hasKey == true)
            {
                // Jika punya maka mengubah status pintu menajdi tidak terkunci
                _isLocked = false;
                // Kemudian buka pintu
                Open();
            }
        }
        else
        {
            // Jika tidak terkunci atau kunci telah dibuka
            // Mengecek apakah pintu sedang terbuka
            if (_isOpen == true)
            {
                // Jika pintu terbuka maka tutup pintu
                Close();
            }
            else
            {
                // Jika pintu tertutup maka buka pintu
                Open();
            }
        }
    }

    //Function untuk membuka pintu
    public virtual void Open() //method virtual agar bisa di override di class child
    {
        _isOpen = true;
        OnDoorOpen?.Invoke(); //memanggil event untuk MEBERITAHU class lain bahwa pintu sudah dibuka
    }
    public virtual void Close() //method virtual agar bisa di override di class child
    {
        _isOpen = false;
        OnDoorClose?.Invoke(); //memanggil event untuk MEBERITAHU class lain bahwa pintu sudah ditutup
    }
}

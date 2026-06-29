using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    // Variable untuk reference ke PlayerCharacter
    [SerializeField]
    private PlayerCharacter _owner;
    // Variable untuk menentukan jarak detector
    [SerializeField]
    private float _detectorDistance;
    // Variable untuk menentukan ukuran box detector dan nilai satu di semua sumbu
    [SerializeField]
    private Vector3 _detectorBoxSize = Vector3.one;
    // Variable untuk menentukan layer object yang akan dideteksi
    [SerializeField]
    private LayerMask _interactableLayer;

    // Variable untuk menyimpan reference object yang terdeteksi
    private IInteractable _detectedInteractable;
    // Variable untuk menentukan status apakah player sedang berinteraksi
    private bool _isInteracting;

    // Membuat property untuk menentukan apakah interaction sedang aktif
    public bool Enabled { get; private set; } = true;
    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }
    private void OnDrawGizmosSelected()
    {
        // Mengubah warna gizmo menjadi merah
        Gizmos.color = Color.red;
        // Mendapatkan reference ke component transform camera
        Transform cameraTransform = Camera.main.transform;
        // Membuan detector dengan boxcast
        // Start detector dari posisi camera, dengan jarak yang sudah
        // ditentukan, arah nya ke depan camera, informasi object yang 
        // terdeteksi disimpan ke dalam hit, sudut rotasi nol di semua sumbu
        // jarak dan layer yang sudah ditentukan menggunakan variable.
        // Akan bernilai true jika ada object interactable terdeteksi
        // Akan bernilai false jika tidak ada object interactable terdeteksi
        bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, _detectorBoxSize * 0.5f, cameraTransform.forward, out RaycastHit hit, Quaternion.identity, _detectorDistance, _interactableLayer);
        if (isDetectingInteractable)
        {
            // Jika ada object interactable terdeteksi
            // Mengubah warna gizmos menjadi hijau
            Gizmos.color = Color.green;
            // Menggambar garis dari posisi camera ke arah depan camera 
            // sampai ke posisi hit dengan interactable object
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position +
                            cameraTransform.forward * hit.distance);
            // Menggambar box dengan ukuran yang telah ditentukan pada variable
            // di ujung dari garis/posisi hit dengan interactable object
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward *
                                hit.distance, _detectorBoxSize);
        }
        else
        {
            // Jika ada object interactable terdeteksi
            // Menggambar garis dari posisi camera ke arah depan camera 
            // dengan jarak yang sudah ditentukan di variable
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position +
                            cameraTransform.forward * _detectorDistance);
            // Menggambar box dengan ukuran yang telah ditentukan pada variable
            // di ujung dari garis/arah depan camera dengan jarak yang ditentukan
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);
        }
    }

    private void Update()
    {
        // Mendeteksi object interactable terus menerus 
        // selama game berjalan
        UpdateDetection();
    }

    // Membuat function untuk mendeteksi object interactable
    private void UpdateDetection()
    {
        // Mengecek dulu apakah sedang berinteraksi
        if (_isInteracting)
        {
            // Jika sedang berinteraksi buat status interaksi telah selesai
            _isInteracting = false;
            // Keluar dari function untuk frame saat ini, 
            // kemudian kembali di frame berikut nya
            return;
        }

        if (Enabled == true)
        {
            // Mendapatkan reference ke component transform camera
            Transform cameraTransform = Camera.main.transform;
            // Membuan detector dengan boxcast
            // Start detector dari posisi camera, dengan jarak yang sudah
            // ditentukan, arah nya ke depan camera, informasi object yang 
            // terdeteksi disimpan ke dalam hit, sudut rotasi nol di semua sumbu
            // jarak dan layer yang sudah ditentukan menggunakan variable.
            // Akan bernilai true jika ada object interactable terdeteksi
            // Akan bernilai false jika tidak ada object interactable terdeteksi
            bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, _detectorBoxSize * 0.5f, cameraTransform.forward, out RaycastHit hit, Quaternion.identity, _detectorDistance, _interactableLayer);// Mengecek apakah ada object interactable yang terdeteksi
            if (isDetectingInteractable)
            {
                // Jika ada object yang terdeteksi
                // Mengecek apakah object punya component class yang implementasi
                // interface interactable
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    // Jika implementasi interface interactable
                    // Maka masukkan object ke dalam variable _detectedInteractable
                    _detectedInteractable = interactable;
                }
            }

        }
    }
    // Function untuk berinteraksi dengan object inetractable
    public void Interact()
    {
        // Mengecek apakah ada object inetractable yang terdeteksi
        if (_detectedInteractable != null)
        {
            // Jika ada object interactable yang terdeteksi
            // Memanggil function Onteract dan mengirimkan PlayerCharacter
            // ke dalam parameter
            _detectedInteractable.Interact(_owner);
            // Megosongkan kembali reference object interactable yang terdeteksi
            _detectedInteractable = null;
            // Mengubah status menjadi sedang berinteraksi dengan object
            _isInteracting = true;
        }
    }
}


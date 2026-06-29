using UnityEngine;

public class SightPerception : MonoBehaviour
{
    // Variable untuk reference ke transform target
    // untuk mengetahui posisi target
    [SerializeField]
    private Transform _target;
    // Variable untuk reference ke transform
    // untuk menentukan posisi mata AI
    [SerializeField]
    private Transform _eyePosition;
    // Variable untuk menentukan jarak pengelihatan AI
    [SerializeField]
    private float _viewDistance = 10f;
    // Variable untuk menentukan sudut pandangan AI
    [SerializeField]
    private float _viewAngle = 70f;
    // Variable untuk layer dari object yang dapat dilihat AI
    [SerializeField]
    private LayerMask _targetLayer;

    // Property untuk menyimpan status
    // apakah AI bisa melihat target 
    public bool CanSeePlayer { get; private set; }
    // Property untuk menyimpan posisi terakhir 
    // target ketika terlihat
    public Vector3 LastSeenPosition { get; private set; }


    private void Update()
    {
        // Memanggil function untuk mengecek apakah
        // target terlihat oleh AI, kemudian memasukkan
        // hasilnya ke dalam property CanSeePlayer
        CanSeePlayer = CheckSight();
    }

    public bool CheckSight()
    {
        // Mengecek jika tidak ada target
        // maka function akan berhenti dan mengembalikan
        // nilai false (target tidak terdeteksi)
        if (_target == null)
        {
            return false;
        }

        // Melakukan pengecekan jarak dari mata AI 
        // ke posisi target
        float distance = Vector3.Distance(_eyePosition.position, _target.position);
        // Mengecek apakah target ada 
        // di dalam jarak pandang AI
        if (distance > _viewDistance)
        {
            // Jika iya, maka function akan berhenti dan 
            // mengembalikan nilai false (target tidak terdeteksi)
            return false;
        }

        // Mendapatkan arah ke target dari posisi mata AI
        Vector3 dirToTarget = _target.position - _eyePosition.position;
        // Menentukan sudut dari arah depan mata ke arah target
        float angle = Vector3.Angle(_eyePosition.forward, dirToTarget);
        // Mengecek apakah target ada di dalam 
        // sudut pandang AI
        if (angle > _viewAngle * 0.5f)
        {
            // Jika iya, maka function akan berhenti dan 
            // mengembalikan nilai false (target tidak terdeteksi)
            return false;
        }

        // Mengecek dengan detector berbentuk garis dari posisi mata AI
        // ke arah depan, dengan jarak pandang AI.
        // Object yang dideteksi menggunakan layer yang ditentukan pada 
        // variable _targetLayer.
        // Hasil pengecekan akan dimasukkan ke dalam variable isSeeTarget
        bool isSeeTarget = Physics.Raycast(_eyePosition.position, dirToTarget.normalized, out RaycastHit hit, _viewDistance, _targetLayer);
        Debug.DrawRay(_eyePosition.position, dirToTarget.normalized * _viewDistance, Color.white);
        if (hit.collider != null)
        {
            Debug.Log("Mata hantu melihat objek: " + hit.transform.name + " di layer: " + LayerMask.LayerToName(hit.transform.gameObject.layer));
        }
        // Jika ada object terlihat
        if (isSeeTarget == true)
        {
            // Memastikan jika object yang terlihat adalah target
            if (hit.transform == _target || hit.transform.root == _target.root)
            {
                // Jika iya, maka simpan posisi terakhir dari target
                // ke property LastSeenPosition.
                LastSeenPosition = _target.position;
                // Function akan berhenti dan mengembalikan
                // nilai true (target terdeteksi)
                return true;
            }
        }
        // Kembalikan nilai false (target tidak terdeteksi)
        // jika target tidak terdeteksi
        return false;
    }

    private void OnDrawGizmos()
    {
        // Mengecek jika eye position belum direference
        // maka langsung keluar function dan gizmos 
        // tidak perlu digambar
        if (_eyePosition == null)
        {
            return;
        }

        // Mengubah warna default gizmos menjadi merah
        Gizmos.color = Color.red;
        // Memanggil function CheckSight untuk 
        // mengecek apakah AI melihat target
        // kemudian memasukkan hasil nya ke variable isSeeTarget
        bool isSeeTarget = CheckSight();
        if (isSeeTarget == true)
        {
            // Jika target terlihat oleh AI, maka warna gizmos
            // berubah menjadi hijau
            Gizmos.color = Color.green;
        }

        // Menggambar sphere(bola) di dalam window scene
        // posisi tengah nya ada di posisi eye
        // radius nya berdasarkan view distance  
        // sphere ini digunakan untuk menggambar jangkauan
        // pandangan AI
        Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

        // Menentukan arah kiri dan kanan, untuk batas kiri 
        // dan batas kanan pengelihatan AI, berdasarkan view angle.
        // Caranya simple, cukup menggeser dari arah depan mata AI
        // ke kiri dan kanan, sebesar sudut pandang AI dibagi dengan dua
        Vector3 left = Quaternion.Euler(0, -_viewAngle / 2, 0)
                       * _eyePosition.forward;
        Vector3 right = Quaternion.Euler(0, _viewAngle / 2, 0)
                       * _eyePosition.forward;

        // Menggambar garis dari posisi mata ke arah batas kiri
        // sejauh jarak pandang AI
        Gizmos.DrawRay(_eyePosition.position, left * _viewDistance);
        // Menggambar garis dari posisi mata ke arah batas kanan
        // sejauh jarak pandang AI
        Gizmos.DrawRay(_eyePosition.position, right * _viewDistance);
    }
}
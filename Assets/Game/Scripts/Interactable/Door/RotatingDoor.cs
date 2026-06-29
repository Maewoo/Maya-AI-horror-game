using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RotatingDoor : Door
{
    [SerializeField]
    private float _openAngle;
    // Variable untuk menentukan sudut rotasi ketika pintu ditutup
    [SerializeField]
    private float _closedAngle;

    public override void Open()
    {
        //override function Open() untuk merubah perilaku membuka pintu
        //cek apakah ada coroutine yang sedang berjalan untuk memutar pintu
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine); //jika ada, maka menghentikan coroutine yang sedang berjalan
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_openAngle)); //memulai coroutine untuk memutar pintu ke sudut terbuka

        base.Open(); //base merujuk pada class Parent (Door)
    }

    public override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine); //jika ada, maka menghentikan coroutine yang sedang berjalan
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_closedAngle)); //memulai coroutine untuk memutar pintu ke sudut tertutup
        base.Close(); //base merujuk pada class Parent (Door)
    }

    private IEnumerator RotateDoor(float targetAngle)
    {
        _isAnimating = true;
        float startAngle = _doorTransform.localEulerAngles.y; //menentukan sudut awal saat ini di sumbu y
        float time = 0f;

        //melakukan proses pengulangan animasi selama waktu animasi kurang dri durasi animasi
        while (time < _duration)
        {
            time = time + Time.deltaTime; //menambahkan waktu yang telah berlalu sejak frame terakhir
            //Melakukan interpolasi sudut awal ke sudut target
            // Menentukan alpha dengan rumus time/duration alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan 0 => sudut rotasi awal, 1 => sudut rotasi akhir 

            float angle = Mathf.LerpAngle(startAngle, targetAngle, time / _duration); //menghitung sudut rotasi saat ini berdasarkan waktu yang telah berlalu
            // Mengubah rotasi sumbu Y pintu dengan sudut yang sudah dihitung
            _doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);
            // Animasi dijalankan setiap frame 
            yield return null;
        }
        // Setelah animasi selesai, memastika rotasi pintu di sumbu Y
        // mencapai sudut target
        _doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0);
        // Mengubah status bahwa animasi memutar pintu sudah selesai
        _isAnimating = false;
    }
}

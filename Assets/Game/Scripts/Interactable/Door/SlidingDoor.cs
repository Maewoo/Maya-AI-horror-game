using UnityEngine;
using System.Collections;

public class SlidingDoor : Door
{
    [SerializeField] private Vector3 _openPosition; // Posisi pintu saat terbuka
    [SerializeField] private Vector3 _closedPosition; // Posisi pintu saat tertutup

    public override void Open()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine); // Hentikan coroutine yang sedang berjalan jika ada
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_openPosition)); // Mulai coroutine untuk membuka pintu
        base.Open(); // Panggil method Open() dari class parent (Door)
    }

    public override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine); // Hentikan coroutine yang sedang berjalan jika ada
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_closedPosition)); // Mulai coroutine untuk menutup pintu
        base.Close(); // Panggil method Close() dari class parent (Door)
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        _isAnimating = true;
        Vector3 startPosition = _doorTransform.localPosition;
        float time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _duration);
            _doorTransform.localPosition = position;
            yield return null;
        }

        _doorTransform.localPosition = targetPosition; // Pastikan pintu mencapai posisi target setelah animasi selesai
        _isAnimating = false;
    }
}

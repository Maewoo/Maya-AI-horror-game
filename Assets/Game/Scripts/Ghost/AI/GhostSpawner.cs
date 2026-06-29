using System.Collections;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    // Variable untuk menyimpan reference
    // Ghost AI Controller
    [SerializeField]
    private GhostAIController _aiController;
    // Variable untuk menentukan waktu delay
    // minimum sebelum respaw ghost AI
    [SerializeField]
    private float _minSpawnDelay = 5f;
    // Variable untuk menentukan waktu delay
    // maximum sebelum respaw ghost AI
    [SerializeField]
    private float _maxSpawnDelay = 8f;
    // Variable untuk menentukan jarak
    // minimum sebelum respaw ghost AI
    [SerializeField]
    private float _minSpawnDistance = 3f;
    // Variable untuk menentukan jarak
    // maximum sebelum respaw ghost AI
    [SerializeField]
    private float _maxSpawnDistance = 5f;

    // Variable untuk menyimpan coroutine 
    // untuk respawn AI ghost
    private Coroutine _spawnCoroutine;

    // Function untuk memunculkan kembali
    // AI Ghost 
    public void RestartSpawn()
    {
        // Mengecek apakah ada coroutine respawn
        // yang sedang berjalan? 
        if (_spawnCoroutine != null)
        {
            // Jika iya, maka hentikan coroutine
            // supaya tidak ada lebih dari satu coroutine
            // yang sedang berjalan
            StopCoroutine(_spawnCoroutine);
        }
        // Menjalankan coroutine respawn dan memasukannya
        // ke dalam variable _spawnCoroutine
        _spawnCoroutine = StartCoroutine(StartSpawn());
    }

    // Function untuk melakukan coroutine respawn
    public IEnumerator StartSpawn()
    {
        // Menentukan waktu delay secara acak untuk melakukan respawn
        // minimum: _minSpawnDelay
        // maximum: _maxSpawnDelay
        float spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);

        // melakukan delay berdasarkan waktu yang telah diacak
        yield return new WaitForSeconds(spawnDelay);

        // mengecek jika tidak ada reference ke target, 
        // atau character sedang hiding,
        // maka akan mengulangi proses respawn
        // (menunggu sampai target tidak hiding)
        if (_aiController.Target == null || _aiController.Target.IsHiding == true)
        {
            RestartSpawn();
            // menghentikan proses coroutine 
            yield break;
        }

        // Memanggil function untuk memunculkan ghost AI 
        SpawnGhost();
    }

    // Function untuk memunculkan ghost AI 
    public void SpawnGhost()
    {
        // Menentukan jarak antara player dan kemunculan ghost
        // secara acak 
        float spawnDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
        // Menghitung posisi munculnya ghost, berada di arah 
        // belakang player dengan jarak yang sudah diacak 
        Vector3 spawnPos = _aiController.Target.transform.position - _aiController.Target.transform.forward * spawnDistance;
        // Memastikan nilai sumbu y ghost berada di posisi yang sama
        // seperti sebelum didespawn 
        spawnPos.y = _aiController.transform.position.y;

        // Mengaktifkan kembali NavMeshAgent
        _aiController.NavMeshAgent.enabled = true;
        // Memindahkan posisi ghost AI ke spawn pos yang sudah dihitung
        // Function warp akan memastikan AI berada tetap di area nav mesh
        _aiController.NavMeshAgent.Warp(spawnPos);
        // Memutar AI untuk menghadap ke arah target (player)
        _aiController.transform.LookAt(_aiController.Target.transform);

        // Mengaktifkan untuk memunculkan kembali object AI Ghost
        _aiController.gameObject.SetActive(true);

        // Reset variable blackboard last seen position
        // ke posisi target (player)
        _aiController.BehaviorGraphAgent.SetVariableValue("LastSeenPosition", _aiController.Target.transform.position);
        // Mengaktifkan kembali behavior graph agent
        _aiController.BehaviorGraphAgent.enabled = true;
    }
}
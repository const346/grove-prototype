using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GravityTarget[] entities;

    private Spawn[] spawns;

    public void Spawn()
    {
        StartCoroutine(SpawnAsync());
    }

    private IEnumerator SpawnAsync()
    {
        yield return new WaitForEndOfFrame();
        spawns = FindObjectsOfType<Spawn>();

        if (IsSpawn())
        {
            foreach (var entityTemplate in entities)
            {
                var entity = Instantiate(entityTemplate);
                Respawn(entity);
            }
        }
    }

    public void Respawn(GravityTarget entity)
    {
        if (!IsSpawn() || !entity)
        {
            return;
        }

        var spawnIndex = Random.Range(0, spawns.Length);
        var spawn = spawns[spawnIndex];

        entity.SetPosition(spawn.GetPosition(), spawn.GetRotation(), true);
    }

    private bool IsSpawn()
    {
        return spawns != null && spawns.Any();
    }
}

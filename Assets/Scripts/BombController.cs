using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public GameObject explosionCenterPrefab, explosionHorizontalPrefab, explosionVerticalPrefab,
        explosionUpPrefab, explosionDownPrefab, explosionLeftPrefab, explosionRightPrefab;
    private Dictionary<Vector2,GameObject> explosionMap;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public GameObject brickExplosionPrefab;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
        explosionMap = new Dictionary<Vector2, GameObject>();
        explosionMap[Vector2.left] = explosionLeftPrefab;
        explosionMap[Vector2.right] = explosionRightPrefab;
        explosionMap[Vector2.up] = explosionUpPrefab;
        explosionMap[Vector2.down] = explosionDownPrefab;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Instantiate(explosionCenterPrefab, position, Quaternion.identity);
        //explosion.SetActiveRenderer(explosion.start);
        //explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Instantiate(explosionMap[direction], position, Quaternion.identity);
        ////explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        //explosion.SetDirection(direction);
        //explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(brickExplosionPrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    //public void AddBomb()
    //{
    //    bombAmount++;
    //    bombsRemaining++;
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
    //        other.isTrigger = false;
    //    }
    //}

}

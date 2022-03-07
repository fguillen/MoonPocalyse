using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Helper class to define a Tile and the associated treshold value on the perlin noise
/// </summary>
[Serializable]
class ThresholdTile
{
    [SerializeField] [Range(0, 1)] float threshold;
    [SerializeField] TileBase tile;

  /// <summary>
  /// Returns the apropiated TileBase corresponding to specific threshold value
  /// </summary>
  /// <param name="thresholdsTiles">The List of thresholdsTiles</param>
  /// <param name="value">The actual value from 0f to 1f</param>
  /// <returns>The TileBase among all in thresholdsTiles corresponding to this threshold value</returns>
  public static TileBase TileAtThresholdValue(List<ThresholdTile> thresholdsTiles, float value)
    {
        ThresholdTile thresholdTile =
            thresholdsTiles.
                Where( e => e.threshold <= value).
                OrderBy( e => e.threshold ).
                LastOrDefault();

        if(thresholdTile == null)
            return null;
        else
            return thresholdTile.tile;
    }
}

/// <summary>
/// Populates the passed Tilemap with the Tiles on the List<ThresholdTile>
/// </summary>
public class BackgroundGridController : MonoBehaviour
{
    // playerTransform and tilemap are required to be set in the Editor
    [SerializeField] Transform playerTranform;
    [SerializeField] Tilemap tilemap;

    [SerializeField] int unitsAroundPlayer = 20;
    [SerializeField] float scaler = 0.1f;
    [SerializeField] List<ThresholdTile> thresholdsTiles = new List<ThresholdTile>();

    void Update()
    {
        // TODO: this requires some optimization if you don't need the whole map to be
        // Created every frame
        CreateBackground();
    }

    void CreateBackground()
    {
        for (int x = (int)(playerTranform.position.x - unitsAroundPlayer); x < (int)(playerTranform.position.x + unitsAroundPlayer); x++)
        {
            for (int y = (int)(playerTranform.position.y - unitsAroundPlayer); y < (int)(playerTranform.position.y + unitsAroundPlayer); y++)
            {
                float value = Mathf.PerlinNoise(x * scaler, y * scaler);
                TileBase tile = ThresholdTile.TileAtThresholdValue(thresholdsTiles, value);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}

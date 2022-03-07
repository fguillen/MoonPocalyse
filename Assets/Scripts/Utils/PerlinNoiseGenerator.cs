using UnityEngine;

public class PerlinNoiseGenerator
{
    float seed;
    float scaler;

    public PerlinNoiseGenerator() : this(0.001f, 0)
    {
    }

    public PerlinNoiseGenerator(float scaler) : this(scaler, 0)
    {
    }

    public PerlinNoiseGenerator(float scaler, float seed)
    {
        this.scaler = scaler;
        this.seed = seed;
    }

    public float Get(float x, float y)
    {
        float _x = (x * scaler) + seed;
        float _y = (y * scaler) + seed;
        float value = Mathf.PerlinNoise(_x, _y);

        // Debug.Log($"Inside: {_x}, {_y} => {value}");

        return value;
    }
}

#ifndef GAUSSIAN_BLUR
#define GAUSSIAN_BLUR

void GaussianBlur_float(float2 texCoord, UnityTexture2D tex, UnitySamplerState samp, float sigma, out float4 color)
{
    color = 0.0;
    float totalWeight = 0.0;
    float2 texelSize = tex.texelSize;

    // Sample the texture multiple times and average the color values
    for (int i = -2; i <= 2; i++)
    {
        for (int j = -2; j <= 2; j++)
        {
            float2 offset = float2(i, j) * texelSize * sigma;
            float weight = exp(-(i * i + j * j) / (2.0 * sigma * sigma));
            color += tex.Sample(samp, texCoord + offset) * weight;
            totalWeight += weight;
        }
    }

    // Normalize the color value by dividing by the total weight
    color /= totalWeight;
}

#endif
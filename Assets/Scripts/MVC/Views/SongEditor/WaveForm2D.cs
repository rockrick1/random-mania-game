using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class WaveForm2D : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject arrow;
    [SerializeField] Camera camera;
    
    int width = 5000;
    int height = 64;
    public Color background = Color.black;
    public Color foreground = Color.yellow;

    int sampleSize;
    float[] samples;
    float[] waveform;
    float arrowoffsetx;

    public void SetAudio (AudioClip clip) => audioSource.clip = clip;

    public void ShowWave ()
    {
        Texture2D waveTexture = GetWaveform();
        Rect rect = new Rect(Vector2.zero, new Vector2(width, height));
        spriteRenderer.sprite = Sprite.Create(waveTexture, rect, Vector2.zero);

        // arrow.transform.position = new Vector3(0f, 0f);
        // arrowoffsetx = -(arrow.GetComponent<SpriteRenderer>().size.x / 2f);

        // camera.transform.position = new Vector3(0f, 0f, -1f);
        // camera.transform.Translate(Vector3.right * (spriteRenderer.size.x / 2f));
    }
    
    // void Update ()
    // {
    //     float xoffset = (audioSource.time / audioSource.clip.length) * spriteRenderer.size.x;
    //     arrow.transform.position = new Vector3(xoffset + arrowoffsetx, 0);
    // }
    
    Texture2D GetWaveform ()
    {
        int halfHeight = height / 2;
        float heightScale = height * 0.75f;
        
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        waveform = new float[width];

        AudioClip clip = audioSource.clip;
        sampleSize = clip.samples * clip.channels;
        samples = new float[sampleSize];
        audioSource.clip.GetData(samples, 0);

        int packsize = (sampleSize / width);
        for (int w = 0; w < width; w++)
        {
            waveform[w] = Mathf.Abs(samples[w * packsize]);
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, background);
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < waveform[x] * heightScale; y++)
            {
                tex.SetPixel(x, halfHeight + y, foreground);
                tex.SetPixel(x, halfHeight - y, foreground);
            }
        }

        tex.Apply();

        return tex;
    }
}
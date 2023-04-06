using System.Collections.Generic;
using UnityEngine;

public class WaveForm2D : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] uint sampleReductionRate = 100;

    int sampleSize;
    float[] samples;
    float[] waveform;
    float arrowoffsetx;

    public void SetAudio (AudioClip clip) => audioSource.clip = clip;

    public void ShowWave ()
    {
        GetWaveFormData();

        var transform1 = lineRenderer.transform;
        float lineWidth = ((RectTransform) transform1).rect.width;
        float lineHeight = ((RectTransform) transform1).rect.height;
        int sampleAmount = (int) (samples.Length / sampleReductionRate);

        float xRatio = lineWidth / sampleAmount;
        xRatio /= sampleReductionRate;
        float yRatio = lineHeight / 2;

        List<Vector3> points = new();
        float val = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            val += samples[i];
            if (i % sampleReductionRate == 0)
            {
                points.Add(new Vector3(i * xRatio, yRatio * val / sampleReductionRate));
                val = 0;
            }
        }

        lineRenderer.positionCount = sampleAmount;
        lineRenderer.SetPositions(points.ToArray());

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

    void GetWaveFormData ()
    {
        AudioClip clip = audioSource.clip;
        sampleSize = clip.samples * clip.channels;
        samples = new float[sampleSize];
        audioSource.clip.GetData(samples, 0);
    }
}
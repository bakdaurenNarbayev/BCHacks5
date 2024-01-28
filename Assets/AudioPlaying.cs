using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource forest, beach, bird;

    void Update()
    {
        if(transform.position.x >= -100 && transform.position.x <= 115 && transform.position.z >= 35 && transform.position.z <= 210)
        {
            if (!forest.isPlaying) { forest.Play(); }
        } else if (forest.isPlaying) { forest.Stop(); }

        if (transform.position.z >= 280)
        {
            if (!beach.isPlaying) { beach.Play(); }
        }
        else if (beach.isPlaying) { beach.Stop(); }

        if (transform.position.z <= 20)
        {
            if (!bird.isPlaying) { bird.Play(); }

        }
        else if (bird.isPlaying) { bird.Stop(); }
    }
}
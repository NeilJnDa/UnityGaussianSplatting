using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParticleManager : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> outerParticleSpawners;
    [SerializeField] List<ParticleSystem> slowerParticleSpawners;


    float angleInterval;
    float slowAngleInterval;
    [SerializeField] float radius = 0.3f;
    [SerializeField] float outerRotateSpeed = 200f;
    [SerializeField] float slowerRotateSpeed = 150f;


    float clock;
    float slowClock;

    // Start is called before the first frame update
    void Start()
    {
        if(outerParticleSpawners.Count > 0)
            angleInterval = 360.0f / outerParticleSpawners.Count;
        for (int i = 0; i < outerParticleSpawners.Count; ++i)
        {
            outerParticleSpawners[i].transform.localRotation = Quaternion.Euler(0f, 0f, angleInterval * i);
            outerParticleSpawners[i].transform.localPosition = outerParticleSpawners[i].transform.localRotation * transform.up * radius;
        }

        if (slowerParticleSpawners.Count > 0)
            slowAngleInterval = 360.0f / slowerParticleSpawners.Count;
        for (int i = 0; i < slowerParticleSpawners.Count; ++i)
        {
            slowerParticleSpawners[i].transform.localRotation = Quaternion.Euler(0f, 0f, slowAngleInterval * i);
            slowerParticleSpawners[i].transform.localPosition = slowerParticleSpawners[i].transform.localRotation * transform.up * radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        clock = Mathf.Repeat(Time.time * outerRotateSpeed, 360.0f);
        if (outerParticleSpawners.Count > 0)
        {
            for (int i = 0; i < outerParticleSpawners.Count; ++i)
            {
                outerParticleSpawners[i].transform.localRotation = Quaternion.Euler(0f, 0f, angleInterval * i + clock);
                outerParticleSpawners[i].transform.localPosition = outerParticleSpawners[i].transform.localRotation * transform.up * radius;
            }
        }

        slowClock = Mathf.Repeat(Time.time * slowerRotateSpeed, 360.0f);

        if (slowerParticleSpawners.Count > 0)
        {
            for (int i = 0; i < slowerParticleSpawners.Count; ++i)
            {
                slowerParticleSpawners[i].transform.localRotation = Quaternion.Euler(0f, 0f, slowAngleInterval * i + slowClock);
                slowerParticleSpawners[i].transform.localPosition = slowerParticleSpawners[i].transform.localRotation * transform.up * radius;
            }
        }



    }
}

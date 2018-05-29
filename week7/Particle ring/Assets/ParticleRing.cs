using UnityEngine;
using System.Collections;

public class ParticleRing: MonoBehaviour
{
    public class particleClass
    {
		//半径
        public float radius = 0.0f;
		//角度
        public float angle = 0.0f;
		//用于游离的时间
		public float time = 0.0f;
        public particleClass(float radius, float angle)
        {
            this.radius = radius;
            this.angle = angle;
        }
    }

    
    public ParticleSystem particleSystem;
    
    private ParticleSystem.Particle[] particlesArray;
    private particleClass[] particleAttr; 
    public int particleNum = 12000;

    
    public float outMinRadius = 5.0f;
    public float outMaxRadius = 10.0f;

    
    public float inMinRadius = 6.0f;
    public float inMaxRadius = 9.0f;

    public float speed = 0.1f;

    public int flag;

    void Start()
    {
		particlesArray = new ParticleSystem.Particle[particleNum];
        particleAttr = new particleClass[particleNum];
        
        particleSystem.maxParticles = particleNum;
        particleSystem.Emit(particleNum);
        particleSystem.GetParticles(particlesArray);

        for (int i = 0; i < particleNum; i++)
        {   
            
            float randomAngle;
            float maxR, minR;

            if(i < particleNum / 2)
            {
                maxR = outMaxRadius;
                minR = outMinRadius;
            }
            else
            {
                maxR = inMaxRadius;
                minR = inMinRadius;
            }

			randomAngle = Random.Range(0.0f, 360.0f);

            float midRadius = (maxR + minR) / 2;

            float min = Random.Range(minR, midRadius);

            float max = Random.Range(midRadius, maxR);

            float randomRadius = Random.Range(min, max);

            
            particleAttr[i] = new particleClass(randomRadius, randomAngle);
			particleAttr[i].time = Random.Range(0.0f, 360.0f);
            particlesArray[i].position = new Vector3(randomRadius * Mathf.Cos(randomAngle), randomRadius * Mathf.Sin(randomAngle), 0.0f);
        }
        particleSystem.SetParticles(particlesArray, particleNum);
    }


    void Update()
    {
        for (int i = 0; i < particleNum; i++)
        {
			speed = 0.08f + (i % 10) * 0.04f;
            
            particleAttr[i].angle = (particleAttr[i].angle - speed) % 360;
            float rad = particleAttr[i].angle / 180 * Mathf.PI;

			particleAttr[i].time += Time.deltaTime;
            float a = Mathf.PingPong(particleAttr[i].time / inMaxRadius / outMaxRadius, 0.02f) - 0.02f / 2.0f;
			particleAttr[i].radius += a;
            
           
            particlesArray[i].position = new Vector3(particleAttr[i].radius * Mathf.Cos(rad), particleAttr[i].radius * Mathf.Sin(rad), 0f);
        }
        particleSystem.SetParticles(particlesArray, particleNum);
    }



}
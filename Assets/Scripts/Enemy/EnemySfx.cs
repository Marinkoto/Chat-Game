using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySfx : MonoBehaviour,ISoundPlayer
    {
        public AudioSource Source { get; set; }
        private void OnEnable()
        {
            EnemyHealth.OnHealthChange.AddListener(() => AudioManager.Instance.PlaySound("Heal", Source, false));
        }
        private void Awake()
        {
            InitializeSoundPlayer();
        }
        public void InitializeSoundPlayer()
        {
            Source = this.AddComponent<AudioSource>();
            Source.volume = 0.5f;
        }
    }
}

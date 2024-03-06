using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Sonidos")]
    [SerializeField] private AudioClip clipShoot;
    [SerializeField] private AudioClip clipMeleePush;
    [SerializeField] private AudioClip clipCharacterDead;
    [SerializeField] private AudioClip clipEnemyEmbestida;
    [SerializeField] private AudioClip clipFondoMusic;

    [Header("MixerGroup")]
    [SerializeField] private AudioMixerGroup groupEffect;
    [SerializeField] private AudioMixerGroup groupAmbient;

    private AudioSource shotingSource;
    private AudioSource meleePushSource;
    private AudioSource characterDeadSource;
    private AudioSource enemyEmbestidaSource;
    private AudioSource fondoMusicSource;

    protected override void Awake()
    {
        base.Awake();
        shotingSource= gameObject.AddComponent<AudioSource>();
        meleePushSource = gameObject.AddComponent<AudioSource>();
        characterDeadSource = gameObject.AddComponent<AudioSource>();
        enemyEmbestidaSource = gameObject.AddComponent<AudioSource>();
        fondoMusicSource = gameObject.AddComponent<AudioSource>();


        shotingSource.outputAudioMixerGroup = groupEffect;
        meleePushSource.outputAudioMixerGroup = groupEffect;
        characterDeadSource.outputAudioMixerGroup = groupEffect;
        enemyEmbestidaSource.outputAudioMixerGroup = groupEffect;

        fondoMusicSource.outputAudioMixerGroup = groupAmbient;
    }

    private void Start()
    {
        PlayAudioFondoMusic();
    }


    public void PlayAudioShooting()
    {
        if (shotingSource == null) return;
        if (shotingSource.isPlaying) return;
        shotingSource.clip = clipShoot;
        shotingSource.loop = false;
        shotingSource.Play();
    }

    public void PlayAudioMeleePush()
    {
        if(meleePushSource == null) return;
        if(meleePushSource.isPlaying) return;
        meleePushSource.clip = clipMeleePush;
        meleePushSource.loop = false;
        meleePushSource.Play();
    }


    public void PlayAudioCharacterDead()
    {
        if (characterDeadSource == null) return;
        if (characterDeadSource.isPlaying) return;
        characterDeadSource.clip = clipCharacterDead;
        characterDeadSource.loop = false;
        characterDeadSource.Play();
    }

    public void PlayAudioEnemyEmbestida()
    {
        if (enemyEmbestidaSource == null) return;
        if (enemyEmbestidaSource.isPlaying) return;
        enemyEmbestidaSource.clip = clipEnemyEmbestida;
        enemyEmbestidaSource.loop = false;
        enemyEmbestidaSource.Play();
    }

    private void PlayAudioFondoMusic()
    {
        if(fondoMusicSource == null) return;
        if(fondoMusicSource.isPlaying) return;
        fondoMusicSource.clip = clipFondoMusic;
        fondoMusicSource.loop = true;
        fondoMusicSource.Play();
    }
}

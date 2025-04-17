using UnityEngine;

[RequireComponent(typeof(Collider))] // 自动添加碰撞体组件
public class SpecialCoinTrigger : MonoBehaviour
{
    [Header("必填设置")]
    [Tooltip("拖入需要消失的物体")]
    public GameObject objectToDisappear; // 会在Inspector显示

    [Header("可选设置")]
    [SerializeField] private AudioClip collectSound; // 收集音效
    [SerializeField] private ParticleSystem collectEffect; // 收集特效
    [SerializeField] private float destroyDelay = 0.5f; // 金币消失延迟

    private AudioSource audioSource;

    private void Start()
    {
        // 自动获取或添加AudioSource组件
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // 验证设置
        if (objectToDisappear == null)
        {
            Debug.LogError($"{name}: 未关联要消失的物体!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 播放音效
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // 播放特效
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // 隐藏目标物体
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
            Debug.Log($"已隐藏物体: {objectToDisappear.name}");
        }

        // 销毁金币（延迟执行）
        Destroy(gameObject, destroyDelay);
    }

    // 在编辑器中可视化触发范围
    private void OnDrawGizmosSelected()
    {
        if (TryGetComponent(out Collider collider))
        {
            Gizmos.color = new Color(1, 0.8f, 0, 0.3f);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }
}
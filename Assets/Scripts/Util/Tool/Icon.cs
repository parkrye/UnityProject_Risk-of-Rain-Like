using UnityEngine;

/// <summary>
/// 아이콘 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu (fileName = "Icon", menuName = "Data/Icon")]
public class Icon : ScriptableObject
{
    public Sprite sprite;   // 아이콘 이미지
    public string desc;     // 아이콘 설명
}

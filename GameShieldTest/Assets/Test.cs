using UnityEngine;
using DevsDaddy.Shared.EventFramework;
using DevsDaddy.GameShield.Core.Payloads;
using DevsDaddy.GameShield.Core.Modules.Memory.SecuredTypes;
using DevsDaddy.GameShield.Core.Modules.Memory;
using DevsDaddy.GameShield.Core; // GameShield 메인 클래스
using TMPro;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public SecuredInt count;
    public TextMeshProUGUI text;
    public Button btn;
    public GameObject obj;
    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            count++;
            text.text = count.ToString();
        });
    }

    private void Start()
    {
        // 1. 이벤트 구독
        EventMessenger.Main.Subscribe<SecurityWarningPayload>(OnSecurityWarning);

        // 2. MemoryProtector 모듈 구동 확인 및 Setup
        MemoryProtector protector = GameShield.Main.GetModule<MemoryProtector>();
        if (protector != null)
        {
            protector.SetupModule(); // 모듈 활성화
        }

        // 3. fakeValue 생성을 위해 초기값 부여 (0이 아닌 값)
        count = 10;
        text.text = count.ToString();
    }

    private void OnDisable()
    {
        EventMessenger.Main.Unsubscribe<SecurityWarningPayload>(OnSecurityWarning);
    }

    private void OnSecurityWarning(SecurityWarningPayload payload)
    {
        Debug.LogError($"[해킹 감지] 메세지: {payload.Message}");
        obj.SetActive(true);    
        // Code 101: 메모리 타입 변조 경고
        if (payload.Code == 101)
        {
           
        }
    }
}
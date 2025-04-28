using UnityEngine;

public class MonoBehaviourExample : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake 在加载脚本实例时调用。    Step 1 ");

        CancelInvoke("Test");//取消该 MonoBehaviour 上的所有 Invoke 调用。
        Invoke("Test", 1f);//在 time 秒后调用 methodName 方法。
        InvokeRepeating("Test", 1f, 1f);//在 time 秒后调用 methodName 方法，然后每 repeatRate 秒调用一次。
        IsInvoking("Test");//是否有任何待处理的 methodName 调用。
        StartCoroutine("Test");//启动协程。
        StopAllCoroutines();//停止在该行为上运行的所有协同程序。
        StopCoroutine("Test");//停止在该行为上运行的第一个名为 methodName 的协同程序或存储在 routine 中的协同程序。

        runInEditMode = false;//允许 MonoBehaviour 的特定实例在编辑模式下运行（仅可在 Editor 中使用）。
        useGUILayout = false;//禁用该属性可跳过 GUI 布局阶段。

        print("打印");//将消息记录到 Unity 控制台（与 Debug.Log 相同）。
    }

    void FixedUpdate()
    {
        Debug.Log("用于物理计算且独立于帧率的 MonoBehaviour.FixedUpdate 消息。    Step 4 ");
    }
    void LateUpdate()
    {
        Debug.Log("如果启用了 Behaviour，则每帧调用 LateUpdate。    Step 6 ");
    }
    void OnAnimatorIK()
    {
        Debug.Log("用于设置动画 IK（反向运动学）的回调。");
    }
    void OnAnimatorMove()
    {
        Debug.Log("用于处理动画移动以修改根运动的回调。");
    }
    void OnApplicationFocus()
    {
        Debug.Log("当玩家获得或失去焦点时，发送给所有 GameObject。");
    }
    void OnApplicationPause()
    {
        Debug.Log("当应用程序暂停时，发送给所有 GameObject。");
    }
    void OnApplicationQuit()
    {
        Debug.Log("在应用程序退出前，发送给所有游戏对象。    Step 11 ");
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        Debug.Log("如果实现了 OnAudioFilterRead，Unity 将在音频 DSP 链中插入一个自定义滤波器。");
    }
    void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible 在渲染器对任何摄像机都不可见时调用。");
    }
    void OnBecameVisible()
    {
        Debug.Log("OnBecameVisible 在渲染器变为对任意摄像机可见时调用。");
    }
    void OnCollisionEnter()
    {
        Debug.Log("当该碰撞体/刚体已开始接触另一个刚体/碰撞体时，调用 OnCollisionEnter。");
    }
    void OnCollisionEnter2D()
    {
        Debug.Log("当传入碰撞体与该对象的碰撞体接触时发送（仅限 2D 物理）。");
    }
    void OnCollisionExit()
    {
        Debug.Log("当该碰撞体/刚体已停止接触另一个刚体/碰撞体时，调用 OnCollisionExit。");
    }
    void OnCollisionExit2D()
    {
        Debug.Log("当另一个对象上的碰撞体停止接触该对象的碰撞体时发送（仅限 2D 物理）。");
    }
    void OnCollisionStay()
    {
        Debug.Log("对应正在接触刚体/碰撞体的每一个碰撞体/刚体，每帧调用一次 :ref::OnCollisionStay。");
    }
    void OnCollisionStay2D()
    {
        Debug.Log("在另一个对象上的碰撞体正在接触该对象的碰撞体时发送每个帧（仅限 2D 物理）。");
    }
    void OnConnectedToServer()
    {
        Debug.Log("成功连接到服务器后在客户端上调用。");
    }
    void OnControllerColliderHit()
    {
        Debug.Log("当该控制器在执行 Move 时撞到碰撞体时调用 OnControllerColliderHit。");
    }
    void OnDestroy()
    {
        Debug.Log("销毁附加的行为将导致游戏或场景收到 OnDestroy。    Step 13 ");
    }
    void OnDisable()
    {
        Debug.Log("该函数在行为被禁用时调用。    Step 12 ");
    }
    void OnDisconnectedFromServer()
    {
        Debug.Log("当连接丢失或与服务器断开连接时，在客户端上调用。");
    }
    void OnDrawGizmosSelected()
    {
        Debug.Log("如果选择了对象，则实现 OnDrawGizmosSelected 来绘制辅助图标。");
    }
    void OnDrawGizmos()
    {
        Debug.Log("如果您想绘制能够选择并且始终绘制的辅助图标，则可以实现 OnDrawGizmos。");
    }
    void OnEnable()
    {
        Debug.Log("该函数在对象变为启用和激活状态时调用。    Step 2 ");
    }
    void OnFailedToConnectToMasterServer()
    {
        Debug.Log("在连接到 MasterServer 时发生问题的情况下，在客户端或服务器上调用。");
    }
    void OnFailedToConnect()
    {
        Debug.Log("出于某种原因连接尝试失败时，在客户端上调用。");
    }
    void OnGUI()
    {
        Debug.Log("系统调用 OnGUI 来渲染和处理 GUI 事件。 ");
    }
    void OnJointBreak()
    {
        Debug.Log("在附加到相同游戏对象的关节断开时调用。");
    }
    void OnJointBreak2D()
    {
        Debug.Log("在附加到相同游戏对象的 Joint2D 断开时调用。");
    }
    void OnMasterServerEvent()
    {
        Debug.Log("在从 MasterServer 报告事件时，在客户端或服务器上调用。");
    }
    void OnMouseDown()
    {
        Debug.Log("当用户在 Collider 上按下鼠标按钮时，将调用 OnMouseDown。");
    }
    void OnMouseDrag()
    {
        Debug.Log("当用户单击 Collider 并仍然按住鼠标时，将调用 OnMouseDrag。");
    }
    void OnMouseEnter()
    {
        Debug.Log("当鼠标进入 Collider 时调用。");
    }
    void OnMouseExit()
    {
        Debug.Log("当鼠标不再处于 Collider 上方时调用。");
    }
    void OnMouseOver()
    {
        Debug.Log("当鼠标悬停在 Collider 上时，每帧调用一次。");
    }
    void OnMouseUpAsButton()
    {
        Debug.Log("松开鼠标时，仅当鼠标在按下时所在的 Collider 上时，才调用 OnMouseUpAsButton。");
    }
    void OnMouseUp()
    {
        Debug.Log("当用户松开鼠标按钮时，将调用 OnMouseUp。");
    }
    void OnNetworkInstantiate()
    {
        Debug.Log("在已通过 Network.Instantiate 进行网络实例化的对象上调用。");
    }
    void OnParticleCollision()
    {
        Debug.Log("当粒子击中碰撞体时，将调用 OnParticleCollision。");
    }
    void OnParticleSystemStopped()
    {
        Debug.Log("系统中的所有粒子都死亡时，便会调用 OnParticleSystemStopped，然后将不再产生新粒子。在调用 Stop 之后，或者超过非循环系统的 Duration 属性时，将停止产生新粒子。");
    }
    void OnParticleTrigger()
    {
        Debug.Log("粒子系统中的任何粒子满足触发模块中的条件时，将调用 OnParticleTrigger。");
    }
    void OnParticleUpdateJobScheduled()
    {
        Debug.Log("调度粒子系统的内置更新作业时，会调用 OnParticleUpdateJobScheduled。");
    }
    void OnPlayerConnected()
    {
        Debug.Log("每当有新玩家成功连接，就在服务器上调用。");
    }
    void OnPlayerDisconnected()
    {
        Debug.Log("每当有玩家与服务器断开连接，就在服务器上调用。");
    }
    void OnPostRender()
    {
        Debug.Log("在摄像机完成场景渲染后，将调用 OnPostRender。    Step 10 ");
    }
    void OnPreCull()
    {
        Debug.Log("在摄像机剔除场景前，将调用 OnPreCull。    Step 8 ");
    }
    void OnPreRender()
    {
        Debug.Log("在摄像机开始渲染场景前，将调用 OnPreRender。    Step 9 ");
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("OnRenderImage 在图像的所有渲染操作全部完成后调用。");
    }
    void OnRenderObject()
    {
        Debug.Log("在摄像机渲染场景后，将调用 OnRenderObject。    Step 7 ");
    }
    void OnSerializeNetworkView()
    {
        Debug.Log("用于在网络视图监视的脚本中自定义变量同步。");
    }
    void OnServerInitialized()
    {
        Debug.Log("每当调用 Network.InitializeServer 并且完成时，对该服务器调用该函数。");
    }
    void OnTransformChildrenChanged()
    {
        Debug.Log("当 GameObject 的变换的子项列表发生更改时，将调用该函数。");
    }
    void OnTransformParentChanged()
    {
        Debug.Log("当 GameObject 的变换的父属性发生更改时，将调用该函数。");
    }
    void OnTriggerEnter()
    {
        Debug.Log("GameObject 与另一个 GameObject 碰撞时，Unity 会调用 OnTriggerEnter。");
    }
    void OnTriggerEnter2D()
    {
        Debug.Log("当另一个对象进入附加到该对象的触发碰撞体时发送（仅限 2D 物理）。");
    }
    void OnTriggerExit()
    {
        Debug.Log("当 Collider other 已停止接触该触发器时调用 OnTriggerExit。");
    }
    void OnTriggerExit2D()
    {
        Debug.Log("当另一个对象离开附加到该对象的触发碰撞体时发送（仅限 2D 物理）。");
    }
    void OnTriggerStay()
    {
        Debug.Log("对于接触触发器的每一个 Collider /other/，每次物理更新调用一次 OnTriggerStay。");
    }
    void OnTriggerStay2D()
    {
        Debug.Log("在另一个对象位于附加到该对象的触发碰撞体之内时发送每个帧（仅限 2D 物理）。");
    }
    void OnWillRenderObject()
    {
        Debug.Log("如果对象可见并且不是 UI 元素，则为每个摄像机调用 OnWillRenderObject。");
    }
    void OnValidate()
    {
        Debug.Log("加载脚本后或检视面板中的值发生更改时，将调用此函数（只能在编辑器中调用）。");
    }
    void Reset()
    {
        Debug.Log("重置为默认值。");
    }
    void Start()
    {
        Debug.Log("在首次调用任何 Update 方法之前启用脚本时，在帧上调用 Start。    Step 3 ");
    }
    void Update()
    {
        Debug.Log("如果启用了 MonoBehaviour，则每帧调用 Update。    Step 5 ");
    }
}

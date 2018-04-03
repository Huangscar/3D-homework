# WEEK 2

标签（空格分隔）： 3D-Homework

---
### 一、简答并用程序验证

 - **游戏对象运动的本质是什么**
 本质是游戏对象变换位置、欧拉角



 -  **请用三种以上方法，实现物体的抛物线运动**
 
- **使用Translate移动坐标**

    ```
    public float g=-10;//重力加速度
    private Vector3 speed;//初速度向量
    private Vector3 Gravity;//重力向量
    void Start () {
        Gravity = Vector3.zero;//重力初始速度为0
        speed = new Vector3(10, 10, 0);
    }
    private float dTime=0;
    // Update is called once per frame
    void FixedUpdate () {

        Gravity.y = g * (dTime += Time.fixedDeltaTime);//v=at
        //模拟位移
        transform.Translate(speed*Time.fixedDeltaTime);
        transform.Translate(Gravity * Time.fixedDeltaTime);
    }
    ```
- **改变Transform属性**
```
public float Power = 10;

public float Angle = 45;

public float Gravity = -10;

private Vector3 MoveSpeed;

private Vector3 GritySpeed = Vector3.zero;

private float dTime;

private Vector3 currentAngle;

void Start()

{

    MoveSpeed = Quaternion.Euler(new Vector3(0, 0, Angle)) * Vector3.right * Power;

    currentAngle = Vector3.zero;

}

void FixedUpdate()

{

    GritySpeed.y = Gravity * (dTime += Time.fixedDeltaTime);

    transform.position += (MoveSpeed + GritySpeed) * Time.fixedDeltaTime;

    currentAngle.z = Mathf.Atan((MoveSpeed.y + GritySpeed.y) / MoveSpeed.x) * Mathf.Rad2Deg;

    transform.eulerAngles = currentAngle;

}
```
- **使用rigid body**
```
private Vector3 speed;
public float power;
public float angle;
// Use this for initialization
void Start () {
    speed = Quaternion.Euler (new Vector3 (0, 0, angle)) * Vector3.right * power;
}

// Update is called once per frame
void Update () {
    transform.position += speed * Time.deltaTime;
}
```

 - **写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。**
 代码见文件夹Solar System
  
 - **牧师与魔鬼**
 - 对象：牧师，魔鬼，船，河岸，河水
 - 动作表
 项目：上船（船已经靠岸且船上至少有一名角色）、下船（船上至少有一名角色）、游戏胜利（所有牧师与魔鬼均到达对岸）、游戏失败（任意一边的魔鬼数量大于牧师数量）



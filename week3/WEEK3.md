# WEEK3

标签（空格分隔）： 3D-Homework

---

###一、操作与总结
 -  **参考 Fantasy Skybox FREE 构建自己的游戏场景**

![avatar](http://m.qpic.cn/psb?/V14DrW9T21aN2c/XkaYmW2M8SPKb4*Fa4bhzL6FKCoGjThBsUFeSlSEog8!/b/dAgBAAAAAAAA&bo=cwNYAgAAAAADBwg!&rf=viewer_4)
  
  -  **写一个简单的总结，总结游戏对象的使用**
 - **游戏对象分为：**
     - Empty（空对象）
     - Camera（摄像机）
     - Light（光线）
     - 3D物体
         - 基础3D物体
         - 构造3D物体
     - Audio（声音）
     - UI基于事件的new UI系统
     - Particle System（粒子系统与特效）
 - **使用**
      - **摄像机**
      添加：菜单->game object->camera
组件：Flare layer（炫光和雾化处理）、GUI layer（渲染遗留的GUI界面）、Audio Listener（挂载拾音器）
     - **光源**
     添加：菜单->GameObject->Light
属性：灯光类型（平行光、聚光灯、点光源、区域光（烘培用））、阴影、剪影
     - **3D物体**
     显示组件：Mesh（形成形状）、Mesh Renderer（显示色彩）
材质与着色器：纹理、材质、数据（着色程序）
     - **音频源**
     创建：菜单->game object->audio->audio source
属性：声音素材、Play时机、Loop
（注意挂载）

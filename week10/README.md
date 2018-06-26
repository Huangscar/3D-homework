# README

## 参考内容

https://pmlpml.github.io/unity3d-learning/13-Multiplayer-and-Networking#22-%E8%81%94%E7%BD%91%E7%9B%B8%E4%BA%92%E5%B0%84%E5%87%BB

## 注意事项

1. [Command] 标记的函数一定要Cmd开头
2. 如果出现network translation更新不够快，可以根据position和rotation里选择延时那一项更改interpolate参数（变大）
3. 对于需要服务器端相通的函数要用[ClientRpc]标记，类似第一点，需要Rpc开头命名函数
4. 摄像机跟随直接Camera.main 就好，但是如果使用这个，就要将camera标记为mainCamera，不要为角色单独配摄像机，而且这部分代码挂在角色处而不是摄像机处

## 玩法

鼠标控制方向，w/s键控制前后，右键/空格控制射击，距离越近中弹伤害越大
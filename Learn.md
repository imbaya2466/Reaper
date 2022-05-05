1. ready和构造constuct不同，构造是自上而下调用，而ready是自下而上
2. 基本使用写完后看看源码，核心关注架构
3. 场景是godot可以保存和实例化组合的部分，同时可以实例化作为节点的一部分，节点是基本单位，用来组合成场景。godot开发的核心是使用各种各样的场景组合（可以实例化的独立单位都应该是场景）https://docs.godotengine.org/zh_CN/latest/getting_started/step_by_step/nodes_and_scenes.html
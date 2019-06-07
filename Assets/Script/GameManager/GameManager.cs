using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum GameState
    {
        BeforeGame,
        InGame,
        AfterGame
    }

    public class GameManager : MonoBehaviour
    {
        //静态变量
        //static 静态修饰符
        //告诉电脑 变量在我们这个程序都一直存在 任何时候都可以得到这个实例
        //使用一个静态的变量 存放自己的实例 在别的对象 就可以直接通过 GameManager.ins获得实例
        public static GameManager ins;

        //游戏状态
        private GameState gameState;

    //为何做一个这个属性 返回一个gameState？
        public GameState GameState
        {
            get
            {
                return gameState;
            }
        }
    private void Update()
    {

        GetComponent<VideoPlayer>().Play();


    }


    private void Awake()//初始化
        {
        //ins 获取游戏控制器组件——
            ins = GetComponent<GameManager>();

            //GameState = "玩家控制游戏对象";
            //Debug.Log(GameState);
            //gameState 为 开场动画
            gameState = GameState.BeforeGame;//初始 开场动画阶段
        }
    //设置游戏开场方法（参数：游戏开始
        public void SetGameState(GameState gameState)
        {
        //为何要这样做？
            this.gameState = gameState;
        }






    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;



namespace TestApp
{
    public partial class MainWindow : Form
    {
        // ads客户端
        public TcAdsClient adsClient;

        // ads连接状态
        public StateInfo adsClientStateInfo;

        // Test
        public Int16 _myIntHand = 0;
        public Int16 _myBoolHand = 0;

        // 定时器
        private Timer timer100ms;
        private Timer timer500ms;



        //
        public double nLeftArm_Left_Pos = 0;
        public double nLeftArm_Left_Velo = 0;

        public MainWindow()
        {
            InitializeComponent();

            timer100ms = new Timer();
            timer100ms.Interval = 100;
            timer100ms.Tick += new EventHandler(get_Axis_Pos_Velo);
            timer100ms.Tick += new EventHandler(readAdsClientState);


            timer500ms = new Timer();
            timer500ms.Interval = 500;


        }


        // Timer Manager


        // ADS connect
        private void btnAdsConnect_Click(object sender, EventArgs e)
        {
            //TODO:netid和port的值未添加校验
            adsClient = new TcAdsClient();
            adsClient.Connect(adsNetIDTextBox.Text, Convert.ToInt32(adsPortTextBox.Text));
            Console.WriteLine("_myIntHand_before:"+ _myIntHand);
            _myIntHand = (Int16)adsClient.ReadSymbol("MAIN.a1", typeof(Int16), false);
            Console.WriteLine("_myIntHand_after:" + _myIntHand);


            // 启动定时器
            timer100ms.Start();
            timer500ms.Start();
        }


        // ADS disconnect
        private void btnAdsDisconnect_Click(object sender, EventArgs e)
        {
            adsClient.Disconnect();
            adsStatusPicBox.Image = Properties.Resources.stop;

            timer500ms.Stop();
        }

        // 检测ADS连接状态（500ms检测一次）
        private void readAdsClientState(object sender, EventArgs e)
        {

            adsClientStateInfo = adsClient.ReadState();
                
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsStatusPicBox.Image = Properties.Resources.connected;
            }
            else if (adsClientStateInfo.AdsState == AdsState.Stop)
            {
                adsStatusPicBox.Image = Properties.Resources.stop;
            }

        }

        private void get_Axis_Pos_Velo(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                nLeftArm_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Left.NcToPlc.ActPos",typeof(double),false);
                labelLeftArmLeftActPos.Text = nLeftArm_Left_Pos.ToString("0.00");
                nLeftArm_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Left.NcToPlc.ActVelo", typeof(double), false);
                labelLeftArmLeftActVelo.Text = nLeftArm_Left_Velo.ToString("0.00");
            }
        }

        // 左臂上电使能，激活手动控制
        private void btnLeftArmPowerOnEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_Enable",true,false); 
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_Enable",true,false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_Enable",true, false);
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Asm_LeftArm", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAutoCtrl_Part_LeftWrist", true, false);     // 左臂腕关节自动控制（随动）
            }
        }

        // 左臂下电，取消激活手动控制
        private void btnLeftArmPowerOnDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Asm_LeftArm", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAutoCtrl_Part_LeftWrist", false, false);     // 关闭左臂腕关节自动控制（随动）

            }
        }


        // 左臂-向前移动（鼠标按下逻辑）
        private void btnLeftArmForward_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Forward", true, false);

            }
        }

        // 左臂-向前移动（鼠标抬起逻辑）
        private void btnLeftArmForward_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Forward", false, false);

            }
        }


        // 左臂-向左移动（鼠标按下逻辑）
        private void btnLeftArmLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Left", true, false);

            }
        }


        // 左臂-向左移动（鼠标抬起逻辑）
        private void btnLeftArmLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Left", false, false);

            }
        }


        // 左臂-向右移动（鼠标按下逻辑）
        private void btnLeftArmRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Right", true, false);

            }
        }

        // 左臂-向右移动（鼠标抬起逻辑）
        private void btnLeftArmRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Right", false, false);

            }
        }


        // 左臂-向后移动（鼠标按下逻辑）
        private void btnLeftArmBackward_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Backward", true, false);
            }
        }

        // 左臂-向后移动（鼠标抬起逻辑）
        private void btnLeftArmBackward_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bLeftArm_Backward", false, false);
            }
        }





        // 大龙门-轴使能
        private void btnBigGatePowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_Enable", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_Enable", true, false);
            }
        }


        // 大龙门-轴失能
        private void btnBigGatePowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_Enable", false, false);
            }
        }

        // 大龙门-上升（鼠标按下逻辑）
        private void btnBigGateUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBigGate_Up", true, false);
            }
        }

        // 大龙门-上升（鼠标抬起逻辑）
        private void btnBigGateUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBigGate_Up", false, false);
            }
        }

        // 大龙门-下降（鼠标按下逻辑）
        private void btnBigGateDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBigGate_Down", true, false);
            }
        }

        // 大龙门-下降（鼠标抬起逻辑）
        private void btnBigGateDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBigGate_Down", false, false);
            }
        }

        // 小龙门-轴使能
        private void btnSmallGatePowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_Enable", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_Enable", true, false);
            }
        }

        // 小龙门-轴失能
        private void btnSmallGatePowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_Enable", false, false);
            }
        }

        //小龙门-升（鼠标按下逻辑）
        private void btnSmallGateUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bSmallGate_Up", true, false);
            }
        }

        //小龙门-升（鼠标抬起逻辑）
        private void btnSmallGateUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bSmallGate_Up", false, false);
            }
        }

        //小龙门-降（鼠标按下逻辑）
        private void btnSmallGateDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bSmallGate_Down", true, false);
            }
        }

        //小龙门-降（鼠标抬起逻辑）
        private void btnSmallGateDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bSmallGate_Down", false, false);
            }
        }

        //后龙门-轴使能
        private void btnBackGatePowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_Enable", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_Enable", true, false);
            }
        }

        //后龙门-轴失能
        private void btnBackGatePowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_Enable", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_Enable", false, false);
            }
        }


        //后龙门-升（鼠标按下逻辑）
        private void btnBackGateUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBackGate_Up", true, false);
            }
        }

        //后龙门-升（鼠标抬起逻辑）
        private void btnBackGateUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBackGate_Up", false, false);
            }
        }

        //后龙门-降（鼠标按下逻辑）
        private void btnBackGateDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBackGate_Down", true, false);
            }
        }


        //后龙门-降（鼠标抬起逻辑）
        private void btnBackGateDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBackGate_Down", false, false);
            }
        }

        // 所有轴上电（鼠标抬起逻辑）
        private void btnAxisAllPowerEnable_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Enable", true, false);
            }
        }

        // 所有轴上电（鼠标松开逻辑）
        private void btnAxisAllPowerEnable_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Enable", false, false);
            }
        }

        // 所有轴下电（鼠标抬起逻辑）
        private void btnAxisAllPowerDisable_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Disable", true, false);
            }
        }

        // 所有轴下电（鼠标抬起逻辑）
        private void btnAxisAllPowerDisable_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Disable", false, false);
            }
        }

        // 所有轴复位清故障mc_reset(鼠标按下逻辑)
        private void btnAllAxisReset_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Reset", true, false);
            }
        }

        // 所有轴复位清故障mc_reset(鼠标抬起逻辑)
        private void btnAllAxisReset_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_All_Reset", false, false);
            }
        }

        // 左臂左电机使能
        private void btnLeftArmLeftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_Enable", true, false);        // 左臂左电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Left", true, false);        // 左臂左电机单轴控制标志位置true
            }
        }

        // 左臂左电机失能
        private void btnLeftArmLeftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_Enable", false, false);        // 左臂左电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Left", false, false);        // 左臂左电机单轴控制标志位置false
            }
        }

        //左臂左慢速Jog正
        private void btnLeftArmLeftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", true, false);        
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", true, false);        
            }
        }
        private void btnLeftArmLeftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", false, false);        
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", false, false);        
            }

        }

        //左臂左慢速Jog反
        private void btnLeftArmLeftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", true, false);        
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", true, false);        
            }
        }
        private void btnLeftArmLeftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", false, false);        
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", false, false);        
            }
        }

        // 左臂左快速Jog正
        private void btnLeftArmLeftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", true, false);
            }
        }
        private void btnLeftArmLeftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogP", false, false);
            }
        }


        // 左臂左快速Jog负
        private void btnLeftArmLeftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", true, false);
            }
        }
        private void btnLeftArmLeftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_JogN", false, false);
            }
        }
    }
}


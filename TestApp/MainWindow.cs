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



        // 从NC读取的实时位置和速度
        public double nLeftArm_Left_Pos = 0;
        public double nLeftArm_Left_Velo = 0;
        public double nLeftArm_Right_Pos = 0;
        public double nLeftArm_Right_Velo = 0;
        public double nLeftArm_Wrist_Pos = 0;
        public double nLeftArm_Wrist_Velo = 0;
        public double nLeftArm_Lift_Pos = 0;
        public double nLeftArm_Lift_Velo = 0;
        public double nRightArm_Left_Pos = 0;
        public double nRightArm_Left_Velo = 0;
        public double nRightArm_Right_Pos = 0;
        public double nRightArm_Right_Velo = 0;
        public double nRightArm_Wrist_Pos = 0;
        public double nRightArm_Wrist_Velo = 0;
        public double nRightArm_Lift_Pos = 0;
        public double nRightArm_Lift_Velo = 0;
        public double nBigGate_Left_Pos = 0;
        public double nBigGate_Left_Velo = 0;
        public double nBigGate_Right_Pos = 0;
        public double nBigGate_Right_Velo = 0;
        public double nSmallGate_Left_Pos = 0;
        public double nSmallGate_Left_Velo = 0;
        public double nSmallGate_Right_Pos = 0;
        public double nSmallGate_Right_Velo = 0;
        public double nBackGate_Left_Pos = 0;
        public double nBackGate_Left_Velo = 0;
        public double nBackGate_Right_Pos = 0;
        public double nBackGate_Right_Velo = 0;




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


        // 动态获取各轴实时速度和位置，100ms
        private void get_Axis_Pos_Velo(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                //左臂左实时位置
                nLeftArm_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Left.NcToPlc.ActPos",typeof(double),false);
                labelLeftArmLeftActPos.Text = nLeftArm_Left_Pos.ToString("0.00");
                //左臂左实时速度
                nLeftArm_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Left.NcToPlc.ActVelo", typeof(double), false);
                labelLeftArmLeftActVelo.Text = nLeftArm_Left_Velo.ToString("0.00");
                //左臂右实时位置
                nLeftArm_Right_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Right.NcToPlc.ActPos", typeof(double), false);
                labelLeftArmRightActPos.Text = nLeftArm_Right_Pos.ToString("0.00");
                //左臂右实时速度
                nLeftArm_Right_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Right.NcToPlc.ActVelo", typeof(double), false);
                labelLeftArmRightActVelo.Text = nLeftArm_Right_Velo.ToString("0.00");
                //左臂腕实时位置
                nLeftArm_Wrist_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Wrist.NcToPlc.ActPos", typeof(double), false);
                labelLeftArmWristActPos.Text = nLeftArm_Wrist_Pos.ToString("0.00");
                //左臂腕实时速度
                nLeftArm_Wrist_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Wrist.NcToPlc.ActVelo", typeof(double), false);
                labelLeftArmWristActVelo.Text = nLeftArm_Wrist_Velo.ToString("0.00");
                //左臂升实时位置
                nLeftArm_Lift_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Lift.NcToPlc.ActPos", typeof(double), false);
                labelLeftArmLiftActPos.Text = nLeftArm_Lift_Pos.ToString("0.00");
                //左臂升实时速度
                nLeftArm_Lift_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_LeftArm_Lift.NcToPlc.ActVelo", typeof(double), false);
                labelLeftArmLiftActVelo.Text = nLeftArm_Lift_Velo.ToString("0.00");


                //右臂左实时位置
                nRightArm_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Left.NcToPlc.ActPos", typeof(double), false);
                labelRightArmLeftActPos.Text = nRightArm_Left_Pos.ToString("0.00");
                //右臂左实时速度
                nRightArm_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Left.NcToPlc.ActVelo", typeof(double), false);
                labelRightArmLeftActVelo.Text = nRightArm_Left_Velo.ToString("0.00");
                //右臂右实时位置
                nRightArm_Right_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Right.NcToPlc.ActPos", typeof(double), false);
                labelRightArmRightActPos.Text = nRightArm_Right_Pos.ToString("0.00");
                //右臂右实时速度
                nRightArm_Right_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Right.NcToPlc.ActVelo", typeof(double), false);
                labelRightArmRightActVelo.Text = nRightArm_Right_Velo.ToString("0.00");
                //右臂腕实时位置
                nRightArm_Wrist_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Wrist.NcToPlc.ActPos", typeof(double), false);
                labelRightArmWristActPos.Text = nRightArm_Wrist_Pos.ToString("0.00");
                //右臂腕实时速度
                nRightArm_Wrist_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Wrist.NcToPlc.ActVelo", typeof(double), false);
                labelRightArmWristActVelo.Text = nRightArm_Wrist_Velo.ToString("0.00");
                //右臂升实时位置
                nRightArm_Lift_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Lift.NcToPlc.ActPos", typeof(double), false);
                labelRightArmLiftActPos.Text = nRightArm_Lift_Pos.ToString("0.00");
                //右臂升实时速度
                nRightArm_Lift_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_RightArm_Lift.NcToPlc.ActVelo", typeof(double), false);
                labelRightArmLiftActVelo.Text = nRightArm_Lift_Velo.ToString("0.00");



                //大龙门左实时位置
                nBigGate_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_BigGate_Left.NcToPlc.ActPos", typeof(double), false);
                labelBigGateLeftActPos.Text = nBigGate_Left_Pos.ToString("0.00");
                //大龙门左实时速度
                nBigGate_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_BigGate_Left.NcToPlc.ActVelo", typeof(double), false);
                labelBigGateLeftActVelo.Text = nBigGate_Left_Velo.ToString("0.00");
                //大龙门右实时位置
                nBigGate_Right_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_BigGate_Right.NcToPlc.ActPos", typeof(double), false);
                labelBigGateRightActPos.Text = nBigGate_Right_Pos.ToString("0.00");
                //大龙门右实时速度
                nBigGate_Right_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_BigGate_Right.NcToPlc.ActVelo", typeof(double), false);
                labelBigGateRightActVelo.Text = nBigGate_Right_Velo.ToString("0.00");



                //小龙门左实时位置
                nSmallGate_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_SmallGate_Left.NcToPlc.ActPos", typeof(double), false);
                labelSmallGateLeftActPos.Text = nSmallGate_Left_Pos.ToString("0.00");
                //小龙门左实时速度
                nSmallGate_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_SmallGate_Left.NcToPlc.ActVelo", typeof(double), false);
                labelSmallGateLeftActVelo.Text = nSmallGate_Left_Velo.ToString("0.00");
                //小龙门右实时位置
                nSmallGate_Right_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_SmallGate_Right.NcToPlc.ActPos", typeof(double), false);
                labelSmallGateRightActPos.Text = nSmallGate_Right_Pos.ToString("0.00");
                //小龙门右实时速度
                nSmallGate_Right_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_SmallGate_Right.NcToPlc.ActVelo", typeof(double), false);
                labelSmallGateRightActVelo.Text = nSmallGate_Right_Velo.ToString("0.00");



                //后龙门左实时位置
                nBackGate_Left_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_BackGate_Left.NcToPlc.ActPos", typeof(double), false);
                labelBackGateLeftActPos.Text = nBackGate_Left_Pos.ToString("0.00");
                //后龙门左实时速度
                nBackGate_Left_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_BackGate_Left.NcToPlc.ActVelo", typeof(double), false);
                labelBackGateLeftActVelo.Text = nBackGate_Left_Velo.ToString("0.00");
                //后龙门右实时位置
                nBackGate_Right_Pos = (double)adsClient.ReadSymbol("Robot_Motor.axis_BackGate_Right.NcToPlc.ActPos", typeof(double), false);
                labelBackGateRightActPos.Text = nBackGate_Right_Pos.ToString("0.00");
                //后龙门右实时速度
                nBackGate_Right_Velo = (double)adsClient.ReadSymbol("Robot_Motor.axis_BackGate_Right.NcToPlc.ActVelo", typeof(double), false);
                labelBackGateRightActVelo.Text = nBackGate_Right_Velo.ToString("0.00");

                //大龙门实时位置及速度（以大龙门左轴为准）
                labelBigGateActPos.Text = nBigGate_Left_Pos.ToString("0.00");
                labelBigGateActVelo.Text = nBigGate_Left_Velo.ToString("0.00");

                //小龙门实时位置及速度（以小龙门左轴为准）
                labelSmallGateActPos.Text = nSmallGate_Left_Pos.ToString("0.00");
                labelSmallGateActVelo.Text = nSmallGate_Left_Velo.ToString("0.00");

                //后龙门实时位置及速度（以后龙门左轴为准）
                labelBackGateActPos.Text = nBackGate_Left_Pos.ToString("0.00");
                labelBackGateActVelo.Text = nBackGate_Left_Velo.ToString("0.00");
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






        /*********************************************************************          大龙门合成运动           *********************************************************************/

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


        // 大龙门速度设定
        private void btnBigGateVeloSet_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetVelo = double.Parse(textBoxBigGateTarVelo.Text);
                    if (nTargetVelo >= 0 & nTargetVelo <= 15)
                    {
                        adsClient.WriteSymbol("Gate_Big_Move.nBigGate_Speed", nTargetVelo, false);
                    }
                    else
                    {
                        MessageBox.Show("请输入有效的速度！速度默认值为5，且要满足0<=速度<=15");
                    }

                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }

        // 大龙门合成MoveAbs
        private void btnBigGateMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBigGateTarPos.Text);
                    adsClient.WriteSymbol("Gate_Big_Move.nBigGate_MoveAbs_TarPos", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bBigGate_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBigGateMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBigGate_MoveAbs", false, false);
            }
        }

        // 大龙门停止
        private void btnBigGateMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_McStop", true, false);
            }
        }
        private void btnBigGateMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_McStop", false, false);
            }
        }






        /************************************************************          小龙门合成运动           *************************************************************************/


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

        // 小龙门速度设定
        private void btnSmallGateVeloSet_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetVelo = double.Parse(textBoxSmallGateTarVelo.Text);
                    if (nTargetVelo >= 0 & nTargetVelo <= 15)
                    {
                        adsClient.WriteSymbol("Gate_Small_Move.nSmallGate_Speed", nTargetVelo, false);
                    }
                    else
                    {
                        MessageBox.Show("请输入有效的速度！速度默认值为5，且要满足0<=速度<=15");
                    }

                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }

        // 小龙门合成MoveAbs
        private void btnSmallGateMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxSmallGateTarPos.Text);
                    adsClient.WriteSymbol("Gate_Small_Move.nSmallGate_MoveAbs_TarPos", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bSmallGate_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnSmallGateMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bSmallGate_MoveAbs", false, false);
            }
        }

        // 小龙门停止
        private void btnSmallGateMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_McStop", true, false);
            }
        }
        private void btnSmallGateMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_McStop", false, false);
            }
        }







        /******************************************************************          后龙门合成运动           ******************************************************************/

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



        // 后龙门速度设定
        private void btnBackGateVeloSet_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetVelo = double.Parse(textBoxBackGateTarVelo.Text);
                    if (nTargetVelo >= 0 & nTargetVelo <= 15)
                    {
                        adsClient.WriteSymbol("Gate_Back_Move.nBackGate_Speed", nTargetVelo, false);
                    }
                    else
                    {
                        MessageBox.Show("请输入有效的速度！速度默认值为5，且要满足0<=速度<=15");
                    }

                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }

        // 后龙门合成MoveAbs
        private void btnBackGateMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBackGateTarPos.Text);
                    adsClient.WriteSymbol("Gate_Back_Move.nBackGate_MoveAbs_TarPos", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bBackGate_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBackGateMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bBackGate_MoveAbs", false, false);
            }
        }

        // 后龙门停止
        private void btnBackGateMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_McStop", true, false);
            }
        }
        private void btnBackGateMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_McStop", false, false);
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


        /*********************************************************************       左臂         ****************************************************************************/

        /********************************************************************* 左臂左轴单轴手动控制 ***************************************************************************/

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


        // 左臂左绝对位置运动MoveAbs
        private void btnLeftArmLeftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxLeftArmLeftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnLeftArmLeftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_MoveAbs", false, false);
            }
        }


        // 左臂左停止运动McStop
        private void btnLeftArmLeftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_McStop", true, false);
            }
        }
        private void btnLeftArmLeftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Left_McStop", false, false);
            }
        }



        /********************************************************************* 左臂右轴单轴手动控制 ***************************************************************************/

        // 左臂右电机上电
        private void btnLeftArmRightPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_Enable", true, false);            // 左臂右电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Right", true, false);        // 左臂右电机单轴控制标志位置true
            }
        }

        // 左臂右电机下电
        private void btnLeftArmRightPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_Enable", false, false);            // 左臂右电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Right", false, false);        // 左臂右电机单轴控制标志位置false
            }
        }

        // 左臂右慢速jog正
        private void btnLeftArmRightJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", true, false);
            }
        }
        private void btnLeftArmRightJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", false, false);
            }
        }

        // 左臂右慢速Jog反
        private void btnLeftArmRightJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", true, false);
            }
        }
        private void btnLeftArmRightJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", false, false);
            }
        }

        // 左臂右快速Jog正
        private void btnLeftArmRightJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", true, false);
            }
        }
        private void btnLeftArmRightJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogP", false, false);
            }
        }

        // 左臂右快速Jog负
        private void btnLeftArmRightJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", true, false);
            }
        }
        private void btnLeftArmRightJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_JogN", false, false);
            }
        }


        // 左臂右MoveAbs
        private void btnLeftArmRightMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxLeftArmRightMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnLeftArmRightMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_MoveAbs", false, false);
            }
        }


        // 左臂右停止Mc_stop
        private void btnLeftArmRightMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_McStop", true, false);
            }
        }
        private void btnLeftArmRightMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Right_McStop", false, false);
            }
        }


        /********************************************************************* 左臂腕电机单轴手动控制 ***************************************************************************/
       
        // LeftArm Wrist PowerOn
        private void btnLeftArmWristPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_Enable", true, false);            // 左臂腕电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Wrist", true, false);        // 左臂腕电机单轴控制标志位置true
            }

        }

        // LeftArm Wrist PowerOff
        private void btnLeftArmWristPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_Enable", false, false);            // 左臂腕电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Wrist", false, false);        // 左臂腕电机单轴控制标志位置false
            }

        }

        // LeftArm Wrist jog slow forward
        private void btnLeftArmWristJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", true, false);
            }
        }
        private void btnLeftArmWristJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {

            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", false, false);
            }

        }

        // LeftArm Wrist jog slow Backward
        private void btnLeftArmWristJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", true, false);
            }
        }
        private void btnLeftArmWristJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", false, false);
            }
        }

        // LeftArm Wrist jog fast forward
        private void btnLeftArmWristJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", true, false);
            }
        }
        private void btnLeftArmWristJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogP", false, false);
            }
        }

        // LeftArm Wrist jog fast backward
        private void btnLeftArmWristJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", true, false);
            }
        }
        private void btnLeftArmWristJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_JogN", false, false);
            }
        }


        // LeftArm Wrist move abs
        private void btnLeftArmWristMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxLeftArmWristMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnLeftArmWristMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_MoveAbs", false, false);
            }
        }

        // LeftArm Wrist stop
        private void btnLeftArmWristMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_McStop", true, false);
            }
        }
        private void btnLeftArmWristMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Wrist_McStop", false, false);
            }
        }




        /********************************************************************* 左臂升电机单轴手动控制 ***************************************************************************/
       
        // LeftArm Lift power on
        private void btnLeftArmLiftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_Enable", true, false);            // 左臂升电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Lift", true, false);        // 左臂升电机单轴控制标志位置true
            }
        }

        // LeftArm Lift Power off
        private void btnLeftArmLiftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_Enable", false, false);            // 左臂腕电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_LeftArm_Lift", false, false);        // 左臂腕电机单轴控制标志位置false
            }
        }

        // LeftArm Lift jog slow forward
        private void btnLeftArmLiftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", true, false);
            }
        }
        private void btnLeftArmLiftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", false, false);
            }
        }

        // LeftArm Lift jog slow backward
        private void btnLeftArmLiftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {

            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", true, false);
            }
        }
        private void btnLeftArmLiftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", false, false);
            }
            
        }

        // LeftArm Lift jog fast forward
        private void btnLeftArmLiftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", true, false);
            }
            
        }
        private void btnLeftArmLiftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogP", false, false);
            }
            
        }

        // LeftArm Lift jog fast backward
        private void btnLeftArmLiftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", true, false);
            }
            
        }

        private void btnLeftArmLiftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_JogN", false, false);
            }
        }

        // LeftArm Lift move abs
        private void btnLeftArmLiftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxLeftArmLiftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnLeftArmLiftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_MoveAbs", false, false);
            }
        }

        // LeftArm Lift mc stop
        private void btnLeftArmLiftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_McStop", true, false);
            }
        }
        private void btnLeftArmLiftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_LeftArm_Lift_McStop", false, false);
            }
        }






        /*********************************************************************       右臂         ****************************************************************************/

        /********************************************************************* 右臂左轴单轴手动控制 ***************************************************************************/

        // 右臂左电机使能
        private void btnRightArmLeftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_Enable", true, false);        // 右臂左电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Left", true, false);        // 右臂左电机单轴控制标志位置true
            }
        }

        // 右臂左电机失能
        private void btnRightArmLeftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_Enable", false, false);        // 右臂左电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Left", false, false);        // 右臂左电机单轴控制标志位置false
            }
        }

        //右臂左慢速Jog正
        private void btnRightArmLeftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", true, false);
            }
        }
        private void btnRightArmLeftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", false, false);
            }

        }

        //右臂左慢速Jog反
        private void btnRightArmLeftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", true, false);
            }
        }
        private void btnRightArmLeftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", false, false);
            }
        }

        // 右臂左快速Jog正
        private void btnRightArmLeftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", true, false);
            }
        }
        private void btnRightArmLeftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogP", false, false);
            }
        }


        // 右臂左快速Jog负
        private void btnRightArmLeftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", true, false);
            }
        }
        private void btnRightArmLeftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_JogN", false, false);
            }
        }


        // 右臂左绝对位置运动MoveAbs
        private void btnRightArmLeftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxRightArmLeftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnRightArmLeftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_MoveAbs", false, false);
            }
        }


        // 右臂左停止运动McStop
        private void btnRightArmLeftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_McStop", true, false);
            }
        }
        private void btnRightArmLeftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Left_McStop", false, false);
            }
        }



        /********************************************************************* 右臂右轴单轴手动控制 ***************************************************************************/

        // 右臂右电机上电
        private void btnRightArmRightPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_Enable", true, false);            // 右臂右电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Right", true, false);        // 右臂右电机单轴控制标志位置true
            }
        }

        // 右臂右电机下电
        private void btnRightArmRightPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_Enable", false, false);            // 右臂右电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Right", false, false);        // 右臂右电机单轴控制标志位置false
            }
        }

        // 右臂右慢速jog正
        private void btnRightArmRightJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", true, false);
            }
        }
        private void btnRightArmRightJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", false, false);
            }
        }

        // 右臂右慢速Jog反
        private void btnRightArmRightJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", true, false);
            }
        }
        private void btnRightArmRightJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", false, false);
            }
        }

        // 右臂右快速Jog正
        private void btnRightArmRightJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", true, false);
            }
        }
        private void btnRightArmRightJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogP", false, false);
            }
        }

        // 右臂右快速Jog负
        private void btnRightArmRightJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", true, false);
            }
        }
        private void btnRightArmRightJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_JogN", false, false);
            }
        }


        // 右臂右MoveAbs
        private void btnRightArmRightMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxRightArmRightMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnRightArmRightMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_MoveAbs", false, false);
            }
        }


        // 右臂右停止Mc_stop
        private void btnRightArmRightMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_McStop", true, false);
            }
        }
        private void btnRightArmRightMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Right_McStop", false, false);
            }
        }


        /********************************************************************* 右臂腕电机单轴手动控制 ***************************************************************************/

        // RightArm Wrist PowerOn
        private void btnRightArmWristPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_Enable", true, false);            // 右臂腕电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Wrist", true, false);        // 右臂腕电机单轴控制标志位置true
            }

        }

        // RightArm Wrist PowerOff
        private void btnRightArmWristPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_Enable", false, false);            // 右臂腕电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Wrist", false, false);        // 右臂腕电机单轴控制标志位置false
            }

        }

        // RightArm Wrist jog slow forward
        private void btnRightArmWristJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", true, false);
            }
        }
        private void btnRightArmWristJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", false, false);
            }
        }

        // RightArm Wrist jog slow Backward
        private void btnRightArmWristJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", true, false);
            }
        }
        private void btnRightArmWristJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", false, false);
            }
        }

        // RightArm Wrist jog fast forward
        private void btnRightArmWristJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", true, false);
            }
        }
        private void btnRightArmWristJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogP", false, false);
            }
        }

        // RightArm Wrist jog fast backward
        private void btnRightArmWristJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", true, false);
            }
        }
        private void btnRightArmWristJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_JogN", false, false);
            }
        }


        // RightArm Wrist move abs
        private void btnRightArmWristMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxRightArmWristMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnRightArmWristMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_MoveAbs", false, false);
            }
        }

        // RightArm Wrist stop
        private void btnRightArmWristMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_McStop", true, false);
            }
        }
        private void btnRightArmWristMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Wrist_McStop", false, false);
            }
        }




        /********************************************************************* 右臂升电机单轴手动控制 ***************************************************************************/

        // RightArm Lift power on
        private void btnRightArmLiftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_Enable", true, false);            // 右臂升电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Lift", true, false);        // 右臂升电机单轴控制标志位置true
            }
        }

        // RightArm Lift Power off
        private void btnRightArmLiftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_Enable", false, false);            // 右臂腕电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_RightArm_Lift", false, false);        // 右臂腕电机单轴控制标志位置false
            }
        }

        // RightArm Lift jog slow forward
        private void btnRightArmLiftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", true, false);
            }
        }
        private void btnRightArmLiftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", false, false);
            }
        }

        // RightArm Lift jog slow backward
        private void btnRightArmLiftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {

            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", true, false);
            }
        }
        private void btnRightArmLiftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", false, false);
            }

        }

        // RightArm Lift jog fast forward
        private void btnRightArmLiftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", true, false);
            }

        }
        private void btnRightArmLiftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogP", false, false);
            }

        }

        // RightArm Lift jog fast backward
        private void btnRightArmLiftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", true, false);
            }

        }

        private void btnRightArmLiftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_JogN", false, false);
            }
        }

        // RightArm Lift move abs
        private void btnRightArmLiftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxRightArmLiftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnRightArmLiftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_MoveAbs", false, false);
            }
        }

        // RightArm Lift mc stop
        private void btnRightArmLiftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_McStop", true, false);
            }
        }
        private void btnRightArmLiftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_RightArm_Lift_McStop", false, false);
            }
        }










        

        /*********************************************************************       大龙门         ****************************************************************************/

        /********************************************************************* 大龙门左轴单轴手动控制 ***************************************************************************/

        // 大龙门左电机使能
        private void btnBigGateLeftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_Enable", true, false);            // 大龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BigGate_Left", true, false);        // 大龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Big_Move.bBigGate_Gearin_do", false, false);                                // 激活大龙门单轴手动控制时要取消大龙门左右轴的电子齿轮。
            }
        }

        // 大龙门左电机失能
        private void btnBigGateLeftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_Enable", false, false);           // 大龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BigGate_Left", false, false);       // 大龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_Big_Move.bBigGate_Gearin_do", true, false);                                 // 激活大龙门左右轴的电子齿轮。
            }
        }

        //大龙门左慢速Jog正
        private void btnBigGateLeftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", true, false);
            }
        }
        private void btnBigGateLeftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", false, false);
            }

        }

        //大龙门左慢速Jog反
        private void btnBigGateLeftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", true, false);
            }
        }
        private void btnBigGateLeftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", false, false);
            }
        }

        // 大龙门左快速Jog正
        private void btnBigGateLeftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", true, false);
            }
        }
        private void btnBigGateLeftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogP", false, false);
            }
        }


        // 大龙门左快速Jog负
        private void btnBigGateLeftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", true, false);
            }
        }
        private void btnBigGateLeftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_JogN", false, false);
            }
        }


        // 大龙门左绝对位置运动MoveAbs
        private void btnBigGateLeftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBigGateLeftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBigGateLeftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_MoveAbs", false, false);
            }
        }


        // 大龙门左停止运动McStop
        private void btnBigGateLeftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_McStop", true, false);
            }
        }
        private void btnBigGateLeftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Left_McStop", false, false);
            }
        }



        /********************************************************************* 大龙门右轴单轴手动控制 ***************************************************************************/

        // 大龙门右电机上电
        private void btnBigGateRightPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_Enable", true, false);            // 大龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BigGate_Right", true, false);        // 大龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Big_Move.bBigGate_Gearin_do", false, false);                         // 激活大龙门单轴手动控制时要取消大龙门左右轴的电子齿轮。
            }
        }

        // 大龙门右电机下电
        private void btnBigGateRightPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_Enable", false, false);            // 大龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BigGate_Right", false, false);        // 大龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_Big_Move.bBigGate_Gearin_do", true, false);                                // 激活大龙门单轴手动控制时要取消大龙门左右轴的电子齿轮。
            }
        }

        // 大龙门右慢速jog正
        private void btnBigGateRightJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", true, false);
            }
        }
        private void btnBigGateRightJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", false, false);
            }
        }

        // 大龙门右慢速Jog反
        private void btnBigGateRightJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", true, false);
            }
        }
        private void btnBigGateRightJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", false, false);
            }
        }

        // 大龙门右快速Jog正
        private void btnBigGateRightJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", true, false);
            }
        }
        private void btnBigGateRightJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogP", false, false);
            }
        }

        // 大龙门右快速Jog负
        private void btnBigGateRightJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", true, false);
            }
        }
        private void btnBigGateRightJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_JogN", false, false);
            }
        }


        // 大龙门右MoveAbs
        private void btnBigGateRightMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBigGateRightMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBigGateRightMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_MoveAbs", false, false);
            }
        }


        // 大龙门右停止Mc_stop
        private void btnBigGateRightMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_McStop", true, false);
            }
        }
        private void btnBigGateRightMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BigGate_Right_McStop", false, false);
            }
        }










        /*********************************************************************       小龙门         ****************************************************************************/

        /********************************************************************* 小龙门左轴单轴手动控制 ***************************************************************************/

        // 小龙门左电机使能
        private void btnSmallGateLeftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_Enable", true, false);            // 小龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_SmallGate_Left", true, false);        // 小龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Small_Move.bSmallGate_Gearin_do", false, false);                                // 激活小龙门单轴手动控制时要取消小龙门左右轴的电子齿轮。
            }
        }

        // 小龙门左电机失能
        private void btnSmallGateLeftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_Enable", false, false);           // 小龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_SmallGate_Left", false, false);       // 小龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_Small_Move.bSmallGate_Gearin_do", true, false);                                 // 激活小龙门左右轴的电子齿轮。
            }
        }

        //小龙门左慢速Jog正
        private void btnSmallGateLeftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", true, false);
            }
        }
        private void btnSmallGateLeftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", false, false);
            }

        }

        //小龙门左慢速Jog反
        private void btnSmallGateLeftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", true, false);
            }
        }
        private void btnSmallGateLeftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", false, false);
            }
        }

        // 小龙门左快速Jog正
        private void btnSmallGateLeftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", true, false);
            }
        }
        private void btnSmallGateLeftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogP", false, false);
            }
        }


        // 小龙门左快速Jog负
        private void btnSmallGateLeftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", true, false);
            }
        }
        private void btnSmallGateLeftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_JogN", false, false);
            }
        }


        // 小龙门左绝对位置运动MoveAbs
        private void btnSmallGateLeftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxSmallGateLeftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnSmallGateLeftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_MoveAbs", false, false);
            }
        }


        // 小龙门左停止运动McStop
        private void btnSmallGateLeftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_McStop", true, false);
            }
        }
        private void btnSmallGateLeftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Left_McStop", false, false);
            }
        }



        /********************************************************************* 小龙门右轴单轴手动控制 ***************************************************************************/

        // 小龙门右电机上电
        private void btnSmallGateRightPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_Enable", true, false);            // 小龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_SmallGate_Right", true, false);        // 小龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Small_Move.bSmallGate_Gearin_do", false, false);                         // 激活小龙门单轴手动控制时要取消小龙门左右轴的电子齿轮。
            }
        }

        // 小龙门右电机下电
        private void btnSmallGateRightPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_Enable", false, false);            // 小龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_SmallGate_Right", false, false);        // 小龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_Small_Move.bSmallGate_Gearin_do", true, false);                                // 激活小龙门单轴手动控制时要取消小龙门左右轴的电子齿轮。
            }
        }

        // 小龙门右慢速jog正
        private void btnSmallGateRightJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", true, false);
            }
        }
        private void btnSmallGateRightJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", false, false);
            }
        }

        // 小龙门右慢速Jog反
        private void btnSmallGateRightJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", true, false);
            }
        }
        private void btnSmallGateRightJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", false, false);
            }
        }

        // 小龙门右快速Jog正
        private void btnSmallGateRightJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", true, false);
            }
        }
        private void btnSmallGateRightJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogP", false, false);
            }
        }

        // 小龙门右快速Jog负
        private void btnSmallGateRightJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", true, false);
            }
        }
        private void btnSmallGateRightJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_JogN", false, false);
            }
        }


        // 小龙门右MoveAbs
        private void btnSmallGateRightMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxSmallGateRightMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnSmallGateRightMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_MoveAbs", false, false);
            }
        }


        // 小龙门右停止Mc_stop
        private void btnSmallGateRightMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_McStop", true, false);
            }
        }
        private void btnSmallGateRightMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_SmallGate_Right_McStop", false, false);
            }
        }




        /*********************************************************************       后龙门         ****************************************************************************/

        /********************************************************************* 后龙门左轴单轴手动控制 ***************************************************************************/

        // 后龙门左电机使能
        private void btnBackGateLeftPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_Enable", true, false);            // 后龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BackGate_Left", true, false);        // 后龙门左电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Back_Move.bBackGate_Gearin_do", false, false);                                // 激活后龙门单轴手动控制时要取消后龙门左右轴的电子齿轮。
            }
        }

        // 后龙门左电机失能
        private void btnBackGateLeftPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_Enable", false, false);           // 后龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BackGate_Left", false, false);       // 后龙门左电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_Back_Move.bBackGate_Gearin_do", true, false);                                 // 激活后龙门左右轴的电子齿轮。
            }
        }

        //后龙门左慢速Jog正
        private void btnBackGateLeftJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", true, false);
            }
        }
        private void btnBackGateLeftJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", false, false);
            }

        }

        //后龙门左慢速Jog反
        private void btnBackGateLeftJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", true, false);
            }
        }
        private void btnBackGateLeftJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", false, false);
            }
        }

        // 后龙门左快速Jog正
        private void btnBackGateLeftJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", true, false);
            }
        }
        private void btnBackGateLeftJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogP", false, false);
            }
        }


        // 后龙门左快速Jog负
        private void btnBackGateLeftJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", true, false);
            }
        }
        private void btnBackGateLeftJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_JogN", false, false);
            }
        }


        // 后龙门左绝对位置运动MoveAbs
        private void btnBackGateLeftMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBackGateLeftMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBackGateLeftMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_MoveAbs", false, false);
            }
        }


        // 后龙门左停止运动McStop
        private void btnBackGateLeftMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_McStop", true, false);
            }
        }
        private void btnBackGateLeftMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Left_McStop", false, false);
            }
        }



        /********************************************************************* 后龙门右轴单轴手动控制 ***************************************************************************/

        // 后龙门右电机上电
        private void btnBackGateRightPowerEnable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_Enable", true, false);            // 后龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BackGate_Right", true, false);        // 后龙门右电机单轴控制标志位置true
                adsClient.WriteSymbol("Gate_Back_Move.bBackGate_Gearin_do", false, false);                         // 激活后龙门单轴手动控制时要取消后龙门左右轴的电子齿轮。
            }
        }

        // 后龙门右电机下电
        private void btnBackGateRightPowerDisable_Click(object sender, EventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_Enable", false, false);            // 后龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Robot_Control_State.bManualCtrl_Part_BackGate_Right", false, false);        // 后龙门右电机单轴控制标志位置false
                adsClient.WriteSymbol("Gate_back_Move.bBackGate_Gearin_do", true, false);                                // 激活后龙门单轴手动控制时要取消后龙门左右轴的电子齿轮。
            }
        }

        // 后龙门右慢速jog正
        private void btnBackGateRightJogSlowP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", true, false);
            }
        }
        private void btnBackGateRightJogSlowP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", false, false);
            }
        }

        // 后龙门右慢速Jog反
        private void btnBackGateRightJogSlowN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", true, false);
            }
        }
        private void btnBackGateRightJogSlowN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", false, false);
            }
        }

        // 后龙门右快速Jog正
        private void btnBackGateRightJogFastP_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", true, false);
            }
        }
        private void btnBackGateRightJogFastP_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogP", false, false);
            }
        }

        // 后龙门右快速Jog负
        private void btnBackGateRightJogFastN_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", true, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", true, false);
            }
        }
        private void btnBackGateRightJogFastN_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Motor.bJogFast", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", false, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_JogN", false, false);
            }
        }


        // 后龙门右MoveAbs
        private void btnBackGateRightMoveAbsStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                try
                {
                    double nTargetPos = double.Parse(textBoxBackGateRightMoveAbsPos.Text);
                    adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", nTargetPos, false);
                    adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_MoveAbs", true, false);
                }
                catch (FormatException)
                {

                    MessageBox.Show("请输入有效的数字！");
                }
            }
        }
        private void btnBackGateRightMoveAbsStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("AxisAbs.nTargetPos_MoveAbs", 0, false);
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_MoveAbs", false, false);
            }
        }


        // 后龙门右停止Mc_stop
        private void btnBackGateRightMoveAbsStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_McStop", true, false);
            }
        }
        private void btnBackGateRightMoveAbsStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (adsClientStateInfo.AdsState == AdsState.Run)
            {
                adsClient.WriteSymbol("Robot_Control_State.bAxis_BackGate_Right_McStop", false, false);
            }
        }










    }
}


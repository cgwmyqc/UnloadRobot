import pyads
import random
import numpy as np
from _ctypes import sizeof

# 连接
plc = pyads.Connection("192.168.1.20.1.1", 851)
plc.open()




########################  写数组数据处理  ######################################
# 左右臂箱子数据
right_data = []
left_data = []

# 产生随机箱子数据
for i in range(0, 10):
    right_data.append(
        [
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         ]
    )
    left_data.append(
        [
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         random.uniform(-100.00,100.00),
         ]
    )

# 转换为 numpy 数组，确保数据类型与plc类型一致
right_data = np.array(right_data, dtype=np.float32)
left_data = np.array(left_data, dtype=np.float32)

# 展平数组
right_data = right_data.flatten()
left_data = left_data.flatten()
########################  写数组数据处理  ###########################################




#######################  回调函数测试  #################################################
# 定义关注的Plc变量
tags = {"Robot_Control_State.bFrontLight_Left_Enable": pyads.PLCTYPE_BOOL}

# 定义回调函数(每次PLC中这个Robot_Control_State.bFrontLight_Left_Enable变量改变都会调用此函数)
def callback(notification, data):
    data_type = tags[data]
    handle, timestamp, value = plc.parse_notification(notification, data_type)
    print(f"bFrontLight_Left_Enable changed: {value}:{timestamp}")

# 回调函数传入变量的的大小
attr = pyads.NotificationAttrib(sizeof(pyads.PLCTYPE_BOOL))

# 注册设备通知
handles = plc.add_device_notification('Robot_Control_State.bFrontLight_Left_Enable', attr, callback)

#######################  回调函数测试  #################################################













try:
    # 写数组
    plc.write_by_name('MAIN.arrLeftBoxStack', left_data, pyads.PLCTYPE_ARR_REAL(left_data.size))
    plc.write_by_name('MAIN.arrRightBoxStack', right_data, pyads.PLCTYPE_ARR_REAL(right_data.size))

    # 读数据
    a1 = plc.read_by_name('MAIN.a1', pyads.PLCTYPE_INT)
    print('MAIN.a1:', a1)

    # 读bool
    b2 = plc.read_by_name('MAIN.b2', pyads.PLCTYPE_BOOL)
    print('MAIN.b2:', b2)

    # 写LREAL类型数据
    plc.write_by_name('MAIN.c3', 126.3996, pyads.PLCTYPE_LREAL)

    # 写Bool量
    plc.write_by_name('Robot_Control_State.bFrontLight_Left_Enable', True, pyads.PLCTYPE_BOOL)

    while True:
        pass

except KeyboardInterrupt:
    print("Exiting...")


finally:
    # 删除回调的句柄
    plc.del_device_notification(handles)
    plc.close()

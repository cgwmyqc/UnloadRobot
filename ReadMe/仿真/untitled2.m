[theta1,theta2] = LeftArmIK(589.27,773.11);
disp("LeftArm_theta1:"+theta1)
disp("LeftArm_theta2:"+theta2)


[x,y] = LeftArmFK(23.72,80.27);
disp("LeftArm_x:"+x)
disp("LeftArm_y:"+y)


[theta1,theta2] = RightArmIK(390.73,877.31);
disp("RightArm_theta1:"+theta1)
disp("RightArm_theta2:"+theta2)


[x,y] = RightArmFK(22.51,75.76);
disp("RightArm_x:"+x)
disp("RightArm_y:"+y)


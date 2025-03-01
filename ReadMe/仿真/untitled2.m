%[theta1,theta2] = LeftArmIK(0,200);
%disp("LeftArm_theta1:"+theta1)
%disp("LeftArm_theta2:"+theta2)


% [x,y] = LeftArmFK(78.13,175);
% disp("LeftArm_x:"+x)
% disp("LeftArm_y:"+y)

leftbeta = LeftArmWristFK(32.68,158.3);
disp("LeftArm_Wrist:"+leftbeta);


% [theta1,theta2] = RightArmIK(0,200);
% disp("RightArm_theta1:"+theta1)
% disp("RightArm_theta2:"+theta2)


[x,y] = RightArmFK(33.2,122);
disp("RightArm_x:"+x)
disp("RightArm_y:"+y)

rightbeta = RightArmWristFK(33.2,122);
disp("RightArm_Wrist:"+rightbeta);

disp("原地旋转转向角："+atand(1450/1050));
%[theta1,theta2] = LeftArmIK(0,200);
%disp("LeftArm_theta1:"+theta1)
%disp("LeftArm_theta2:"+theta2)


% [x,y] = LeftArmFK(78.13,175);
% disp("LeftArm_x:"+x)
% disp("LeftArm_y:"+y)

% leftbeta = LeftArmWristFK(78.13,175);
% disp("LeftArm_Wrist:"+leftbeta);


% [theta1,theta2] = RightArmIK(0,200);
% disp("RightArm_theta1:"+theta1)
% disp("RightArm_theta2:"+theta2)


[x,y] = RightArmFK(3.16,106.09);
disp("RightArm_x:"+x)
disp("RightArm_y:"+y)

rightbeta = RightArmWristFK(3.16,106.09);
disp("RightArm_Wrist:"+rightbeta);

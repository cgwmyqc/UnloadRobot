function [x,y] = LeftArmFK(theta1,theta2)
%UNTITLED 并联臂左臂运动正解
%x 左臂腕关节中心点相对于左臂基座的X坐标
%y 左臂腕关节中心点相对于左臂基座的Y坐标
%theta1 左臂右电机角度（单位度）
%theta2 左臂左电机角度（单位度）

LeftArm_h				=584.21;
LeftArm_i				=500;
LeftArm_g				=418;
LeftArm_f				=418;
LeftArm_w				=110;
LeftArm_angle_a			=134.7;			%数模实测值,单位为度
LeftArm_j				=147.73;
LeftArm_n				=612.98;		%数模实测值
LeftArm_angle_FCE		=9.86;			%数模实测值,单位为度


rr = sqrt((2*LeftArm_w+LeftArm_f*cosd(theta1)-LeftArm_g*cosd(theta2))^2 + (LeftArm_f*sind(theta1)-LeftArm_g*sind(theta2))^2);  

u = sqrt((2*LeftArm_w-LeftArm_g*cosd(theta2))^2 + (-LeftArm_g*sind(theta2))^2);


angle_ACD = acosd((rr*rr+LeftArm_f*LeftArm_f-u*u)/(2*rr*LeftArm_f));


angle_DCE = acosd((rr*rr + LeftArm_i*LeftArm_i -LeftArm_h*LeftArm_h)/(2*rr*LeftArm_i));


angle_ACF = angle_ACD + angle_DCE + LeftArm_angle_FCE;


angle_FCG = angle_ACF - theta1;


angle_FCI = 180 - angle_FCG;

x = LeftArm_w + LeftArm_f*cosd(theta1) + LeftArm_n*cosd(angle_FCI);
y = LeftArm_f*sind(theta1) + LeftArm_n*sind(angle_FCI);
end


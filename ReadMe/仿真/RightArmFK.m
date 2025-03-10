function [x,y] = RightArmFK(theta1,theta2)
%UNTITLED 并联臂右臂运动正解
%x 右臂腕关节中心点相对于右臂基座的X坐标
%y 右臂腕关节中心点相对于右臂基座的Y坐标
%theta1 右臂右电机角度（单位度）
%theta2 右臂左电机角度（单位度）

RightArm_h				=500;
RightArm_i				=584.21;
RightArm_g				=418;
RightArm_f				=418;
RightArm_w				=110;
RightArm_angle_a		=137.57;			%单位度
RightArm_j				=147.73;
RightArm_m				=617.76;
RightArm_angle_FDE		=9.29;			    %单位度

rr = sqrt((2*RightArm_w+RightArm_f*cosd(theta1)-RightArm_g*cosd(theta2))^2 + (RightArm_f*sind(theta1)-RightArm_g*sind(theta2))^2);  

u = sqrt((2*RightArm_w+RightArm_f*cosd(theta1))^2 + (RightArm_f*sind(theta1))^2);

angle_BDC = acosd((rr*rr+RightArm_g*RightArm_g-u*u)/(2*rr*RightArm_g));

angle_CDE = acosd((rr*rr + RightArm_h*RightArm_h -RightArm_i*RightArm_i)/(2*rr*RightArm_h));

angle_BDF = angle_BDC + angle_CDE + RightArm_angle_FDE;

angle_FDG = angle_BDF - (180 - theta2);

x = -RightArm_w + RightArm_g*cosd(theta2) + RightArm_m*cosd(angle_FDG);
y = RightArm_g*sind(theta2) + RightArm_m*sind(angle_FDG);

end


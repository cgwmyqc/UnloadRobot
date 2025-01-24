function [theta1,theta2] = RightArmIK(x,y)
%UNTITLED 并联臂右臂运动逆解
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



p = sqrt((x+RightArm_w)*(x+RightArm_w)+y*y);
q = sqrt(x*x + y*y);
 
angle_DBF = acosd((RightArm_g*RightArm_g+p*p-RightArm_m*RightArm_m)/(2*RightArm_g*p));
angle_OBF = acosd((RightArm_w*RightArm_w+p*p-q*q)/(2*RightArm_w*p));
theta2 = angle_OBF + angle_DBF;

angle_BDF = acosd((RightArm_g*RightArm_g+RightArm_m*RightArm_m-p*p)/(2*RightArm_g*RightArm_m));
angle_BDE = angle_BDF - RightArm_angle_FDE;

k = sqrt(RightArm_h*RightArm_h+RightArm_g*RightArm_g-2*RightArm_h*RightArm_g*cosd(angle_BDE));

angle_DBE = acosd((RightArm_g*RightArm_g+k*k-RightArm_h*RightArm_h)/(2*RightArm_g*k));

angle_EBA = theta2 - angle_DBE;

l = sqrt(k*k + 4*RightArm_w*RightArm_w - 4*k*RightArm_w*cosd(angle_EBA));

angle_EAB = acosd((l*l+4*RightArm_w*RightArm_w-k*k)/(4*l*RightArm_w));

angle_CAE = acosd((RightArm_f*RightArm_f+l*l-RightArm_i*RightArm_i)/(2*RightArm_f*l));

theta1 = 180 - angle_CAE - angle_EAB;

end


function [theta1,theta2] = LeftArmIK(x,y)
%UNTITLED 并联臂左臂运动逆解
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




p = sqrt((x-LeftArm_w)*(x-LeftArm_w)+y*y);
q = sqrt(x*x + y*y);
 
angle_CAF = acosd((LeftArm_f*LeftArm_f+p*p-LeftArm_n*LeftArm_n)/(2*LeftArm_f*p));
angle_OAF = acosd((LeftArm_w*LeftArm_w+p*p-q*q)/(2*LeftArm_w*p));
theta1 = 180-angle_CAF-angle_OAF;

angle_ACF = acosd((LeftArm_f*LeftArm_f+LeftArm_n*LeftArm_n-p*p)/(2*LeftArm_f*LeftArm_n));

angle_ACE = angle_ACF - (LeftArm_angle_FCE);
l = sqrt(LeftArm_i*LeftArm_i+LeftArm_f*LeftArm_f-2*LeftArm_i*LeftArm_f*cosd(angle_ACE));
angle_CAE = acosd((LeftArm_f*LeftArm_f+l*l-LeftArm_i*LeftArm_i)/(2*LeftArm_f*l));
angle_EAB = 180-theta1 - angle_CAE;
k = sqrt(l*l + 4*LeftArm_w*LeftArm_w - 4*l*LeftArm_w*cosd(angle_EAB));
angle_EBA = acosd((k*k+4*LeftArm_w*LeftArm_w-l*l)/(4*k*LeftArm_w));
angle_DBE = acosd((LeftArm_g*LeftArm_g+k*k-LeftArm_h*LeftArm_h)/(2*LeftArm_g*k));
theta2 = angle_EBA + angle_DBE;


end


function [x,y] = ArmFK_TestArm(theta1,theta2)
%theta1右电机角度（单位度）
%theta2左电机角度（单位度）
w = 110;
g = 150;
f = 150;
h = 260;
i = 260;
j = 40;
angle_a = 137.5;

n = sqrt(i*i+j*j-2*i*j*cosd(angle_a));
disp("n:"+n);
angle_FCE = acosd((n*n+i*i-j*j)/(2*n*i));
disp("angle_FCE:"+angle_FCE);
r = sqrt((2*w+f*cosd(theta1)-g*cosd(theta2)).^2 + (f*sind(theta1)-g*sind(theta2)).^2);  % CD两点坐标已知。
disp("r:"+r);
u = sqrt((2*w-g*cosd(theta2)).^2 + (-g*sind(theta2)).^2);
disp("u:"+u);

angle_ACD = acosd((r*r+f*f-u*u)/(2*r*f));
disp("angle_ACD:"+angle_ACD);

angle_DCE = acosd((r*r + i*i -h*h)/(2*r*i));
disp("angle_DCE:"+angle_DCE);

angle_ACF = angle_ACD + angle_DCE + angle_FCE;
disp("angle_ACF:"+angle_ACF);

angle_FCG = angle_ACF - theta1;
disp("angle_FCG:"+angle_FCG);

angle_FCI = 180 - angle_FCG;
x = w + f*cosd(theta1) + n*cosd(angle_FCI);
y = f*sind(theta1) + n*sind(angle_FCI);

end


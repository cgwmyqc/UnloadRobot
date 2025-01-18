function [theta1,theta2] = ArmIK(x,y)
%UNTITLED 并联臂运动逆解
w = 110;
g = 150;
f = 150;
h = 260;
i = 260;
j = 40;
angle_a = 137.5;

p = sqrt((x-w)*(x-w)+y*y);
q = sqrt(x*x + y*y);
n = sqrt(i*i+j*j-2*i*j*cosd(angle_a));
angle_CAF = acosd((f*f+p*p-n*n)/(2*f*p));
angle_OAF = acosd((w*w+p*p-q*q)/(2*w*p));
theta1 = 180-angle_CAF-angle_OAF;
angle_ACF = acosd((f*f+n*n-p*p)/(2*f*n));
angle_FCE = acosd((n*n+i*i-j*j)/(2*n*i));
angle_ACE = angle_ACF-angle_FCE;
l = sqrt(i*i+f*f-2*i*f*cosd(angle_ACE));
angle_CAE = acosd((f*f+l*l-i*i)/(2*f*l));
angle_EAB = 180-theta1 - angle_CAE;
k = sqrt(l*l + 4*w*w - 4*l*w*cosd(angle_EAB));
angle_EBA = acosd((k*k+4*w*w-l*l)/(4*k*w));
angle_DBE = acosd((g*g+k*k-h*h)/(2*g*k));
theta2 = angle_EBA + angle_DBE;


end


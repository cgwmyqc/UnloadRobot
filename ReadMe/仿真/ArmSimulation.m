% 运动学逆解

% 参数声明
A = [110,0];
B = [-110,0];
w = 110;
g = 150;
f = 150;
h = 260;
i = 260;
j = 40;
angle_a = 137.5;
t = linspace(0, 2*pi, 50);
%F = [x, y];

% 图初始化
figure;
hold on;
grid on;
axis equal;
xlabel('X轴');
ylabel('Y轴');
title('机械臂动态仿真');
axis([-400 400 -100 300])



for t = 1:length(t)
    x = 50*cos(t);
    y = 50*sin(t) + 150;%F坐标
    scatter(x,y,5,'red', 'filled');
    hold on;
    
end

% n = sqrt(i*i+j*j-2*i*j*cosd(angle_a));
% disp(n);
% angle_FCE = acosd((n*n+i*i-j*j)/(2*n*i));
% disp(angle_FCE);





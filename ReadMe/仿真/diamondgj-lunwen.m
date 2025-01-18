% 参数设定
clc; clear all;

Ls = 581.74;La = 418;L = 110;l = 0;
Lb = 100;Lp = 273;

% % 直线轨迹
% x_traj = linspace(50, 150, 100); % X方向轨迹
% y_traj = linspace(-300, -400, 100); % Y方向轨迹
% 曲线轨迹 (正弦曲线)
x_traj = linspace(-200, 100, 200); % X方向轨迹
% y_traj = -300 + 50 * sin(2 * pi * x_traj / 200); % Y方向轨迹 (正弦波)
aa = 0.001; % 二次项系数
bb = 0.1;   % 一次项系数
cc = -300;  % 常数项
 
y_traj = aa * x_traj.^2 + bb * x_traj + cc + 50 * sin(2 * pi * x_traj / 200) - 100*cos( pi * x_traj / 100);



% 初始化工作空间点
X = []; Y = [];

% 绘制工作空间
figure;
hold on;
plot(X, Y, '.b', 'MarkerSize', 2);
title('工作空间');
xlabel('X [mm]');
ylabel('Y [mm]');
axis equal;

% M 和 N 点固定
Mx = -L/2; My = 0;
Nx = L/2; Ny = 0;
% 固定的XY轴范围
axis_range = [-600 600 -800 400];

% 绘制机械臂
% 初始化绘图对象
% h1 = plot([Mx, Nx], [My, Ny], 'k-', 'LineWidth', 2); % 画MN连线
% 在动画循环之前绘制理论曲线
% plot(x_traj, y_traj, 'g-', 'LineWidth', 1.5, 'DisplayName', '理论轨迹');

% 动画循环
for i = 1:length(x_traj)
    % 当前轨迹点
    x = x_traj(i); 
    y = y_traj(i);

    % 计算逆解
    a1 = 2 * (x + L/2 - l/2) * La;
    b1 = 2 * y * La;
    c1 = Ls^2 - (x + L/2 - l/2)^2 - La^2 - y^2;
    t1 = (b1 + sqrt(b1^2 - c1^2 + a1^2))/(a1 + c1);
    a2 = - 2 * (x + l/2 - L/2) * La;
    b2 = 2 * y * La;
    c2 = Ls^2 - (x + l/2 - L/2)^2 - La^2 - y^2;
    t2 = (b2 + sqrt(b2^2 - c2^2 + a2^2))/(a2 + c2);

    % 逆解
    theta1(i) = asin((2 * t1)/(1 + t1^2));
    theta2(i) = asin((2 * t2)/(1 + t2^2));

    % 正解
    a = (l/2 - La * cos(theta2(i)) - L/2)^2 + La^2 * sin(theta2(i))^2 - Ls^2;
    b = l - 2 * La * cos(theta2(i)) - L;
    c = 2 * La * sin(theta2(i));
    d = (2 * La * (sin(theta2(i)) - sin(theta1(i))))/(2 * La * (cos(theta1(i)) + cos(theta2(i))) + 2 * (L - l));
    e = (La * (l - L) * (cos(theta1(i)) - cos(theta2(i))))/(2 * La * (cos(theta1(i)) + cos(theta2(i))) + 2 * (L - l));   
    A = 2 * d * e + b * d + c;
    B = d^2 + 1;
    C = e^2 + a + b * e;
    
    Cx = - La * cos(theta1(i)) - L/2;
    Cy = - La * sin(theta1(i));
    Dx = La * cos(theta2(i)) + L/2;
    Dy = - La * sin(theta2(i));
    
    % 正运动学验证
    Y(i) = -A /(2 * B) - (sqrt(A^2 - 4 * B * C))/(2 * B);
    X(i) = d * Y(i) + e;
    
    %延长
    ux = (Dx - X(i))/Ls;
    uy = (Dy - Y(i))/Ls;
    Bx = X(i) + Lb * (ux * cos(-110*pi/180) - uy * sin(-110*pi/180));
    By = Y(i) + Lb * (ux * sin(-110*pi/180) + uy * cos(-110*pi/180));
    Px = Bx;
    Py = By - Lp;
    % 保存 D 点轨迹
    Ax_traj(i) = X(i);Ay_traj(i) = Y(i);
    Dx_traj(i) = Dx;Dy_traj(i) = Dy;
    Bx_traj(i) = Bx;By_traj(i) = By;
    Px_traj(i) = Px;Py_traj(i) = Py;
  % 清除前一帧
    cla;
    % 绘制轨迹点及机械臂连接
%     set(h1, 'XData', [Mx, Nx], 'YData', [My, Ny]); % 绘制基准连杆
    % 绘制机械臂连杆
    plot([Mx, Nx], [My, Ny], 'k-', 'LineWidth', 2); % M-N连杆
    plot([Mx, Cx], [My, Cy], 'b-', 'LineWidth', 2); % M-C连杆
    plot([Nx, Dx], [Ny, Dy], 'b-', 'LineWidth', 2); % N-D连杆
    plot([Cx, X(i)], [Cy, Y(i)], 'ro-', 'MarkerSize', 6, 'LineWidth', 2);  % C到A
    plot([Dx, X(i)], [Dy, Y(i)], 'ro-', 'MarkerSize', 6, 'LineWidth', 2);  % D到A
%     plot(X(i), Y(i), 'mo-', 'MarkerSize', 6, 'LineWidth', 2); % 绘制绿点A
    plot([Bx, X(i)], [By, Y(i)], 'go-', 'MarkerSize', 6, 'LineWidth', 2);  % B到A
    plot([Bx, Px], [By, Py], 'ko-', 'MarkerSize', 6, 'LineWidth', 2);  % B到P
    plot(Ax_traj, Ay_traj, 'm--', 'LineWidth', 1.5, 'DisplayName', '理论轨迹');
    plot(Dx_traj, Dy_traj, 'm--', 'LineWidth', 1.5, 'DisplayName', 'D 点轨迹');
    plot(Bx_traj, By_traj, 'm--', 'LineWidth', 1.5, 'DisplayName', 'B 点轨迹');
    plot(Px_traj, Py_traj, 'm--', 'LineWidth', 1.5, 'DisplayName', 'P 点轨迹');
    axis(axis_range);

    % 暂停以创建动画效果
    pause(0.05);
end

% 绘制轨迹比较
figure;
plot(x_traj, y_traj, 'r.', 'DisplayName', '理论轨迹'); % 理论轨迹
hold on;
plot(X, Y, 'g-', 'DisplayName', '正解验证'); % 正解轨迹
legend();
xlabel('X 坐标 (mm)');
ylabel('Y 坐标 (mm)');
title('轨迹反解与正解验证');
grid on;
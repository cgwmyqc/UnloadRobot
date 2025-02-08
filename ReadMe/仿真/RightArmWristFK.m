function [beta] = RightArmWristFK(theta1,theta2)
%LEFTARMWRISTFK 根据左臂左右电机的角度，解算腕关节角度，使得末端一直朝向+y
%theta1 左臂右电机角度（单位度）
%theta2 左臂左电机角度（单位度）
%beta 腕关节相对与腕关节基座角度（单位度）

    RightArm_h				=500;
    RightArm_i				=584.21;
    RightArm_g				=418;
    RightArm_f				=418;
    RightArm_w				=110;
    
    [x_F,y_F] = RightArmFK(theta1, theta2);
 
    rr = sqrt((2*RightArm_w+RightArm_f*cosd(theta1)-RightArm_g*cosd(theta2))^2 + (RightArm_f*sind(theta1)-RightArm_g*sind(theta2))^2);  

    u = sqrt((2*RightArm_w+RightArm_f*cosd(theta1))^2 + (RightArm_f*sind(theta1))^2);
    
    angle_BDC = acosd((rr*rr+RightArm_g*RightArm_g-u*u)/(2*rr*RightArm_g));
    
    angle_CDE = acosd((rr*rr + RightArm_h*RightArm_h -RightArm_i*RightArm_i)/(2*rr*RightArm_h));
    
    angle_BDE = angle_BDC + angle_CDE;

    angle_EDG = angle_BDE - (180-theta2);

    x_E = -RightArm_w + RightArm_g*cosd(theta2) + RightArm_h*cosd(angle_EDG);
    y_E = RightArm_g*sind(theta2) + RightArm_h*sind(angle_EDG);

    jx = x_F - x_E;
    jy = y_F - y_E;
    vx = 0;
    vy = 1;
    
    result_cross = jx*vy -jy*vx;
    
    if result_cross>0
        beta = acosd((jx*vx+jy*vy)/(sqrt(jx*jx+jy*jy)*sqrt(vx*vx+vy*vy)));
    elseif result_cross<0
        beta = -acosd((jx*vx+jy*vy)/(sqrt(jx*jx+jy*jy)*sqrt(vx*vx+vy*vy)));
    else
        beta = 0;
    end


end


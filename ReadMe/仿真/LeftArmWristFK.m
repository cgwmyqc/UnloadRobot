function [beta] = LeftArmWristFK(theta1,theta2)
%LEFTARMWRISTFK 根据左臂左右电机的角度，解算腕关节角度，使得末端一直朝向+y
%theta1 左臂右电机角度（单位度）
%theta2 左臂左电机角度（单位度）
%beta 腕关节相对与腕关节基座角度（单位度）

    LeftArm_h				=584.21;
    LeftArm_i				=500;
    LeftArm_g				=418;
    LeftArm_f				=418;
    LeftArm_w				=110;
    
    [x_F,y_F] = LeftArmFK(theta1, theta2);
    
    u = sqrt((2*LeftArm_w-LeftArm_g*cosd(theta2))^2 + (-LeftArm_g*sind(theta2))^2);
    
    rr = sqrt((2*LeftArm_w+LeftArm_f*cosd(theta1)-LeftArm_g*cosd(theta2))^2 + (LeftArm_f*sind(theta1)-LeftArm_g*sind(theta2))^2);
    
    angle_ACD = acosd((rr*rr+LeftArm_f*LeftArm_f-u*u)/(2*rr*LeftArm_f));
    
    angle_DCE = acosd((rr*rr + LeftArm_i*LeftArm_i -LeftArm_h*LeftArm_h)/(2*rr*LeftArm_i));
    
    angle_ACE = angle_ACD + angle_DCE;
    
    angle_ECG = angle_ACE - theta1;
    
    angle_ECI = 180 - angle_ECG;
    
    x_E = LeftArm_w + LeftArm_f*cosd(theta1) + LeftArm_i*cosd(angle_ECI);
    y_E = LeftArm_f*sind(theta1) + LeftArm_i*sind(angle_ECI);

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


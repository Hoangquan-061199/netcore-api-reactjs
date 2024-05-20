import { Button, Form, FormProps, Input, message } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { useLoginMutation } from '../../redux/auth/Auth.service';
import { LoginRequest } from '../../types/Auth.type';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../redux/auth/Auth.slice';

const LoginPage = () => {
    const [loginapi, { isLoading }] = useLoginMutation();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const onFinish: FormProps<LoginRequest>['onFinish'] = async (values) => {
        try {
            let rs: any = await loginapi(values).unwrap();
            if (rs.error && rs.error.status === 400) {
                message.error(rs.error.data.message);
            } else {
                message.success(rs.message);
                dispatch(setCredentials({ ...rs }));
                navigate('/');
            }
        } catch (e) {
            message.error('Đăng nhập thất bại :)');
        }
    };

    return (
        <div style={{ width: '100vw', height: '100vh', display: 'flex', backgroundColor: '#f0f0f0' }}>
            <Form
                name="normal_login"
                style={{
                    margin: 'auto',
                    width: 400,
                    padding: 20,
                    borderRadius: 10,
                    boxShadow: '0 0 10px rgba(0,0,0,0.1)',
                    backgroundColor: '#fff',
                }}
                className="login-form"
                initialValues={{ remember: true }}
                onFinish={onFinish}>
                <h1 style={{ textAlign: 'center', textTransform: 'uppercase', margin: 15 }}>Đăng nhập</h1>
                <Form.Item name="username" rules={[{ required: true, message: 'Vui lòng nhập tài khoản!' }]}>
                    <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="Tài khoản" />
                </Form.Item>
                <Form.Item name="password" rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}>
                    <Input.Password
                        prefix={<LockOutlined className="site-form-item-icon" />}
                        placeholder="Mật khẩu"
                    />
                </Form.Item>

                <Form.Item style={{ display: 'flex', justifyContent: 'center' }}>
                    <Button type="primary" loading={isLoading} htmlType="submit" className="login-form-button">
                        Đăng nhập
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default LoginPage;
